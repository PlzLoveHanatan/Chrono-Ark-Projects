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
using Spine;
using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace EnDub
{
	public class EnDub_Plugin : ChronoArkPlugin
	{
		public const string modname = "EnDub";
		public const string version = "1.11";
		public const string author = "Midana";

		public static ModInfo ThisMod => ModManager.getModInfo(modname);
		Harmony harmony = new Harmony("EnDub");

		public override void Dispose()
		{
			//DialogueFixer.Restore();
			harmony?.UnpatchSelf();			
		}

		public override void Initialize()
		{
			try
			{
				DialogueData.LoadDialogue();
				Console.Instance?.ShowConsole();
				SaveManager.Instance?.Save();
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
				string character = null;
				//if (inText.Contains("Not bad."))
				//{
				//	character = PlayData.TSavedata.Party.Any(a => a.KeyData == GDEItemKeys.Character_Mement) ? "Johan" : "Charon";
				//}

				DialogueData.GetLineByText(inText)?.Let(line => Utils.PlayCharacterAudio(character ?? line.Character, line.Skin, line.AudioFile));
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
				string charName = DialogueData.GetCharacterName(skill.Master.Info.KeyData);
				string path = $"Assets/{charName}/Skills/{skill.MySkill.KeyID}";
				Utils.PlayAudio(path, charName);
			}
		}
	}
}