using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Sorrow : EquipBase
	{
		public override void Init()
		{
			PlusStat.cri = 15;
			PlusStat.hit = 15;
		}
	}

	public class En_Sorrow_L : EquipBase
	{
		public override void Init()
		{
			PlusStat.cri = 30;
			PlusStat.hit = 30;
		}
	}
}
