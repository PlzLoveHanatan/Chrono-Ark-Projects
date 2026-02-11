using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using static MiyukiSone.Utils;
using System.Linq;

namespace MiyukiSone
{
	[RequireComponent(typeof(RectTransform))]
	public class MiyukiInputField : MonoBehaviour
	{
		[Header("Input Field Settings")]
		public string placeholderText = "Type text here";
		public int characterLimit = 100;
		public TMP_InputField.ContentType contentType = TMP_InputField.ContentType.Standard;
		public Vector2 size = new Vector2(200, 30);
		public Vector2 position = Vector2.zero;

		[Header("Text Alignment")]
		public TextAlignmentOptions textAlignment = TextAlignmentOptions.Center;
		public TextAlignmentOptions placeholderAlignment = TextAlignmentOptions.Center;

		[Header("Visual Settings")]
		public Color normalColor = Color.white;
		public Color focusedColor = new Color(0.95f, 0.95f, 0.95f, 1f);
		public Color textColor = Color.black;
		public Color placeholderColor = new Color(0.5f, 0.5f, 0.5f, 0.7f);
		public float fontSize = 14f;

		[Header("Cursor Settings")]
		public bool showCursor = true;
		public Color cursorColor = Color.black;
		public float cursorWidth = 2f;
		public float cursorBlinkSpeed = 0.5f;
		public bool cursorBlinking = true;

		[Header("Padding")]
		public float paddingLeft = 5f;
		public float paddingRight = 5f;
		public float paddingTop = 0f;
		public float paddingBottom = 0f;

		[Header("Events")]
		public UnityEngine.Events.UnityEvent<string> onValueChanged;
		public UnityEngine.Events.UnityEvent<string> onSubmit;
		public UnityEngine.Events.UnityEvent onSelect;
		public UnityEngine.Events.UnityEvent onDeselect;

		private TMP_InputField inputFieldComponent;
		private Image backgroundImage;
		private TextMeshProUGUI placeholderTextComponent;
		private TextMeshProUGUI textComponent;
		private RectTransform rectTransform;
		private GameObject cursorObject;
		private Coroutine cursorBlinkCoroutine;
		private Coroutine cursorUpdateCoroutine;
		private string lastSubmittedText = "";

		private readonly Dictionary<string, System.Action> wordResponses = new Dictionary<string, System.Action>()
		{
			//{  "I love you", () => MiyukiAffection.ChangePoints(1) }, 
			{  "I love you", () => GiveMoney() }, 
			{  "I hate you", () => KillRandomAlly() }, 
		};

		private static void GiveMoney()
		{
			PlayData.TSavedata._Gold += 1000;
			PlayData.TSavedata._Soul += 10;
		}

		private static void KillRandomAlly()
		{
			var allies = AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki);
			if (allies.Count() > 0)
			{
				allies.First().Dead(true);
			}
		}

