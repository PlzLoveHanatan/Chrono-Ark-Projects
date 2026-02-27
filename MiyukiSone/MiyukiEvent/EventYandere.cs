using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MiyukiSone.Event;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsScripts;
using static MiyukiSone.EventData;
using GameDataEditor;

namespace MiyukiSone
{
	public static class EventYandere
	{
		public static void YandereAction(bool? isDere = null)
		{
			List<int> availableIndexes = GetAvailableActions();
			var lastAction = MiyukiData.LastYandereAction;
			if (lastAction != -1 && availableIndexes.Count > 1) availableIndexes.Remove(lastAction);
			int randomIndex = availableIndexes[RandomManager.RandomInt("MiyukiDereAction", 0, availableIndexes.Count)];
			MiyukiData.LastYandereAction = randomIndex;

			switch (randomIndex)
			{
				case 1: ChangeInventoryNum(2); break;
				case 2: ChangeRelicBarNum(1); break;
				case 3: ChangeGold(250); break;
				case 4: ChangeSoulstones(1); break;
				case 5: DamageAlly(); break;
				case 6: DebuffAllies(); break;
				case 7: KillRandomAlly(); break;
				case 8: RemoveItems(); break;
				case 9: BuffEnemies(); break;
				default: break;
			}
			MiyukiTextEvent(isDere);
		}

		private static List<int> GetAvailableActions()
		{
			List<int> actions = new List<int>();
			bool aliveAllies = AllyTeam.AliveChars?.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).Count() > 0;
			bool aliveEnemies = Bs.EnemyTeam.AliveChars?.Count() > 0;
			if (Pd.Inventory.Count > 0) actions.Add(1);
			if (Pd.ArkPassivePlus > 0) actions.Add(2);
			if (Pd._Gold >= 250) actions.Add(3);
			if (Pd._Soul >= 1) actions.Add(4);
			if (aliveAllies) actions.AddRange(new int[] { 5, 6, 7 });
			if (aliveEnemies) actions.Add(8);
			if (Pd.Inventory?.Where(i => i != null).Count() > 0) actions.Add(9);
			return actions;
		}

		private static void DamageAlly()
		{
			int damage = PlayData.TSavedata.StageNum * 5;
			var allies = AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList();
			bool isPainDamage = AllyTeam.AliveChars.Any(a => a.BuffReturn(GDEItemKeys.Buff_B_Queen_10_T, false) == null);
			if (allies.Count > 0) TakeNonLethalDamage(allies[RandomManager.RandomInt("MiyukiRandom", 0, allies.Count())], damage, isPainDamage);
		}

		private static void KillRandomAlly()
		{
			//AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("MiyukiRandom").Dead();
			AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("MiyukiRandom").HP = -99;
		}

		private static void RemoveItems()
		{
			var items = PlayData.TSavedata.Inventory?.Where(i => i != null).ToList();

			if (items != null && items.Count > 0)
			{
				var target = items.Random("MiyukiRandomRemove");
				PartyInventory.InvenM?.DelItem(target);
				PartyInventory.Ins?.UpdateInvenUI();
			}
		}

		private static void DebuffAllies()
		{
			AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().ForEach(a => AddBuff(a, ModItemKeys.Buff_B_Miyuki_Debuff));
		}

		private static void BuffEnemies()
		{
			Bs.EnemyTeam.AliveChars_Vanish.ForEach(a => AddBuff(a, ModItemKeys.Buff_B_Miyuki_Buff));
		}
	}
}
