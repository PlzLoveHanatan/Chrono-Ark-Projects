using UnityEngine.EventSystems;
using UnityEngine;
using EmotionSystem;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;

namespace EmotionSystem
{
    public class EmotionSystem_EGO_Button_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private EmotionSystem_EGO_Button EGO_Button;
        public int Width = 180;
        public int TextSize = 20;
        public string Text_NoEGO => ModLocalization.EGO_Button_Empty ?? "";
        public string Text_SwitchEGO => ModLocalization.EGO_Button_ChangeToEGOHand;
		public string Text_SwitchHand => ModLocalization.EGO_Button_ChangeToHand;

		public void Awake()
        {
			EGO_Button = gameObject.GetComponent<EmotionSystem_EGO_Button>();
        }


		public string TooltipText
		{
			get
			{
				if (EGO_Button == null)
				{
					return "";
				}

				string text;

				if (EGO_Button.ActiveEGOHand)
				{
					text = Text_SwitchHand;
				}
				else if (EGO_Button.HasEGOSkill)
				{
					text = Text_SwitchEGO;
				}
				else
				{
					text = Text_NoEGO;
				}
				return text;
			}
		}


		public void OnPointerEnter(PointerEventData eventData)
        {
            if (EGO_Button != null)
            {
                var tooltip = ToolTipWindow.NewToolTip(transform, TooltipText, Width,
                new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0f));

                tooltip.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 20);

                var tipWindow = tooltip.GetComponent<ToolTipWindow>();
                var desc = tipWindow.Description;

                desc.fontSize = TextSize;

                desc.alignment = TextAlignmentOptions.Center;

                desc.enableWordWrapping = true;

                desc.margin = Vector4.zero;
                //desc.margin = new Vector4(5, 5, 5, 5);

                float maxWidth = 300f;
                Vector2 preferredValues = desc.GetPreferredValues(TooltipText, maxWidth, 1000f);

                tooltip.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredValues.x);
                tooltip.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredValues.y);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (EGO_Button != null)
            {
                ToolTipWindow.ToolTipDestroy();
            }
        }
    }
}
