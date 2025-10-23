using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeyu
{
	public class Extended
	{
		public class Ex0 : Skill_Extended
		{
			public override bool CanSkillEnforce(Skill MainSkill)
			{
				return base.CanSkillEnforce(MainSkill) && MainSkill.AP >= 2;
			}

			public override void Init()
			{
				MySkill.APChange = -1;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				BattleSystem.instance.AllyTeam.Draw();
			}
		}
	}
}
