using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Perfection : EquipBase
	{
		public override void Init()
		{
			PlusStat.cri = 15;
			PlusStat.Penetration = 50;
		}
	}
}
