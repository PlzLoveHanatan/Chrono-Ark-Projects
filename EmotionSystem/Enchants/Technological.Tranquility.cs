using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Tranquility : EquipBase
	{
		public override void Init()
		{
			PlusStat.HIT_CC = 10;
			PlusStat.HIT_DEBUFF = 10;
			PlusStat.HIT_DOT = 10;
			PlusStat.RES_CC = 10;
			PlusStat.RES_DEBUFF = 10;
			PlusStat.RES_DOT = 10;
		}
	}

	public class En_Tranquility_L : EquipBase
	{
		public override void Init()
		{
			PlusStat.HIT_CC = 30;
			PlusStat.HIT_DEBUFF = 30;
			PlusStat.HIT_DOT = 30;
			PlusStat.RES_CC = 30;
			PlusStat.RES_DEBUFF = 30;
			PlusStat.RES_DOT = 30;
		}
	}
}
