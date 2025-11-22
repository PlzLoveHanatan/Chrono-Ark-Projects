using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Equip
	{
		public class History
		{
			public class Crown : EquipBase
			{
				public override void Init()
				{
					PlusStat.DMGTaken = -25;
					PlusStat.RES_CC = 25f;
					PlusStat.RES_DEBUFF = 25f;
					PlusStat.RES_DOT = 25f;
				}
			}

			public class Millarca : EquipBase
			{
				public override void Init()
				{
					PlusPerStat.Damage = 25;
					PlusStat.cri = 25;
				}
			}
		}
	}
}
