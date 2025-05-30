using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using Dialogical;
using I2.Loc;
using System.Drawing;

namespace Raphi
{
    public class Raphi_Button_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Raphi_Button RaphiButton;
        public int Width = 200;
        public int TextSize = 20;

        private static readonly Dictionary<string, string> RaphiButtonTexts = new Dictionary<string, string>
        {
            { "English", "Click to activate <color=#7B68EE>Celestial Connection</color>." },
            { "Korean", "" },
            { "Japanese", "" },
            { "Chinese", "点击以使用<color=#7B68EE>天堂联络</color>。" },
            { "Chinese-TW", "點擊以使用<color=#7B68EE>天堂聯絡</color>。" }
        };

        private static readonly Dictionary<string, string> RaphiButtonTextsHotkey = new Dictionary<string, string>
        {
            { "English", "Click or press [C] to activate <color=#7B68EE>Celestial Connection</color>." },
            { "Korean", "" },
            { "Japanese", "" },
            { "Chinese", "点击或按下C键以使用<color=#7B68EE>天堂联络</color>。" },
            { "Chinese-TW", "點擊或按下C鍵以使用<color=#7B68EE>天堂聯絡</color>。" }
        };
        private static readonly Dictionary<string, string> RaphiButtonTextsNoBuff = new Dictionary<string, string>
        {
            { "English", "<color=#7B68EE>Celestial Connection</color> is not available." },
            { "Korean", "" },
            { "Japanese", "" },
            { "Chinese", "<color=#7B68EE>天堂联络</color>处于不可用状态。" },
            { "Chinese-TW", "<color=#7B68EE>天堂聯絡</color>處於不可用狀態。" }
        };

        public void Awake()
        {
            RaphiButton = gameObject.GetComponent<Raphi_Button>();
        }

        public string TooltipText
        {
            get
            {
                Raphi_Button_Script buttonScript = GetComponent<Raphi_Button_Script>();
                string language = LocalizationManager.CurrentLanguage;

                bool useHotkey = RaphiButton != null && buttonScript.interactable && Utils.RaphiButtonHotkey;

                string text = "";

                if (useHotkey)
                {
                    if (RaphiButtonTextsHotkey.ContainsKey(language) && !string.IsNullOrEmpty(RaphiButtonTextsHotkey[language]))
                    {
                        text = RaphiButtonTextsHotkey[language];
                    }
                    else
                    {
                        text = RaphiButtonTextsHotkey["English"];
                    }
                }
                else
                {
                    if (RaphiButtonTexts.ContainsKey(language) && !string.IsNullOrEmpty(RaphiButtonTexts[language]))
                    {
                        text = RaphiButtonTexts[language];
                    }
                    else
                    {
                        text = RaphiButtonTexts["English"];
                    }
                }
                if (!buttonScript.interactable)
                {
                    if (RaphiButtonTextsNoBuff.ContainsKey(language) && !string.IsNullOrEmpty(RaphiButtonTexts[language]))
                    {
                        text = RaphiButtonTextsNoBuff[language];
                    }
                    else
                    {
                        text = RaphiButtonTextsNoBuff["English"];
                    }
                }

                return text;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (RaphiButton != null)
            {
                var tooltip = ToolTipWindow.NewToolTip(transform, TooltipText, Width,
                    new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0f));

                tooltip.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 40f);

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
            if (RaphiButton != null)
            {
                ToolTipWindow.ToolTipDestroy();
            }
        }
    }
}