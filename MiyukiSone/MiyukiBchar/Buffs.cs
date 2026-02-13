using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MiyukiSone.EventData;
using static MiyukiSone.Affection;
using static MiyukiSone.EventStringLoader;

namespace MiyukiSone
{
	public class Buffs
	{
		// buff which will redirect heal to miyuki ?
		public class MiyukiBuff : Buff, IP_DamageChange, IP_DamageTakeChange
		{
			//change all taking damage depends on Miyuki affection state
			public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
			{
				if (!MiyukiActing && !Hit.Info.Ally) return Dmg;

				Dmg = IsLoving ? (int)(Dmg * 0.85f) : (int)(Dmg * 1.15f);
				//MiyukiTextEvent(EventState.changeDamageTake);
				return Dmg;
			}

			// change all damage output depends on Miyuki affection state
			public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
			{
				if (!MiyukiActing && Target.Info.Ally) return Damage;

				Damage = IsLoving ? (int)(Damage * 0.85f) : (int)(Damage * 1.15f);
				//MiyukiTextEvent(EventState.changeDamageDeal);
				return Damage;
			}
		}
	}
}
