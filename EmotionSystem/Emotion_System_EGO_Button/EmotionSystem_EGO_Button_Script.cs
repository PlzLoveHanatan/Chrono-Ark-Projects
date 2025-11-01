using System.Collections.Generic;
using System.Diagnostics.Contracts;
using EmotionSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using static EmotionSystem.DataStore;

namespace EmotionSystem
{
	public class EmotionSystem_EGO_Button_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
	{
		public Image Img;
		public EmotionSystem_EGO_Button EGO_Button;

		[Header("Interaction Colors")]
		public Color normalColor = Color.white;
		public Color hoverColor = new Color(0.8f, 0.8f, 0.8f);
		public Color pressedColor = new Color(0.9f, 0.9f, 0.9f);
		public Color disabledColor = new Color(1f, 1f, 1f, 0.5f);

		public bool interactable;
		public bool Rotation;

		[Header("Rotation Settings")]
		public float rotationAmplitude = 10f;
		public float rotationFrequency = 2f;
		private float initialRotationZ;

		private readonly Dictionary<VisualUi.EGOUi.SpriteTypeEGOButton, Sprite> EgoSprites = new Dictionary<VisualUi.EGOUi.SpriteTypeEGOButton, Sprite>();

		public void Awake()
		{
			initialRotationZ = transform.eulerAngles.z;
			Img = GetComponent<Image>();
			EGO_Button = GetComponent<EmotionSystem_EGO_Button>();
			gameObject.AddComponent<EmotionSystem_EGO_Button_Tooltip>();

			LoadEGOSprites();
		}

		private void LoadEGOSprites()
		{
			var floorType = DataStore.LibraryFloor.CurrentFloorType;

			var setType = Utils.ChibiAngela
		? DataStore.VisualUi.EGOUi.SpriteSetType.Angela
		: DataStore.Instance.Visual.EGOButton.GetSetForFloor(floorType);

			var spriteDict = DataStore.Instance.Visual.EGOButton.SpriteSets[setType];

			foreach (var kvp in spriteDict)
			{
				var key = kvp.Key;
				var data = kvp.Value;

				Utils_Ui.GetSpriteAsync(data.Path, (AsyncOperationHandle handle) =>
				{
					if (handle.Result is Sprite sprite)
					{
						EgoSprites[key] = sprite;
					}
					else
					{
						Debug.LogWarning($"[EGO_UI] Failed to load sprite at {data.Path}");
					}
				});
			}
		}

		public void Update()
		{
			if (Img == null || EGO_Button == null) return;

			VisualUi.EGOUi.SpriteTypeEGOButton spriteType;

			if (EGO_Button.OpenEGOHand)
			{
				spriteType = VisualUi.EGOUi.SpriteTypeEGOButton.EGO_Normal_Open;
				interactable = true;
			}
			else if (EGO_Button.HasEGOSkill)
			{
				spriteType = VisualUi.EGOUi.SpriteTypeEGOButton.EGO_Normal_Active;
				interactable = true;
			}
			else
			{
				spriteType = VisualUi.EGOUi.SpriteTypeEGOButton.EGO_Normal_Empty;
				interactable = false;
			}

			Img.color = interactable ? normalColor : disabledColor;

			// обновляем Image
			if (EgoSprites.TryGetValue(spriteType, out var sprite) && sprite != null)
			{
				if (Img.sprite != sprite) // чтобы не перезаписывать без надобности
				{
					Img.sprite = sprite;
				}
			}

			if (Rotation)
			{
				float angle = Mathf.Sin(Time.time * rotationFrequency * Mathf.PI * 2f) * rotationAmplitude;
				transform.rotation = Quaternion.Euler(0f, 0f, initialRotationZ + angle);
			}
			else
			{
				transform.rotation = Quaternion.Euler(0f, 0f, initialRotationZ);
			}
		}

		public void StartRotation()
		{
			Rotation = true;
		}

		public void StopRotation()
		{ 
			Rotation = false;
			transform.rotation = Quaternion.Euler(0f, 0f, initialRotationZ);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			StopRotation();

			if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;
			Img.rectTransform.localScale = 1.1f * Vector3.one;
			Img.color = interactable ? hoverColor : disabledColor;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;
			Img.rectTransform.localScale = Vector3.one;
			Img.color = interactable ? normalColor : disabledColor;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;
			Img.color = interactable ? pressedColor : disabledColor;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove || EGO_Button == null) return;

			if (EGO_Button.OpenEGOHand)
			{
				EGO_Button.ChangeHand();
			}
			else
			{
				EGO_Button.ChangeHand(true);
			}

			Img.color = interactable ? hoverColor : disabledColor;
		}
	}
}
