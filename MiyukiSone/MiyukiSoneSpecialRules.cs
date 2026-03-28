using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyukiSone
{
	public class MiyukiSpecialRules : SpecialRule
	{
		public override void Init()
		{
			base.Init();
			OnePassive = true;
		}

		public override void GameSetting()
		{
			RuleChange.CharacterSkillMin = 7;
		}
	}
}
