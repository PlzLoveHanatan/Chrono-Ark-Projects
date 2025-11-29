using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using HarmonyLib;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin
	{
		// Buff stack remove patch
		[HarmonyPatch(typeof(Buff), "TurnUpdate")]
		public class NoTimeDecreasePatch
		{
			private static readonly HashSet<string> Exception = new HashSet<string>()
			{
				ModItemKeys.Buff_B_EmotionSystem_Bleed,
				ModItemKeys.Buff_B_EmotionSystem_Burn,
			};

			public static void Prefix(Buff __instance)
			{
				if (!Utils.Data.TorchActive) return;

				var buffKey = __instance.BuffData.BuffTag.Key;

				if (__instance.TimeUseless && buffKey == GDEItemKeys.BuffTag_DOT && !Exception.Contains(__instance.BuffData.Key))
				{
					foreach (var stack in __instance.StackInfo)
					{
						stack.RemainTime++;
					}
				}
			}
		}
	}
}
