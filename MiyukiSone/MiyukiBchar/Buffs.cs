using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MiyukiSone.EventData;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public class Buffs
	{
		// Hidden buff
		public class MiyukiBuff : Buff, IP_DamageChange, IP_DamageTakeChange
		{
			// Сhange all taking damage for allies depends on Miyuki current affection
			public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
			{
				int multiplier = MiyukiResult(10);
				if (multiplier == 0 || !Hit.Info.Ally) return Dmg;
				float factor = 1f - (multiplier / 100f);
				Dmg = (int)(Dmg * factor);
				return Dmg;
			}

			// Сhange all deal damage for all enemies depends on Miyuki affection
			public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
			{
				int multiplier = MiyukiResult(10);
				if (multiplier == 0 || !Target.Info.Ally) return Damage;
				float factor = 1f - (multiplier / 100f);
				Damage = (int)(Damage * factor);
				return Damage;
			}
		}

		// Hidden Debuff
		public class MiyukiDebuff : Buff, IP_PlayerTurn
		{
			public override void Init()
			{
				BChar.Info.PlusActCount.Add(1);
				base.Init();
			}

			public void Turn()
			{
				SelfDestroy();
			}
		}
	}
}
