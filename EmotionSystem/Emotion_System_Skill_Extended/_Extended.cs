using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Extended
	{
		public class Ex_PosAbnoSelection : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				EmotionManager.AbnormalitySelection(2, true);
				SelfDestroy();
			}
		}
		
		public class Ex_NegAbnoSelection : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				EmotionManager.AbnormalitySelection(2, false, true);
				SelfDestroy();
			}
		}

		public class Ex_EgoSelection : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				EmotionManager.EGOSelection();
				SelfDestroy();
			}
		}
	}
}
