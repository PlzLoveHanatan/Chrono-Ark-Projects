using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using MiyukiSone;
using NLog.Targets;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsScripts;

namespace MiyukiSone
{
	public static class UtilsScripts
	{
		public static void RemoveSkill(Skill skill, bool isExclude = false)
		{
			if (skill == null) return;

			if (isExclude)
			{
				skill.Except();
			}
			else
			{
				BattleSystem.instance?.StartCoroutine(RemoveSkillCo(skill));
			}
		}

		private static IEnumerator RemoveSkillCo(Skill skill)
		{
			DeckList(AllyTeam.Skills, skill);
			DeckList(AllyTeam.Skills_Deck, skill);
			DeckList(AllyTeam.Skills_UsedDeck, skill);
			BattleSystem.instance?.ActWindow.Draw(AllyTeam, false);
			yield break;
		}

		private static void DeckList(List<Skill> list, Skill skill)
		{
			int index = list.FindIndex(s => s.CharinfoSkilldata == skill.CharinfoSkilldata);

			if (index != -1)
			{
				list.RemoveAt(index);
			}
		}

		public static void HealTarget(BattleChar target, BattleChar user, int healingNum, bool isParticleOut = true, bool isOverHeal = false)
		{
			BattleSystem.DelayInputAfter(HealingParticleCo(target, user, isParticleOut, healingNum, false, isOverHeal));
		}

		public static void HealLowestAlly(BattleChar user, int healingNum, bool isParticleOut = true, bool isOverHeal = false)
		{
			BattleSystem.DelayInputAfter(HealingParticleCo(null, user, isParticleOut, healingNum, true, isOverHeal));
		}

		private static IEnumerator HealingParticleCo(BattleChar target, BattleChar user, bool isParticleOut, int healingNum, bool isHealLowestAlly, bool isOverHeal)
		{
			yield return null;

			if (user == null) user = DummyChar;

			BattleChar healTarget = target;

			if (isHealLowestAlly)
			{
				healTarget = AllyTeam.AliveChars.Where(x => x != null && x.HP < x.GetStat.maxhp).OrderBy(x => x.HP).FirstOrDefault() ?? AllyTeam.FindChar_LowHP();
			}

			healTarget?.Heal(user, healingNum, false, isOverHeal, null);

			if (isParticleOut)
			{
				Skill healingParticle = Skill.TempSkill("", user, user.MyTeam);
				healingParticle.PlusHit = true;
				healingParticle.FreeUse = true;
				healTarget.ParticleOut(healingParticle, healTarget);
			}
		}

		public static void RemoveActions(BattleChar target, int actions = 1)
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

		public static void RemoveActions(List<BattleChar> targets, int actions = 1)
		{
			foreach (var target in targets)
			{
				RemoveActions(target, actions);
			}
		}

		private static void CastingWasteFixed(this BattleActWindow window, CastingSkill cast)
		{
			SkillButton[] componentsInChildren = window.CastingGroup.GetComponentsInChildren<SkillButton>();
			SkillButton skillButton = componentsInChildren.FirstOrDefault(bt => bt.castskill == cast);
			foreach (IP_SkillCastingQuit ip_SkillCastingQuit in cast.skill.IReturn<IP_SkillCastingQuit>())
			{
				if (ip_SkillCastingQuit != null)
				{
					ip_SkillCastingQuit.SkillCastingQuit(cast);
				}
			}
			if (skillButton != null)
			{
				skillButton.UseWaste();
			}
			window.SetCountSkillVL((window.CastingGroup.GetComponentsInChildren<SkillButton>().Length >= 13) ? 30 : 45);
		}

		public static void AddBuff(BattleChar user, BattleChar target, string buffKey, int buffNum = 1)
		{
			if (user == null || string.IsNullOrEmpty(buffKey)) return;

			for (int i = 0; i < buffNum; i++)
			{
				target.BuffAdd(buffKey, user, false, 0, false, -1, false);
			}
		}

		public static void AddBuff(BattleChar user, List<BattleChar> targets, string buffKey, int buffNum = 1)
		{
			foreach (var target in targets)
			{
				AddBuff(user, target, buffKey, buffNum);
			}
		}

		public static void AddBuff(BattleChar target, string buffKey, int buffNum = 1)
		{
			AddBuff(DummyChar, target, buffKey, buffNum);
		}

		public static Buff SecureBuff(BattleChar target, BattleChar user, string buffKey, int percentage = 0)
		{
			if (string.IsNullOrEmpty(buffKey)) return null;
			var buff = target.BuffReturn(buffKey, false) ?? target.BuffAdd(buffKey, user, false, percentage, false, -1, false);
			return buff;
		}

		public static void TakeNonLethalDamage(BattleChar taker, int damage = 0, bool isPain = true)
		{
			if (taker == null) return;
			AddBuff(taker, GDEItemKeys.Buff_B_Momori_P_NoDead);
			taker.Damage(DummyChar, damage, false, isPain, true);
			taker.BuffRemove(GDEItemKeys.Buff_B_Momori_P_NoDead);
		}

		public static Skill_Extended ApplyExtended(Skill skill, string extendedKey, bool isMultipleExtended = false, bool isBattleExtended = false)
		{
			Skill_Extended result = null;

			if (skill == null || string.IsNullOrEmpty(extendedKey))
			{
				return result;
			}

			Skill_Extended existing = skill.ExtendedFind_DataName(extendedKey);

			if (isMultipleExtended || existing == null)
			{
				if (isBattleExtended == true)
				{
					skill.ExtendedAdd_Battle(extendedKey);
				}
				else
				{
					skill.ExtendedAdd(extendedKey);
				}

				result = skill.ExtendedFind_DataName(extendedKey);
			}
			else
			{
				result = existing;
			}

			return result;
		}
	}
}
