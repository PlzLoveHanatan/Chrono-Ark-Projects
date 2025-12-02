using System.Linq;
using EmotionSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QoH
{
	public class QoH_Sanity_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
	{
		public Image Img;

		[Header("Interaction Colors")]
		public Color normalColor = Color.white;
		public Color hoverColor = new Color(0.8f, 0.8f, 0.8f);
		public Color pressedColor = new Color(0.9f, 0.9f, 0.9f);
		public Color disabledColor = new Color(1f, 1f, 1f, 0.5f);

		public const string Sanity_M_Path = "Visual/QoH/Sanity/Sanity_M.png";
		public const string Sanity_H_Path = "Visual/QoH/Sanity/Sanity_H.png";

		private Sprite sanity_M;
		private Sprite sanity_H;

		private bool wasInteractable;
		private bool interactable;
		public bool AllowPulse = true;

		private float pulseSpeed = 4f;
		private float pulseAmount = 0.08f;

		private Vector3 baseScale;

		public void Awake()
		{
			Img = GetComponent<Image>();

			if (Img != null)
			{
				baseScale = Img.rectTransform.localScale;
			}

			sanity_M = Utils_Ui.GetSprite(Sanity_M_Path);
			sanity_H = Utils_Ui.GetSprite(Sanity_H_Path);
		}

		public void Update()
		{			
			var queen = Utils.AllyTeam.AliveChars.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_QoH);
			var sanity = Utils.ReturnBuff(queen, ModItemKeys.Buff_B_QoH_Sanity) as Buffs.QoHSanity;

			interactable = sanity.CanSwitchForm || sanity.UnlimitedSwitches;

			// Смена спрайта в зависимости от текущего баффа
			if (sanity.MagicalGirlMode)
			{
				Img.sprite = sanity_M;
			}
			else
			{
				Img.sprite = sanity_H;
			}

			// Стало НЕ активно
			if (!interactable && wasInteractable)
			{
				Img.rectTransform.localScale = baseScale;
				Img.color = disabledColor;
			}

			// Стало активно
			if (interactable && !wasInteractable)
			{
				Img.rectTransform.localScale = baseScale;
				Img.color = normalColor;
			}

			// ========== ПУЛЬСАЦИЯ ==========

			if (interactable && AllowPulse)
			{
				float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
				Img.rectTransform.localScale = baseScale * pulse;
			}

			wasInteractable = interactable;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (interactable && BattleSystem.instance.ActWindow.CanAnyMove)
			{
				AllowPulse = false;
				Img.rectTransform.localScale = 1.1f * baseScale;
				Img.color = hoverColor;
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			Img.rectTransform.localScale = baseScale;
			Img.color = interactable ? normalColor : disabledColor;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (interactable && BattleSystem.instance.ActWindow.CanAnyMove)
			{
				Img.color = pressedColor;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (interactable && BattleSystem.instance.ActWindow.CanAnyMove)
			{
				var queen = Utils.AllyTeam.AliveChars.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_QoH);
				var sanity = Utils.ReturnBuff(queen, ModItemKeys.Buff_B_QoH_Sanity) as Buffs.QoHSanity;

				if (sanity != null)
				{
					sanity.ChangeSanity();
					AllowPulse = false;
				}

				Img.rectTransform.localScale = baseScale;
				Img.color = disabledColor;
			}
		}
	}
}