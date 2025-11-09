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
		public class Protocol : Buff
		{
			public override void Init()
			{
				PlusStat.PlusCriDmg = 15;
				PlusStat.cri = 15;
			}
		}

		public class JudgmentManifest : Buff
		{
			public override void Init()
			{
				PlusStat.AggroPer = 100;
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				User.Damage(BChar, Dmg, false, true);
				resist = true;
			}
		}

		public class Precision : Buff
		{
			public override void Init()
			{
				PlusStat.PlusCriDmg = BChar.GetStat.PlusCriDmg;
			}
		}
	}
}
