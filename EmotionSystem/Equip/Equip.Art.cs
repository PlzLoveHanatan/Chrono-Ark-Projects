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
					PlusPerStat.Heal = 25;
					PlusStat.dod = 25;
				}
			}

			public class Thrill : EquipBase
			{
				public override void Init()
				{
					PlusPerStat.Damage = 25;
					PlusStat.PlusCriDmg = 25;
				}
			}
		}
	}
}
