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
		private static List<string> ParseCsvLine(string line)
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
					// Проверяем на экранированную кавычку ("" внутри поля)
					if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
					{
						current.Append('"');
						i += 2;
						continue;
					}

					// Переключаем режим кавычек
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

			// Добавляем последнее поле
			result.Add(current.ToString());

			return result;
		}

		/// <summary>
		/// Парсит весь CSV файл с учетом многострочных полей
		/// </summary>
		private static List<List<string>> ParseCsvFile(string filePath)
		{
			var allLines = File.ReadAllLines(filePath, Encoding.UTF8);
			var records = new List<List<string>>();
			var currentRecord = new List<string>();
			var currentField = new StringBuilder();
			bool inQuotes = false;
			bool firstField = true;

			foreach (string line in allLines)
			{
				if (string.IsNullOrEmpty(line) && !inQuotes)
				{
					if (currentRecord.Count > 0)
					{
						records.Add(new List<string>(currentRecord));
						currentRecord.Clear();
					}
					continue;
				}

				int i = 0;
				while (i < line.Length)
				{
					char c = line[i];

					if (c == '"')
					{
						if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
						{
							currentField.Append('"');
							i += 2;
							continue;
						}
						inQuotes = !inQuotes;
						i++;
					}
					else if (c == ',' && !inQuotes)
					{
						currentRecord.Add(currentField.ToString());
						currentField.Clear();
						i++;
					}
					else
					{
						currentField.Append(c);
						i++;
					}
				}

				// Если мы внутри кавычек, строка продолжается
				if (inQuotes)
				{
					currentField.Append('\n');
				}
				else
				{
					// Конец записи
					currentRecord.Add(currentField.ToString());
					currentField.Clear();

					if (currentRecord.Count > 0)
					{
						records.Add(new List<string>(currentRecord));
						currentRecord.Clear();
					}
				}
			}

			// Добавляем последнюю запись если есть
			if (currentField.Length > 0 || currentRecord.Count > 0)
			{
				if (currentField.Length > 0)
					currentRecord.Add(currentField.ToString());
				if (currentRecord.Count > 0)
					records.Add(new List<string>(currentRecord));
			}

			return records;
		}

		/// <summary>
		/// Форматирует поле для записи в CSV
		/// </summary>
		private static string FormatCsvField(string field)
		{
			if (string.IsNullOrEmpty(field)) return field;

			// Всегда оборачиваем в кавычки, если есть спецсимволы
			bool needsQuotes = field.Contains(",") || field.Contains("\"") ||
							  field.Contains("\n") || field.Contains("\r");

			if (needsQuotes)
			{
				field = field.Replace("\"", "\"\"");
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
			var records = ParseCsvFile(Utils.Path_CSV);

			foreach (var fields in records)
			{
				if (fields.Count < 5) continue;

				string id = fields[0].Trim();
				string enText = fields[4].Trim();

				if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(enText)) continue;
				if (!id.Contains("Text")) continue;

				idToText.Add((id, enText));
			}

			// Ищем дубликаты
			var groups = new Dictionary<string, List<string>>();
			foreach (var (id, text) in idToText)
			{
				if (!groups.ContainsKey(text))
					groups[text] = new List<string>();
				groups[text].Add(id);
			}

			// Создаем фиксы для дубликатов
			foreach (var kv in groups)
			{
				if (kv.Value.Count > 1)
				{
					string originalText = kv.Key;
					OriginalText[originalText] = new List<string>(kv.Value);
					kv.Value.Sort();

					string currentText = originalText;
					for (int i = 1; i < kv.Value.Count; i++)
					{
						string newText = currentText + (currentText.Length > 0 ? currentText.Last().ToString() : ".");
						TextFixes[kv.Value[i]] = newText;
						currentText = newText;
					}
				}
			}

			Debug.Log($"Found {TextFixes.Count} duplicate entries across {groups.Count(g => g.Value.Count > 1)} groups");
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

			var records = ParseCsvFile(Utils.Path_CSV);
			int modified = 0;

			foreach (var fields in records)
			{
				if (fields.Count < 5) continue;

				string id = fields[0].Trim();
				if (replacements.TryGetValue(id, out string newText))
				{
					fields[4] = newText;
					modified++;
				}
			}

			// Записываем обратно
			var outputLines = new List<string>();
			foreach (var fields in records)
			{
				var formattedFields = new string[fields.Count];
				for (int i = 0; i < fields.Count; i++)
				{
					formattedFields[i] = FormatCsvField(fields[i]);
				}
				outputLines.Add(string.Join(",", formattedFields));
			}

			File.WriteAllLines(Utils.Path_CSV, outputLines, Encoding.UTF8);
			Debug.Log($"CSV updated: {modified} replacements applied");
		}

		private static void FixTextJSON(Dictionary<string, string> replacements)
		{
			if (!File.Exists(Utils.Path_Json) || replacements == null || replacements.Count == 0) return;

			try
			{
				var jsonText = File.ReadAllText(Utils.Path_Json, Encoding.UTF8);
				var dialogues = JsonConvert.DeserializeObject<List<DialogueLine>>(jsonText);

				if (dialogues == null || dialogues.Count == 0)
				{
					Debug.LogWarning("JSON file is empty or invalid");
					return;
				}

				int modified = 0;
				int notFound = 0;

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
					else
					{
						notFound++;
						continue;
					}

					string jsonCharacter = DialogueData.GetCharacterName(character, false);

					var dialogue = dialogues.Find(d =>
						d.Character == jsonCharacter &&
						d.Skin == skin &&
						d.Key == key);

					if (dialogue != null && dialogue.English != kv.Value)
					{
						dialogue.English = kv.Value;
						modified++;
					}
					else if (dialogue == null)
					{
						notFound++;
					}
				}

				if (modified > 0)
				{
					File.WriteAllText(Utils.Path_Json,
						JsonConvert.SerializeObject(dialogues, Formatting.Indented),
						Encoding.UTF8);
					Debug.Log($"JSON updated: {modified} entries changed, {notFound} not found");
				}
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error fixing JSON: {ex.Message}");
			}
		}

		private static void SaveFixesToFile()
		{
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(FixesPath));
				var data = new FixesData
				{
					OriginalText = OriginalText,
					TextFixes = TextFixes
				};
				File.WriteAllText(FixesPath,
					JsonConvert.SerializeObject(data, Formatting.Indented),
					Encoding.UTF8);
				Debug.Log($"Fixes saved to: {FixesPath}");
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error saving fixes: {ex.Message}");
			}
		}

		private static void LoadFixesFromFile()
		{
			if (!File.Exists(FixesPath)) return;

			try
			{
				var data = JsonConvert.DeserializeObject<FixesData>(
					File.ReadAllText(FixesPath, Encoding.UTF8));

				if (data != null)
				{
					OriginalText = data.OriginalText ?? new Dictionary<string, List<string>>();
					TextFixes = data.TextFixes ?? new Dictionary<string, string>();
					Debug.Log($"Fixes loaded from: {FixesPath} ({TextFixes.Count} entries)");
				}
			}
			catch (Exception ex)
			{
				Debug.LogError($"Error loading fixes: {ex.Message}");
			}
		}

		public static void Restore()
		{
			LoadFixesFromFile();
			var restoreMap = OriginalText
				.SelectMany(kv => kv.Value, (kv, id) => new { id, text = kv.Key })
				.ToDictionary(x => x.id, x => x.text);

			if (restoreMap.Count == 0)
			{
				Debug.Log("No entries to restore");
				return;
			}

			FixTextCSV(restoreMap);
			FixTextJSON(restoreMap);
			Debug.Log($"Restored {restoreMap.Count} entries");
		}
	}
}