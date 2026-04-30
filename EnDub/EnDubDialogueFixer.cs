using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using UnityEngine;

namespace EnDub
{
	[Serializable]
	public class FixesData
	{
		public Dictionary<string, List<string>> OriginalText { get; set; }
		public Dictionary<string, string> TextFixes { get; set; }
	}

	public static class DialogueFixer
	{
		private static string FixesPath => Path.Combine(Application.persistentDataPath, "Endub", "TextFixes.json");

		public static Dictionary<string, List<string>> OriginalText = new Dictionary<string, List<string>>();
		public static Dictionary<string, string> TextFixes = new Dictionary<string, string>();

		public static void Initialize()
		{
			LoadFixesFromFile();

			if (TextFixes.Count > 0)
			{
				Debug.Log($"Applying existing fixes ({TextFixes.Count} entries)");
				ApplyFixes();
			}
			else
			{
				Debug.Log("No existing fixes found, scanning for duplicates...");
				ScanAndCreateFixes();
				if (TextFixes.Count > 0)
				{
					Debug.Log($"Found {TextFixes.Count} duplicates, applying fixes");
					ApplyFixes();
					SaveFixesToFile();
				}
				else
				{
					Debug.Log("No duplicates found");
				}
			}
		}

		private static void ScanAndCreateFixes()
		{
			if (!File.Exists(Utils.Path_CSV)) return;

			OriginalText.Clear();
			TextFixes.Clear();

			var idToText = new List<(string id, string text)>();

			using (var parser = new TextFieldParser(Utils.Path_CSV, Encoding.UTF8))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				parser.HasFieldsEnclosedInQuotes = true;

				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();
					if (fields.Length < 5) continue;
					string id = fields[0];
					string enText = fields[4];
					if (id.Contains("Text") && !string.IsNullOrEmpty(enText))
					{
						idToText.Add((id, enText));
					}
				}
			}

			var groups = new Dictionary<string, List<string>>();
			foreach (var (id, text) in idToText)
			{
				if (!groups.ContainsKey(text)) groups[text] = new List<string>();
				groups[text].Add(id);
			}

			foreach (var kv in groups)
			{
				if (kv.Value.Count > 1)
				{
					string originalText = kv.Key;
					OriginalText[originalText] = new List<string>(kv.Value);

					string currentText = originalText;
					for (int i = 1; i < kv.Value.Count; i++)
					{
						char lastChar = currentText[currentText.Length - 1];
						string newText = currentText + lastChar;
						TextFixes[kv.Value[i]] = newText;
						currentText = newText;
					}
				}
			}
		}

		private static void ApplyFixes()
		{
			if (TextFixes.Count == 0) return;
			FixTextCSV(TextFixes);
			FixTextJSON(TextFixes);
		}

		private static void FixTextCSV(Dictionary<string, string> replacements)
		{
			if (replacements == null || replacements.Count == 0) return;

			var allRows = new List<string[]>();

			using (var parser = new TextFieldParser(Utils.Path_CSV, Encoding.UTF8))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				parser.HasFieldsEnclosedInQuotes = true;

				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();
					if (fields.Length >= 5 && replacements.TryGetValue(fields[0], out string newText))
					{
						fields[4] = newText;
					}
					allRows.Add(fields);
				}
			}

			using (var writer = new StreamWriter(Utils.Path_CSV, false, Encoding.UTF8))
			{
				foreach (var row in allRows)
				{
					for (int i = 0; i < row.Length; i++)
					{
						string field = row[i] ?? "";
						bool needsQuotes = field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r") || field.Contains("\t");
						if (needsQuotes)
						{
							field = field.Replace("\"", "\"\"");
							field = "\"" + field + "\"";
						}
						row[i] = field;
					}
					writer.WriteLine(string.Join(",", row));
				}
			}
		}

		private static void FixTextJSON(Dictionary<string, string> replacements)
		{
			if (!File.Exists(Utils.Path_Json) || replacements == null || replacements.Count == 0) return;

			var jsonText = File.ReadAllText(Utils.Path_Json, Encoding.UTF8);
			var dialogues = JsonConvert.DeserializeObject<List<DialogueLine>>(jsonText);
			if (dialogues == null) return;

			int modified = 0;
			foreach (var kv in replacements)
			{
				var matchNormal = Regex.Match(kv.Key, @"Character/([^_]+)_Text_(.+)");
				var matchSkin = Regex.Match(kv.Key, @"Character_Skin/([^_]+)_([^_]+)_Text_(.+)");

				string character = "", key = "", skin = "Normal";

				if (matchNormal.Success)
				{
					character = matchNormal.Groups[1].Value;
					key = matchNormal.Groups[2].Value;
				}
				else if (matchSkin.Success)
				{
					character = matchSkin.Groups[1].Value;
					skin = matchSkin.Groups[2].Value;
					key = matchSkin.Groups[3].Value;
				}
				else continue;

				string jsonCharacter = DialogueData.GetCharacterName(character, false);
				var dialogue = dialogues.Find(d => d.Character == jsonCharacter && d.Skin == skin && d.Key == key);

				if (dialogue != null && dialogue.English != kv.Value)
				{
					dialogue.English = kv.Value;
					modified++;
				}
			}

			if (modified > 0)
			{
				File.WriteAllText(Utils.Path_Json, JsonConvert.SerializeObject(dialogues, Formatting.Indented), Encoding.UTF8);
			}
		}

		private static void SaveFixesToFile()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(FixesPath));
			var data = new FixesData { OriginalText = OriginalText, TextFixes = TextFixes };
			File.WriteAllText(FixesPath, JsonConvert.SerializeObject(data, Formatting.Indented), Encoding.UTF8);
		}

		private static void LoadFixesFromFile()
		{
			if (!File.Exists(FixesPath)) return;
			var data = JsonConvert.DeserializeObject<FixesData>(File.ReadAllText(FixesPath, Encoding.UTF8));
			if (data != null)
			{
				OriginalText = data.OriginalText ?? new Dictionary<string, List<string>>();
				TextFixes = data.TextFixes ?? new Dictionary<string, string>();
			}
		}

		public static void Restore()
		{
			LoadFixesFromFile();
			var restoreMap = OriginalText.SelectMany(kv => kv.Value, (kv, id) => new { id, text = kv.Key }).ToDictionary(x => x.id, x => x.text);
			if (restoreMap.Count == 0) return;
			FixTextCSV(restoreMap);
			FixTextJSON(restoreMap);
			//TextFixes.Clear();
			//OriginalText.Clear();
			SaveFixesToFile();
		}
	}
}