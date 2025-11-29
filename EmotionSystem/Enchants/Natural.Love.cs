
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class Love : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Heal = 10;
			PlusStat.PlusCriHeal = 10;
		}
	}
}
