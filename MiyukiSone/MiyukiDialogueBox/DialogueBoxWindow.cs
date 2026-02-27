using UnityEngine;
using UnityEngine.UI;
using static MiyukiSone.Affection;
using static MiyukiSone.DialogueBoxData;
using static MiyukiSone.DialogueBox;
using static MiyukiSone.Utils;
using static MiyukiSone.EventData;
using static MiyukiSone.EventHelp;
using System.Collections.Generic;
using System.EnterpriseServices;
using UnityEngine.EventSystems;
using System.Collections;
using System.Linq;

namespace MiyukiSone
{
	public class DialogueBoxWindow : MonoBehaviour
	{
		public Image Img;

		[Header("Button Sprites")]
		public Sprite yesButtonSprite;
		public Sprite noButtonSprite;

		[Header("Button Positions")]
		public Vector2 yesButtonPosition = new Vector3(145, -25);
		public Vector2 noButtonPosition = new Vector3(270, -25);
		public Vector2 buttonSize = new Vector2(100, 33);

		private GameObject btn_yes;
		private GameObject btn_no;

		private const string Btn_Yes_Sprite = "MiyukiVisual/DialogueBox/box_btn_yes.png";
		private const string Btn_No_Sprite = "MiyukiVisual/DialogueBox/box_btn_no.png";
		private const string Btn_Kiss_Sprite = "MiyukiVisual/DialogueBox/box_btn_kiss.png";

		private bool isClicked = false;
		public DialogueBoxState currentDialogueBoxState;

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
			// add the voice if trying to end turn and skip the window
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
			Sprite spriteYes = yesButtonSprite ?? UtilsUI.GetSprite(Btn_Yes_Sprite);
			Sprite spriteNo = noButtonSprite ?? UtilsUI.GetSprite(Btn_No_Sprite);

			if (currentDialogueBoxState == DialogueBoxState.kiss)
			{
				spriteYes = UtilsUI.GetSprite(Btn_Kiss_Sprite);
			}

			bool swapBtnPos = RandomManager.RandomPer("MiyukiRandomSwap", 100, 30);
			bool twoYesButtons = RandomManager.RandomPer("MiyukiRandomDouble", 100, 15);

			Vector2 yesPos = swapBtnPos ? noButtonPosition : yesButtonPosition;
			Vector2 noPos = swapBtnPos ? yesButtonPosition : noButtonPosition;

			btn_yes = CreateButton("btn_yes", spriteYes, yesPos, OnYesClicked);

			if (twoYesButtons)
			{
				CreateButton("btn_yes2", spriteYes, noPos, OnYesClicked);
				btn_no = null;
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
			ClickHandler(true, 1);
			Debug.Log("Btn yes clicked");
		}

		private void OnNoClicked()
		{
			ClickHandler(false, -1);
			Debug.Log("Btn no clicked");
		}

		private void ClickHandler(bool isYesClick, int points)
		{
			if (isClicked) return;

			//bool heartsOnButton = RandomManager.RandomPer("MiyukiHeartPos", 100, 50);

			ChangeAffectionPoints(points);

			switch (currentDialogueBoxState)
			{
				case DialogueBoxState.love: ClickLove(isYesClick); break;
				case DialogueBoxState.kiss: ClickKiss(isYesClick); break;
				case DialogueBoxState.help: ClickHelp(isYesClick); break;
				default: break;
			}

			if ((currentDialogueBoxState == DialogueBoxState.kiss && !isYesClick) || currentDialogueBoxState == DialogueBoxState.help) return;

			isClicked = true;
			ClickBonusAction(isYesClick);
		}

		private void ClickLove(bool isYes)
		{
			MiyukiTextBoxLove(isYes);
		}

		private void ClickKiss(bool isYes)
		{
			if (isYes) ResetAllKissNo();
			MiyukiTextBoxKiss(isYes);
		}

