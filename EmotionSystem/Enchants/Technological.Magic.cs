using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Magic : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Damage = 10;
			PlusStat.hit = 10;
		}
	}
}