		void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			SetupRectTransform();
			CheckAndCreateComponents();
		}

		void Start()
		{
			SetupInputField();
			if (showCursor) CreateCursor();
		}

		void OnEnable()
		{
			if (cursorObject != null && showCursor && inputFieldComponent != null)
			{
				if (cursorBlinking)
				{
					StartCursorBlinking();
				}
				else
				{
					cursorObject.SetActive(inputFieldComponent.isFocused);
				}
			}
		}

		void OnDisable()
		{
			if (cursorBlinkCoroutine != null)
			{
				StopCoroutine(cursorBlinkCoroutine);
				cursorBlinkCoroutine = null;
			}

			if (cursorUpdateCoroutine != null)
			{
				StopCoroutine(cursorUpdateCoroutine);
				cursorUpdateCoroutine = null;
			}
		}

		void Update()
		{
			if (cursorObject != null && inputFieldComponent != null && inputFieldComponent.isFocused)
			{
				UpdateCursorPosition();
			}
		}

		private void SetupRectTransform()
		{
			if (rectTransform == null) return;
			rectTransform.sizeDelta = size;
			rectTransform.anchoredPosition = position;
			rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
			rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
			rectTransform.pivot = new Vector2(0.5f, 0.5f);
		}

		private void CheckAndCreateComponents()
		{
			inputFieldComponent = GetComponent<TMP_InputField>();

			if (inputFieldComponent == null)
			{
				inputFieldComponent = gameObject.AddComponent<TMP_InputField>();
				backgroundImage = GetComponent<Image>();
				if (backgroundImage == null)
				{
					backgroundImage = gameObject.AddComponent<Image>();
					backgroundImage.color = normalColor;
					backgroundImage.raycastTarget = true;
				}
				CreateTextComponent();
				CreatePlaceholderComponent();
			}
			else
			{
				backgroundImage = GetComponent<Image>();
				if (backgroundImage == null)
				{
					backgroundImage = gameObject.AddComponent<Image>();
					backgroundImage.color = normalColor;
					backgroundImage.raycastTarget = true;
				}
				FindExistingTextComponents();
			}
		}

		private void CreateTextComponent()
		{
			GameObject textObj = new GameObject("Text");
			textObj.transform.SetParent(transform, false);
			textObj.layer = gameObject.layer;

			RectTransform textRt = textObj.AddComponent<RectTransform>();
			textRt.anchorMin = Vector2.zero;
			textRt.anchorMax = Vector2.one;
			textRt.offsetMin = new Vector2(paddingLeft, paddingBottom);
			textRt.offsetMax = new Vector2(-paddingRight, -paddingTop);

			textComponent = textObj.AddComponent<TextMeshProUGUI>();
			textComponent.color = textColor;
			textComponent.fontSize = fontSize;
			textComponent.alignment = textAlignment;
			textComponent.enableWordWrapping = false;
			textComponent.overflowMode = TextOverflowModes.Overflow;

			inputFieldComponent.textComponent = textComponent;
			inputFieldComponent.textViewport = textRt;
		}

		private void CreatePlaceholderComponent()
		{
			GameObject placeholderObj = new GameObject("Placeholder");
			placeholderObj.transform.SetParent(transform, false);
			placeholderObj.layer = gameObject.layer;

			RectTransform placeholderRt = placeholderObj.AddComponent<RectTransform>();
			placeholderRt.anchorMin = Vector2.zero;
			placeholderRt.anchorMax = Vector2.one;
			placeholderRt.offsetMin = new Vector2(paddingLeft, paddingBottom);
			placeholderRt.offsetMax = new Vector2(-paddingRight, -paddingTop);

			placeholderTextComponent = placeholderObj.AddComponent<TextMeshProUGUI>();
			placeholderTextComponent.color = placeholderColor;
			placeholderTextComponent.fontSize = fontSize;
			placeholderTextComponent.fontStyle = FontStyles.Italic;
			placeholderTextComponent.alignment = placeholderAlignment;
			placeholderTextComponent.text = placeholderText;

			inputFieldComponent.placeholder = placeholderTextComponent;
		}

		private void FindExistingTextComponents()
		{
			foreach (Transform child in transform)
			{
				if (child.name == "Text" || child.name.EndsWith("(Text)"))
				{
					textComponent = child.GetComponent<TextMeshProUGUI>();
					if (textComponent != null)
					{
						inputFieldComponent.textComponent = textComponent;
					}
				}
				else if (child.name == "Placeholder")
				{
					placeholderTextComponent = child.GetComponent<TextMeshProUGUI>();
					if (placeholderTextComponent != null)
					{
						inputFieldComponent.placeholder = placeholderTextComponent;
					}
				}
			}

			if (inputFieldComponent.placeholder == null)
			{
				CreatePlaceholderComponent();
			}
		}

		private void SetupInputField()
		{
			if (inputFieldComponent == null) return;

			inputFieldComponent.characterLimit = characterLimit;
			inputFieldComponent.contentType = contentType;

			if (textComponent != null) textComponent.alignment = textAlignment;
			if (placeholderTextComponent != null) placeholderTextComponent.alignment = placeholderAlignment;

			if (backgroundImage != null)
			{
				backgroundImage.color = normalColor;
				backgroundImage.raycastTarget = true;
			}

			if (placeholderTextComponent != null)
			{
				placeholderTextComponent.text = placeholderText;
				placeholderTextComponent.color = placeholderColor;
				placeholderTextComponent.fontSize = fontSize;
			}

			if (textComponent != null)
			{
				textComponent.color = textColor;
				textComponent.fontSize = fontSize;
			}

			inputFieldComponent.onValueChanged.AddListener(OnInputValueChanged);
			inputFieldComponent.onSubmit.AddListener(OnInputSubmit);
			inputFieldComponent.onSelect.AddListener(OnInputSelect);
			inputFieldComponent.onDeselect.AddListener(OnInputDeselect);
		}

		private void CreateCursor()
		{
			cursorObject = new GameObject("Cursor");
			cursorObject.transform.SetParent(transform, false);
			cursorObject.layer = gameObject.layer;

			RectTransform cursorRT = cursorObject.AddComponent<RectTransform>();
			cursorRT.anchorMin = new Vector2(0, 0.5f);
			cursorRT.anchorMax = new Vector2(0, 0.5f);
			cursorRT.pivot = new Vector2(0.5f, 0.5f);
			cursorRT.sizeDelta = new Vector2(cursorWidth, fontSize * 1.2f);
			cursorRT.anchoredPosition = new Vector2(paddingLeft, 0);

			Image cursorImage = cursorObject.AddComponent<Image>();
			cursorImage.color = cursorColor;

			cursorObject.SetActive(false);
		}

		private void UpdateCursorPosition()
		{
			if (cursorObject == null || textComponent == null || rectTransform == null || inputFieldComponent == null)
				return;

			if (cursorUpdateCoroutine != null)
			{
				StopCoroutine(cursorUpdateCoroutine);
			}

			textComponent.ForceMeshUpdate();
			cursorUpdateCoroutine = StartCoroutine(DelayedCursorUpdate());
		}

		private IEnumerator DelayedCursorUpdate()
		{
			yield return new WaitForEndOfFrame();

			if (cursorObject == null || textComponent == null || rectTransform == null || inputFieldComponent == null)
				yield break;

			RectTransform cursorRT = cursorObject.GetComponent<RectTransform>();

			float textWidth = textComponent.preferredWidth;
			if (textWidth <= 0 && !string.IsNullOrEmpty(inputFieldComponent.text))
			{
				textWidth = CalculateTextWidth(inputFieldComponent.text);
			}

			float fieldWidth = rectTransform.rect.width - paddingLeft - paddingRight;
			float cursorPosX = 0f;

			if (textAlignment == TextAlignmentOptions.Center ||
				textAlignment == TextAlignmentOptions.Midline ||
				textAlignment == TextAlignmentOptions.MidlineFlush ||
				textAlignment == TextAlignmentOptions.CenterGeoAligned ||
				textAlignment == TextAlignmentOptions.MidlineGeoAligned)
			{
				if (string.IsNullOrEmpty(inputFieldComponent.text))
				{
					cursorPosX = fieldWidth / 2f;
				}
				else
				{
					cursorPosX = (fieldWidth / 2f) + (textWidth / 2f);
				}
			}
			else if (textAlignment == TextAlignmentOptions.Right ||
					 textAlignment == TextAlignmentOptions.BaselineRight ||
					 textAlignment == TextAlignmentOptions.Flush)
			{
				cursorPosX = fieldWidth;
			}
			else
			{
				cursorPosX = textWidth;
			}

			cursorRT.anchoredPosition = new Vector2(cursorPosX + paddingLeft, 0);
		}

		private float CalculateTextWidth(string text)
		{
			if (string.IsNullOrEmpty(text)) return 0f;
			return text.Length * fontSize * 0.6f;
		}

		private void StartCursorBlinking()
		{
			if (cursorBlinkCoroutine != null)
			{
				StopCoroutine(cursorBlinkCoroutine);
			}

			if (cursorBlinking)
			{
				cursorBlinkCoroutine = StartCoroutine(BlinkCursor());
			}
			else
			{
				if (cursorObject != null && showCursor && inputFieldComponent != null)
				{
					cursorObject.SetActive(inputFieldComponent.isFocused);
				}
			}
		}

		private IEnumerator BlinkCursor()
		{
			while (cursorObject != null)
			{
				if (inputFieldComponent != null && inputFieldComponent.isFocused && showCursor)
				{
					cursorObject.SetActive(!cursorObject.activeSelf);
				}
				else
				{
					cursorObject.SetActive(false);
				}
				yield return new WaitForSeconds(cursorBlinkSpeed);
			}
		}

		public void SetSize(Vector2 newSize)
		{
			size = newSize;
			if (rectTransform != null) rectTransform.sizeDelta = newSize;
		}

		public void SetPosition(Vector2 newPosition)
		{
			position = newPosition;
			if (rectTransform != null) rectTransform.anchoredPosition = newPosition;
		}

		public void SetPosition(float x, float y)
		{
			SetPosition(new Vector2(x, y));
		}

		public void SetSize(float width, float height)
		{
			SetSize(new Vector2(width, height));
		}

		public void SetAnchors(Vector2 min, Vector2 max)
		{
			if (rectTransform != null)
			{
				rectTransform.anchorMin = min;
				rectTransform.anchorMax = max;
			}
		}

		public void SetPivot(Vector2 pivot)
		{
			if (rectTransform != null) rectTransform.pivot = pivot;
		}

		public void SetTextAlignment(TextAlignmentOptions alignment)
		{
			textAlignment = alignment;
			placeholderAlignment = alignment;

			if (textComponent != null) textComponent.alignment = alignment;
			if (placeholderTextComponent != null) placeholderTextComponent.alignment = alignment;

			UpdateCursorPosition();
		}

		public void SetCursorVisible(bool visible)
		{
			showCursor = visible;
			if (cursorObject != null && inputFieldComponent != null)
			{
				cursorObject.SetActive(visible && inputFieldComponent.isFocused);
			}
		}

		public void SetCursorBlinking(bool blinking)
		{
			cursorBlinking = blinking;
			if (cursorObject != null && inputFieldComponent != null)
			{
				if (blinking)
				{
					StartCursorBlinking();
				}
				else
				{
					if (cursorBlinkCoroutine != null)
					{
						StopCoroutine(cursorBlinkCoroutine);
						cursorBlinkCoroutine = null;
					}
					cursorObject.SetActive(inputFieldComponent.isFocused);
				}
			}
		}

		public string GetText()
		{
			return inputFieldComponent != null ? inputFieldComponent.text : "";
		}

		public string GetLastSubmittedText()
		{
			return lastSubmittedText;
		}

		public void SetText(string text)
		{
			if (inputFieldComponent != null)
			{
				inputFieldComponent.text = text;
				UpdateCursorPosition();
			}
		}

		public void Clear()
		{
			SetText("");
		}

		public void Focus()
		{
			if (inputFieldComponent != null)
			{
				inputFieldComponent.Select();
				inputFieldComponent.ActivateInputField();

				if (cursorObject != null && showCursor)
				{
					if (cursorBlinking)
					{
						StartCursorBlinking();
					}
					else
					{
						cursorObject.SetActive(true);
					}
				}
			}
		}

		public void SetInteractable(bool interactable)
		{
			if (inputFieldComponent != null)
			{
				inputFieldComponent.interactable = interactable;

				if (backgroundImage != null)
				{
					backgroundImage.color = interactable ? normalColor :
						new Color(normalColor.r * 0.8f, normalColor.g * 0.8f, normalColor.b * 0.8f, normalColor.a * 0.5f);
				}
			}
		}

		public void SetPlaceholderText(string text)
		{
			placeholderText = text;
			if (placeholderTextComponent != null)
			{
				placeholderTextComponent.text = text;
			}
		}

		private void OnInputValueChanged(string value)
		{
			UpdateCursorPosition();
			onValueChanged?.Invoke(value);
		}

		private void OnInputSubmit(string value)
		{
			lastSubmittedText = value;
			onSubmit?.Invoke(value);
			Debug.Log($"MiyukiInputField: Отправлен текст: {value}");
			CheckTextInput(value);
			Clear();
			Focus();
		}

		private void CheckTextInput(string text)
		{
			if (string.IsNullOrEmpty(text)) return;

			string lowerText = text.ToLower();

			foreach (var kvp in wordResponses)
			{
				if (lowerText.Contains(kvp.Key.ToLower()))
				{
					Debug.Log($"Обнаружено слово: '{kvp.Key}' в тексте: '{text}'");
					kvp.Value?.Invoke(); // Выполняем действие
					break;
				}
			}
		}

		private void OnInputSelect(string value)
		{
			if (backgroundImage != null) backgroundImage.color = focusedColor;

			if (cursorObject != null && showCursor)
			{
				if (cursorBlinking)
				{
					StartCursorBlinking();
				}
				else
				{
					cursorObject.SetActive(true);
				}
			}

			onSelect?.Invoke();
		}

		private void OnInputDeselect(string value)
		{
			if (backgroundImage != null) backgroundImage.color = normalColor;

			if (cursorObject != null)
			{
				cursorObject.SetActive(false);
			}

			if (cursorBlinkCoroutine != null)
			{
				StopCoroutine(cursorBlinkCoroutine);
				cursorBlinkCoroutine = null;
			}

			onDeselect?.Invoke();
		}

		public void UpdateVisualSettings()
		{
			if (backgroundImage != null) backgroundImage.color = normalColor;

			if (placeholderTextComponent != null)
			{
				placeholderTextComponent.color = placeholderColor;
				placeholderTextComponent.fontSize = fontSize;
				placeholderTextComponent.text = placeholderText;
				placeholderTextComponent.alignment = placeholderAlignment;
			}

			if (textComponent != null)
			{
				textComponent.color = textColor;
				textComponent.fontSize = fontSize;
				textComponent.alignment = textAlignment;
			}

			if (inputFieldComponent != null)
			{
				inputFieldComponent.characterLimit = characterLimit;
				inputFieldComponent.contentType = contentType;
			}
		}

		public static MiyukiInputField CreateChatInput(string spritePath, Transform parentWindow, Vector2 windowSize, Vector3 windowPosition,
	string placeholder, Vector2? inputPosition = null, Vector2? inputSize = null)
		{
			try
			{
				string path = string.IsNullOrEmpty(spritePath) ? "MiyukiVisual/dlog_test.png" : spritePath;
				if (parentWindow == null) parentWindow = BattleSystem.instance.ActWindow.transform;
				Sprite sprite = MiyukiUI.GetSprite(path);
				GameObject window = MiyukiUI.CreateUIImage("ChatWindow", parentWindow, sprite, windowSize, windowPosition, true);
				window.AddComponent<MiyukiWindowDragHandler>();

				Vector2 finalInputPosition = inputPosition ?? new Vector2(0, -20);
				Vector2 finalInputSize = inputSize ?? new Vector2(400, 35);

				MiyukiInputField inputField = Create(window.transform, finalInputPosition, finalInputSize);

				string text = string.IsNullOrEmpty(placeholder) ? "Input text here" : placeholder;
				inputField.placeholderText = text;
				inputField.characterLimit = 20;
				inputField.normalColor = Color.white;
				inputField.textColor = Color.black;
				inputField.focusedColor = new Color(0.95f, 0.95f, 1f, 1f);
				inputField.textAlignment = TMPro.TextAlignmentOptions.Center;
				inputField.placeholderAlignment = TMPro.TextAlignmentOptions.Center;
				inputField.cursorColor = Color.magenta;
				inputField.cursorBlinkSpeed = 0.6f;

				inputField.UpdateVisualSettings();

				//if (onSubmitCallback != null)
				//{
				//	inputField.onSubmit.AddListener(onSubmitCallback.Invoke);
				//}

				BattleSystem.instance.StartCoroutine(DelayedFocusCoroutine(inputField));

				return inputField;
			}
			catch
			{
				return null;
			}
		}

		private static IEnumerator DelayedFocusCoroutine(MiyukiInputField inputField)
		{
			yield return new WaitForSeconds(0.1f);
			inputField?.Focus();
		}

		public static MiyukiInputField Create(Transform parent, Vector2 position, Vector2 size)
		{
			GameObject inputObj = new GameObject("MiyukiInputField");
			inputObj.transform.SetParent(parent, false);

			MiyukiInputField inputField = inputObj.AddComponent<MiyukiInputField>();
			if (inputField != null)
			{
				inputField.SetPosition(position);
				inputField.SetSize(size);
			}

			return inputField;
		}
	}
}