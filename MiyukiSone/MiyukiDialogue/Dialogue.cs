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
	}

	public static class Dialogue
	{
		#region Data & Constructors
		public static readonly Dictionary<DialogueState, List<string>> DialogueSprites = new Dictionary<DialogueState, List<string>>()
		{
			{ DialogueState.love, new List<string> {"dlg_love_01", "dlg_love_02", "dlg_love_03", "dlg_love_04", "dlg_love_05", "dlg_love_06", "dlg_love_07", "dlg_love_08", "dlg_love_09", "dlg_love_010", "dlg_love_011" } },
			{ DialogueState.kiss, new List<string> { "dlg_kiss_01", "dlg_kiss_02" } },
		};

		public static List<GameObject> DialogueWindows = new List<GameObject>();
		private static string LastDialogueSpriteKey = null;
		private static Canvas DialogueCanvas = null;
		#endregion

		public static void CreateDialogue()
		{
			CreateDialogue(null, null, 0);
		}

		public static void CreateDialogue(int amount)
		{
			CreateDialogue(null, null, amount);
		}

		public static void CreateDialogue(DialogueState? state = null, Vector3 ?position = null, int amount = 0, bool? isDoubleButton = null, float? rotationZ = null)
		{
			if (amount == 0) amount = RandomManager.RandomPer("MiyukiDialogueAmount", 100, 30) ? Affection.MiyukiRandomResult(3) : 1;

			for (int i = 0; i < amount; i++)
			{
				DialogueState finalState = state ?? DialogueState.love;
				if (RandomManager.RandomPer("MiyukiDialogueKiss", 100, 15)) finalState = DialogueState.kiss;

				string randomSprite;
				if (!MiyukiSaveManager.Instance.CurrentData.EternalPromise)
				{
					randomSprite = "dlg_eternal_01";
				}
				else
				{
					List<string> availableSprites = new List<string>(DialogueSprites[finalState]);
					if (!string.IsNullOrEmpty(LastDialogueSpriteKey) && availableSprites.Contains(LastDialogueSpriteKey) && availableSprites.Count > 1) availableSprites.Remove(LastDialogueSpriteKey);
					int randomSpriteIndex = RandomManager.RandomInt("MiyukiRandomDialogue", 0, availableSprites.Count);
					randomSprite = availableSprites[randomSpriteIndex];
					LastDialogueSpriteKey = randomSprite;
				}			

				Canvas canvas = GetOrCreateDialogueCanvas();
				Sprite sprite = UtilsUI.GetSpriteFromMod("MiyukiVisual/Dialogue/" + randomSprite + ".png");
				Vector2 size = new Vector2(700, 168);
				Vector3 finalPosition = position ?? GetRandomPosition(size);
				GameObject newWindow = UtilsUI.CreateUIImage($"Dialogue_{randomSprite}", canvas.transform, sprite, size, finalPosition, true);

				float angle = rotationZ ?? RandomManager.RandomInt("MiyukiDialogueRotation", -10, 10);
				newWindow.transform.rotation = Quaternion.Euler(0, 0, angle);
				newWindow.AddComponent<DialogueWindow>();
				newWindow.GetComponent<DialogueWindow>().IsDoubleButton = isDoubleButton ?? false;		
				newWindow.GetComponent<DialogueWindow>().CurrentDialogueState = finalState;
				newWindow.AddComponent<DialogueDragHandler>();
				newWindow.transform.SetAsLastSibling();		
				DialogueWindows.Add(newWindow);
			}
		}

		public static void RemoveWindow(GameObject window)
		{
			if (window != null) Object.Destroy(window);
			if (DialogueWindows.Contains(window)) DialogueWindows.Remove(window);
			DialogueWindows.RemoveAll(w => w == null);
		}

		private static Canvas GetOrCreateDialogueCanvas()
		{
			if (DialogueCanvas != null) return DialogueCanvas;

			GameObject canvasObj = GameObject.Find("MiyukiDialogueCanvas");
			if (canvasObj == null)
			{
				canvasObj = new GameObject("MiyukiDialogueCanvas");
				Object.DontDestroyOnLoad(canvasObj);
			}

			DialogueCanvas = canvasObj.GetComponent<Canvas>();
			if (DialogueCanvas == null)
			{
				DialogueCanvas = canvasObj.AddComponent<Canvas>();
				DialogueCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
				DialogueCanvas.sortingOrder = 100;

				canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
				canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
			}

			return DialogueCanvas;
		}

		public static Vector3 GetRandomPosition(Vector2 size)
		{
			if (BattleSystem.instance != null && BattleSystem.instance.ActWindow != null)
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

				float randomX = RandomManager.RandomInt("MiyukiRandomPosX", (int)minX, (int)maxX);
				float randomY = RandomManager.RandomInt("MiyukiRandomPosY", (int)minY, (int)maxY);

				return new Vector3(randomX, randomY, 0);
			}
			else
			{
				float minX = size.x / 2 + 50f;
				float maxX = Screen.width - size.x / 2 - 50f;
				float minY = size.y / 2 + 50f;
				float maxY = Screen.height - size.y / 2 - 50f;

				float randomX = RandomManager.RandomInt("MiyukiRandomPosX", (int)minX, (int)maxX);
				float randomY = RandomManager.RandomInt("MiyukiRandomPosY", (int)minY, (int)maxY);

				return new Vector3(randomX, randomY, 0);
			}
		}
	}
}