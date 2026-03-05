using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MiyukiSone.DialogueData;

namespace MiyukiSone
{
	public enum DialogueState
	{
		love,
		kiss,
		help,
		//help
	}

	public static class Dialogue
	{
		public static List<GameObject> dialogueWindows = new List<GameObject>();
		private static string lastSpriteKey = null;

		public static void CreateDialogue(DialogueState? state = null)
		{
			int windowCount = RandomManager.RandomPer("MiyukiRandomWindow", 100, 15) ? 2 : 1;

			for (int i = 0; i < 1; i++)
			{
				DialogueState currentState = state ?? DialogueState.love;
				var sprites = DialogueSprites[currentState];
				List<string> availableSprites = new List<string>(sprites);

				if (!string.IsNullOrEmpty(lastSpriteKey) && availableSprites.Contains(lastSpriteKey) && availableSprites.Count > 1)
				{
					availableSprites.Remove(lastSpriteKey);
				}

				int randomSpriteIndex = RandomManager.RandomInt("MiyukiRandomBox", 0, availableSprites.Count);
				string randomSprite = availableSprites[randomSpriteIndex];
				if (!MiyukiSoneSaveManager.Instance.CurrentData.EternalPromise) randomSprite = "dlg_eternal_01";
				lastSpriteKey = randomSprite;

				var transform = BattleSystem.instance.ActWindow.transform;
				Sprite sprite = UtilsUI.GetSprite("MiyukiVisual/Dialogue/" + randomSprite + ".png");
				Vector2 size = DialogueSize[currentState];
				Vector3 position = GetRandomPosition(size, "MiyukiRandomPos");

				GameObject newWindow = UtilsUI.CreateUIImage($"Dialogue_{randomSprite}", transform, sprite, size, position, true);
				newWindow.AddComponent<DialogueWindow>();
				newWindow.AddComponent<DialogueDragHandler>();
				newWindow.GetComponent<DialogueWindow>().currentDialogueState = currentState;
				newWindow.transform.SetAsLastSibling();
				dialogueWindows.Add(newWindow);
			}
		}

		public static void RemoveWindow(GameObject window)
		{
			Object.Destroy(window);
			if (dialogueWindows.Contains(window))
			{
				dialogueWindows.Remove(window);
				dialogueWindows.RemoveAll(w => w == null);
			}
		}

		private static Vector3 GetRandomPosition(Vector2 size, string randomKey)
		{
			RectTransform actWindowRect = BattleSystem.instance.ActWindow.GetComponent<RectTransform>();

			float leftRightPadding = 100f;
			float topBottomPadding = 100f;

			float minX = -actWindowRect.rect.width / 2 + size.x / 2 + leftRightPadding;
			float maxX = actWindowRect.rect.width / 2 - size.x / 2 - leftRightPadding;
			float minY = -actWindowRect.rect.height / 2 + size.y / 2 + topBottomPadding;
			float maxY = actWindowRect.rect.height / 2 - size.y / 2 - topBottomPadding;

			if (minX > maxX)
			{
				float centerX = (minX + maxX) / 2;
				minX = centerX - 50;
				maxX = centerX + 50;
			}

			if (minY > maxY)
			{
				float centerY = (minY + maxY) / 2;
				minY = centerY - 30;
				maxY = centerY + 30;
			}

			float randomX = RandomManager.RandomInt(randomKey + "_PosX", (int)minX, (int)maxX);
			float randomY = RandomManager.RandomInt(randomKey + "_PosY", (int)minY, (int)maxY);

			return new Vector3(randomX, randomY, 0);
		}

		public static readonly Dictionary<DialogueState, List<string>> DialogueSprites = new Dictionary<DialogueState, List<string>>()
		{
			{ DialogueState.love, new List<string> {"dlg_love_01", "dlg_love_02", "dlg_love_03", "dlg_love_04", "dlg_love_05", "dlg_love_06", "dlg_love_07", "dlg_love_08", "dlg_love_09", "dlg_love_010", "dlg_love_011" } },
			{ DialogueState.kiss, new List<string> { "dlg_kiss_01", "dlg_kiss_02" } },
			{ DialogueState.help, new List<string> { "dlg_kiss_01", "dlg_kiss_01" } },
		};


		public static readonly Dictionary<DialogueState, Vector2> DialogueSize = new Dictionary<DialogueState, Vector2>()
		{
			{ DialogueState.love, new Vector3(700, 168, 0) },
			{ DialogueState.kiss, new Vector3(700, 168, 0) },
			{ DialogueState.help, new Vector3(700, 168, 0) },
			//{ DialogueState.help, new Vector3(0, 90, 0) }
		};

		public static readonly Dictionary<DialogueState, Vector3> Dialogueposition = new Dictionary<DialogueState, Vector3>()
		{
			{ DialogueState.love, new Vector3(170, 170, 0) },
			{ DialogueState.kiss, new Vector3(170, 170, 0) },
			//{ DialogueState.sex, new Vector3(0, 120, 0) },
			//{ DialogueState.help, new Vector3(0, 90, 0) }
		};
	}
}