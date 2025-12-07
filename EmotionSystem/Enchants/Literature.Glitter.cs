using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Glitter : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Damage = 5;
			PlusStat.PlusCriDmg = 5;
			PlusStat.AggroPer = 25;
		}
	}

	public class En_Glitter_L : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Damage = 15;
			PlusStat.PlusCriDmg = 15;
			PlusStat.AggroPer = 50;
		}
	}
}
