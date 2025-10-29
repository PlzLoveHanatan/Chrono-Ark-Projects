using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using ChronoArkMod.Plugin;
using UnityEngine;
using ChronoArkMod.ModData;
using ChronoArkMod;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin : ChronoArkPlugin
	{
		public const string modname = "EmotionSystem";

		public const string version = "1.0";

		public const string author = "Midana && surprise4u";

		public static ModInfo ThisMod => ModManager.getModInfo(modname);

		Harmony harmony = new Harmony("EmotionSystem");

		public override void Dispose()
		{
			if (harmony != null)
			{
				harmony.UnpatchSelf();
			}
		}

		public override void Initialize()
		{
			try
			{
				harmony.PatchAll();
			}
			catch (Exception e)
			{
				Debug.Log("EmotionSystem: Patch Catch: " + e.ToString());
			}
		}
	}
}
