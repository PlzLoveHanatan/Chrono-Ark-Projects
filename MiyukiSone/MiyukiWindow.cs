using UnityEngine;
using UnityEngine.UI;
using static MiyukiSone.Affection;
using static MiyukiSone.DialogueBox;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public class MiyukiWindow : MonoBehaviour
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

		public BoxState currentBoxState;

		private void Awake()
		{
			InitializeWindow();
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
			// Используем назначенные спрайты или загружаем по умолчанию
			Sprite spriteYes = yesButtonSprite ?? MiyukiUI.GetSprite("MiyukiVisual/btn_yes_click.png");
			Sprite spriteNo = noButtonSprite ?? MiyukiUI.GetSprite("MiyukiVisual/btn_no_click.png");

			// Создаем кнопку Yes
			btn_yes = MiyukiUI.CreateUIImage("btn_yes", transform, spriteYes,
				buttonSize, yesButtonPosition, true);

			if (btn_yes != null)
			{
				Button yesButton = btn_yes.AddComponent<Button>();
				yesButton.onClick.AddListener(OnYesClicked);

				// Опционально: настройка цветов кнопки
				var colors = yesButton.colors;
				colors.normalColor = Color.white;
				colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f);
				colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
				yesButton.colors = colors;
			}

			// Создаем кнопку No
			btn_no = MiyukiUI.CreateUIImage("btn_no", transform, spriteNo,
				buttonSize, noButtonPosition, true);

			if (btn_no != null)
			{
				Button noButton = btn_no.AddComponent<Button>();
				noButton.onClick.AddListener(OnNoClicked);

				// Опционально: настройка цветов кнопки
				var colors = noButton.colors;
				colors.normalColor = Color.white;
				colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f);
				colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
				noButton.colors = colors;
			}
		}

		private void OnYesClicked()
		{
			switch (currentBoxState)
			{
				case BoxState.love: ClickLove(); break;
				case BoxState.kiss: ClickKiss(); break;
				case BoxState.sex: ClickSex(); break;
				case BoxState.help: ClickHelp(); break;
				default: break;
			}
			Debug.Log("Btn Yes clicked");
			ChangePoints(1);
			Destroy(gameObject);
		}

		private void ClickLove()
		{

		}

		private void ClickKiss()
		{
			PlaySound("Kiss");
		}

		private void ClickSex()
		{

		}

		private void ClickHelp()
		{

		}

		private void OnNoClicked()
		{
			Debug.Log("Btn No clicked");
			ChangePoints();
			Destroy(gameObject);
		}
	}
}