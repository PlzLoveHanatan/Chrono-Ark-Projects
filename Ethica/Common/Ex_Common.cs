using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethica
{
	partial class CommonSkills
	{
		public class Ex
		{

			public class ThrummingHatchet : Skill_Extended, IP_PlayerTurn_1
			{
				public bool SkillPLayed;

				public void Turn1()
				{
					SkillPLayed = false;
				}
			}

			public class Bolas : Skill_Extended, IP_PlayerTurn_1
			{
				public bool SkillPLayed;

				public void Turn1()
				{
					SkillPLayed = false;
				}
			}
		}
	}
}
