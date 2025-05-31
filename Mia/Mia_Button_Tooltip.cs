using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using I2.Loc;

namespace Mia
{
    public class Mia_Button_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Mia_Button MiaButton;
        public int Width = 160;
        public int TextSize = 20;

        // change these to translatable texts if available
        public string Text_MiaDiscard => "Click to activate <color=#FF0070>Savage Impulse</color>";

        public string Text_MiaDead => "";

        public void Awake()
        {
            MiaButton = gameObject.GetComponent<Mia_Button>();
        }

        public string TooltipText
        {
            get
            {
                Mia_Button_Script buttonScript = GetComponent<Mia_Button_Script>();
                string language = LocalizationManager.CurrentLanguage;

                bool useHotkey = MiaButton != null && buttonScript.interactable && Utils.MiaButtonHotkey;

                var normalText = ModLocalization.MiaButton_0;
                var hotkeyText = ModLocalization.MiaButton_1;
                var notAvailableText = ModLocalization.MiaButton_2;

                string text = "";

                if (useHotkey)
                {
                    text = hotkeyText;
                }
                else
                {
                    text = normalText;
                }
                if (!buttonScript.interactable)
                {
                    text = notAvailableText;
                }

                return text;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (MiaButton != null)
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
            if (MiaButton != null)
            {
                ToolTipWindow.ToolTipDestroy();
            }
        }
    }
}
