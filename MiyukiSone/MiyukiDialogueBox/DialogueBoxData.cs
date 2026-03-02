using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using I2.Loc;
using static MiyukiSone.DialogueBox;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using static MiyukiSone.EventDialogueBox;
using System.Collections;
using GameDataEditor;
using DarkTonic.MasterAudio;
using ChronoArkMod;

namespace MiyukiSone
{
	public enum TryType
	{
		FirstTry,
		SecondTry,
		ThirdTry,
		FourthTry,
		FifthTry,
		SixthTry,
		SeventhTry,
		EighthTry,
		NinthTry,
		TenthTry,
		EleventhTry,
	}

	public static class DialogueBoxData
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

			[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
			public bool? IsSpecial { get; set; }
		}

		private class LoveDialogueData
		{
			[JsonProperty("yes")]
			public List<DialogueLine> Yes { get; set; }

			[JsonProperty("no")]
			public List<DialogueLine> No { get; set; }
		}

		private static LoveDialogueData loveDialogues;
		private static readonly HashSet<string> usedLoveYesKeys = new HashSet<string>();
		private static readonly HashSet<string> usedLoveNoKeys = new HashSet<string>();

		// ============= KISS =============
		private class KissDialogueData
		{
			[JsonProperty("yes")]
			public List<DialogueLine> Yes { get; set; }

			[JsonProperty("no")]
			public Dictionary<string, List<DialogueLine>> No { get; set; }
		}

		private static KissDialogueData kissDialogues;

		// ============= TURN END =============
		private static Dictionary<string, List<DialogueLine>> turnDialogues;

		// ============= МЕТОДЫ ЗАГРУЗКИ =============
		public static void LoadDialogues()
		{
			if (loveDialogues != null && kissDialogues != null && turnDialogues != null)
			{
				Debug.Log("Диалоги уже загружены, повторная загрузка не нужна.");
				return;
			}

			LoadLoveDialogues();
			LoadKissDialogues();
			LoadTurnDialogues();
		}

		private static void LoadLoveDialogues()
		{
			string jsonContent = MiyukiJsonReader.LoadJson("DialogueBoxLove.json");

			if (jsonContent == null)
			{
				Debug.LogError("Не удалось загрузить DialogueBoxLove.json");
				return;
			}

			try
			{
				loveDialogues = JsonConvert.DeserializeObject<LoveDialogueData>(jsonContent);
				int yesCount = loveDialogues?.Yes?.Count ?? 0;
				int noCount = loveDialogues?.No?.Count ?? 0;
				Debug.Log($"Диалоги LOVE загружены. Yes: {yesCount}, No: {noCount}");
			}
			catch (Exception ex)
			{
				Debug.LogError($"Ошибка десериализации DialogueBoxLove.json: {ex.Message}");
			}
		}

		private static void LoadKissDialogues()
		{
			string jsonContent = MiyukiJsonReader.LoadJson("DialogueBoxKiss.json");

			if (jsonContent == null)
			{
				Debug.LogError("Не удалось загрузить DialogueBoxKiss.json");
				return;
			}

			try
			{
				kissDialogues = JsonConvert.DeserializeObject<KissDialogueData>(jsonContent);
				int yesCount = kissDialogues?.Yes?.Count ?? 0;
				int noCategories = kissDialogues?.No?.Count ?? 0;
				Debug.Log($"Kiss диалоги загружены. Yes: {yesCount}, No категорий: {noCategories}");
			}
			catch (Exception ex)
			{
				Debug.LogError($"Ошибка десериализации DialogueBoxKiss.json: {ex.Message}");
			}
		}

		private static void LoadTurnDialogues()
		{
			string jsonContent = MiyukiJsonReader.LoadJson("DialogueBoxTurn.json");

			if (jsonContent == null)
			{
				Debug.LogError("Не удалось загрузить DialogueBoxTurn.json");
				return;
			}

			try
			{
				turnDialogues = JsonConvert.DeserializeObject<Dictionary<string, List<DialogueLine>>>(jsonContent);
				Debug.Log($"Turn диалоги загружены. Найдено типов: {turnDialogues?.Count ?? 0}");
			}
			catch (Exception ex)
			{
				Debug.LogError($"Ошибка десериализации DialogueBoxTurn.json: {ex.Message}");
			}
		}

		private static string GetLocalizedLine(DialogueLine line)
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

