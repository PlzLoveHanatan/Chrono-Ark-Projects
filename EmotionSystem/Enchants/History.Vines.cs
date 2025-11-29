using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Vines : EquipBase
	{
		public override void Init()
		{
			PlusStat.DMGTaken = -10;
			PlusStat.RES_CC = 10;
			PlusStat.RES_DEBUFF = 10;
			PlusStat.RES_DOT = 10;
		}
	}
}
