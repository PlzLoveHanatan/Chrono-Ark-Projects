using System.Diagnostics.Contracts;
using EmotionalSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace EmotionalSystem
{
	public class EmotionalSystem_EGO_Button_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
	{
		/// <summary>
		/// Sprite when no EGO is available
		/// </summary>
		public Sprite SpriteOff;
		public const string SpriteOffPath = "EGO_Button.png";

		/// <summary>
		/// Sprite when EGO is available
		/// </summary>
		public Sprite SpriteOn;
		public const string SpriteOnPath = "EGO_Active.png";

		/// <summary>
		/// Sprite when EGO hand is active
		/// </summary>
		public Sprite SpriteActive;
		public const string SpriteActivePath = "BattleIcon_EgoOn.png";

		public Image Img;
		public EmotionalSystem_EGO_Button EGO_Button;
		public bool interactable;

		public float rotationAmplitude = 10f;
		public float rotationFrequency = 2f;
		private float initialRotationZ;
		public bool RotationOn;

		public void Awake()
		{
			initialRotationZ = transform.eulerAngles.z;

			Utils_Ui.GetSpriteAsync(SpriteOffPath, delegate (AsyncOperationHandle handle)
			{
				SpriteOff = (Sprite)handle.Result;
			});
			Utils_Ui.GetSpriteAsync(SpriteOnPath, delegate (AsyncOperationHandle handle)
			{
				SpriteOn = (Sprite)handle.Result;
			});
			Utils_Ui.GetSpriteAsync(SpriteActivePath, delegate (AsyncOperationHandle handle)
			{
				SpriteActive = (Sprite)handle.Result;
			});
			Img = gameObject.GetComponent<Image>();
			EGO_Button = gameObject.GetComponent<EmotionalSystem_EGO_Button>();
			gameObject.AddComponent<EmotionalSystem_EGO_Button_Tooltip>();
		}

		public void Update()
		{
			if (EGO_Button != null && Img != null)
			{
				if (EGO_Button.ActiveEGOHand)
				{
					Img.sprite = SpriteActive;
					interactable = true;
				}
				else if (EGO_Button.HasEGOSkill)
				{
					Img.sprite = SpriteOn;
					interactable = true;
				}
				else
				{
					Img.sprite = SpriteOff;
					interactable = false;
				}

				if (RotationOn)
				{
					float angle = Mathf.Sin(Time.time * rotationFrequency * Mathf.PI * 2) * rotationAmplitude;
					transform.rotation = Quaternion.Euler(0f, 0f, initialRotationZ + angle);
				}

				//if (interactable && BattleSystem.instance.ActWindow.CanAnyMove && Utils.EGOButtonHotkey && Input.GetKeyDown(KeyCode.S))
				//{
				//	if (EGO_Button.ActiveEGOHand)
				//	{
				//		EGO_Button.ChangeHand();
				//	}
				//	else
				//	{
				//		EGO_Button.ChangeHand(true);
				//	}
				//}
			}
		}

		public void StartRotation()
		{
			RotationOn = true;
		}

		public void ResetRotation()
		{
			RotationOn = false;
			transform.rotation = Quaternion.Euler(0f, 0f, initialRotationZ);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			ResetRotation();

			if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;

			Img.rectTransform.localScale = 1.1f * Vector3.one;
			Img.color = new Color(1f, 1f, 1f, 0.8f);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;

			Img.rectTransform.localScale = Vector3.one;
			Img.color = new Color(1f, 1f, 1f, 1f);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;

			Img.rectTransform.localScale = Vector3.one;
			Img.color = new Color(1f, 1f, 1f, 1f);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove || EGO_Button == null) return;

			if (EGO_Button.ActiveEGOHand)
			{
				EGO_Button.ChangeHand(); // clicked when EGO hand is active, switch back to normal hand
			}
			else
			{
				EGO_Button.ChangeHand(true); // clicked when EGO hand is not active, switch to EGO hand
			}

			Img.rectTransform.localScale = 1.1f * Vector3.one;
			Img.color = new Color(1f, 1f, 1f, 0.8f);
		}
	}
}
