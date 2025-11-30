using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Petal : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Heal = 5;
			PlusStat.dod = 5;
		}
	}

	public class En_Petal_L : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Heal = 15;
			PlusStat.dod = 15;
		}
	}
}
