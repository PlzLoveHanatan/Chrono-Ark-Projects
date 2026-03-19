using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public partial class DialoguePaws
	{
		private static readonly List<Action> DereDialoguePawsAction = new List<Action>
		{
			GainRandomPotion,
			GainRandomRelic,
			GainRandomEquip,
			GainRandomConsumable,
			GainRandomBook,
			GainRandomMisc
		};

		public static void DerePaws()
		{
			try
			{
				var paws = DereDialoguePawsAction.ToList();
				if (MiyukiData.LastDereDialoguePaw != -1 && paws.Count > 1) paws.RemoveAt(MiyukiData.LastDereDialoguePaw);
				int randomIndex = RandomManager.RandomInt("MiyukiDerePaw", 0, paws.Count);
				paws[randomIndex].Invoke();
				MiyukiData.LastDereDialoguePaw = randomIndex;
			}
			catch (Exception e)
			{
				Debug.Log(e.ToString());
			}
		}

		#region Dere Paws Functions
		private static void AddItem(string itemKey)
		{
			ItemBase item = ItemBase.GetItem(itemKey);
			AddItem(item);
		}

		private static void AddItem(ItemBase itemKey)
		{
			if (itemKey == null || PartyInventory.Ins == null) return;

			InventoryManager.Reward(itemKey);
			//PartyInventory.InvenM.AddNewItem(itemKey);
			Debug.Log($"Gain reward: {itemKey.ItemTypeKey}");
		}

		public static void GainRandomPotion()
		{
			AddItem(ItemBase.GetPotionRandom());
		}

		public static void GainRandomRelic()
		{
			AddItem(PlayData.GetPassiveRandom());
		}

		public static void GainRandomEquip()
		{
			int rarity = PlayData.TSavedata.StageNum > 4 ? 4 : PlayData.TSavedata.StageNum;
			string equipKey = PlayData.GetEquipRandom(rarity);
			ItemBase equipItem = ItemBase.GetItem(equipKey);
			AddItem(equipItem);
		}

		public static void GainRandomConsumable()
		{
			AddItem(DereConsumeKeys.Random("MiyukiRandomConsumable"));
		}

		public static void GainRandomBook()
		{
			AddItem(DereBookKeys.Random("MiyukiRandomBook"));
		}

		public static void GainRandomMisc()
		{
			AddItem(DereMiscKeys.Random("MiyukiRandomMisc"));
		}
		#endregion
	}
}
