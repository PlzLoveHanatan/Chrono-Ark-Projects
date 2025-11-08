using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace ZenaBaral
{
	public partial class Buffs
	{
		public class CognitiveSuppression : Buff
		{
			public override void Init()
			{
				PlusStat.DMGTaken -= 10;
				PlusStat.Strength = true;
			}
		}

		public class SerumPulse : Buff, IP_DealDamage
		{
			public override void Init()
			{
				PlusStat.def = 20;
			}

			public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
			{
				if (Damage >= 1)
				{
					BChar.Heal(BChar, (int)(Damage * 0.2), false, false, null);
				}
			}
		}

		public class AuthorityMatrix : Buff
		{
			public override void Init()
			{
				PlusPerStat.Damage = 40;
				PlusStat.HitMaximum = true;
			}
		}

		public class BalarBleed : Debuffs.Bleed, IP_BuffAddAfter
		{
			public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
			{
				if (addedbuff == this)
				{
					SelfDestroy();
				}
			}
		}

		public class BalarDisarm : Debuffs.Disarm, IP_BuffAdd
		{
			public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
			{
				if (addedbuff == this)
				{
					Utils.AddBuff(BuffTaker, EmotionSystem.ModItemKeys.Buff_B_EmotionSystem_Disarm);
					SelfDestroy();
				}
			}
		}

		public class BalarFeeble : Debuffs.Feeble, IP_BuffAdd
		{
			public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
			{
				if (addedbuff == this)
				{
					Utils.AddBuff(BuffTaker, EmotionSystem.ModItemKeys.Buff_B_EmotionSystem_Feeble);
					SelfDestroy();
				}
			}
		}
	}
}
