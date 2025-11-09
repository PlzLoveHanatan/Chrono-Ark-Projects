using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace ZenaBaral
{
	public class Extended
	{
		public class Ex_0 : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.AllyTeam.Draw(1);
				BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(BChar.MyTeam.Skills, button => ZenaScripts.Selection(button), ModLocalization.Zena_Lucy, false, true));
			}
		}

		public class Ex_1 : Skill_Extended
		{
			public override bool CanSkillEnforce(Skill MainSkill)
			{
				return MainSkill.IsDamage || MainSkill.IsHeal;
			}

			public override void Init()
			{
				PlusSkillStat.cri = 100;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(BChar.MyTeam.Skills, button => ZenaScripts.Selection(button), ModLocalization.Zena_Lucy, false, true));
			}
		}
	}
}
