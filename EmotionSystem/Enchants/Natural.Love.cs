
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Love : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Heal = 5;
			PlusStat.cri = 5;
		}
	}

	public class En_Love_L : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Heal = 15;
			PlusStat.cri = 15;
		}
	}
}
