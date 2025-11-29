using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin
	{
		[HarmonyPatch(typeof(Buff), "TurnUpdate")]
		public class NoTimeDecreasePatch
		{
			public static void Prefix(Buff __instance)
			{
				if (__instance.TimeUseless)
				{
					foreach (var stack in __instance.StackInfo)
					{
						stack.RemainTime = int.MaxValue;
					}
				}
			}
		}
	}
}
