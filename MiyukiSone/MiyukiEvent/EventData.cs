using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using I2.Loc;
using static MiyukiSone.Dialogue;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using DarkTonic.MasterAudio;
using ChronoArkMod;

namespace MiyukiSone
{
	public static class EventData
	{
		private class EventLine
		{
			public string Key { get; set; }
			public string English { get; set; }
			public string Korean { get; set; }
			public string Japanese { get; set; }
			public string Chinese { get; set; }
			public string Chinese_TW { get; set; }
			public string AudioFile { get; set; }
		}

		private class EventLineData
		{
			[JsonProperty("Dere")]
			public List<EventLine> Dere { get; set; }

			[JsonProperty("Yandere")]
			public List<EventLine> Yandere { get; set; }
		}

		private static EventLineData events;
		private static readonly HashSet<string> usedDereKeys = new HashSet<string>();
		private static readonly HashSet<string> usedYandereKeys = new HashSet<string>();

		// ============= МЕТОДЫ ЗАГРУЗКИ =============
		public static void LoadEvents()
		{
			if (events != null)
			{
				Debug.Log("События уже загружены, повторная загрузка не нужна.");
				return;
			}

			string jsonContent = MiyukiJsonReader.LoadJson("Event.json");

			if (jsonContent == null)
			{
				Debug.LogError("Не удалось загрузить Event.json");
				return;
			}

			try
			{
				events = JsonConvert.DeserializeObject<EventLineData>(jsonContent);
				int dereCount = events?.Dere?.Count ?? 0;
				int yandereCount = events?.Yandere?.Count ?? 0;
				Debug.Log($"События загружены. Dere: {dereCount}, Yandere: {yandereCount}");
			}
			catch (Exception ex)
			{
				Debug.LogError($"Ошибка десериализации Event.json: {ex.Message}");
			}
		}

		private static string GetLocalizedLine(EventLine line)
		{
			string currentLanguage = LocalizationManager.CurrentLanguage;

			switch (currentLanguage)
			{
				case "Korean": return !string.IsNullOrEmpty(line.Korean) ? line.Korean : line.English;
				case "Japanese": return !string.IsNullOrEmpty(line.Japanese) ? line.Japanese : line.English;
				case "Chinese": return !string.IsNullOrEmpty(line.Chinese) ? line.Chinese : line.English;
				case "Chinese_TW": return !string.IsNullOrEmpty(line.Chinese_TW) ? line.Chinese_TW : line.English;
				default: return line.English;
			}
		}

		// ============= ПОЛУЧЕНИЕ СЛУЧАЙНОЙ СТРОКИ =============
		private static EventLine GetRandomLine(bool isDere)
		{
			LoadEvents();

			List<EventLine> allLines = isDere ? events?.Dere : events?.Yandere;
			if (allLines == null || allLines.Count == 0)
			{
				Debug.LogError($"Нет фраз для состояния: {(isDere ? "DERE" : "YANDERE")}");
				return null;
			}

			HashSet<string> usedKeys = isDere ? usedDereKeys : usedYandereKeys;

			if (usedKeys.Count >= allLines.Count)
			{
				Debug.Log($"Все {(isDere ? "DERE" : "YANDERE")} фразы были показаны. Сбрасываем историю.");
				usedKeys.Clear();
			}

			var availableLines = allLines.Where(line => !usedKeys.Contains(line.Key)).ToList();

			if (availableLines.Count == 0)
			{
				usedKeys.Clear();
				availableLines = allLines;
			}

			int index = UnityEngine.Random.Range(0, availableLines.Count);
			var selectedLine = availableLines[index];

			usedKeys.Add(selectedLine.Key);
			return selectedLine;
		}

		// ============= ОСНОВНОЙ МЕТОД ДЛЯ ВЫЗОВА =============
		public static string MiyukiTextEvent(MiyukiAffection? affection = null, bool isEvent = true)
		{
			bool textState = false;
			if (affection.HasValue == IsDere) textState = true;

			var line = GetRandomLine(textState);

			if (line == null)
			{
				Debug.LogError($"Не найдена строка для состояния: {(IsDere ? "DERE" : "YANDERE")}");
				return null;
			}

			string text = GetLocalizedLine(line);
			ShowText(text, isEvent);

			Debug.Log($"Event dialog - State: {(IsDere ? "DERE" : "YANDERE")}, Audio: {line.AudioFile}, Key: {line.Key}");

			// Путь к папке с аудио (предполагаю, что файлы лежат в Assets/Events/)
			string folder = IsDere ? "Dere" : "Yandere";
			PlaySoundFromAsset($"Assets/Events/{folder}/{line.AudioFile}.ogg", true);

			return text;
		}
	}
}