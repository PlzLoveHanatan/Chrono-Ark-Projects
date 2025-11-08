using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace ZenaBaral
{
	public partial class Skills
	{
		public class Verdict : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				ZenaScripts.PlaySounds("Zena_Lucy_Verdict");
				Utils.AllyTeam.Draw(2);

				foreach (var enemy in Utils.EnemyTeam.AliveChars)
				{
					int damage = (int)(enemy.GetStat.maxhp * 0.25f);
					enemy.Damage(Utils.DummyChar, damage, false, true);
				}
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
