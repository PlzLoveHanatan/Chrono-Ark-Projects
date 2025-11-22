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
		[HarmonyPatch(typeof(FieldStore))]
		public static class NewShopItems
		{
			[HarmonyPatch("Init")]
			[HarmonyPostfix]
			public static void NewItems(FieldStore __instance)
			{
				if (!Utils.DistortedBosses) return;

				for (int i = 0; i < 2; i++)
				{
					__instance.StoreItems.Add(ItemBase.GetPotionRandom());
				}
			}
		}
	}
}
