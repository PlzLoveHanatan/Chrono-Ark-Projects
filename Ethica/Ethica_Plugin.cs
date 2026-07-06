using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using ChronoArkMod.ModData;
using HarmonyLib;
namespace Ethica
{
    public class Ethica_Plugin: ChronoArkPlugin
    {
		public const string modname = "Ethica";

		public const string version = "1.0";

		public const string author = "Midana";

		public static ModInfo ThisMod => ModManager.getModInfo(modname);

		Harmony harmony = new Harmony("Ethica");

		public override void Dispose()
		{
			harmony?.UnpatchSelf();
		}

		public override void Initialize()
		{
			try
			{
				harmony.PatchAll();
			}
			catch (Exception e)
			{
				Debug.Log("Ethica: Patch Catch: " + e.ToString());
			}
		}
	}
}