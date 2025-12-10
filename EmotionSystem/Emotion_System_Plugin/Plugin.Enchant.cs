using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ChronoArkMod;
using DarkTonic.MasterAudio;
using EItem;
using GameDataEditor;
using HarmonyLib;
using Scrolls;
using UnityEngine;

namespace EmotionSystem
{
	[HarmonyPatch]
	public partial class EmotionlSystem_Plugin
	{
		// patch enchant ui for whetstone
		[HarmonyPrefix]
		[HarmonyPatch(typeof(UI_Enchant), nameof(UI_Enchant.ItemUse))]
		static bool UI_Enchant_Prefix(UI_Enchant __instance, ItemObject select)
		{
			if (__instance.UseItem.itemkey == ModItemKeys.Item_Consume_C_EmotionSystem_Whetstone)
			{
				if (select.Item is Item_Equip equip /*&& select.Item.itemkey != GDEItemKeys.Item_Equip_EndlessScroll*/)
				{
					if (equip.Enchant != null && equip.Enchant.CurseEnchant)
					{
						return false;
					}

					List<string> allAvailableEnchants = new List<string>();
					allAvailableEnchants.AddRange(DataStore.Instance.EnchantsAbno);

					if (equip.Enchant.Key != null && equip.itemkey != GDEItemKeys.Item_Equip_EndlessScroll)
					{
						allAvailableEnchants.Remove(equip.Enchant.Key);
					}
					
					if (allAvailableEnchants.Count > 0)
					{
						int index = RandomManager.RandomInt(RandomClassKey.CursedEnchant, 0, allAvailableEnchants.Count);
						string randomKey = allAvailableEnchants[index];
						ItemEnchant.RandomEnchantTarget(equip, randomKey);
					}

					equip._Isidentify = true;

					//if (equip.ItemScript is EquipItem_Enchent ench)
					//{
					//	ench.Enchent();
					//}

					MasterAudio.PlaySound("Enchent", 1f);
					__instance.SelfDestroy();

					//__instance.UseItem.MyManager.DelItem(__instance.UseItem, 1);
					Scroll_UseDefult.ScrollUseEffect(__instance.UseItem.GetName, __instance.UseItem.itemkey);

					if (FieldSystem.instance != null)
					{
						foreach (AllyWindow ally in FieldSystem.instance.PartyWindow)
						{
							ally.EquipUpdate();
						}
					}

					Utils.Data.WhetstoneCharge--;
					//PlayData.TSavedata._Gold -= 150;

					return false;
				}

				return false;
			}
			return true;
		}

		// replica stat
		[HarmonyPostfix]
		[HarmonyPatch(typeof(Replica), "BattleStartUIOnBefore")]
		private static void ReplicaPostfix(Replica __instance)
		{
			B_Replica b_Replica = __instance.BChar.BuffReturn("B_Replica", false) as B_Replica;
			bool flag = b_Replica != null;
			if (flag)
			{
				Item_Equip equipNew = __instance.MyChar.Equip[b_Replica.Index] as Item_Equip;
				Utils_UI.CopyEquipEnchantCurse(equipNew, __instance.MyItem);
			}
		}

		// Gain Enchants when forging 2 Legendary Equips at campfire
		[HarmonyPatch(typeof(CampAnvilEvent))]
		[HarmonyPatch(nameof(CampAnvilEvent.B1Fuc))]

		[HarmonyPrefix]
		static bool Prefix(CampAnvilEvent __instance)
		{
			if (!__instance.CombineBtn.interactable)
			{
				return true;
			}
			if (__instance.InventoryItems[0] != null && __instance.InventoryItems[1] != null)
			{
				if (__instance.InventoryItems[0] is Item_Equip && __instance.InventoryItems[1] is Item_Equip)
				{
					Debug.Log("Item 1: " + __instance.InventoryItems[0].itemkey);
					Debug.Log("Item 2: " + __instance.InventoryItems[1].itemkey);
					if (__instance.InventoryItems[0].ItemClassNum == 4 && __instance.InventoryItems[1].ItemClassNum == 4)
					{
						List<ItemBase> items = new List<ItemBase> {
							ItemBase.GetItem(__instance.InventoryItems[0].itemkey),
							ItemBase.GetItem(__instance.InventoryItems[0].itemkey),
							ItemBase.GetItem(__instance.InventoryItems[0].itemkey),
							ItemBase.GetItem(__instance.InventoryItems[1].itemkey),
							ItemBase.GetItem(__instance.InventoryItems[1].itemkey),
							ItemBase.GetItem(__instance.InventoryItems[1].itemkey),
							};


						List<string> allAvailableEnchants = new List<string>();
						List<string> selectedRandomEnchants = new List<string>();

						allAvailableEnchants.AddRange(DataStore.Instance.LegendaryEnchantsAbno);
						int maxEnchantCount = Math.Min(6, allAvailableEnchants.Count);

						for (int i = 0; i < maxEnchantCount; i++)
						{
							int randomIndex = RandomManager.RandomInt(RandomClassKey.CursedEnchant, 0, allAvailableEnchants.Count);
							selectedRandomEnchants.Add(allAvailableEnchants[randomIndex]);
							allAvailableEnchants.RemoveAt(randomIndex);
						}

						int maxEquipCount = Math.Min(6, items.Count);

						for (int i = 0; i < maxEquipCount; i++)
						{
							if (items[i] is Item_Equip currentEquip && i < selectedRandomEnchants.Count)
							{
								currentEquip.Enchant = ItemEnchant.NewEnchant(currentEquip, selectedRandomEnchants[i]);
								currentEquip._Isidentify = true;
							}
						}

						MasterAudio.PlaySound("Anvil", 1f, null, 0f, null, null, false, false);
						UIManager.InstantiateActive(UIManager.inst.SelectItemUI).GetComponent<SelectItemUI>().Init(items, null, false);
						__instance.DelItem(0);
						__instance.DelItem(1);
						PlayData.TSavedata.AnvilCount--;
						return false;

					}
				}
				return true;
			}
			return true;
		}

		// make reading enchant type correct
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(ItemEnchant), nameof(ItemEnchant.NewEnchant))]
		static IEnumerable<CodeInstruction> NewEnchantTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			foreach (var instruction in instructions)
			{
				if (instruction.Is(OpCodes.Call, AccessTools.Method(typeof(ModManager),
					nameof(ModManager.GetType), new Type[] { typeof(string), typeof(string) })))
				{
					instruction.operand = AccessTools.Method(typeof(Utils_UI), nameof(Utils_UI.GetEnchantType));
				}
				yield return instruction;
			}
		}

		// patch to show full info of enchants
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(Item_Equip), nameof(Item_Equip.ToolTip))]
		static IEnumerable<CodeInstruction> ItemTooltipTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			bool flag = false;
			int num = 0;
			foreach (var instruction in instructions)
			{
				yield return instruction;
				if (instruction.Is(OpCodes.Ldfld, AccessTools.Field(typeof(ItemEnchant), nameof(ItemEnchant.CurseEnchant))))
				{
					flag = true;
				}
				if (flag && num < 2 && instruction.Is(OpCodes.Call, AccessTools.Method(typeof(Misc), nameof(Misc.InputColor))))
				{
					num++;
					yield return new CodeInstruction(OpCodes.Pop);
					yield return new CodeInstruction(OpCodes.Ldarg_0);
					yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Item_Equip), nameof(Item_Equip.Enchant)));
					yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Utils_UI), nameof(Utils_UI.GetDescription)));
				}
			}
		}
	}
}
