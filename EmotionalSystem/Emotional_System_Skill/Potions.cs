using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using static EmotionalSystem.EmotionalManager;
using GameDataEditor;
using Steamworks;
using EmotionalSystemBuff;

namespace EmotionalSystemSkill
{
	public class Potions
	{
		public class DistilledSuffering : Extended_PotionIdentify
		{
			//public override bool TargetSelectExcept(BattleChar ExceptTarget)
			//{
			//	return !ExceptTarget.Info.Ally;
			//}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.ApplyBleed(Targets[0], Utils.DummyChar, 6);
			}
		}

		public class IgnitedRemorse : Extended_PotionIdentify
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.ApplyBurn(Targets[0], Utils.DummyChar, 8);
			}
		}

		public class EssenceWrath : Extended_PotionIdentify
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				//EmotionalManager.GetNegEmotion(Targets[0], null, 5);

				if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is EmotionsLucy lucyEmotion)
				{
					BattleSystem.DelayInputAfter(lucyEmotion.LucyEmotionLevelUp(4, false, true));
				}
			}
		}

		public class EssenceTranquility : Extended_PotionIdentify
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				//EmotionalManager.GetPosEmotion(Targets[0], null, 5);

				if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is EmotionsLucy lucyEmotion)
				{
					BattleSystem.DelayInputAfter(lucyEmotion.LucyEmotionLevelUp(4, true));
				}
			}
		}

		public class DistortionFragment : Extended_PotionIdentify
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is EmotionsLucy lucyEmotion)
				{
					BattleSystem.DelayInputAfter(lucyEmotion.SelectEGO());
				}
			}
		}

		public class PureTune : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				foreach (var ally in Targets)
				{
					if (ally != null)
					{
						EmotionalManager.SetEmotionCapAlly(ally);
					}
				}
			}
		}

		public class DarkTune : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				foreach (var enemy in Targets)
				{
					if (enemy != null)
					{
						EmotionalManager.SetEmotionCapEnemy(enemy, true);
					}
				}
			}
		}
	}
}
