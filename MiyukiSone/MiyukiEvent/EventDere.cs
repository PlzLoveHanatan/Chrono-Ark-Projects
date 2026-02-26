using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using GameDataEditor;
using I2.Loc;
using static MiyukiSone.Utils;
using static MiyukiSone.Event;

namespace MiyukiSone
{
	public static class EventDere
	{
		private static readonly List<Action> DereActions = new List<Action>
		{
			() => ChangeInventoryNum(2),
			() => ChangeRelicBarNum(1),
			() => ChangeGold(250),
			() => ChangeSoulstones(1),
			() => GainRandomPotion(),
			() => GainRandomRelic(),
			() => GainRandomEquip(),
			() => GainRandomConsumable(),
			() => GainRandomBook(),
			() => GainRandomMisc()
		};

		public static void DereAction()
		{
			var availableActions = DereActions.ToList();
			if (MiyukiData.LastDereAction != -1 && availableActions.Count > 1) availableActions.RemoveAt(MiyukiData.LastDereAction);
			int randomIndex = RandomManager.RandomInt("MiyukiDereAction", 0, availableActions.Count);
			availableActions[randomIndex].Invoke();
			MiyukiData.LastDereAction = randomIndex;
		}
	}
}
