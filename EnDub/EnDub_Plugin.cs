using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using GameDataEditor;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using HarmonyLib;
using Debug = UnityEngine.Debug;
using ChronoArkMod.ModData;
using NLog.Config;
using System.Windows.Markup;

namespace EnDub
{
	public class EnDub_Plugin : ChronoArkPlugin
	{
		public const string modname = "EnDub";
		public const string version = "1.0";
		public const string author = "Midana";

		public static ModInfo ThisMod => ModManager.getModInfo(modname);
		Harmony harmony = new Harmony("EnDub");

		public override void Dispose()
		{
			harmony?.UnpatchSelf();
		}

		public override void Initialize()
		{
			try
			{
				EnDubDialogueData.LoadDialogue();
				var console = EnDubConsole.Instance;
				console.ShowConsole();
				harmony.PatchAll();
			}
			catch (System.Exception e)
			{
				Debug.Log("EnDub: Patch Catch: " + e.ToString());
			}
		}

		[HarmonyPatch(typeof(PrintText))]
		[HarmonyPatch(nameof(PrintText.TextInput))]
		public class VoiceOn
		{
			[HarmonyPrefix]
			public static bool Prefix(PrintText __instance, string inText)
			{
				EnDubDialogueData.GetLineByText(inText)?.Let(line => EnDubUtils.PlayCharacterAudio(line.character, line.skin, line.AudioFile));
				return true;
			}
		}

		[HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.HandUseIPFunc))]
		public static class SkillVoicePatch
		{
			[HarmonyPrefix]
			public static void PlayVoice(BattleSystem __instance)
			{
				var skill = __instance.SelectedSkill;
				if (skill?.Master?.Info?.KeyData == null) return;
				string charName = EnDubDialogueData.GetCharacterName(skill.Master.Info.KeyData);
				string path = $"Assets/{charName}/Skills/{skill.MySkill.KeyID}.wav";
				EnDubUtils.PlayAudio(path, charName);
			}
		}
	}
}