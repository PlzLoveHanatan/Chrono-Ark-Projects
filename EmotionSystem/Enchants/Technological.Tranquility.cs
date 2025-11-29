using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Tranquility : EquipBase
	{
		public override void Init()
		{
			PlusStat.HIT_CC = 15;
			PlusStat.HIT_DEBUFF = 15;
			PlusStat.HIT_DOT = 15;
			PlusStat.RES_CC = 15;
			PlusStat.RES_DEBUFF = 15;
			PlusStat.RES_DOT = 15;
		}
	}
}