		private void ClickHelp(bool isYes)
		{
			MiyukiTextEvent(isYes); // change to help event
			if (isYes) MiyukiHelpAction();
		}

		private void ClickBonusAction(bool isYes)
		{
			if (RandomManager.RandomPer("MiyukiRandomAnim", 100, 70))
			{
				if (isYes) PlayRandomYesAnimation();
				else PlayRandomNoAnimation();
			}
			else
			{
				RemoveWindow(gameObject);
				isClicked = false;
			}	
		}

		private void PlayRandomYesAnimation()
		{
			var animations = new List<System.Func<IEnumerator>>
			{
				() => FloatUpAndFade(2f),
				() => LoveBurst(2f),
			};
			Transform extraTarget = null;
			var available = new List<System.Func<IEnumerator>>(animations);
			if (MiyukiData.LastYesBoxAnimation != -1 && available.Count > 1) available.Remove(animations[MiyukiData.LastYesBoxAnimation]);
			int index = RandomManager.RandomInt("MiyukiYesAnim", 0, available.Count);
			PlayAnimationAndDestroy(available[index].Invoke());
			MiyukiData.LastYesBoxAnimation = animations.IndexOf(available[index]);
			if (RandomManager.RandomPer("MiyukiHeartsSpawn", 100, 50)) return;
			Transform mainTarget = RandomManager.RandomPer("MiyukiHeartPos", 100, 50) ? btn_yes.transform : MiyukiBchar.transform;
			MiyukiVisual.Instance.SpawnHearts(mainTarget);
			if (RandomManager.RandomPer("MiyukiHeartExtra", 100, 25)) extraTarget = mainTarget == btn_yes.transform ? MiyukiBchar.transform : btn_yes.transform;
			MiyukiVisual.Instance.SpawnHearts(extraTarget);		
		}

		private void PlayRandomNoAnimation()
		{
			var animations = new List<System.Func<IEnumerator>>
			{
				() => TiltFallDestroy(2f),
				() => ShatterDrop(2f),
			};

			var available = new List<System.Func<IEnumerator>>(animations);
			if (MiyukiData.LastNoBoxAnimation != -1 && available.Count > 1) available.Remove(animations[MiyukiData.LastNoBoxAnimation]);
			int index = RandomManager.RandomInt("MiyukiNoAnim", 0, available.Count);
			PlayAnimationAndDestroy(available[index].Invoke());
			MiyukiData.LastNoBoxAnimation = animations.IndexOf(available[index]);		
		}

		private void PlayAnimationAndDestroy(IEnumerator animation)
		{
			StartCoroutine(AnimateAndDestroy(animation));
		}

		private IEnumerator AnimateAndDestroy(IEnumerator animation)
		{
			yield return StartCoroutine(animation);
			RemoveWindow(gameObject);
			isClicked = false;
		}

		private IEnumerator FloatUpAndFade(float duration)
		{
			Transform t = transform;
			CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
			Vector3 startPos = t.localPosition;
			Vector3 startScale = t.localScale;

			// случайное смещение по X: влево или вправо
			float xOffset = Random.Range(-50f, 50f);
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
			CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
			if (canvasGroup == null)
				canvasGroup = gameObject.AddComponent<CanvasGroup>();

			Quaternion startRot = t.rotation;

			// Случайное направление падения: либо -25°, либо +25°
			float angleZ = Random.value < 0.5f ? -25f : 25f;
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
			CanvasGroup cg = GetComponent<CanvasGroup>();
			if (cg == null)
				cg = gameObject.AddComponent<CanvasGroup>();

			Vector3 startScale = t.localScale;
			Quaternion startRot = t.rotation;

			float maxScaleMultiplier = 1.16f;
			float maxTilt = 4f;

			// случайный наклон вправо или влево
			float tiltDirection = Random.value < 0.5f ? 1f : -1f;

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
						Random.Range(-currentShake, currentShake),
						Random.Range(-currentShake, currentShake),
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
	}
}