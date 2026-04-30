using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using GameDataEditor;
using ChronoArkMod;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.ServiceModel.Configuration;

namespace EnDub
{
	[Serializable]
	public class DialogueLine
	{
		public string Key;
		public string Character;
		public string Skin;
		public string Korean;
		public string English;
		public string Japanese;
		public string Chinese;
		public string Chinese_TW;
		public string AudioFile;
	}

	public static class DialogueData
	{
		private static List<DialogueLine> allLines;
		private static Dictionary<string, DialogueLine> textIndex;

		public static void LoadDialogue()
		{
			string jsonPath = Path.Combine(Utils.ThisMod.DirectoryName, "Assets", "Dialogues.json");

			if (File.Exists(jsonPath))
			{
				try
				{
					string jsonContent = File.ReadAllText(jsonPath);
					allLines = JsonConvert.DeserializeObject<List<DialogueLine>>(jsonContent);
					textIndex = new Dictionary<string, DialogueLine>();
					allLines.ForEach(line => AddTextIndex(line));
					Debug.Log($"Loaded {allLines.Count} dialogue lines");
					DialogueFixer.Initialize();
				}
				catch (Exception ex)
				{
					Debug.LogError($"Error loading dialogues: {ex.Message}");
				}
			}
			else
			{
				Debug.LogError($"Json file is not found in the {jsonPath} path");
			}
		}

		private static void AddTextIndex(DialogueLine line)
		{
			string[] texts = { line.English, line.Korean, line.Japanese, line.Chinese, line.Chinese_TW };

			foreach (string text in texts)
			{
				if (!string.IsNullOrEmpty(text) && !textIndex.ContainsKey(text))
				{
					textIndex.Add(text, line);
				}
			}
		}

		public static DialogueLine GetLineByText(string text)
		{
			return textIndex != null && textIndex.TryGetValue(text, out var line) ? line : null;
		}

		public static List<DialogueLine> GetLineByCharacter(string character)
		{
			return allLines?.Where(l => l.Character == character).ToList() ?? new List<DialogueLine>();
		}

		public static List<DialogueLine> GetLineByCharacterAndSkin(string character, string skin)
		{
			return allLines?.Where(l => l.Character == character && l.Skin == skin).ToList() ?? new List<DialogueLine>();
		}

		public static string GetCharacterName(string gameKey, bool isGameKey = true)
		{
			var dicGameKey = new Dictionary<string, string>()
			{
				{ GDEItemKeys.Character_Azar, "Azar" },
				{ GDEItemKeys.Character_Control, "Narhan" },
				{ GDEItemKeys.Character_Lian, "Lian" },
				{ GDEItemKeys.Character_Mement, "Johan" },
				{ GDEItemKeys.Character_Queen, "Huz" },
				{ GDEItemKeys.Character_ShadowPriest, "Charon" },
				{ GDEItemKeys.Character_SilverStein, "Silverstein" },
				{ GDEItemKeys.Character_Sizz, "Sizz" },
			};

			var dicString = new Dictionary<string, string>()
			{
				{ "Azar", "Azar" },
				{ "Control", "Narhan" },
				{ "Lian", "Lian" },
				{ "Mement", "Johan" },
				{ "Queen", "Huz" },
				{ "ShadowPriest", "Charon" },
				{ "SilverStein", "Silverstein" },
				{ "Sizz", "Sizz" },
			};

			var dict = isGameKey ? dicGameKey : dicString;

			return dict.TryGetValue(gameKey, out string name) ? name : "";
		}
	}
}