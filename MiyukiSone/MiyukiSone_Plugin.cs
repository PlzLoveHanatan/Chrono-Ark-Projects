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
using static MiyukiSone.MiyukiAffection;
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

		// Reset custom save file
		[HarmonyPatch(typeof(PlayData))]
		[HarmonyPatch(nameof(PlayData.GameEndInit))]
		public class Patch_Reset_Save
		{
			[HarmonyPostfix]
			public static void Postfix()
			{
				MiyukiSaveManager.Instance.ResetSave();
				Debug.Log("Miyuki save file reset coimplete");
			}
		}

		// Redirect skills in Yandere mood
		[HarmonyPatch(typeof(BattleAlly))]
		[HarmonyPatch(nameof(BattleAlly.UseSkill), new Type[] { typeof(Skill), typeof(List<BattleChar>) })]
		public class Skill_Redirect_Patch
		{
			[HarmonyPrefix]
			public static void Prefix(BattleAlly __instance, Skill skill, ref List<BattleChar> Target)
			{
				if (!Affection.IsYandere || skill.Master.Info.KeyData != ModItemKeys.Character_Miyuki || !MiyukiInParty()) return;
				if (RandomManager.RandomPer("MiyukiYandereRedirect", 100, 70)) return;

				List<BattleChar> possibleTargets = new List<BattleChar>();

				if (skill.IsDamage) possibleTargets = AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList();
				else if (skill.IsHeal) possibleTargets = Utils.EnemyTeam.AliveChars.Where(a => a != null).ToList();

				if (possibleTargets.Count > 0)
				{
					BattleChar target = possibleTargets.Random("MiyukiRandomTarget");
					Target.Clear();
					Target.Add(target);
					EventsData.MiyukiTextEvent(Affection.CurrentAffection);
				}
			}
		}

		[HarmonyPatch(typeof(BattleSystem))]
		[HarmonyPatch(nameof(BattleSystem.TurnEnd))]
		class Patch_BattleSystem_TurnEnd
		{
			[HarmonyPrefix]
			public static bool Prefix()
			{
				if (Dialogue.DialogueWindows.Count > 0)
				{
					DialogueData.StartTurnEndDialogue();
					if (Affection.MiyukiDecides) Affection.CurrentAffection = Yandere;
					if (Affection.IsYandere) Events.YandereActionCut();
					foreach (var windowObj in Dialogue.DialogueWindows)
					{
						if (windowObj != null)
						{
							
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
				var data = MiyukiSaveManager.Instance.CurrentData;
				if (!data.GameRestarted && data.GameUpdated)
				{
					data.GameRestarted = true;
					MiyukiSaveManager.Instance.Save();
					Fs?.StartCoroutine(WaitForSeconds());
				}

			}

			private static IEnumerator WaitForSeconds()
			{
				yield return new WaitForSeconds(2.5f);
				yield return Events.ExitGame();
			}
		}
	}
}