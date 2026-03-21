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

	public static class DialogueData
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

		#region Inizialization
		public static void LoadDialogues()
		{
			if (LoveDialogues != null && KissDialogues != null && TurnEndDialogues != null) return;

			try
			{
				LoadLoveDialogues();
				LoadKissDialogues();
				LoadTurnEndDialogues();
			}
			catch (Exception ex)
			{
				Debug.LogError($"[Miyuki] LoadLoveDialogues: {ex.Message}");
			}
		}

		private static void LoadLoveDialogues()
		{
			string jsonContent = MiyukiJsonReader.LoadJson("DialogueLove.json");
			if (jsonContent == null) return;
			LoveDialogues = JsonConvert.DeserializeObject<LoveDialogueData>(jsonContent);
		}

		private static void LoadKissDialogues()
		{
			string jsonContent = MiyukiJsonReader.LoadJson("DialogueKiss.json");
			if (jsonContent == null) return;
			KissDialogues = JsonConvert.DeserializeObject<KissDialogueData>(jsonContent);
		}

		private static void LoadTurnEndDialogues()
		{
			string jsonContent = MiyukiJsonReader.LoadJson("DialogueTurn.json");
			if (jsonContent == null) return;
			TurnEndDialogues = JsonConvert.DeserializeObject<Dictionary<string, List<DialogueLine>>>(jsonContent);
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
		#endregion

		#region Dialogue Love
		private class LoveDialogueData
		{
			[JsonProperty("yes")]
			public List<DialogueLine> Yes { get; set; }

			[JsonProperty("no")]
			public List<DialogueLine> No { get; set; }
		}

		private static LoveDialogueData LoveDialogues;
		private static readonly HashSet<string> UsedLoveYesKeys = new HashSet<string>();
		private static readonly HashSet<string> UsedLoveNoKeys = new HashSet<string>();

		private static DialogueLine GetRandomLoveLine(bool isYes)
		{
			List<DialogueLine> allLines = isYes ? LoveDialogues?.Yes : LoveDialogues?.No;
			if (allLines == null || allLines.Count == 0) return null;

			HashSet<string> usedKeys = isYes ? UsedLoveYesKeys : UsedLoveNoKeys;

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

		public static void MiyukiTextBoxLove(bool isYes)
		{
			var line = GetRandomLoveLine(isYes);
			if (line == null)
			{
				Debug.LogError($"Не найдена строка для love диалога, isYes: {isYes}");
				return;
			}

			string text = GetLocalizedLine(line);
			StartMiyukiText(text);
			Debug.Log($"Love dialog - Audio: {line.AudioFile}, Key: {line.Key}");
			PlaySoundFromAsset($"Assets/Audio/Dialogue/Love/{line.AudioFile}.ogg", true);
		}
		#endregion

		#region Dialogue Kiss
		private class KissDialogueData
		{
			[JsonProperty("yes")]
			public List<DialogueLine> Yes { get; set; }

			[JsonProperty("no")]
			public Dictionary<string, List<DialogueLine>> No { get; set; }
		}

		private static KissDialogueData KissDialogues;
		private static string GetKissNoKey(TryType tryType)
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

		private static TryType CurrentKissTry
		{
			get => (TryType)MiyukiData.CurrentKissNoType;
			set => MiyukiData.CurrentKissNoType = (int)value;
		}

		private static int KissTryCount
		{
			get => MiyukiData.KissNoCallCount;
			set => MiyukiData.KissNoCallCount = value;
		}

		private static DialogueLine GetRandomKissYesLine()
		{
			if (KissDialogues?.Yes == null || KissDialogues.Yes.Count == 0) return null;
			int index = RandomManager.RandomInt("MiyukiKissAnswer", 0, KissDialogues.Yes.Count);
			return KissDialogues.Yes[index];
		}

		private static DialogueLine GetCurrentKissNoLine()
		{
			if (KissDialogues?.No == null) return null;

			string key = GetKissNoKey(CurrentKissTry);
			if (!KissDialogues.No.TryGetValue(key, out var phrases) || phrases == null || phrases.Count == 0) return null;
			int index = (KissTryCount == 0) ? 0 : (phrases.Count > 1 ? 1 : 0);
			return index < phrases.Count ? phrases[index] : null;
		}

		public static void StartKissDialogue(bool isYes)
		{
			DialogueLine line = isYes ? GetRandomKissYesLine() : GetCurrentKissNoLine();
			if (line == null) return;

			StartMiyukiText(GetLocalizedLine(line));
			PlaySoundFromAsset($"Assets/Audio/Dialogue/Kiss/{line.AudioFile}.ogg", true);

			if (!isYes) AdvanceKissNoDialogue();
		}

		public static void AdvanceKissNoDialogue()
		{
			KissTryCount++;

			if (CurrentKissTry == TryType.EleventhTry && KissTryCount >= 2)
			{
				//MiyukiSaveManager.Instance.CurrentData.LockedState = RandomManager.RandomInt("MiyukiRandomAffection", 1, 3);
				MiyukiSaveManager.Instance.CurrentData.GameUpdated = true;
				MiyukiSaveManager.Instance.Save();
				EventSpecial.RestartRun();

				if (DialogueWindows.Count > 0) DialogueWindows.Where(obj => obj != null).ToList().ForEach(RemoveWindow);
			}
			else if (KissTryCount == 2)
			{
				CurrentKissTry++;
				KissTryCount = 0;
			}
		}

		public static void ResetKissNoDialogue()
		{
			CurrentKissTry = TryType.FirstTry;
			KissTryCount = 0;
		}
		#endregion

		#region Turn End Dialogue
		private static Dictionary<string, List<DialogueLine>> TurnEndDialogues;

		private static string GetTryKey(TryType tryType)
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

		private static TryType CurrentTry
		{
			get => (TryType)MiyukiSaveManager.Instance.CurrentData.TurnEndTryIndex;
			set => MiyukiSaveManager.Instance.CurrentData.TurnEndTryIndex = (int)value;
		}

		private static int TryCount
		{
			get => MiyukiSaveManager.Instance.CurrentData.TurnEndTryCallCount;
			set => MiyukiSaveManager.Instance.CurrentData.TurnEndTryCallCount = value;
		}

		private static DialogueLine GetCurrentTurnEndLine()
		{
			if (TurnEndDialogues == null) return null;
			string key = GetTryKey(CurrentTry);
			if (!TurnEndDialogues.TryGetValue(key, out var phrases) || phrases == null || phrases.Count == 0) return null;
			int index = CurrentTry == TryType.EleventhTry ? 0 : TryCount;
			return index < phrases.Count ? phrases[index] : null;
		}

		public static void StartTurnEndDialogue()
		{
			var line = GetCurrentTurnEndLine();
			if (line == null) return;

			StartMiyukiText(GetLocalizedLine(line));
			PlaySoundFromAsset($"Assets/Audio/Dialogue/Turn/{line.AudioFile}.ogg", true);
			AdvanceTurnEndDialogue();
		}

		public static void AdvanceTurnEndDialogue()
		{
			if (CurrentTry == TryType.EleventhTry) return;

			TryCount++;

			if (TryCount == 2)
			{
				CurrentTry++;
				TryCount = 0;
			}
			MiyukiSaveManager.Instance.Save();
		}

		public static void ResetTurnEnd()
		{
			CurrentTry = 0;
			TryCount = 0;
		}
	}

	#endregion
}