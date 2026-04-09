using UnityEngine;
using UnityEngine.UI;
using static MiyukiSone.Affection;
using static MiyukiSone.DialogueData;
using static MiyukiSone.Dialogue;
using static MiyukiSone.Utils;
using static MiyukiSone.EventsData;
using System.Collections.Generic;
using System.EnterpriseServices;
using UnityEngine.EventSystems;
using System.Collections;
using System.Linq;
using Dialogical;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace MiyukiSone
{
	public class DialogueWindow : MonoBehaviour
	{
		public Image Img;

		[Header("Button Sprites")]
		public Sprite yesButtonSprite;
		public Sprite noButtonSprite;

		[Header("Button Positions")]
		public Vector2 yesButtonPosition = new Vector3(155, -45);
		public Vector2 noButtonPosition = new Vector3(270, -45);
		public Vector2 buttonSize = new Vector2(100, 33);

		private GameObject btn_yes;
		private GameObject btn_no;

		private const string Btn_Yes_Sprite = "MiyukiVisual/Dialogue/dlg_btn_yes.png";
		private const string Btn_No_Sprite = "MiyukiVisual/Dialogue/dlg_btn_no.png";
		private const string Btn_Kiss_Sprite = "MiyukiVisual/Dialogue/dlg_btn_kiss.png";

		public DialogueState CurrentDialogueState;
		public bool IsDoubleButton;
		public bool IsClicked = false;
		private int click;

		private void Awake()
		{
			InitializeWindow();
			LoadDialogues();
		}

		private void Start()
		{
			CreateButtons();
			//FindAndHookSkipButton();
		}

		private void Update()
		{
			if (BattleSystem.instance == null && FieldSystem.instance == null) RemoveWindow(gameObject);
		}

		private void InitializeWindow()
		{
			if (Img == null)
			{
				Img = GetComponent<Image>();
			}
		}

		private void CreateButtons()
		{
			Sprite spriteYes = yesButtonSprite ?? UtilsUI.GetSpriteFromMod(Btn_Yes_Sprite);
			Sprite spriteNo = noButtonSprite ?? UtilsUI.GetSpriteFromMod(Btn_No_Sprite);

			if (CurrentDialogueState == DialogueState.kiss) spriteYes = UtilsUI.GetSpriteFromMod(Btn_Kiss_Sprite);

			bool swapButtonsPos = MiyukiDecides;
			Vector2 yesPos = swapButtonsPos ? noButtonPosition : yesButtonPosition;
			Vector2 noPos = swapButtonsPos ? yesButtonPosition : noButtonPosition;

			btn_yes = CreateButton("btn_yes", spriteYes, yesPos, OnYesClicked);

			if (MiyukiDecides || IsDoubleButton)
			{
				CreateButton("btn_yes2", spriteYes, noPos, OnYesClicked);
				btn_no = null;

				if (!MiyukiSaveManager.Instance.CurrentData.EternalPromise)
				{
					MiyukiSaveManager.Instance.CurrentData.EternalPromise = true;
					MiyukiSaveManager.Instance.CurrentData.SaveExists = true;
					MiyukiSaveManager.Instance.Save();
				}
			}
			else
			{
				btn_no = CreateButton("btn_no", spriteNo, noPos, OnNoClicked);
			}
		}

		private GameObject CreateButton(string name, Sprite sprite, Vector2 position, UnityEngine.Events.UnityAction onClickAction)
		{
			var btn = UtilsUI.CreateUIImage(name, transform, sprite, buttonSize, position, true);
			if (btn != null)
			{
				Button button = btn.AddComponent<Button>();
				button.onClick.AddListener(onClickAction);
				SetButtonColors(button);
			}
			return btn;
		}

		private void SetButtonColors(Button button)
		{
			var colors = button.colors;
			colors.normalColor = Color.white;
			colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f);
			colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
			button.colors = colors;
		}

		private void OnYesClicked()
		{
			ClickHandler(true);
		}

		private void OnNoClicked()
		{
			ClickHandler(false);
		}


		private void ClickHandler(bool isYesClickClick)
		{
			click++;

			if (click >= 2 || CurrentDialogueState == DialogueState.kiss && click >= 25) RemoveWindow(gameObject);

			if (FieldSystem.instance == null && BattleSystem.instance == null || IsClicked) return;

			IsClicked = CurrentDialogueState != DialogueState.kiss;

			if (IsKuudere)
			{
				MiyukiTextEvent();
				RemoveWindow(gameObject);
			}
			else if (CurrentDialogueState == DialogueState.love)
			{
				DialoguePaws.ChoosePaws();
				MiyukiTextBoxLove(isYesClickClick);
				StartAnimationAndClose(isYesClickClick);
			}
			else if (CurrentDialogueState == DialogueState.kiss)
			{
				if (!isYesClickClick)
				{
					CurrentAffection = MiyukiAffection.Yandere;
					if (BattleSystem.instance != null) (MiyukiDecides ? (Action)DialoguePaws.YanderePaws : Events.YandereAction)();
					else if (FieldSystem.instance != null && MiyukiDecides) Events.YandereActionCut();
					StartKissDialogue(false);
				}
				else
				{
					CurrentAffection = MiyukiAffection.DereDere;
					Events.DereAction();
					ResetKissNoDialogue();
					StartKissDialogue(true);
					StartAnimationAndClose(true);
				}
			}
		}

		private void StartAnimationAndClose(bool isYesClick)
		{
			if (MiyukiDecides) (isYesClick ? (Action)PlayRandomYesAnimation : PlayRandomNoAnimation)();
			else RemoveWindow(gameObject);
		}

		#region Animations
		private void PlayRandomYesAnimation()
		{
			var animations = new List<System.Func<IEnumerator>>
			{
				() => FloatUpAndFade(2f),
				() => LoveBurst(2f),
			};
			Transform extraTarget = null;
			PlayAnimationAndDestroy(animations.RandomElement()?.Invoke());

			if (MiyukiDecides)
			{
				Transform mainTarget = MiyukiDecides ? btn_yes.transform : MiyukiBchar.transform;
				MiyukiVisual.Instance?.SpawnHearts(mainTarget);

				if (MiyukiDecides)
				{
					extraTarget = mainTarget == btn_yes.transform ? MiyukiBchar.transform : btn_yes.transform;
					MiyukiVisual.Instance?.SpawnHearts(extraTarget);
				}
			}
		}

		private void PlayRandomNoAnimation()
		{
			var animations = new List<System.Func<IEnumerator>>
			{
				() => TiltFallDestroy(2f),
				() => ShatterDrop(2f),
			};

			PlayAnimationAndDestroy(animations.RandomElement()?.Invoke());
		}

		private void PlayAnimationAndDestroy(IEnumerator animation)
		{
			if (animation == null) RemoveWindow(gameObject);
			else StartCoroutine(AnimateAndDestroy(animation));
		}

		private IEnumerator AnimateAndDestroy(IEnumerator animation)
		{
			yield return StartCoroutine(animation);
			RemoveWindow(gameObject);
		}

		private IEnumerator FloatUpAndFade(float duration)
		{
			Transform t = transform;
			CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
			Vector3 startPos = t.localPosition;
			Vector3 startScale = t.localScale;

			// случайное смещение по X: влево или вправо
			float xOffset = UnityEngine.Random.Range(-50f, 50f);
			Vector3 endPos = startPos + new Vector3(xOffset, 150f, 0);

			float elapsed = 0f;

			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				float progress = elapsed / duration;

				// движение вверх с рандомным X
				t.localPosition = Vector3.Lerp(startPos, endPos, progress);

				// легкий fade
				canvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);

				// пульс / сердцебиение
				float pulse = Mathf.Sin(progress * Mathf.PI * 2f) * 0.03f; // частота и амплитуда
				t.localScale = startScale * (1f + pulse);

				yield return null;
			}

			// аккуратно сбросить масштаб
			t.localScale = startScale;
		}

		private IEnumerator TiltFallDestroy(float duration)
		{
			Transform t = transform;

			// CanvasGroup для фейда
			CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
			Quaternion startRot = t.rotation;

			// Случайное направление падения: либо -25°, либо +25°
			float angleZ = UnityEngine.Random.value < 0.5f ? -25f : 25f;
			Quaternion endRot = Quaternion.Euler(0, 0, angleZ);

			Vector3 startPos = t.localPosition;
			Vector3 endPos = startPos + new Vector3(0, -200f, 0);

			Vector3 startScale = t.localScale;
			Vector3 endScale = startScale * 0.9f; // лёгкое "сжатие" перед смертью

			float elapsed = 0f;

			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				float p = elapsed / duration;

				// Плавность аниме-стиля (ease-in)
				float eased = p * p;

				t.rotation = Quaternion.Lerp(startRot, endRot, eased);
				t.localPosition = Vector3.Lerp(startPos, endPos, eased);
				t.localScale = Vector3.Lerp(startScale, endScale, eased);

				canvasGroup.alpha = Mathf.Lerp(1f, 0f, p);

				yield return null;
			}
		}

		private IEnumerator LoveBurst(float duration)
		{
			Transform t = transform;
			CanvasGroup cg = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
			Vector3 startScale = t.localScale;
			Quaternion startRot = t.rotation;

			float maxScaleMultiplier = 1.16f;
			float maxTilt = 4f;

			// случайный наклон вправо или влево
			float tiltDirection = UnityEngine.Random.value < 0.5f ? 1f : -1f;

			float elapsed = 0f;

			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				float p = elapsed / duration;

				float easeOut = 1f - Mathf.Pow(1f - p, 3f);
				float pulse = Mathf.Sin(p * Mathf.PI) * 0.06f;
				float scale = Mathf.Lerp(1f, maxScaleMultiplier, easeOut) + pulse;

				t.localScale = startScale * scale;

				// наклон вправо или влево
				float tilt = Mathf.Sin(p * Mathf.PI) * maxTilt * tiltDirection;
				t.rotation = Quaternion.Euler(0, 0, tilt);

				float fadeStart = 0.35f;
				if (p > fadeStart)
				{
					float fadeProgress = (p - fadeStart) / (1f - fadeStart);
					cg.alpha = 1f - fadeProgress;
				}

				yield return null;
			}

			t.rotation = startRot;
			t.localScale = startScale;
		}

		private IEnumerator ShatterDrop(float duration)
		{
			Transform t = transform;

			CanvasGroup cg = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
			Vector3 originalPos = t.localPosition;
			Vector3 fallPos = originalPos + new Vector3(0, -400f, 0);

			float elapsed = 0f;
			float shakeDuration = duration * 0.3f;
			float maxShake = 4f; // ← регулируй тут

			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				float p = elapsed / duration;

				if (elapsed < shakeDuration)
				{
					float shakeProgress = 1f - (elapsed / shakeDuration); // затухание
					float currentShake = maxShake * shakeProgress;

					t.localPosition = originalPos + new Vector3(
						UnityEngine.Random.Range(-currentShake, currentShake),
						UnityEngine.Random.Range(-currentShake, currentShake),
						0);
				}
				else
				{
					float fallProgress = (elapsed - shakeDuration) / (duration - shakeDuration);
					float easedFall = fallProgress * fallProgress; // ускорение падения

					t.localPosition = Vector3.Lerp(originalPos, fallPos, easedFall);
					cg.alpha = 1f - p;
				}

				yield return null;
			}
		}
		#endregion
	}
}