using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Dialogical;
using EmotionalSystem;
using static CharacterDocument;
using System.Collections;


namespace EmotionalSystem
{
	public class EmotionalSystem_Scripts
	{
		public static void ChargeLucyNeck(BattleChar bchar, bool isShowText = false)
		{
			if (PartyInventory.InvenM.InventoryItems.FirstOrDefault(a => a != null && EmotionalSystem_DataStore.LucyNecklace.Contains(a.itemkey)) is Item_Active neck)
			{
				neck.ChargeNow++;
			}

			if (isShowText)
			{
				GameObject gameObject = Misc.UIInst(bchar.BattleInfo.EffectViewOb);
				if (bchar.Info.Ally)
				{
					gameObject.transform.position = bchar.GetPos();
				}
				else
				{
					gameObject.transform.position = bchar.GetTopPos();
				}
				gameObject.GetComponent<EffectView>().TextOut(bchar.Info.Ally, "Recharged");
			}
		}

		public static void AttackRedirect(BattleChar bchar, Skill skillD, List<BattleChar> targets, int damage = 0)
		{
			var newTargets = Utils.AllyTeam.AliveChars.Where(a => a != null && a != bchar)
				.Concat(Utils.EnemyTeam.AliveChars.Where(e => e != null)).ToList();

			if (newTargets.Count == 0) return;

			int index = RandomManager.RandomInt(bchar.GetRandomClass().Target, 0, newTargets.Count);
			var randomTarget = newTargets[index];

			if (skillD.IsDamage && skillD.Master == bchar && !skillD.FreeUse && !skillD.PlusHit)
			{
				targets.Clear();

				if (skillD.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy)
				{
					foreach (var enemy in Utils.EnemyTeam.AliveChars)
					{
						if (enemy != randomTarget)
						{
							Utils.AddBuff(bchar, enemy, ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet_1);
						}
					}

					targets.Add(randomTarget);
					randomTarget?.Damage(bchar, damage, false, false);

					Utils.EnemyTeam.AliveChars.ForEach(e => Utils.RemoveBuff(e, ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet_1, true));
				}

				else if (randomTarget != null)
				{
					targets.Add(randomTarget);
				}
			}
		}

		public static void DestroyActions(BattleChar target, int actions = 1)
		{
			if (BattleSystem.instance.EnemyCastSkills.Count > 0 && !target.Info.Ally)
			{
				var targetSkill = BattleSystem.instance.EnemyCastSkills.Where(skill => skill.Usestate == target).FirstOrDefault();

				for (int i = 0; i < actions; i++)
				{
					BattleSystem.instance.EnemyCastSkills.Remove(targetSkill);
					BattleSystem.instance.ActWindow.CastingWasteFixed(targetSkill);
				}
			}
		}

		public static IEnumerator RecastSkill(BattleChar Target, BattleChar user, string skillKey, int recastNum = 1)
		{
			for (int i = 0; i < recastNum; i++)
			{
				yield return new WaitForSecondsRealtime(0.3f);

				Skill skill = Skill.TempSkill(skillKey, user, user.MyTeam);
				skill.PlusHit = true;
				skill.FreeUse = true;

				if (Target.IsDead)
				{
					user.ParticleOut(skill, user.BattleInfo.EnemyList.Random(user.GetRandomClass().Target));
				}
				else
				{
					user.ParticleOut(skill, Target);
				}
			}
			yield break;
		}

		// Вот это единственный общий экземпляр, который используется всегда
		private static EmotionalSystem_DataStore DataStore = new EmotionalSystem_DataStore();

		public static void SynchronizeWithEGO(BattleChar bchar, string desynchronizeSkill, List<string> skillsToSync)
		{
			if (bchar == null || skillsToSync == null || skillsToSync.Count == 0 || string.IsNullOrEmpty(desynchronizeSkill))
			{
				return;
			}

			var data = new EmotionalSystem_DataStore.CharacterSkills();

			// Сохраняем текущие колоды персонажа
			data.Hand.AddRange(Utils.AllyTeam.Skills.Where(s => s.Master == bchar));
			data.DrawPile.AddRange(Utils.AllyTeam.Skills_Deck.Where(s => s.Master == bchar));
			data.DiscardPile.AddRange(Utils.AllyTeam.Skills_UsedDeck.Where(s => s.Master == bchar));

			// Удаляем старые навыки
			for (int i = Utils.AllyTeam.Skills.Count - 1; i >= 0; i--)
			{
				Skill skill = Utils.AllyTeam.Skills[i];
				if (skill.Master == bchar)
				{
					skill.Remove();
				}
			}

			Utils.AllyTeam.Skills.RemoveAll(s => s.Master == bchar);
			Utils.AllyTeam.Skills_Deck.RemoveAll(s => s.Master == bchar);
			Utils.AllyTeam.Skills_UsedDeck.RemoveAll(s => s.Master == bchar);

			// Сохраняем фиксированную способность
			var fixedSkill = (bchar as BattleAlly)?.MyBasicSkill?.buttonData;
			if (fixedSkill != null)
			{
				data.FixedAbility.Add(fixedSkill.MySkill.KeyID);
			}

			// Добавляем временный скилл "Desynchronize"
			Skill newFixedSkill = Skill.TempSkill(desynchronizeSkill, bchar, bchar.MyTeam);
			if (newFixedSkill != null)
			{
				(bchar as BattleAlly)?.MyBasicSkill?.SkillInput(newFixedSkill);
			}

			// Добавляем новые скиллы из списка
			foreach (var key in skillsToSync)
			{
				var skill = Skill.TempSkill(key, bchar);
				if (skill != null)
				{
					Utils.InsertSkillInDeck(bchar, skill);
					Utils.UnlockSkillPreview(key);
				}
			}

			// Сохраняем данные персонажа
			DataStore.SavedSkills[bchar] = data;
		}

		public static void DeSynchronize(BattleChar bchar)
		{
			if (bchar == null)
			{
				return;
			}

			if (!DataStore.SavedSkills.ContainsKey(bchar))
			{
				return;
			}

			var data = DataStore.SavedSkills[bchar];

			// Удаляем все текущие скиллы персонажа
			for (int i = Utils.AllyTeam.Skills.Count - 1; i >= 0; i--)
			{
				Skill skill = Utils.AllyTeam.Skills[i];
				if (skill.Master == bchar)
				{
					skill.Remove();
				}
			}

			Utils.AllyTeam.Skills.RemoveAll(s => s.Master == bchar);
			Utils.AllyTeam.Skills_Deck.RemoveAll(s => s.Master == bchar);
			Utils.AllyTeam.Skills_UsedDeck.RemoveAll(s => s.Master == bchar);

			// Восстанавливаем старые колоды
			Utils.AllyTeam.Skills_Deck.AddRange(data.DrawPile);
			Utils.AllyTeam.Skills_UsedDeck.AddRange(data.DiscardPile);

			// Возвращаем скиллы персонажа в руку
			foreach (var skill in data.Hand)
			{
				BattleSystem.instance.StartCoroutine(Utils.AllyTeam.AddSkillNoDrawEffect(skill, -1));
			}

			// Восстанавливаем фиксированную способность
			if (data.FixedAbility.Count > 0)
			{
				var fixedSkill = Skill.TempSkill(data.FixedAbility[0], bchar, bchar.MyTeam);
				if (fixedSkill != null)
				{
					(bchar as BattleAlly)?.MyBasicSkill?.SkillInput(fixedSkill);
				}
			}

			// Удаляем данные персонажа после восстановления
			DataStore.SavedSkills.Remove(bchar);
		}
	}
}