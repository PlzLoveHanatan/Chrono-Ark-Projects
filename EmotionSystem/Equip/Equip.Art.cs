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
					PlusPerStat.Heal = 30;
					PlusStat.dod = 30;
				}
			}

			public class Thrill : EquipBase
			{
				public override void Init()
				{
					PlusPerStat.Damage = 30;
					PlusStat.PlusCriDmg = 30;
				}
			}
		}
	}
}
