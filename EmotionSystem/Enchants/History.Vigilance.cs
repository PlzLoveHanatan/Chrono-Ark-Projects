using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Vigilance : EquipBase
	{
		public override void Init()
		{
			PlusStat.HEALTaken = 15;
			PlusStat.AggroPer = 100;
			PlusStat.Strength = true;
		}
	}
}
