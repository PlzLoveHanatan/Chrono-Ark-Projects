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
using EmotionSystem;
namespace QoH
{
	public class QoH_Plugin : ChronoArkPlugin
	{
		public const string modname = "QoH";

		public const string version = "1.0";

		public const string author = "Midana";

		public static ModInfo ThisMod => ModManager.getModInfo(modname);

		Harmony harmony = new Harmony("QoH");

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
				Debug.Log("QoH: Patch Catch: " + e.ToString());
			}
		}

		public static bool QoHInParty()
		{
			return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_QoH);
		}
	}
}