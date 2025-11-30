using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Gift : EquipBase
	{
		public override void Init()
		{
			PlusStat.PlusCriDmg = 15;
			PlusStat.PlusCriHeal = 15;
		}
	}

	public class En_Gift_L : EquipBase
	{
		public override void Init()
		{
			PlusStat.PlusCriDmg = 30;
			PlusStat.PlusCriHeal = 30;
		}
	}
}
