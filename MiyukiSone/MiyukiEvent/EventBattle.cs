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
	public static class EventBattle
	{
		public static void MiyukiAction()
		{
			if (IsLoving) LoveActions();
			if (IsHating) HateActions();
		}

		private static void LoveActions()
		{
			List<int> availableIndexes = new List<int> { 0, 1, 2, 3, 4 };
			var lastAction = MiyukiData.LastLoveAction;
			if (lastAction != -1 && availableIndexes.Count > 1) availableIndexes.Remove(lastAction);
			int randomIndex = availableIndexes[RandomManager.RandomInt("MiyukiLoveAction", 0, availableIndexes.Count)];
			MiyukiData.LastLoveAction = randomIndex;

			switch (randomIndex)
			{
				case 0: ChangeMana(1); break;
				case 1: ChangeInventoryNum(1); break;
				case 2: ChangeGold(250); break;
				case 3: ChangeSoulstones(1); break;
				case 4: FetchSkill(); break;
				default: break;
			}
			MiyukiTextEvent();
		}

		private static void HateActions()
		{
			List<int> availableIndexes = new List<int> { 0, 1 };

			var lastAction = MiyukiData.LasthateAction;
			if (lastAction != -1 && availableIndexes.Count > 1) availableIndexes.Remove(lastAction);
			int randomIndex = availableIndexes[RandomManager.RandomInt("MiyukiHateAction", 0, availableIndexes.Count)];
			MiyukiData.LasthateAction = randomIndex;

			if (Pd._Soul >= 1 || Pd._Soul >= 2 && IsHating) availableIndexes.Add(2);
			if (Pd._Soul >= 2) availableIndexes.Add(3);

			switch (randomIndex)
			{
				case 0: ChangeMana(1); break;
				case 1: ChangeInventoryNum(1); break;
				case 2: ChangeGold(250); break;
				case 3: ChangeSoulstones(1); break;
			}
			MiyukiTextEvent();
		}

		private static void ChangeMana(int amount)
		{
			int finalValue = IsAdoring || IsHating ? amount * 2 : amount;
			AllyTeam.AP += IsLoving ? finalValue : -finalValue;
		}

		private static void ChangeGold(int amount)
		{
			int finalValue = IsAdoring || IsHating ? amount * 2 : amount;
			Pd._Gold += IsLoving ? finalValue : -finalValue;
		}

		private static void ChangeSoulstones(int amount)
		{
			int finalValue = IsAdoring || IsHating ? amount * 2 : amount;
			Pd._Soul += IsLoving ? finalValue : -finalValue;
		}

		private static void ChangeInventoryNum(int amount)
		{
			int finalValue = IsAdoring || IsHating ? amount * 2 : amount;
			finalValue = IsLoving ? finalValue : -finalValue;
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
