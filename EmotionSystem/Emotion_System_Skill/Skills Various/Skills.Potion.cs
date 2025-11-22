using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Skills
	{
		public class Potion
		{
			public class DistilledSuffering : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.ApplyBleed(Targets[0], Utils.DummyChar, 8);
				}
			}

			public class IgnitedRemorse : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.ApplyBurn(Targets[0], Utils.DummyChar, 12);
				}
			}

			public class EssenceWrath : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					EmotionManager.AbnormalitySelection(2, false, true);
				}
			}

			public class EssenceTranquility : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					EmotionManager.AbnormalitySelection(2, true);
				}
			}

			public class DistortionFragment : Extended_PotionIdentify
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					EmotionManager.EGOSelection();
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
							EmotionManager.SetEmotionCapInvestigator(ally);
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
							EmotionManager.SetEmotionCapGuest(enemy, true);
						}
					}
				}
			}
		}
	}
}
