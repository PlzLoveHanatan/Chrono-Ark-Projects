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
					PlusStat.DMGTaken = -40;
					PlusStat.RES_CC = 40f;
					PlusStat.RES_DEBUFF = 40f;
					PlusStat.RES_DOT = 40f;
				}
			}

			public class Millarca : EquipBase
			{
				public override void Init()
				{
					PlusPerStat.Damage = 40;
					PlusStat.cri = 40;
				}
			}
		}
	}
}
