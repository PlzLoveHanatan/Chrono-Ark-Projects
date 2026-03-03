using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MiyukiSone.DialogueBoxData;

namespace MiyukiSone
{
	public static class DialogueBox
	{
		public static List<GameObject> dialogueWindows = new List<GameObject>();
		private static string lastSpriteKey = null;

		public static void CreateDialogueBox(DialogueBoxState? state = null)
		{
			int windowCount = RandomManager.RandomPer("MiyukiRandomWindow", 100, 15) ? 2 : 1;

			for (int i = 0; i < 1; i++)
			{
				DialogueBoxState currentState = state ?? DialogueBoxState.love;
				var sprites = DialogueSprites[currentState];
				List<string> availableSprites = new List<string>(sprites);

				if (!string.IsNullOrEmpty(lastSpriteKey) && availableSprites.Contains(lastSpriteKey) && availableSprites.Count > 1)
				{
					availableSprites.Remove(lastSpriteKey);
				}

				int randomSpriteIndex = RandomManager.RandomInt("MiyukiRandomBox", 0, availableSprites.Count);
				string randomSprite = availableSprites[randomSpriteIndex];
				if (!MiyukiSoneSaveManager.Instance.CurrentData.EternalPromise) randomSprite = "";
				lastSpriteKey = randomSprite;

				var transform = BattleSystem.instance.ActWindow.transform;
				Sprite sprite = UtilsUI.GetSprite("MiyukiVisual/DialogueBox/" + randomSprite);
				Vector2 size = DialogueSize[currentState];
				Vector3 position = GetRandomPosition(size, "MiyukiRandomPos");

				GameObject newWindow = UtilsUI.CreateUIImage($"DialogueBox_{randomSprite}", transform, sprite, size, position, true);
				newWindow.AddComponent<DialogueBoxWindow>();
				newWindow.AddComponent<DialogueBoxDragHandler>();
				newWindow.GetComponent<DialogueBoxWindow>().currentDialogueBoxState = currentState;
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

		public enum DialogueBoxState
		{
			love,
			kiss,
			help,
			//help
		}

		public static readonly Dictionary<DialogueBoxState, List<string>> DialogueSprites = new Dictionary<DialogueBoxState, List<string>>()
		{
			{ DialogueBoxState.love, new List<string> {"box_love_0.png", "box_love_1.png", "box_love_2.png", "box_love_3.png", "box_love_4.png",
				"box_love_5.png", "box_love_6.png", "box_love_7.png", "box_love_8.png", "box_love_9.png", "box_love_10.png", "box_love_11.png", "box_love_12.png" } },
			{ DialogueBoxState.kiss, new List<string> { "box_kiss_0.png", "box_kiss_1.png" } },
			{ DialogueBoxState.help, new List<string> { "box_kiss_0.png", "box_kiss_1.png" } },
		};


		public static readonly Dictionary<DialogueBoxState, Vector2> DialogueSize = new Dictionary<DialogueBoxState, Vector2>()
		{
			{ DialogueBoxState.love, new Vector3(700, 130, 0) },
			{ DialogueBoxState.kiss, new Vector3(700, 130, 0) },
			{ DialogueBoxState.help, new Vector3(700, 130, 0) },
			//{ DialogueBoxState.help, new Vector3(0, 90, 0) }
		};

		public static readonly Dictionary<DialogueBoxState, Vector3> Dialogueposition = new Dictionary<DialogueBoxState, Vector3>()
		{
			{ DialogueBoxState.love, new Vector3(170, 170, 0) },
			{ DialogueBoxState.kiss, new Vector3(170, 170, 0) },
			//{ DialogueBoxState.sex, new Vector3(0, 120, 0) },
			//{ DialogueBoxState.help, new Vector3(0, 90, 0) }
		};
	}
}