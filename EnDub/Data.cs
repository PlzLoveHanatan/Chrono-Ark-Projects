using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using GameDataEditor;
using ChronoArkMod;

namespace EnDub
{
	[Serializable]
	public class DialogueLine
	{
		public string key;
		public string character;
		public string skin;
		public string Korean;
		public string English;
		public string Japanese;
		public string Chinese;
		public string Chinese_TW;
		public string AudioFile;
	}

	public class AudioInfo
	{
		public string character;
		public string skin;
		public string audioFile;
	}

	public static class Data
	{
		// character -> skin -> key -> DialogueLine
		private static Dictionary<string, Dictionary<string, Dictionary<string, DialogueLine>>> AllLines = new Dictionary<string, Dictionary<string, Dictionary<string, DialogueLine>>>();
		public static Dictionary<string, AudioInfo> TextToAudio = new Dictionary<string, AudioInfo>();
		public static List<string> AvailableCharacters = new List<string>();

		public static Dictionary<string, string> CharacterKey = new Dictionary<string, string>()
		{
			{ GDEItemKeys.Character_ShadowPriest, "Charon" },
			{ GDEItemKeys.Character_Queen, "Huz" },
			{ GDEItemKeys.Character_Lian, "Lian" },
			{ GDEItemKeys.Character_SilverStein, "Silverstein" },
			{ GDEItemKeys.Character_Sizz, "Sizz" },
			{ GDEItemKeys.Character_Azar, "Azar" },
			{ GDEItemKeys.Character_Control, "Narhan" },
		};

		public static Dictionary<string, string> SkinKey = new Dictionary<string, string>()
		{
			{ GDEItemKeys.Character_Skin_Charon_Swimsuit, "Swimsuit" },
			{ GDEItemKeys.Character_Skin_Huz_Swimsuit, "Swimsuit" },
			{ GDEItemKeys.Character_Skin_SilverStein_Casino, "Casino" },
			{ GDEItemKeys.Character_Skin_Sizz_Swimsuit, "Swimsuit" },
			{ GDEItemKeys.Character_Skin_Azar_Swimsuit, "Swimsuit" },
			{ GDEItemKeys.Character_Skin_Narhan_Casino, "Casino" },
		};	

		public static void LoadAllDialogues()
		{
			string modPath = Utils.ThisMod.DirectoryName;
			string jsonPath = Path.Combine(modPath, "Assets", "Dialogues.json");

			if (!File.Exists(jsonPath))
			{
				Debug.LogError($"Dialogues file not found: {jsonPath}");
				return;
			}

			try
			{
				string jsonContent = File.ReadAllText(jsonPath);
				DialogueLine[] dialogueArray = JsonConvert.DeserializeObject<DialogueLine[]>(jsonContent);

				AllLines.Clear();
				TextToAudio.Clear();
				AvailableCharacters.Clear();

				foreach (var line in dialogueArray)
				{
					if (string.IsNullOrEmpty(line.character)) continue;

					if (!AllLines.ContainsKey(line.character))
					{
						AllLines[line.character] = new Dictionary<string, Dictionary<string, DialogueLine>>();
						AvailableCharacters.Add(line.character);
					}

					string skin = string.IsNullOrEmpty(line.skin) ? "Normal" : line.skin;
					if (!AllLines[line.character].ContainsKey(skin))
					{
						AllLines[line.character][skin] = new Dictionary<string, DialogueLine>();
					}

					AllLines[line.character][skin][line.key] = line;

					var audioInfo = new AudioInfo
					{
						character = line.character,
						skin = skin,
						audioFile = line.AudioFile
					};

					AddIfNotEmpty(line.English, audioInfo);
					AddIfNotEmpty(line.Korean, audioInfo);
					AddIfNotEmpty(line.Japanese, audioInfo);
					AddIfNotEmpty(line.Chinese, audioInfo);
					AddIfNotEmpty(line.Chinese_TW, audioInfo);
				}

				Debug.Log($"Loaded {dialogueArray.Length} lines for {AvailableCharacters.Count} characters");
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error loading dialogues: {ex.Message}");
			}
		}

		private static bool AddIfNotEmpty(string text, AudioInfo info)
		{
			if (!string.IsNullOrEmpty(text) && !TextToAudio.ContainsKey(text))
			{
				TextToAudio.Add(text, info);
				return true;
			}
			return false;
		}

		public static bool HasSkin(string characterName, string skinName)
		{
			if (AllLines.TryGetValue(characterName, out var skins))
			{
				return skins.ContainsKey(skinName);
			}
			return false;
		}

		public static DialogueLine GetLine(string characterName, string skinName, string key)
		{
			if (AllLines.TryGetValue(characterName, out var skins))
			{
				if (skins.TryGetValue(skinName, out var lines))
				{
					if (lines.TryGetValue(key, out var line))
					{
						return line;
					}
				}
				if (skins.TryGetValue("Normal", out var normalLines))
				{
					if (normalLines.TryGetValue(key, out var normalLine))
					{
						return normalLine;
					}
				}
			}
			return null;
		}

		public static DialogueLine GetLine(string characterName, string key)
		{
			return GetLine(characterName, "Normal", key);
		}

		public static string GetCharacterName(string gameId)
		{
			if (CharacterKey.TryGetValue(gameId, out string characterName))
			{
				return characterName;
			}
			return null;
		}

		public static string GetSkinNameByKey(string skinKey)
		{
			if (string.IsNullOrEmpty(skinKey)) return "Normal";

			if (SkinKey.TryGetValue(skinKey, out string mappedName))
			{
				return mappedName;
			}

			if (CharacterSkinData._swimSuitSkinKeys.Contains(skinKey)) return "Swimsuit";
			if (CharacterSkinData._casinoSkinKeys.Contains(skinKey)) return "Casino";
			return "Normal";
		}
	}
}