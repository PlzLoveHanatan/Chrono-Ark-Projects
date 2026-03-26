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
			if (harmony != null)
			{
				harmony.UnpatchSelf();
			}
		}

		public override void Initialize()
		{
			try
			{
				Data.LoadAllDialogues();
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
				var partyCharacters = GetPartyCharacters();

				if (partyCharacters.Count == 0) return true;

				if (Data.TextToAudio.TryGetValue(inText, out var audioInfo))
				{
					if (partyCharacters.TryGetValue(audioInfo.character, out string activeSkin))
					{
						string targetSkin = (audioInfo.skin == activeSkin) ? activeSkin : (audioInfo.skin == "Normal") ? "Normal" : null;

						if (targetSkin != null)
						{
							Utils.PlayCharacterAudio(audioInfo.character, targetSkin, audioInfo.audioFile);
						}
					}
				}

				return true;
			}

			private static Dictionary<string, string> GetPartyCharacters()
			{
				var partyCharacters = new Dictionary<string, string>();

				foreach (var character in PlayData.TSavedata.Party)
				{
					string charId = character.KeyData;
					string characterName = Data.GetCharacterName(charId);

					if (!string.IsNullOrEmpty(characterName))
					{
						string skinName = Utils.GetCharacterSkin(charId);

						if (Data.HasSkin(characterName, skinName))
						{
							partyCharacters[characterName] = skinName;
						}
						else
						{
							partyCharacters[characterName] = "Normal";
						}
					}
				}

				return partyCharacters;
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

				string charId = skill.Master.Info.KeyData;
				string charName = Data.GetCharacterName(charId);
				if (string.IsNullOrEmpty(charName)) return;

				string path = $"Assets/{charName}/Skills/{skill.MySkill.KeyID}.wav";
				Utils.PlaySoundFromAsset(path);
			}
		}
	}
}