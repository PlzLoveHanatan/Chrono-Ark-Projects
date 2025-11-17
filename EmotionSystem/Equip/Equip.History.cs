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
					PlusStat.DMGTaken = -30;
					PlusStat.RES_CC = 30f;
					PlusStat.RES_DEBUFF = 30f;
					PlusStat.RES_DOT = 30f;
				}
			}

			public class Millarca : EquipBase
			{
				public override void Init()
				{
					PlusPerStat.Damage = 30;
					PlusStat.cri = 30;
				}
			}
		}
	}
}
