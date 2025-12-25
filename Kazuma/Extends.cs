using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazuma
{
	public class Extends
	{
		public class Landlord : Skill_Extended
		{
			public override bool Terms()
			{
				return !MySkill.IsDamage && BChar.BuffReturn(ModItemKeys.Buff_B_Contract_08_Landlord, false) != null;
			}
		}
	}
}
