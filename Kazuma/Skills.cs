using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Targets;
using UnityEngine;

namespace Kazuma
{
	public class Skills
	{
		public class Steal : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Debug.Log("Steal Start skill");
				Kazuma.Scripts.Steal(BChar, Targets[0], 100);
			}
		}
	}
}
