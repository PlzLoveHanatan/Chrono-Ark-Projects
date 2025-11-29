using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Sorrow : EquipBase
	{
		public override void Init()
		{
			PlusStat.cri = 15;
			PlusStat.hit = 15;
		}
	}
}
