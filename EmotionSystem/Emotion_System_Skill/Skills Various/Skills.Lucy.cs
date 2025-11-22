using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Skills
	{
		public class Lucy
		{
			public class MusicBox : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw();

					foreach (var ally in Targets)
					{
						if (ally != null)
						{
							EmotionManager.SetEmotionCapInvestigator(ally);
						}
					}
				}
			}

			public class HippityHop : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw(2);

					foreach (BattleChar ally in Utils.AllyTeam.AliveChars)
					{
						ally.GetPosEmotion(null, 3);
					}

					if (Utils.AllyTeam.AliveChars.All(bc => bc.EmotionLevel() >= 5))
					{
						Utils.AllyTeam.Draw();
					}
				}
			}

			public class Candy : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw(2);

					if (Utils.AllyTeam.AliveChars.All(bc => bc.EmotionLevel() >= 5))
					{
						if (Utils.ReturnBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level) is Investigators.EmotionLucy lucyEmotion)
						{
							BattleSystem.DelayInputAfter(lucyEmotion.LucyEmotionLevelUp(1, null, null, true));
						}
					}
					else
					{
						var ally = Utils.AllyTeam.AliveChars.Where(a => a != null).OrderBy(a => a.MyEmotion().Level).ThenBy(a => a.MyEmotion().CoinNum).FirstOrDefault();
						ally?.AddEmotionLevel(true);
					}
				}
			}

			public class RainbowSea : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw(3);
					Utils.AllyTeam.AP += 2;

					foreach (BattleChar enemy in Utils.EnemyTeam.AliveChars)
					{
						if (Utils.ReturnBuff(enemy, ModItemKeys.Buff_B_Guest_Emotional_Level) is Guests.Emotion.Level buff && !buff.EmotionBlock)
						{
							enemy.GetNegEmotion(null, 3);
						}
					}

					if (Utils.AllyTeam.AliveChars.All(bc => bc.EmotionLevel() >= 5))
					{
						Utils.AllyTeam.AP += 1;
					}
				}
			}
		}
	}
}
