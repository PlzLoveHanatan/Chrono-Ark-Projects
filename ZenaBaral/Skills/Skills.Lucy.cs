using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using NLog.Targets;
using ChronoArkMod;
using System.Collections;
using UnityEngine;

namespace ZenaBaral
{
	public partial class Skills
	{
		public class Verdict : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				BattleSystem.DelayInput(Visual());
			}

			private IEnumerator Visual()
			{
				yield return null;

				foreach (var enemy in Utils.EnemyTeam.AliveChars)
				{
					int damage = (int)(enemy.GetStat.maxhp * 0.25f);
					enemy.Damage(Utils.DummyChar, damage, false, true);
				}

				var skill = Skill.TempSkill(ModItemKeys.Skill_S_Lucy_Verdict_0, BChar, BChar?.MyTeam);

				if (skill == null || BChar == null || MySkill == null) yield break;

				var targets = Utils.EnemyTeam.AliveChars;
				if (targets.Count > 0)
				{
					BChar.ParticleOut(MySkill, skill, targets[0]);
					ZenaScripts.PlaySounds("Zena_Lucy_Verdict");
				}

				yield break;
			}
		}

		public class Extraction : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.AllyTeam.Draw(2);
				BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(BChar.MyTeam.Skills, button => ZenaScripts.Selection(button, true), ModLocalization.Zena_Lucy, false, true));
			}
		}
	}
}
