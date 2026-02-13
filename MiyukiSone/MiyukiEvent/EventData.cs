using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using static MiyukiSone.DialogueBoxData;

namespace MiyukiSone
{
	public static class EventData
	{
		public enum EventState
		{
			draw,
			mana,
			inventory,
			gold,
			soulstones,
			consumable,
			redirectHeal,
			redirectAttack,
			changeDamageDeal,
			changeDamageTake,
			witchCurse,
			other
		}
	}
}
