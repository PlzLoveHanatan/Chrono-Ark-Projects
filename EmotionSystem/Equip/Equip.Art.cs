using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Equip
	{
		public class Art
		{
			public class Lotus : EquipBase
			{
				public override void Init()
				{
					PlusPerStat.Heal = 40;
					PlusStat.dod = 40;
				}
			}

			public class Thrill : EquipBase
			{
				public override void Init()
				{
					PlusPerStat.Damage = 40;
					PlusStat.PlusCriDmg = 40;
				}
			}
		}
	}
}
