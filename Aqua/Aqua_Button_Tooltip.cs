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

namespace Aqua
{
    public class Aqua_Button_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Aqua_Button AquaButton;
        public int Width = 200;
        public int TextSize = 20;

        private static readonly Dictionary<string, string> AquaButtonTexts = new Dictionary<string, string>
        {
            { "English", "Click to let Aqua-sama bless you with her voice." },
            { "Korean", "클릭하면 아쿠아님의 목소리로 축복받을 수 있습니다." },
            { "Japanese", "" },
            { "Chinese", "点击以让阿库娅大人藉由她的圣言来祝福你" },
            { "Chinese-TW", "點擊以讓阿克婭大人藉由她的聖言來祝福你" }
        };

        private static readonly Dictionary<string, string> AquaButtonTextsHotkey = new Dictionary<string, string>
        {
            { "English", "Click or press [N] to let Aqua-sama bless you with her voice." },
            { "Korean", "클릭하거나 N키를 눌러 아쿠아님의 목소리로 축복을 받으세요." },
            { "Japanese", "" },
            { "Chinese", "点击或按下N键以让阿库娅大人藉由她的圣言来祝福你" },
            { "Chinese-TW", "點擊或按下N鍵以讓阿克婭大人藉由她的聖言來祝福你" }
        };

        public void Awake()
        {
            AquaButton = gameObject.GetComponent<Aqua_Button>();
        }

        public string TooltipText
        {
            get
            {
                Aqua_Button_Script buttonScript = GetComponent<Aqua_Button_Script>();
                string language = LocalizationManager.CurrentLanguage;

                bool useHotkey = AquaButton != null && buttonScript.interactable && Utils.AquaVoiceButtonHotkey;

                string text = "";

                if (useHotkey)
                {
                    if (AquaButtonTextsHotkey.ContainsKey(language) && !string.IsNullOrEmpty(AquaButtonTextsHotkey[language]))
                    {
                        text = AquaButtonTextsHotkey[language];
                    }
                    else
                    {
                        text = AquaButtonTextsHotkey["English"];
                    }
                }
                else
                {
                    if (AquaButtonTexts.ContainsKey(language) && !string.IsNullOrEmpty(AquaButtonTexts[language]))
                    {
                        text = AquaButtonTexts[language];
                    }
                    else
                    {
                        text = AquaButtonTexts["English"];
                    }
                }

                return text;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (AquaButton != null)
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
            if (AquaButton != null)
            {
                ToolTipWindow.ToolTipDestroy();
            }
        }
    }
}
