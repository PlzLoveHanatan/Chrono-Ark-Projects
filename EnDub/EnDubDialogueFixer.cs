using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

		/// <summary>
		/// Парсит CSV строку, сохраняя оригинальную структуру
		/// </summary>
		private static string[] ParseCsvLine(string line)
		{
			var result = new List<string>();
			var current = new StringBuilder();
			bool inQuotes = false;
			int i = 0;

			while (i < line.Length)
			{
				char c = line[i];

				if (c == '"')
				{
					// Начало или конец кавычек
					if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
					{
						// Экранированная кавычка внутри поля
						current.Append('"');
						i += 2;
						continue;
					}
					inQuotes = !inQuotes;
					i++;
				}
				else if (c == ',' && !inQuotes)
				{
					// Конец поля
					result.Add(current.ToString());
					current.Clear();
					i++;
				}
				else
				{
					current.Append(c);
					i++;
				}
			}

			// Последнее поле
			result.Add(current.ToString());

			return result.ToArray();
		}

		/// <summary>
		/// Форматирует поле для записи в CSV (только если действительно нужно)
		/// </summary>
		private static string FormatCsvField(string field)
		{
			if (string.IsNullOrEmpty(field)) return field;

			// Экранируем только если есть спецсимволы
			bool needsQuotes = field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r");

			if (needsQuotes)
			{
				// Экранируем кавычки
				field = field.Replace("\"", "\"\"");
				// Оборачиваем в кавычки
				return "\"" + field + "\"";
			}

			return field;
		}

		private static void ScanAndCreateFixes()
		{
			if (!File.Exists(Utils.Path_CSV)) return;

			OriginalText.Clear();
			TextFixes.Clear();

			var idToText = new List<(string id, string text)>();
			var lines = File.ReadAllLines(Utils.Path_CSV, Encoding.UTF8);
			var parsedLines = new List<string[]>();

			for (int idx = 0; idx < lines.Length; idx++)
			{
				string line = lines[idx];
				if (string.IsNullOrWhiteSpace(line)) continue;

				string[] fields = ParseCsvLine(line);
				parsedLines.Add(fields);

				if (fields.Length < 5) continue;

				string id = fields[0];
				string enText = fields[4];

				if (id.Contains("Text") && !string.IsNullOrEmpty(enText))
				{
					idToText.Add((id, enText));
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

			var lines = File.ReadAllLines(Utils.Path_CSV, Encoding.UTF8);
			var outputLines = new List<string>();

			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					outputLines.Add(line);
					continue;
				}

				string[] fields = ParseCsvLine(line);

				if (fields.Length >= 5 && replacements.TryGetValue(fields[0], out string newText))
				{
					fields[4] = newText;
				}

				// Собираем строку обратно, форматируя только нужные поля
				var formattedFields = new string[fields.Length];
				for (int i = 0; i < fields.Length; i++)
				{
					formattedFields[i] = FormatCsvField(fields[i]);
				}

				outputLines.Add(string.Join(",", formattedFields));
			}

			File.WriteAllLines(Utils.Path_CSV, outputLines, Encoding.UTF8);
			Debug.Log($"CSV updated: {replacements.Count} replacements applied");
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
				Debug.Log($"JSON updated: {modified} entries changed");
			}
		}

		private static void SaveFixesToFile()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(FixesPath));
			var data = new FixesData { OriginalText = OriginalText, TextFixes = TextFixes };
			File.WriteAllText(FixesPath, JsonConvert.SerializeObject(data, Formatting.Indented), Encoding.UTF8);
			Debug.Log($"Fixes saved to: {FixesPath}");
		}

		private static void LoadFixesFromFile()
		{
			if (!File.Exists(FixesPath)) return;
			var data = JsonConvert.DeserializeObject<FixesData>(File.ReadAllText(FixesPath, Encoding.UTF8));
			if (data != null)
			{
				OriginalText = data.OriginalText ?? new Dictionary<string, List<string>>();
				TextFixes = data.TextFixes ?? new Dictionary<string, string>();
				Debug.Log($"Fixes loaded from: {FixesPath}");
			}
		}

		public static void Restore()
		{
			LoadFixesFromFile();
			var restoreMap = OriginalText.SelectMany(kv => kv.Value, (kv, id) => new { id, text = kv.Key }).ToDictionary(x => x.id, x => x.text);
			if (restoreMap.Count == 0) return;
			FixTextCSV(restoreMap);
			FixTextJSON(restoreMap);
			Debug.Log($"Restored {restoreMap.Count} entries");
		}
	}
}