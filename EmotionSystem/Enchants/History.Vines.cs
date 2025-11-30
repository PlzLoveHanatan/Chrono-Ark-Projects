using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Vines : EquipBase
	{
		public override void Init()
		{
			PlusStat.DMGTaken = -5;
			PlusStat.RES_CC = 5;
			PlusStat.RES_DEBUFF = 5;
			PlusStat.RES_DOT = 5;
		}
	}

	public class En_Vines_L : EquipBase
	{
		public override void Init()
		{
			PlusStat.DMGTaken = -15;
			PlusStat.RES_CC = 15;
			PlusStat.RES_DEBUFF = 15;
			PlusStat.RES_DOT = 15;
		}
	}
}
