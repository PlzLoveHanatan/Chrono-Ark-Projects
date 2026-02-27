using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Security.X509.Extensions;
using static MiyukiSone.EventData;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using GameDataEditor;
using UnityEngine;
using I2.Loc;
using System.IO.Ports;
using System.Windows.Forms;

namespace MiyukiSone
{
	public static class Event
	{
		public static void MiyukiAction()
		{
			//if (MiyukiPoints)
			if (IsDere) EventDere.DereAction();
			if (IsYandere) EventYandere.YandereAction();
		}

		private static void AddItem(string itemKey)
		{
			ItemBase item = ItemBase.GetItem(itemKey);
			AddItem(item);
		}

		private static void AddItem(ItemBase itemKey)
		{
			if (itemKey == null || PartyInventory.Ins == null) return;

			if (!PartyInventory.Ins.IsEmpty)
			{
				PartyInventory.InvenM.AddNewItem(itemKey);
				Debug.Log($"Item added directly to inventory: {itemKey}");
			}
			else
			{
				InventoryManager.Reward(itemKey);
				Debug.Log($"Inventory full, showing reward window: {itemKey}");
			}
		}
		public static void ChangeInventoryNum(int amount)
		{
			int finalValue = IsDere ? amount : -amount;
			PartyInventory.InvenM.ChangeMaxInventoryNum(finalValue);
		}

		public static void ChangeRelicBarNum(int amount)
		{
			//for (int i = PlayData.TSavedata.ArkPassivePlus; i < PlayData.TSavedata.ArkPassivePlus + num; i++)
			//{
			//	PlayData.TSavedata.Passive_Itembase.Add(null);
			//}
			PlayData.TSavedata.ArkPassivePlus += amount;
			if (UIManager.NowActiveUI is ArkPartsUI) UIManager.NowActiveUI.Delete();
		}

		public static void ChangeGold(int amount)
		{
			int gold = IsDere ? RandomManager.RandomInt("MiyukiRandomGold", 50, amount) : amount;
			int finalValue = IsDere ? amount : -amount;
			Pd._Gold += finalValue;
		}

		public static void ChangeSoulstones(int amount)
		{
			int finalValue = IsDere ? amount : -amount;
			Pd._Soul += finalValue;
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
			int rarity = 2;
			string equipKey = PlayData.GetEquipRandom(rarity);
			ItemBase equipItem = ItemBase.GetItem(equipKey);
			AddItem(equipItem);
		}

		public static void GainRandomConsumable()
		{
			List<string> keys = new List<string>
			{
				//GDEItemKeys.Item_Consume_Bread,
				GDEItemKeys.Item_Consume_GoldenBread,
				GDEItemKeys.Item_Consume_GoldenApple,
				GDEItemKeys.Item_Consume_Celestial,
				GDEItemKeys.Item_Consume_Herb,
				GDEItemKeys.Item_Consume_SodaWater,
				GDEItemKeys.Item_Consume_RedHammer,
				GDEItemKeys.Item_Consume_RedHerb,
				GDEItemKeys.Item_Consume_Dinner,
				GDEItemKeys.Item_Consume_Ilya_PassiveConsume,
				GDEItemKeys.Item_Consume_FriendShipPouch,
				GDEItemKeys.Item_Consume_RedWing, // ?
			};
			AddItem(keys.Random("MiyukiRandomConsumable"));
		}

		public static void GainRandomBook()
		{
			List<string> keys = new List<string>
			{
				GDEItemKeys.Item_Consume_SkillBookCharacter,
				GDEItemKeys.Item_Consume_SkillBookInfinity,
				GDEItemKeys.Item_Consume_SkillBookSuport,
				//GDEItemKeys.Item_Consume_SkillBookCharacter_Rare,
				//GDEItemKeys.Item_Consume_SkillBookLucy,
				//GDEItemKeys.Item_Consume_SkillBookLucy_Rare,
			};
			AddItem(keys.Random("MiyukiRandomBook"));
		}

		public static void GainRandomMisc()
		{
			List<string> keys = new List<string>
			{
				GDEItemKeys.Item_Misc_ArtifactPlusInven,
				GDEItemKeys.Item_Misc_BlackironMoru,
				GDEItemKeys.Item_Misc_Item_Key,
				GDEItemKeys.Item_Misc_Scrap_0,
				GDEItemKeys.Item_Misc_Scrap_1,
				GDEItemKeys.Item_Misc_RWEnterItem,
				//GDEItemKeys.Item_Misc_TimeMoney,
			};
			AddItem(keys.Random("MiyukiRandomConsumable"));
		}
	}
}
