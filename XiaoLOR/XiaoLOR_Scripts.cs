using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using UnityEngine;

namespace XiaoLOR
{
	public class XiaoLOR_Scripts
	{
		public static void StartChangeSkills(BattleChar bchar)
		{
			foreach (var key in XiaoLOR_DataStore.XiaoEGO)
			{
				var skill = Skill.TempSkill(key, bchar);
				if (skill != null)
				{
					EmotionSystem_EGO_Button.instance?.AddEGOSkill(skill);
					Utils.UnlockSkillPreview(key);
				}
			}

			CheckEGOHand();
			BattleSystem.DelayInputAfter(ChangeFixedAbility(bchar));
			BattleSystem.DelayInputAfter(ChangeSkills(bchar));
		}

		private static IEnumerator ChangeFixedAbility(BattleChar bchar)
		{
			yield return null;

			var fixedSkill = (bchar as BattleAlly)?.MyBasicSkill?.buttonData;

			if (fixedSkill?.MySkill != null && XiaoLOR_DataStore.SkillsList.TryGetValue(fixedSkill.MySkill.KeyID, out var newFixedSkill))
			{
				var newSkill = Skill.TempSkill(newFixedSkill, fixedSkill.Master, fixedSkill.Master.MyTeam);
				Skill refillSkill = newSkill.CloneSkill();
				(bchar as BattleAlly).MyBasicSkill.SkillInput(refillSkill);
				(bchar as BattleAlly).BattleBasicskillRefill = refillSkill;
				(bchar as BattleAlly).BasicSkill = refillSkill;
				int ind = bchar.MyTeam.Chars.IndexOf(bchar);
				if (ind >= 0)
				{
					bchar.MyTeam.Skills_Basic[ind] = refillSkill;
				}
			}
		}

		private static IEnumerator ChangeSkills(BattleChar bchar)
		{
			var allyTeam = XiaoUtils.AllyTeam;
			var deck = allyTeam.Skills.Concat(allyTeam.Skills_UsedDeck).Concat(allyTeam.Skills_Deck);

			foreach (Skill skill in deck)
			{
				if (XiaoLOR_DataStore.SkillsList.TryGetValue(skill.MySkill.KeyID, out var newXiaoSkill))
				{
					var newSkill = Skill.TempSkill(newXiaoSkill, skill.Master, skill.Master.MyTeam);
					skill.SkillChange(newSkill);
				}
			}
			yield break;
		}

		public static void CheckDrawSkill(BattleChar bchar, Skill drawSkill)
		{
			if (drawSkill?.MySkill == null || bchar.EmotionLevel() < 2) return;

			if (XiaoLOR_DataStore.SkillsList.TryGetValue(drawSkill.MySkill.KeyID, out var newXiaoSkill))
			{
				var newSkill = Skill.TempSkill(newXiaoSkill, drawSkill.Master, drawSkill.Master.MyTeam);
				drawSkill.SkillChange(newSkill);
			}
		}

		private static void CheckEGOHand()
		{
			if (EmotionSystem_EGO_Button.instance != null && EmotionSystem_EGO_Button.instance.OpenEGOHand)
			{
				EmotionSystem_EGO_Button.instance.ChangeHand();
			}
		}

		public static IEnumerator FixXiaoFixedAbillity(BattleChar bchar)
		{
			yield return new WaitForEndOfFrame();
			(bchar as BattleAlly)?.MyBasicSkill.SkillInput(bchar.BattleBasicskillRefill);
			yield break;
		}

		public static void XiaoLevelUp(Character myChar, int level)
		{
			switch (level)
			{
				case 2:
					IncreaseStats(myChar);
					break;
				case 3:
					IncreaseStats(myChar);
					break;
				case 4:
					IncreaseStats(myChar);
					break;
				case 5:
					IncreaseStats(myChar);
					break;
				case 6:
					IncreaseStats(myChar);
					break;
			}
		}

		public static void IncreaseStats(Character character)
		{
			character.OriginStat.DeadImmune += 5;
		}
	}
}
