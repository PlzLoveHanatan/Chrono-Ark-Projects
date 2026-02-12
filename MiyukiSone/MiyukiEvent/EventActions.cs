using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Security.X509.Extensions;
using static MiyukiSone.EventData;
using static MiyukiSone.EventStringLoader;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using GameDataEditor;
using UnityEngine;
using I2.Loc;

namespace MiyukiSone
{
	public static class EventAction
	{
		private static int lastPick = -1;

		public static void MiyukiAction()
		{
			if (CurrentAffection == MiyukiAffectionState.indifference) return;

			bool isDouble = CurrentAffection == MiyukiAffectionState.adoration || CurrentAffection == MiyukiAffectionState.eradication;
			bool isLove = CurrentAffection == MiyukiAffectionState.love || CurrentAffection == MiyukiAffectionState.adoration;

			List<int> availableIndexes = new List<int> { 0, 1, 2 };

			if (Pd._Gold >= 250 && !isLove || Pd._Gold >= 500 && !isLove && isDouble) availableIndexes.Add(3);
			if (Pd._Soul >= 1 && !isLove || Pd._Soul >= 2 && !isLove && isDouble) availableIndexes.Add(4);

			int needCount = isDouble ? 2 : 1;
			int consumableCount = Pd.Inventory?.Count(i => i != null && i is Item_Consume) ?? 0;
			if (consumableCount >= needCount) availableIndexes.Add(5);

			if (lastPick != -1 && availableIndexes.Count > 1)
			{
				availableIndexes.Remove(lastPick);
			}

			int pick = availableIndexes[RandomManager.RandomInt("MiyukiRandomAction", 0, availableIndexes.Count)];
			lastPick = pick;

			EventState selectedEvent = EventState.consumable;

			switch (pick)
			{
				case 0: ChangeMana(1, isLove, isDouble); selectedEvent = EventState.mana; break;
				case 1: ChangeInventoryNum(2, isLove, isDouble); selectedEvent = EventState.inventory; break;
				case 2: ChangeGold(250, isLove, isDouble); selectedEvent = EventState.gold; break;
				case 3: ChangeSoulstones(1, isLove, isDouble); selectedEvent = EventState.soulstones; break;
				case 4: ChangeConsumables(isLove, isDouble); selectedEvent = EventState.consumable; break;
			}

			MiyukiTextEvent(selectedEvent, isLove);
		}

		private static void ChangeMana(int amount, bool isLove, bool isDouble)
		{
			int finalValue = isDouble ? amount * 2 : amount;
			AllyTeam.AP += isLove ? finalValue : -finalValue;
		}

		private static void ChangeGold(int amount, bool isLove, bool isDouble)
		{
			int finalValue = isDouble ? amount * 2 : amount;
			Pd._Gold += isLove ? finalValue : -finalValue;
		}

		private static void ChangeSoulstones(int amount, bool isLove, bool isDouble)
		{
			int finalValue = isDouble ? amount * 2 : amount;
			Pd._Soul += isLove ? finalValue : -finalValue;
		}

		private static void ChangeInventoryNum(int amount, bool isLove, bool isDouble)
		{
			int finalValue = isDouble ? amount * 2 : amount;
			finalValue = isLove ? finalValue : -finalValue;
			PartyInventory.InvenM.ChangeMaxInventoryNum(finalValue);
		}

		public static void ChangeConsumables(bool isLove, bool isDouble, int classNum = -1)
		{
			int amount = isDouble ? 2 : 1;

			if (isLove)
			{
				for (int a = 0; a < amount; a++)
				{
					string key = classNum == -1
						? PlayData.GetConsumeRandom(RandomManager.RandomInt($"MiyukiRandomConsume", 0, 5))
						: PlayData.GetConsumeRandom(classNum);

					var item = ItemBase.GetItem(key);

					if (PartyInventory.InvenM != null)
					{
						PartyInventory.InvenM.AddNewItem(item);
					}
					else
					{
						for (int i = 0; i < Pd.Inventory.Count; i++)
						{
							if (Pd.Inventory[i] == null)
							{
								Pd.Inventory[i] = item;
								break;
							}
						}
					}
					Debug.Log($"Item added: {key}");
				}
				PartyInventory.Ins?.UpdateInvenUI();
			}
			else
			{
				var consumables = Pd.Inventory?.Where(i => i != null && i is Item_Consume).ToList();
				int needCount = amount;

				if (consumables != null && consumables.Count >= needCount)
				{
					for (int i = 0; i < needCount; i++)
					{
						if (consumables.Count == 0) break;

						var target = consumables[RandomManager.RandomInt($"MiyukiRandomConsume", 0, consumables.Count)];

						if (PartyInventory.InvenM != null)
							PartyInventory.InvenM.DelItem(target);
						else
						{
							int index = PlayData.FindItem(target.itemkey);
							if (index != -1) Pd.Inventory[index] = null;
						}

						consumables.Remove(target);
						Debug.Log($"Item removed: {target.itemkey}");
					}

					PartyInventory.Ins?.UpdateInvenUI();
					Debug.Log($"Removed {needCount} item(s)");
				}
			}
		}
		
		private static void FetchSkill()
		{
			BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(AllyTeam.Skills_Deck,
				new SkillButton.SkillClickDel(b => b.Myskill.Master.MyTeam.ForceDraw(b.Myskill)), ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));
		}
	}
}
