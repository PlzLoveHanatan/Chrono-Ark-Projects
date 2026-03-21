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

			[JsonProperty("Kuudere")]
			public List<EventLine> Kuudere { get; set; }
		}

		private static EventLineData Events;
		private static readonly HashSet<string> UsedDereKeys = new HashSet<string>();
		private static readonly HashSet<string> UsedYandereKeys = new HashSet<string>();
		private static readonly HashSet<string> UsedKuudereKeys = new HashSet<string>();

		public static void LoadEvents()
		{
			if (Events != null) return;

			string jsonContent = MiyukiJsonReader.LoadJson("Event.json");
			if (jsonContent == null) return;

			try
			{
				Events = JsonConvert.DeserializeObject<EventLineData>(jsonContent);
			}
			catch (Exception ex)
			{
				Debug.LogError($"[Miyuki] LoadEvents: {ex.Message}");
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

		private static EventLine GetRandomLine(MiyukiAffection affection)
		{
			LoadEvents();

			List<EventLine> allLines = null;
			HashSet<string> usedKeys = null;

			switch (affection)
			{
				case MiyukiAffection.DereDere:
					allLines = Events?.Dere;
					usedKeys = UsedDereKeys;
					break;
				case MiyukiAffection.Yandere:
					allLines = Events?.Yandere;
					usedKeys = UsedYandereKeys;
					break;
				case MiyukiAffection.Kuudere:
					allLines = Events?.Kuudere;
					usedKeys = UsedKuudereKeys;
					break;
			}

			if (allLines == null || allLines.Count == 0) return null;

			if (usedKeys.Count >= allLines.Count)
			{
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

		public static string MiyukiTextEvent(MiyukiAffection? affection = null)
		{
			MiyukiAffection affectionState = affection ?? CurrentAffection;

			var line = GetRandomLine(affectionState);

			if (line == null) return null;

			string text = GetLocalizedLine(line);
			StartMiyukiText(text);

			string folder = affectionState.ToString();
			PlaySoundFromAsset($"Assets/Audio/Events/{folder}/{line.AudioFile}.ogg", true);

			return text;
		}
	}
}