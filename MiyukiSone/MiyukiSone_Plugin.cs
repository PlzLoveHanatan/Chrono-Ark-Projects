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
using UseItem;
using System.Reflection.Emit;
using static MiyukiSone.Affection;
namespace MiyukiSone
{
	public class MiyukiSone_Plugin : ChronoArkPlugin
	{
		public const string modname = "MiyukiSone";

		public const string version = "0.9";

		public const string author = "MiyukiSone";

		private readonly Harmony harmony = new Harmony("MiyukiSone");

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
				Debug.Log("MiyukiSone: Patch Catch: " + e.ToString());
			}
		}

		public static bool MiyukiInParty()
		{
			return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Miyuki);
		}

		[HarmonyPatch(typeof(BattleSystem))]
		[HarmonyPatch(nameof(BattleSystem.TurnEnd))]
		class Patch_BattleSystem_TurnEnd
		{
			[HarmonyPrefix]
			public static bool Prefix()
			{
				if (DialogueBox.dialogueWindows.Count > 0)
				{
					foreach (var windowObj in DialogueBox.dialogueWindows)
					{
						if (windowObj != null)
						{
							DialogueBoxData.MiyukiTextBoxTurn();
						}
					}
					return false;
				}
				return true;
			}
		}
	}
}