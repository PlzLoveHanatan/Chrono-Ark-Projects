using System;
using System.Collections.Generic;
using UnityEngine;
using MiyukiSone;
using static MiyukiSone.DialogueBoxData;
using System.Drawing;
using UnityEngine.UI;
using Spine;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class DialogueBox
	{
		private static GameObject dialogueWindowObj;
		//private static DialogueBoxState? lastDialogueBoxState = null;
		private static string lastSpriteKey = null;
		//private static readonly List<DialogueBoxState> availableStates = new List<DialogueBoxState>();
		private const string MiyukiRandom = "MiyukiRandomBox";

		public static void CreateDialogueBox(DialogueBoxState? state = null)
		{
			//DialogueBoxState currentState = state.HasValue ? state.Value : currentState = DialogueBoxState.love;
			DialogueBoxState currentState = state ?? DialogueBoxState.love;

			//if (state.HasValue)
			//{
			//	state = state.Value;
			//}
			//else
			//{
			//	availableStates.Clear();
			//	var allStates = Enum.GetValues(typeof(DialogueBoxState));
			//	foreach (DialogueBoxState s in allStates)
			//	{
			//		availableStates.Add(s);
			//	}

			//	if (lastDialogueBoxState.HasValue && availableStates.Count > 1)
			//	{
			//		availableStates.Remove(lastDialogueBoxState.Value);
			//	}

			//	int randomIndex = RandomManager.RandomInt(MiyukiRandom, 0, availableStates.Count);
			//	state = availableStates[randomIndex];
			//}

			//lastDialogueBoxState = state;

			var sprites = DialogueSprites[currentState];
			List<string> availableSprites = new List<string>(sprites);

			if (!string.IsNullOrEmpty(lastSpriteKey) && availableSprites.Contains(lastSpriteKey) && availableSprites.Count > 1)
			{
				availableSprites.Remove(lastSpriteKey);
			}

			int randomSpriteIndex = RandomManager.RandomInt(MiyukiRandom, 0, availableSprites.Count);
			string randomSprite = availableSprites[randomSpriteIndex];
			lastSpriteKey = randomSprite;

			var transform = BattleSystem.instance.ActWindow.transform; // battle window
			Sprite sprite = UtilsUI.GetSprite("MiyukiVisual/DialogueBox/" + randomSprite);
			Vector2 size = DialogueSize[currentState];
			Vector3 position = GetRandomPosition(size, "MiyukiRandomPos"); // default position Dialogueposition[currentState];

			dialogueWindowObj = UtilsUI.CreateUIImage($"DialogueBox_{randomSprite}",
				transform, sprite, size, position, true);

			dialogueWindowObj.AddComponent<DialogueBoxWindow>();
			dialogueWindowObj.AddComponent<DialogueBoxDragHandler>();
			dialogueWindowObj.GetComponent<DialogueBoxWindow>().currentDialogueBoxState = currentState;
		}

		private static Vector3 GetRandomPosition(Vector2 size, string randomKey)
		{
			RectTransform actWindowRect = BattleSystem.instance.ActWindow.GetComponent<RectTransform>();

			float minX = -actWindowRect.rect.width / 2 + size.x / 2;
			float maxX = actWindowRect.rect.width / 2 - size.x / 2;
			float minY = -actWindowRect.rect.height / 2 + size.y / 2;
			float maxY = actWindowRect.rect.height / 2 - size.y / 2;

			float randomX = RandomManager.RandomInt(randomKey + "_PosX", (int)minX, (int)maxX);
			float randomY = RandomManager.RandomInt(randomKey + "_PosY", (int)minY, (int)maxY);

			return new Vector3(randomX, randomY, 0);
		}
	}
}