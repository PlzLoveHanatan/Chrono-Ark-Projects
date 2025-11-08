using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenaBaral
{
	public partial class Buffs
	{
		public class Protocol : Buff
		{
			public override void Init()
			{
				PlusStat.cri = 10;
				PlusStat.PlusCriDmg = 10;
			}
		}

		public class Protocol_0 : Buff
		{
			public override void Init()
			{
				PlusStat.cri = 20;
				PlusStat.PlusCriDmg = 20;
			}
		}

		public class Protocol_1 : Buff
		{
			public override void Init()
			{
				PlusStat.cri = 30;
				PlusStat.PlusCriDmg = 30;
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
	}
}
