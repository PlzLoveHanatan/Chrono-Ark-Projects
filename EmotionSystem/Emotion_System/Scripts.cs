using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Dialogical;
using EmotionSystem;
using static CharacterDocument;
using System.Collections;
using Spine;
using System.Web;
using System.Drawing;
using I2.Loc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using NLog.Targets;
using System.Runtime.InteropServices.WindowsRuntime;


namespace EmotionSystem
{
	public class Scripts
	{
		public static void ChargeLucyNeck()
		{
			if (PartyInventory.InvenM.InventoryItems.FirstOrDefault(a => a != null && DataStore.Instance.LucyNecklace.Contains(a.itemkey)) is Item_Active neck)
			{
				neck.ChargeNow++;
			}

			//if (isShowText)
			//{
			//	GameObject gameObject = Misc.UIInst(bchar.BattleInfo.EffectViewOb);
			//	if (bchar.Info.Ally)
			//	{
			//		gameObject.transform.position = bchar.GetPos();
			//	}
			//	else
			//	{
			//		gameObject.transform.position = bchar.GetTopPos();
			//	}
			//	gameObject.GetComponent<EffectView>().TextOut(bchar.Info.Ally, "Recharged");
			//}
		}

		public static void AttackRedirect(BattleChar user, Skill skill, List<BattleChar> targets)
		{
			if (user == null || skill == null) return;

			var newTargets = Utils.AllyTeam.AliveChars.Where(a => a != null && a != user)
				.Concat(Utils.EnemyTeam.AliveChars.Where(e => e != null))
				.ToList();

			if (newTargets.Count == 0) return;

			int index = RandomManager.RandomInt(RandomClassKey.Active, 0, newTargets.Count);
			var randomTarget = newTargets[index];
			var targetKey = skill.MySkill.Target.Key;
			var oldTargetType = targetKey;
			bool edgeCase = skill.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy;

			if (edgeCase)
			{
				skill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_onetarget);
				newTargets = user.MyTeam.AliveChars_Vanish.ToList();
				targets.Clear();
				targets.Add(randomTarget);
				user.StartCoroutine(ReturnTargetType(skill, oldTargetType));
			}
			else if (randomTarget != null)
			{
				targets.AddRange(user.BattleInfo.SkillTargetReturn(skill, randomTarget, null));
			}
			ShowConfuseText(user);
		}

		public static void AttackRedirect(BattleChar user, Skill skill, List<BattleChar> targets, bool isSelfTarget = false, int chance = 0)
		{
			bool neverLucky = RandomManager.RandomPer(RandomClassKey.Active, 100, chance);

			if (skill == null || user == null || !neverLucky) return;

			var targetKey = skill.MySkill.Target.Key;
			var oldTargetType = targetKey;
			bool edgeCase = skill.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy;

			if (edgeCase)
			{
				// временно меняем таргет на одиночный
				skill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_onetarget);

				List<BattleChar> newTargets;

				if (!isSelfTarget)
				{
					newTargets = user.MyTeam.AliveChars_Vanish.Where(a => a != user).ToList();
				}
				else
				{
					newTargets = user.MyTeam.AliveChars_Vanish.ToList();
				}

				if (newTargets == null || newTargets.Count == 0)
				{
					user.StartCoroutine(ReturnTargetType(skill, oldTargetType)); // обязательно вернуть обратно
					Debug.Log("AttackRedirect: No valid targets for AOE redirect.");
					return;
				}

				targets.Clear();
				targets.AddRange(newTargets);

				user.StartCoroutine(ReturnTargetType(skill, oldTargetType));
			}
			else
			{
				BattleChar target;

				var candidates = isSelfTarget
					? user.MyTeam.AliveChars_Vanish.ToList()
					: user.MyTeam.AliveChars_Vanish.Where(a => a != user).ToList();

				if (candidates == null || candidates.Count == 0)
				{
					Debug.Log("AttackRedirect: No candidates for redirect.");
					return;
				}

				// выбираем случайного
				target = candidates.Random(RandomClassKey.Active);

				targets.Clear();
				targets.AddRange(user.BattleInfo.SkillTargetReturn(skill, target, null));
			}

