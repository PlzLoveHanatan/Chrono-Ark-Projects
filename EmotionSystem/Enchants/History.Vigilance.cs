using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Vigilance : EquipBase
	{
		public override void Init()
		{
			PlusStat.HEALTaken = 10;
			PlusStat.AggroPer = 50;
		}
	}

	public class En_Vigilance_L : EquipBase
	{
		public override void Init()
		{
			PlusStat.HEALTaken = 20;
			PlusStat.AggroPer = 100;
			PlusStat.Strength = true;
		}
	}
}
