using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using I2.Loc;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class EventStringLoader
	{
		public class EventLine
		{
			public string Key { get; set; }
			public string English { get; set; }
			public string Korean { get; set; }
			public string Japanese { get; set; }
			public string Chinese { get; set; }
			public string Chinese_TW { get; set; }
			public string AudioFile { get; set; }
		}

		private class RootEvents
		{
			[JsonProperty("love")]
			public List<EventLine> Love { get; set; }

			[JsonProperty("hate")]
			public List<EventLine> Hate { get; set; }
		}

		private static EventLine[] cachedLove;
		private static EventLine[] cachedHate;

		// Храним показанные ключи
		private static readonly HashSet<string> usedLoveKeys = new HashSet<string>();
		private static readonly HashSet<string> usedHateKeys = new HashSet<string>();

		public static void LoadEvents()
		{
			if (cachedLove != null && cachedHate != null)
			{
				Debug.Log("События уже загружены");
				return;
			}

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ThisMod.DirectoryName, "Assets", "MiyukiEvent.json");
			if (!File.Exists(path))
			{
				Debug.LogError("Файл событий не найден: " + path);
				return;
			}

			var root = JsonConvert.DeserializeObject<RootEvents>(File.ReadAllText(path));

			cachedLove = root?.Love?.ToArray() ?? new EventLine[0];
			cachedHate = root?.Hate?.ToArray() ?? new EventLine[0];

			Debug.Log($"События LOVE загружены: {cachedLove.Length}");
			Debug.Log($"События HATE загружены: {cachedHate.Length}");
		}

		private static EventLine GetRandomLine(bool isLove)
		{
			LoadEvents();

			var allLines = isLove ? cachedLove : cachedHate;
			if (allLines.Length == 0) return null;

			var usedKeys = isLove ? usedLoveKeys : usedHateKeys;

			if (usedKeys.Count >= allLines.Length)
			{
				Debug.Log($"Все фразы для {(isLove ? "LOVE" : "HATE")} были показаны. Сбрасываем.");
				usedKeys.Clear();
			}

			var availableLines = allLines.Where(line => !usedKeys.Contains(line.Key)).ToList();

			if (availableLines.Count == 0)
			{
				usedKeys.Clear();
				availableLines = allLines.ToList();
			}

			int index = RandomManager.RandomInt("MiyukiEventRandom", 0, availableLines.Count);
			var selectedLine = availableLines[index];

			usedKeys.Add(selectedLine.Key);
			return selectedLine;
		}

		public static string MiyukiTextEvent(bool isEvent = true)
		{
			var line = GetRandomLine(IsInLove);

			if (line == null)
			{
				Debug.LogError($"Не найдена строка для состояния: {(IsLoving ? "LOVE" : "HATE")}");
				return null;
			}

			string text = GetLocalizedText(line);
			ShowText(text, isEvent);

			Debug.Log($"Displayed text key: {line.Key}");

			Debug.Log($"Playing audio: {line.AudioFile}");
			PlaySound(line.AudioFile, true);
			return text;
		}
	}
}