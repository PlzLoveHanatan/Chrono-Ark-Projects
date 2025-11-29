using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Glitter : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Damage = 10;
			PlusStat.AggroPer = 50;
		}
	}
}
