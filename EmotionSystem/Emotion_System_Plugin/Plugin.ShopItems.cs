using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using HarmonyLib;

namespace EmotionSystem
{
	public partial class Plugin
	{
		// Regular shops
		[HarmonyPatch(typeof(FieldStore))]
		public static class NewShopItems
		{
			[HarmonyPatch("Init")]
			[HarmonyPostfix]
			public static void NewItems(FieldStore __instance)
			{
				//if (!Utils.DistortedBosses) return;

				int stage = PlayData.TSavedata.StageNum;

				__instance.StoreItems.Add(ItemBase.GetPotionRandom());
				__instance.StoreItems.Add(ItemBase.GetPotionRandom());
				__instance.StoreItems.Add(ItemBase.GetItem(ModItemKeys.Item_Consume_C_EmotionSystem_DreamingCurrent));
				__instance.StoreItems.Add(ItemBase.GetItem(ModItemKeys.Item_Consume_C_EmotionSystem_Fractured));

				if (stage == 1)
				{
					__instance.StoreItems.Add(ItemBase.GetItem(ModItemKeys.Item_Consume_C_EmotionSystem_Whetstone));
				}
			}
		}

		// Shop in White Space before Azar fight
		[HarmonyPatch(typeof(SpecialStore))]
		public static class NewSpecialShopItems
		{
			[HarmonyPatch("Start")]
			[HarmonyPostfix]
			public static void NewItems(SpecialStore __instance)
			{
				__instance.StoreItems.Add(ItemBase.GetPotionRandom());
				__instance.StoreItems.Add(ItemBase.GetPotionRandom());
				__instance.StoreItems.Add(ItemBase.GetItem(ModItemKeys.Item_Consume_C_EmotionSystem_DreamingCurrent));
				__instance.StoreItems.Add(ItemBase.GetItem(ModItemKeys.Item_Consume_C_EmotionSystem_Fractured));
			}
		}
	}
}
