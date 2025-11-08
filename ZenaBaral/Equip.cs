using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenaBaral
{
	public class Equip
	{
		public class Vial : EquipBase
		{
			public override void Init()
			{
				PlusPerStat.Damage = 40;
				PlusStat.DMGTaken = -20;
			}
		}

		public class Glyph : EquipBase
		{
			public override void Init()
			{
				PlusStat.PlusCriDmg = 50;
				PlusStat.cri = 25;
			}
		}
	}
}
