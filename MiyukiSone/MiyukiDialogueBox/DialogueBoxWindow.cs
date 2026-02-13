using UnityEngine;
using UnityEngine.UI;
using static MiyukiSone.Affection;
using static MiyukiSone.DialogueBoxData;
using static MiyukiSone.Utils;
using static MiyukiSone.DialogueBoxStringLoader;
using System.Collections.Generic;

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

		public DialogueBoxState currentDialogueBoxState;

		private void Awake()
		{
			InitializeWindow();	
			LoadDialogues();
		}

		private void Start()
		{
			CreateButtons();
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
			ChangeAffectionPoints(points);
			switch (currentDialogueBoxState)
			{
				case DialogueBoxState.love: ClickLove(isYesClick); break;
				case DialogueBoxState.kiss: ClickKiss(isYesClick); break;
				//case BoxState.sex: ClickSex(); break;
				//case BoxState.help: ClickHelp(); break;
				default: break;
			}
			Destroy(gameObject);
		}

		private void ClickLove(bool isYes = true)
		{
			MiyukiTextBox(currentDialogueBoxState, isYes);
		}

		private void ClickKiss(bool isYes = true)
		{
			MiyukiTextBox(currentDialogueBoxState, isYes);
			//PlaySound("Kiss");
		}

		private void ClickSex()
		{

		}

		private void ClickHelp()
		{

		}
	}
}