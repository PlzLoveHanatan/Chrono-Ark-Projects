using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using NLog.Targets;
using UnityEngine;
using EmotionalSystemBuff;

namespace EmotionalSystemSkill
{
	public class LucySkills
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
						EmotionalManager.SetEmotionCapAlly(ally);
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
			}
		}

		public class Candy : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.AllyTeam.Draw(2);

				var ally = Utils.AllyTeam.AliveChars.Where(a => a != null).OrderBy(a => a.MyEmotion().Level).ThenBy(a => a.MyEmotion().CoinNum).FirstOrDefault();

				ally?.AddEmotionLevel(true);
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
					if (Utils.ReturnBuff(enemy, ModItemKeys.Buff_B_Enemy_Emotional_Level) is EmotionsEnemy.EmotionalLevelEnemy buff && !buff.EmotionBlock)
					{
						enemy.GetNegEmotion(null, 3);
					}
				}
			}
		}
	}
}