		// ============= LOVE =============
		private static DialogueLine GetRandomLoveLine(bool isYes)
		{
			List<DialogueLine> allLines = isYes ? loveDialogues?.Yes : loveDialogues?.No;
			if (allLines == null || allLines.Count == 0) return null;

			HashSet<string> usedKeys = isYes ? usedLoveYesKeys : usedLoveNoKeys;

			if (usedKeys.Count >= allLines.Count)
			{
				Debug.Log($"Все love фразы (yes:{isYes}) были показаны. Сбрасываем историю.");
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

		public static void MiyukiTextBoxLove(bool isYes, bool isEvent = true)
		{
			var line = GetRandomLoveLine(isYes);
			if (line == null)
			{
				Debug.LogError($"Не найдена строка для love диалога, isYes: {isYes}");
				return;
			}

			string text = GetLocalizedLine(line);
			ShowText(text, isEvent);
			Debug.Log($"Love dialog - Audio: {line.AudioFile}, Key: {line.Key}");
			PlaySoundFromAsset($"Assets/DialogueBox/Love/{line.AudioFile}.ogg", true);
			if (!isYes && MiyukiDecides) PawsPrank();
		}

		// ============= KISS =============
		private static string GetKissNoKeyFromType(TryType tryType)
		{
			switch (tryType)
			{
				case TryType.FirstTry: return "Try_01";
				case TryType.SecondTry: return "Try_02";
				case TryType.ThirdTry: return "Try_03";
				case TryType.FourthTry: return "Try_04";
				case TryType.FifthTry: return "Try_05";
				case TryType.SixthTry: return "Try_06";
				case TryType.SeventhTry: return "Try_07";
				case TryType.EighthTry: return "Try_08";
				case TryType.NinthTry: return "Try_09";
				case TryType.TenthTry: return "Try_10";
				case TryType.EleventhTry: return "Try_11";
				default: return "";
			}
		}

		private static DialogueLine GetRandomKissYesLine()
		{
			if (kissDialogues?.Yes == null || kissDialogues.Yes.Count == 0)
			{
				Debug.LogError("Нет Yes фраз для Kiss диалогов");
				return null;
			}

			int index = UnityEngine.Random.Range(0, kissDialogues.Yes.Count);
			var selectedLine = kissDialogues.Yes[index];

			if (selectedLine.IsSpecial == true)
			{
				Debug.Log($"Выбрана особенная Yes фраза: {selectedLine.Key}");
			}

			return selectedLine;
		}

		private static DialogueLine GetCurrentKissNoLine()
		{
			if (kissDialogues?.No == null)
			{
				Debug.LogError("Kiss диалоги не загружены или нет No секции");
				return null;
			}

			string tryKey = GetKissNoKeyFromType(MiyukiData.CurrentKissNoType);

			if (!kissDialogues.No.TryGetValue(tryKey, out List<DialogueLine> phrases) || phrases == null || phrases.Count == 0)
			{
				Debug.LogError($"Нет No фраз для типа {MiyukiData.CurrentKissNoType} (ключ: {tryKey})");
				return null;
			}

			MiyukiData.KissNoCallCount++;

			int phraseIndex = MiyukiData.KissNoCallCount == 1 ? 0 : (phrases.Count > 1 ? 1 : 0);

			if (phraseIndex >= phrases.Count)
			{
				Debug.LogError($"Для типа {MiyukiData.CurrentKissNoType} нет фразы с индексом {phraseIndex}");
				return null;
			}

			return phrases[phraseIndex];
		}

		public static void MiyukiTextBoxKiss(bool isYes, bool isEvent = true)
		{
			DialogueLine line;
			if (isYes)
			{
				line = GetRandomKissYesLine();
				if (line == null)
				{
					Debug.LogError("Не найдена Yes фраза для Kiss диалога");
					return;
				}
				Debug.Log($"Kiss Yes dialog - Audio: {line.AudioFile}, Key: {line.Key}, isSpecial: {line.IsSpecial}");

				// Yes фразы в папке Kiss/Yes
				PlaySoundFromAsset($"Assets/DialogueBox/Kiss/{line.AudioFile}.ogg", true);
			}
			else
			{
				line = GetCurrentKissNoLine();
				if (line == null)
				{
					Debug.LogError("Не найдена No фраза для Kiss диалога");
					return;
				}
				Debug.Log($"Kiss No dialog - Type: {MiyukiData.CurrentKissNoType}, Audio: {line.AudioFile}, Key: {line.Key}, Call count: {MiyukiData.KissNoCallCount}");

				// No фразы в папке Kiss/No
				PlaySoundFromAsset($"Assets/DialogueBox/Kiss/{line.AudioFile}.ogg", true);

				if (MiyukiData.KissNoCallCount == 2)
				{
					UnlockNextKissNo();
				}
			}

			string text = GetLocalizedLine(line);
			ShowText(text, isEvent);
		}

		public static void UnlockNextKissNo()
		{
			if (MiyukiData.CurrentKissNoType == TryType.EleventhTry && MiyukiData.KissNoCallCount > 1)
			{
				EventRandom.PawWithGame();
				Debug.Log("Достигнут последний No тип для Kiss (EleventhTry). Нельзя разблокировать дальше");
				return;
			}

			MiyukiData.CurrentKissNoType++;
			MiyukiData.KissNoCallCount = 0;
			Debug.Log($"Разблокирован новый No тип для Kiss: {MiyukiData.CurrentKissNoType}");
		}

		public static TryType GetCurrentKissNoType()
		{
			return MiyukiData.CurrentKissNoType;
		}

		public static void ResetAllKissNo()
		{
			MiyukiData.CurrentKissNoType = TryType.FirstTry;
			MiyukiData.KissNoCallCount = 0;
			Debug.Log("Все Kiss No диалоги сброшены. Начинаем с FirstTry");
		}

		// ============= TURN END =============
		private static string GetTurnKeyFromType(TryType tryType)
		{
			switch (tryType)
			{
				case TryType.FirstTry: return "Try_01";
				case TryType.SecondTry: return "Try_02";
				case TryType.ThirdTry: return "Try_03";
				case TryType.FourthTry: return "Try_04";
				case TryType.FifthTry: return "Try_05";
				case TryType.SixthTry: return "Try_06";
				case TryType.SeventhTry: return "Try_07";
				case TryType.EighthTry: return "Try_08";
				case TryType.NinthTry: return "Try_09";
				case TryType.TenthTry: return "Try_10";
				case TryType.EleventhTry: return "Try_11";
				default: return "";
			}
		}

		private static DialogueLine GetCurrentTurnLine()
		{
			if (turnDialogues == null)
			{
				Debug.LogError("Turn диалоги не загружены");
				return null;
			}

			string tryKey = GetTurnKeyFromType(MiyukiData.CurrentTryType);

			if (!turnDialogues.TryGetValue(tryKey, out List<DialogueLine> phrases) || phrases == null || phrases.Count == 0)
			{
				Debug.LogError($"Нет фраз для типа {MiyukiData.CurrentTryType} (ключ: {tryKey})");
				return null;
			}

			MiyukiData.CurrentTryCallCount++;

			int phraseIndex = MiyukiData.CurrentTryType == TryType.EleventhTry ? 0 : (MiyukiData.CurrentTryCallCount == 1 ? 0 : 1);

			if (phraseIndex >= phrases.Count)
			{
				Debug.LogError($"Для типа {MiyukiData.CurrentTryType} нет фразы с индексом {phraseIndex}");
				return null;
			}

			return phrases[phraseIndex];
		}

		public static void MiyukiTextBoxTurn(bool isEvent = true)
		{
			var line = GetCurrentTurnLine();
			if (line == null)
			{
				Debug.LogError($"Не найдена строка для текущего Turn диалога: {MiyukiData.CurrentTryType}");
				return;
			}

			string text = GetLocalizedLine(line);
			ShowText(text, isEvent);
			Debug.Log($"Turn dialog - Type: {MiyukiData.CurrentTryType}, Audio: {line.AudioFile}, Key: {line.Key}, Call count: {MiyukiData.CurrentTryCallCount}");

			// Путь к папке Turn
			PlaySoundFromAsset($"Assets/DialogueBox/Turn/{line.AudioFile}.ogg", true);
		}

		public static void UnlockNextTurnEndTry()
		{
			if (MiyukiData.CurrentTryType == TryType.EleventhTry || MiyukiData.CurrentTryCallCount == 1 || MiyukiData.CurrentTryCallCount == 0)
			{
				Debug.Log("Достигнут последний тип (EleventhTry). Нельзя разблокировать дальше");
				return;
			}

			MiyukiData.CurrentTryType++;
			MiyukiData.CurrentTryCallCount = 0;
			Debug.Log($"Разблокирован новый тип Turn: {MiyukiData.CurrentTryType}");
		}

		public static TryType GetCurrentTryType()
		{
			return MiyukiData.CurrentTryType;
		}

		public static void ResetAllTries()
		{
			MiyukiData.CurrentTryType = TryType.FirstTry;
			MiyukiData.CurrentTryCallCount = 0;
			Debug.Log("Все Try диалоги сброшены. Начинаем с FirstTry");
		}
	}
}