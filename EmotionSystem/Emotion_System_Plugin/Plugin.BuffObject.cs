using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using HarmonyLib;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin
	{
		[HarmonyPatch(typeof(BuffObject))]
		public class BuffObjectUpdatePlugin
		{
			[HarmonyPatch("Update")]
			[HarmonyPostfix]
			public static void Update_Patch(BuffObject __instance)
			{
				if (__instance.MyBuff != null)
				{
					IP_BuffObject_Updata ip_BuffObject_Updata = __instance.MyBuff as IP_BuffObject_Updata;
					if (ip_BuffObject_Updata != null)
					{
						ip_BuffObject_Updata.BuffObject_Updata(__instance);
					}
				}
			}
		}
	}
}
