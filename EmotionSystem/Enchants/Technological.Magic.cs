using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public class En_Magic : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Damage = 5;
			PlusStat.hit = 5;
		}
	}

	public class En_Magic_L : EquipBase
	{
		public override void Init()
		{
			PlusPerStat.Damage = 15;
			PlusStat.hit = 15;
		}
	}
}
