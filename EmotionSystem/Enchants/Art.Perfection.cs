using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Perfection : EquipBase
	{
		public override void Init()
		{
			PlusStat.cri = 15;
			PlusStat.Penetration = 50;
		}
	}

	public class En_Perfection_L : EquipBase
	{
		public override void Init()
		{
			PlusStat.cri = 30;
			PlusStat.Penetration = 100;
		}
	}
}
