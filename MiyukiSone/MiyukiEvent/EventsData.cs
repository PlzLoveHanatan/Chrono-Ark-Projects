using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using I2.Loc;
using static MiyukiSone.Affection;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public static class EventsData
	{
		public class EventLine
		{
			public string Key { get; set; }
			public string Type { get; set; }
			public string English { get; set; }
			public string Korean { get; set; }
			public string Japanese { get; set; }
			public string Chinese { get; set; }
			public string Chinese_TW { get; set; }
			public string AudioFile { get; set; }
		}

		private static List<EventLine> allEvents;
		private static List<EventLine> customEvents;
		private static Dictionary<string, EventLine> eventByKey;
		private static Dictionary<string, HashSet<string>> usedKeys;

		public static void LoadEvents()
		{
			if (allEvents != null) return;

			string jsonContent = MiyukiJsonReader.LoadJson("EventsData.json");
			if (jsonContent == null) return;

			try
			{
				allEvents = JsonConvert.DeserializeObject<List<EventLine>>(jsonContent);
				customEvents = allEvents?.Where(e => e.Type == "Custom").ToList() ?? new List<EventLine>();

				eventByKey = new Dictionary<string, EventLine>();
				foreach (var line in allEvents)
				{
					if (!string.IsNullOrEmpty(line.Key))
					{
						eventByKey[line.Key] = line;
					}
				}

				usedKeys = new Dictionary<string, HashSet<string>>();
				foreach (var type in allEvents.Select(e => e.Type).Distinct())
				{
					usedKeys[type] = new HashSet<string>();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError($"[Miyuki] LoadEvents: {ex.Message}");
			}
		}

		private static string GetLocalizedLine(EventLine line)
		{
			if (line == null) return null;

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

		public static EventLine GetLineByKey(string key)
		{
			LoadEvents();
			return eventByKey?.GetValueOrDefault(key);
		}

		public static string GetAudioFileByKey(string key)
		{
			return GetLineByKey(key)?.AudioFile;
		}

		public static string GetTextByKey(string key)
		{
			var line = GetLineByKey(key);
			return line != null ? GetLocalizedLine(line) : null;
		}

		public static List<EventLine> GetLinesByType(string type)
		{
			LoadEvents();
			return allEvents?.Where(e => e.Type == type).ToList();
		}

		public static EventLine GetRandomLine(string type)
		{
			LoadEvents();

			var allLines = GetLinesByType(type);
			if (allLines == null || allLines.Count == 0) return null;

			var used = usedKeys[type];

			if (used.Count >= allLines.Count)
			{
				used.Clear();
			}

			var availableLines = allLines.Where(line => !used.Contains(line.Key)).ToList();

			if (availableLines.Count == 0)
			{
				used.Clear();
				availableLines = allLines;
			}

			int index = UnityEngine.Random.Range(0, availableLines.Count);
			var selectedLine = availableLines[index];

			used.Add(selectedLine.Key);
			return selectedLine;
		}

		public static string MiyukiTextEvent(MiyukiAffection? affection = null)
		{
			MiyukiAffection affectionState = affection ?? CurrentAffection;
			string typeKey = affectionState == MiyukiAffection.DereDere ? "DereDere" : affectionState.ToString();

			var line = GetRandomLine(typeKey);
			if (line == null) return null;

			string text = GetLocalizedLine(line);
			StartMiyukiText(text);

			string folder = typeKey;
			PlaySoundFromAsset($"Assets/Audio/Events/{folder}/{line.AudioFile}.ogg", true);

			return text;
		}

		public static string GetAudioFileByText(string text)
		{
			LoadEvents();
			if (customEvents == null) return null;

			string language = LocalizationManager.CurrentLanguage;

			switch (language)
			{
				case "Korean": return customEvents.FirstOrDefault(e => e.Korean == text)?.AudioFile;
				case "Japanese": return customEvents.FirstOrDefault(e => e.Japanese == text)?.AudioFile;
				case "Chinese": return customEvents.FirstOrDefault(e => e.Chinese == text)?.AudioFile;
				case "Chinese_TW": return customEvents.FirstOrDefault(e => e.Chinese_TW == text)?.AudioFile;
				default: return customEvents.FirstOrDefault(e => e.English == text)?.AudioFile;
			}
		}

		public static List<EventLine> GetCustomEvents()
		{
			LoadEvents();
			return customEvents;
		}

		public static EventLine GetRandomCustomEvent()
		{
			LoadEvents();
			return GetRandomLine("Custom");
		}
	}
}