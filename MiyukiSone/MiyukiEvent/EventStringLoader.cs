using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.EventData;
using static MiyukiSone.Affection;
using EItem;

namespace MiyukiSone
{
	public static class EventStringLoader
	{
		public class EventLine
		{
			public string English { get; set; }
			public string Korean { get; set; }
			public string Japanese { get; set; }
			public string Chinese { get; set; }
			public string Chinese_TW { get; set; }
		}

		private static readonly Dictionary<EventState, EventLine[]> cachedLove = new Dictionary<EventState, EventLine[]>();
		private static readonly Dictionary<EventState, EventLine[]> cachedHate = new Dictionary<EventState, EventLine[]>();

		public static void LoadEvents()
		{
			if (cachedLove.Count > 0 && cachedHate.Count > 0)
			{
				Debug.Log("События уже загружены, повторная загрузка не нужна.");
				return;
			}

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ThisMod.DirectoryName, "Assets", "MiyukiEvent.json");
			if (!File.Exists(path))
			{
				Debug.Log("Файл событий не найден: " + path);
				return;
			}

			var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<EventLine>>>>(File.ReadAllText(path));

			foreach (var kvp in jsonDict)
			{
				if (!Enum.TryParse(kvp.Key, true, out EventState e))
				{
					Debug.Log($"Не удалось распознать EventState: {kvp.Key}");
					continue;
				}

				kvp.Value.TryGetValue("love", out List<EventLine> loveLines);
				kvp.Value.TryGetValue("hate", out List<EventLine> hateLines);

				if (!cachedLove.ContainsKey(e))
				{
					cachedLove[e] = loveLines != null ? loveLines.ToArray() : new EventLine[0];
				}

				if (!cachedHate.ContainsKey(e))
				{
					cachedHate[e] = hateLines != null ? hateLines.ToArray() : new EventLine[0];
				}
			}
			Debug.Log("События успешно загружены.");
		}

		public static EventLine GetRandomLine(EventState e, bool isLove)
		{
			LoadEvents();
			var dic = isLove ? cachedLove : cachedHate;

			EventLine[] lines;
			if (dic.TryGetValue(e, out lines) && lines.Length > 0)
			{
				int index = RandomManager.RandomInt("EventStateRandom", 0, lines.Length);
				return lines[index];
			}

			return null;
		}

		public static string GetLocalizedLine(EventState state, bool isLove)
		{
			var line = GetRandomLine(state, isLove);

			if (line == null)
			{
				Debug.Log($"Line is null for {state}, isLove={isLove}");
				return $"[Missing: {state}]";
			}

			// ТЕПЕРЬ line ТОЧНО EventLine, и метод GetLocalizedText сработает
			return GetLocalizedText(line);
		}

		public static string MiyukiTextEvent(EventState e, bool isEvent = true)
		{
			//if (IsIndifferent) return null;
			string text = GetLocalizedLine(e, IsLoving);
			ShowText(text, isEvent);
			return text;
		}
	}
}