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
		public static Dictionary<string, AudioInfo> textToAudio = new Dictionary<string, AudioInfo>();
		public static Dictionary<string, Dictionary<string, DialogueLine[]>> characterSkinLines = new Dictionary<string, Dictionary<string, DialogueLine[]>>();
		public static List<string> availableCharacters = new List<string>();

		public static Dictionary<string, string> gameIdToCharacterName = new Dictionary<string, string>()
		{
			{ GDEItemKeys.Character_ShadowPriest, "Charon" },
			{ GDEItemKeys.Character_Queen, "Huz" },
			{ GDEItemKeys.Character_Lian, "Lian" },
		};

		public static Dictionary<string, string> skinKeyToName = new Dictionary<string, string>()
		{
			{ GDEItemKeys.Character_Skin_Charon_Swimsuit, "Swimsuit" },
			{ GDEItemKeys.Character_Skin_Huz_Swimsuit, "Swimsuit" },
		};

		public static void LoadAllCharacterAudio()
		{
			string modPath = Utils.ThisMod.DirectoryName;
			string audioDataPath = Path.Combine(modPath, "Assets");

			if (!Directory.Exists(audioDataPath))
			{
				Debug.LogError($"AudioData folder not found at: {audioDataPath}");
				return;
			}

			textToAudio.Clear();
			characterSkinLines.Clear();
			availableCharacters.Clear();

			foreach (string characterDir in Directory.GetDirectories(audioDataPath))
			{
				string characterName = Path.GetFileName(characterDir);
				availableCharacters.Add(characterName);

				if (!characterSkinLines.ContainsKey(characterName))
				{
					characterSkinLines[characterName] = new Dictionary<string, DialogueLine[]>();
				}

				foreach (string jsonFile in Directory.GetFiles(characterDir, "*.json"))
				{
					string skinName = Path.GetFileNameWithoutExtension(jsonFile);
					LoadCharacterSkin(characterName, skinName, jsonFile);
				}
			}

			Debug.Log($"Loaded {availableCharacters.Count} characters with {textToAudio.Count} total text mappings");
		}

		private static void LoadCharacterSkin(string characterName, string skinName, string jsonPath)
		{
			try
			{
				string jsonContent = File.ReadAllText(jsonPath);
				DialogueLine[] dialogueArray = JsonConvert.DeserializeObject<DialogueLine[]>(jsonContent);

				if (skinName == characterName)
				{
					skinName = "Normal";
				}
				else
				{
					string prefix = characterName + "_";
					if (skinName.StartsWith(prefix))
					{
						skinName = skinName.Substring(prefix.Length);
					}
				}

				characterSkinLines[characterName][skinName] = dialogueArray;

				foreach (var line in dialogueArray)
				{
					var audioInfo = new AudioInfo
					{
						character = characterName,
						skin = skinName,
						audioFile = line.AudioFile
					};

					AddIfNotEmpty(line.English, audioInfo);
					AddIfNotEmpty(line.Korean, audioInfo);
					AddIfNotEmpty(line.Japanese, audioInfo);
					AddIfNotEmpty(line.Chinese, audioInfo);
					AddIfNotEmpty(line.Chinese_TW, audioInfo);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error loading {jsonPath}: {ex.Message}");
			}
		}

		private static bool AddIfNotEmpty(string text, AudioInfo info)
		{
			if (!string.IsNullOrEmpty(text) && !textToAudio.ContainsKey(text))
			{
				textToAudio.Add(text, info);
				return true;
			}
			return false;
		}

		public static string GetCharacterNameByGameId(string gameId)
		{
			if (gameIdToCharacterName.TryGetValue(gameId, out string characterName))
			{
				return characterName;
			}
			return null;
		}

		public static string GetSkinNameByKey(string skinKey)
		{
			if (string.IsNullOrEmpty(skinKey)) return "Normal";

			if (skinKeyToName.TryGetValue(skinKey, out string mappedName))
			{
				return mappedName;
			}

			if (CharacterSkinData._swimSuitSkinKeys.Contains(skinKey)) return "Swimsuit";
			if (CharacterSkinData._casinoSkinKeys.Contains(skinKey)) return "Casino";
			return "Normal";
		}

		public static bool HasSkin(string characterName, string skinName)
		{
			if (characterSkinLines.TryGetValue(characterName, out var skins))
			{
				return skins.ContainsKey(skinName);
			}
			return false;
		}

		public static List<string> GetAvailableSkins(string characterName)
		{
			if (characterSkinLines.TryGetValue(characterName, out var skins)) return skins.Keys.ToList();
			return new List<string>();
		}
	}
}