using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using I2.Loc;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public static class DialogueBoxStringLoader
	{
		private class DialogueLine
		{
			public string Key { get; set; }
			public string English { get; set; }
			public string Korean { get; set; }
			public string Japanese { get; set; }
			public string Chinese { get; set; }
			public string Chinese_TW { get; set; }
			public string AudioFile { get; set; }
		}

		private class LoveDialogues
		{
			[JsonProperty("yes")]
			public List<DialogueLine> Yes { get; set; }

			[JsonProperty("no")]
			public List<DialogueLine> No { get; set; }
		}

		private class RootDialogues
		{
			[JsonProperty("love")]
			public LoveDialogues Love { get; set; }

			[JsonProperty("kiss")]
			public LoveDialogues Kiss { get; set; }

			// Сюда потом sex, help и т.д.
		}

		private static readonly Dictionary<DialogueBoxData.DialogueBoxState, DialogueLine[]> dialogueYes = new Dictionary<DialogueBoxData.DialogueBoxState, DialogueLine[]>();
		private static readonly Dictionary<DialogueBoxData.DialogueBoxState, DialogueLine[]> dialogueNo = new Dictionary<DialogueBoxData.DialogueBoxState, DialogueLine[]>();

		// Храним ВСЕ показанные фразы
		private static readonly Dictionary<DialogueBoxData.DialogueBoxState, HashSet<string>> usedYesKeys = new Dictionary<DialogueBoxData.DialogueBoxState, HashSet<string>>();
		private static readonly Dictionary<DialogueBoxData.DialogueBoxState, HashSet<string>> usedNoKeys = new Dictionary<DialogueBoxData.DialogueBoxState, HashSet<string>>();

		public static void LoadDialogues()
		{
			if (dialogueYes.Count > 0 && dialogueNo.Count > 0)
			{
				Debug.Log("Диалоги уже загружены, повторная загрузка не нужна.");
				return;
			}

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ThisMod.DirectoryName, "Assets", "MiyukiDialogueBox.json");
			if (!File.Exists(path))
			{
				Debug.LogError("Файл диалогов не найден: " + path);
				return;
			}

			var root = JsonConvert.DeserializeObject<RootDialogues>(File.ReadAllText(path));

			if (root?.Love?.Yes != null)
			{
				dialogueYes[DialogueBoxData.DialogueBoxState.love] = root.Love.Yes.ToArray();
			}
			if (root?.Love?.No != null)
			{
				dialogueNo[DialogueBoxData.DialogueBoxState.love] = root.Love.No.ToArray();
			}

			// Загружаем KISS, БЛЯТЬ!
			if (root?.Kiss?.Yes != null)
			{
				dialogueYes[DialogueBoxData.DialogueBoxState.kiss] = root.Kiss.Yes.ToArray();
			}
			if (root?.Kiss?.No != null)
			{
				dialogueNo[DialogueBoxData.DialogueBoxState.kiss] = root.Kiss.No.ToArray();
			}

			Debug.Log($"Диалоги LOVE загружены. Yes: {dialogueYes[DialogueBoxData.DialogueBoxState.love]?.Length ?? 0}, No: {dialogueNo[DialogueBoxData.DialogueBoxState.love]?.Length ?? 0}");
			Debug.Log($"Диалоги KISS загружены. Yes: {dialogueYes[DialogueBoxData.DialogueBoxState.kiss]?.Length ?? 0}, No: {dialogueNo[DialogueBoxData.DialogueBoxState.kiss]?.Length ?? 0}");
		}

		private static DialogueLine GetRandomLine(DialogueBoxData.DialogueBoxState state, bool isYes)
		{
			var dic = isYes ? dialogueYes : dialogueNo;
			if (!dic.TryGetValue(state, out var allLines) || allLines.Length == 0) return null;

			var usedKeys = isYes ? usedYesKeys : usedNoKeys;

			if (!usedKeys.ContainsKey(state))
			{
				usedKeys[state] = new HashSet<string>();
			}

			if (usedKeys[state].Count >= allLines.Length)
			{
				Debug.Log($"Все фразы для {state} (yes:{isYes}) были показаны. Сбрасываем историю.");
				usedKeys[state].Clear();
			}

			var availableLines = allLines.Where(line => !usedKeys[state].Contains(line.Key)).ToList();

			if (availableLines.Count == 0)
			{
				usedKeys[state].Clear();
				availableLines = allLines.ToList();
			}

			int index = RandomManager.RandomInt("DialogueBoxRandom", 0, availableLines.Count);
			var selectedLine = availableLines[index];

			usedKeys[state].Add(selectedLine.Key);
			return selectedLine;
		}

		private static string GetLocalizedLine(DialogueLine line)
		{
			return GetLocalizedText(line);
		}

		public static void MiyukiTextBox(DialogueBoxData.DialogueBoxState state, bool isYes, bool isEvent = true)
		{
			var line = GetRandomLine(state, isYes);
			if (line == null)
			{
				Debug.LogError($"Не найдена строка для state: {state}, isYes: {isYes}");
				return;
			}

			string text = GetLocalizedLine(line);
			ShowText(text, isEvent);
			Debug.Log($"Audio file: {line.AudioFile}");
			Debug.Log($"Displayed text key: {line.Key}");
			PlaySound(line.AudioFile, true);
		}
	}
}