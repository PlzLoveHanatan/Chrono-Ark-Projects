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
using static MiyukiSone.Utils;
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

		[HarmonyPatch(typeof(PlayData))]
		[HarmonyPatch(nameof(PlayData.GameEndInit))]
		public class Patch_Reset_Save
		{
			[HarmonyPostfix]
			public static void Postfix()
			{
				MiyukiSoneSaveManager.Instance.ResetSave();
				Debug.Log("Miyuki save file reset coimplete");
			}
		}

		[HarmonyPatch(typeof(BattleSystem))]
		[HarmonyPatch(nameof(BattleSystem.TurnEnd))]
		class Patch_BattleSystem_TurnEnd
		{
			[HarmonyPrefix]
			public static bool Prefix()
			{
				if (Dialogue.dialogueWindows.Count > 0)
				{
					foreach (var windowObj in Dialogue.dialogueWindows)
					{
						if (windowObj != null)
						{
							DialogueData.MiyukiTextBoxTurn();
							ChangeAffectionPointsRandom();
						}
					}
					return false;
				}
				return true;
			}
		}

		[HarmonyPatch(typeof(FieldSystem))]
		[HarmonyPatch(nameof(FieldSystem.StageStart))]
		public static class StagePatch
		{
			[HarmonyPostfix]
			public static void StageStartPostfix()
			{
				var data = MiyukiSoneSaveManager.Instance.CurrentData;
				if (!data.GameRestarted && data.GameUpdated)
				{
					data.GameRestarted = true;
					MiyukiSoneSaveManager.Instance.Save();
					Fs?.StartCoroutine(WaitForSeconds());
				}

			}

			private static IEnumerator WaitForSeconds()
			{
				yield return new WaitForSeconds(4f);
				yield return EventRandom.ExitGame();
			}
		}
	}
}