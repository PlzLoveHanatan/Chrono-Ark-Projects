using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Gift : EquipBase
	{
		public override void Init()
		{
			PlusStat.PlusCriDmg = 15;
			PlusStat.PlusCriHeal = 15;
		}
	}
}