			ShowConfuseText(user);
		}

		private static IEnumerator ReturnTargetType(Skill skill, string oldTargetKey)
		{
			yield return null;
			skill.MySkill.Target = new GDEs_targettypeData(oldTargetKey);
		}

		public static void ShowConfuseText(BattleChar user)
		{
			var fx = Misc.UIInst(user.BattleInfo.EffectViewOb);
			fx.transform.position = user.Info.Ally ? user.GetPos() : user.GetTopPos();
			fx.GetComponent<EffectView>().TextOut(user.Info.Ally, " " + ScriptLocalization.System_Curse.Confu_Text);
		}

		public static void DestroyActions(BattleChar target, int actions = 1)
		{
			if (target == null || target.Info.Ally) return;

			if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;

			for (int i = 0; i < actions; i++)
			{
				var targetSkill = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(skill => skill.Usestate == target);

				if (targetSkill == null) break;

				BattleSystem.instance.EnemyCastSkills.Remove(targetSkill);
				BattleSystem.instance.ActWindow.CastingWasteFixed(targetSkill);
			}
		}

		public static void DestroyActions(List<BattleChar> targets, int actions = 1)
		{
			if (targets == null || targets.Count == 0) return;

			if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;

			foreach (var target in targets)
			{
				if (target == null || target.Info.Ally) continue;

				for (int i = 0; i < actions; i++)
				{
					var targetSkill = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(skill => skill.Usestate == target);

					if (targetSkill == null) break;

					BattleSystem.instance.EnemyCastSkills.Remove(targetSkill);
					BattleSystem.instance.ActWindow.CastingWasteFixed(targetSkill);
				}
			}
		}

		public static void DestroyAllActions(BattleChar target)
		{
			if (target == null || target.Info.Ally) return;

			if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;

			var targetSkill = BattleSystem.instance.EnemyCastSkills.FindAll(skill => skill.Usestate == target); // cant return null

			foreach (var skill in targetSkill)
			{
				BattleSystem.instance.EnemyCastSkills.Remove(skill);
				BattleSystem.instance.ActWindow.CastingWasteFixed(skill);
			}
		}

		public static void DestroyAllActions(List<BattleChar> targets)
		{
			if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;

			foreach (var target in targets)
			{
				if (target == null || target.Info.Ally) continue;

				var targetSkill = BattleSystem.instance.EnemyCastSkills.FindAll(skill => skill.Usestate == target); // cant return null

				foreach (var skill in targetSkill)
				{
					BattleSystem.instance.EnemyCastSkills.Remove(skill);
					BattleSystem.instance.ActWindow.CastingWasteFixed(skill);
				}
			}
		}

		private static Skill TempSkill(BattleChar user, string skillKey, bool isPlusHit = true, bool isFreeUse = true)
		{
			Skill skill = Skill.TempSkill(skillKey, user, user.MyTeam);

			if (skill == null)
			{
				return null;
			}

			skill.PlusHit = isPlusHit;
			skill.FreeUse = isFreeUse;
			return skill;
		}


		private static void ParticleOut(BattleChar target, BattleChar user, Skill skill)
		{

			if (target == null) return;

			var realTarget = target.IsDead
				? user.BattleInfo.EnemyList.Random(user.GetRandomClass().Target)
				: target;

			user.ParticleOut(skill, realTarget);
		}

		public static IEnumerator RecastSkill(BattleChar target, BattleChar user, string skillKey, int recastNum = 1)
		{
			if (string.IsNullOrEmpty(skillKey) || target == null) yield break;

			for (int i = 0; i < recastNum; i++)
			{
				yield return new WaitForSecondsRealtime(0.3f);

				var skill = TempSkill(user, skillKey);
				if (skill != null)
				{
					ParticleOut(target, user, skill);
				}
			}
		}

		public static IEnumerator RecastSkill(BattleChar target, BattleChar user, string skillKey, int recastNum = 1, int healingNum = 0, bool isHealLowestAlly = false, bool isPrimaryHeal = false)
		{
			if (isPrimaryHeal)
			{
				yield return Utils.HealingParticle(user, Utils.DummyChar, healingNum, true, false, isHealLowestAlly, true, true);
			}

			for (int i = 0; i < recastNum; i++)
			{
				yield return new WaitForSecondsRealtime(0.2f);
				yield return Utils.HealingParticle(user, Utils.DummyChar, healingNum, true, false, isHealLowestAlly, true, true);

				var skill = TempSkill(user, skillKey);
				if (skill != null)
				{
					ParticleOut(target, user, skill);
				}
			}
			yield break;
		}

		public static IEnumerator RecastSkill(BattleChar target, BattleChar user, string skillKey, string debuffKey, int recastNum = 1, int percentage = 0)
		{
			for (int i = 0; i < recastNum; i++)
			{
				yield return new WaitForSecondsRealtime(0.3f);

				var skill = TempSkill(user, skillKey);
				if (skill != null)
				{
					ParticleOut(target, user, skill);
					Utils.AddDebuff(target, user, debuffKey, percentage);
				}
			}
			yield break;
		}

		public static IEnumerator RecastSkillBleed(BattleChar target, BattleChar user, string skillKey, int recastNum = 1, int bleedNum = 1, int percentage = 0)
		{
			Utils.ApplyBleed(target, user, bleedNum, percentage);

			for (int i = 0; i < recastNum; i++)
			{
				yield return new WaitForSecondsRealtime(0.3f);

				var skill = TempSkill(user, skillKey);
				if (skill != null)
				{
					ParticleOut(target, user, skill);
					Utils.ApplyBleed(target, user, bleedNum, percentage);
				}
			}
			yield break;
		}

		public static IEnumerator RecastSkillErosion(BattleChar target, BattleChar user, string skillKey, int recastNum = 1, int erosionNum = 1, int percentage = 0)
		{
			Utils.ApplyErosion(target, user, erosionNum, percentage);

			for (int i = 0; i < recastNum; i++)
			{
				yield return new WaitForSecondsRealtime(0.3f);

				var skill = TempSkill(user, skillKey);
				if (skill != null)
				{
					ParticleOut(target, user, skill);
					Utils.ApplyErosion(target, user, erosionNum, percentage);
				}
			}
			yield break;
		}

		public static void SynchronizeWithEGO(BattleChar bchar, string desynchronizeSkill, List<string> skillsToSync)
		{
			if (bchar == null || skillsToSync == null || skillsToSync.Count == 0 || string.IsNullOrEmpty(desynchronizeSkill))
			{
				return;
			}

			var data = new DataStore.Synchronize.CharacterSkills();

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
					Utils.InsertSkillInDeck(skill);
					Utils.UnlockSkillPreview(key);
				}
			}

			// Сохраняем данные персонажа
			DataStore.Instance.Synchronization.SavedSkills[bchar] = data;
		}

		public static void DeSynchronize(BattleChar bchar)
		{
			if (bchar == null)
			{
				return;
			}

			if (!DataStore.Instance.Synchronization.SavedSkills.ContainsKey(bchar))
			{
				return;
			}

			var data = DataStore.Instance.Synchronization.SavedSkills[bchar];

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
			DataStore.Instance.Synchronization.SavedSkills.Remove(bchar);
		}

		public static IEnumerator ForceTurnEnd()
		{
			var bs = BattleSystem.instance;
			bs.TargetSelectCancel();
			var selectors = GameObject.FindObjectsOfType<TargetSelect>();
			foreach (var sel in selectors)
			{
				GameObject.Destroy(sel.gameObject);
			}

			var enemySkills = bs.EnemyCastSkills.ToList();
			foreach (var skill in enemySkills)
			{
				bs.ActWindow.CastingWasteFixed(skill);
				bs.EnemyCastSkills.Remove(skill);
			}

			var allySkills = bs.CastSkills.ToList();
			foreach (var skill in allySkills)
			{
				bs.ActWindow.CastingWasteFixed(skill);
				bs.EnemyCastSkills.Remove(skill);
			}

			bs.ActWindow.WasteButton?.Quit();
			bs.ActWindow.On = false;
			bs.ActWindow.TurnEndFlag = true;

			ChildClear.Clear(bs.ActWindow.ItemSkillView);
			bs.CastSkills.Clear();
			bs.SaveSkill.Clear();

			bs.StartCoroutine(bs.EnemyTurn(true));

			yield break;
		}

		public static void GlobalAbnormalitiesCheck(string buffKey, string soundKey = null, bool isAllyTeam = false, bool isEnemyTeam = false, int percentage = 999)
		{
			Utils.PlaySound(soundKey);

			List<BattleChar> team = new List<BattleChar>();

			if (isAllyTeam)
			{
				team.AddRange(Utils.AllyTeam.AliveChars_Vanish);
			}
			if (isEnemyTeam)
			{
				team.AddRange(Utils.EnemyTeam.AliveChars_Vanish);
			}

			foreach (var target in team)
			{
				Utils.GetOrAddBuff(target, buffKey, percentage);
			}
		}

		public static void AbnormalitySkillSelection(BattleChar user, DataStore.AbnormalitySkillsData.AbnormalityNatural abnoKey)
		{
			if (user.GetStat.Stun || !BattleSystem.instance.ActWindow.CanAnyMove)
			{
				return;
			}
			BattleSystem.DelayInputAfter(SkillSelectionCoroutine(user, abnoKey));
		}

		private static IEnumerator SkillSelectionCoroutine(BattleChar user, DataStore.AbnormalitySkillsData.AbnormalityNatural abnoKey)
		{
			List<Skill> list = new List<Skill>();

			if (DataStore.Instance.AbnormalitySkill.NaturalSkills.TryGetValue(abnoKey, out var dataList))
			{
				foreach (var data in dataList)
				{
					int gold = data.Gold;
					string skillKey = data.Skill;

					if (PlayData.TSavedata._Gold >= gold)
					{
						var skill = Skill.TempSkill(skillKey, user, user.MyTeam);
						if (skill != null)
						{
							list.Add(skill);
						}
					}
				}

				if (list.Count > 0)
				{
					BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(btn => OnSkillSelected(btn, user)),
							ScriptLocalization.System_SkillSelect.EffectSelect, true, false));
				}

				yield break;
			}
		}

		private static void OnSkillSelected(SkillButton myButton, BattleChar user)
		{
			if (myButton == null || myButton.Myskill == null || myButton.Myskill.MySkill == null)
			{
				return;
			}

			if (DataStore.Instance.AbnormalitySkill.NaturalSkillCost.TryGetValue(myButton.Myskill.MySkill.KeyID, out var gold))
			{
				PlayData.TSavedata._Gold = gold;
			}

			EmotionSystem_EGO_Button.instance.AddEGOSkill(user, myButton.Myskill.MySkill.KeyID);
		}

		public static bool LoseDrawBacks(BattleChar user)
		{
			// Проверяем наличие Nix

			var nix = Utils.ReturnBuff(user, ModItemKeys.Buff_B_Abnormality_NaturalLv3_Nix) as NaturalBuff.Abnormality.Lv3.Nix;

			if (nix == null)
			{
				return false;
			}

			// Получаем все баффы
			var despair = Utils.ReturnBuff(user, ModItemKeys.Buff_B_Abnormality_NaturalLv1_Despair) as NaturalBuff.Abnormality.Lv1.Despair;
			var hate = Utils.ReturnBuff(user, ModItemKeys.Buff_B_Abnormality_NaturalLv1_Hate) as NaturalBuff.Abnormality.Lv1.Hate;
			var greed = Utils.ReturnBuff(user, ModItemKeys.Buff_B_Abnormality_NaturalLv2_Greed) as NaturalBuff.Abnormality.Lv2.Greed;
			var wrath = Utils.ReturnBuff(user, ModItemKeys.Buff_B_Abnormality_NaturalLv2_Wrath) as NaturalBuff.Abnormality.Lv2.Wrath;

			// Если любого баффа нет, возвращаем false
			if (despair == null || hate == null || greed == null || wrath == null)
			{
				return false;
			}

			// Меняем флаги
			despair.DespairDrawBack = false;
			despair.Init();

			hate.HateDrawBack = false;
			hate.Init();

			greed.GreedDrawBack = false;
			greed.Init();

			wrath.WrathDrawBack = false;

			nix.noDrawBacks = true;

			// Все баффы были — возвращаем true
			return true;
		}
	}
}