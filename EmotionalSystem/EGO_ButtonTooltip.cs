using UnityEngine.EventSystems;
using UnityEngine;
using EmotionalSystem;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;

namespace EmotionalSystem
{
    public class EGO_ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private EGO_System EGO;
        public int Width = 180;
        public int TextSize = 20;

        // change these to translatable texts if available
        //public string Text_NoEGO => "No EGO skill is available";
        //public string Text_SwitchEGO => "Switch to EGO skills";
        //public string Text_SwitchHand => "Switch back to hand";

        public void Awake()
        {
            EGO = gameObject.GetComponent<EGO_System>();
        }

        public string TooltipText
        {
            get
            {
                var SwitchEGO = ModLocalization.EGO_SwitchEGO;
                var SwitchEGOHotkey = ModLocalization.EGO_SwitchEGOHotkey;
                var SwitchHand = ModLocalization.EGO_SwitchHand;
                var SwitchHandHotkey = ModLocalization.EGO_SwitchHandHotkey;
                var NoEGO = ModLocalization.EGO_NoEGO;
                string text = "";

                if (EGO != null)
                {
                    if (EGO.egoActive)
                    {
                        if (Utils.EGOButtonHotkey)
                        {
                            text = SwitchHandHotkey;
                        }
                        else
                            text = SwitchHand;
                    }
                    else
                    {
                        if (EGO.hasEGOSkill)
                        {
                            if (Utils.EGOButtonHotkey)
                            {
                                text = SwitchHandHotkey;
                            }
                            else
                                text = SwitchEGO;
                        }
                        else
                        {
                            text = NoEGO;
                        }
                    }
                    return text;
                }
                else
                {
                    return "";
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (EGO != null)
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
            if (EGO != null)
            {
                ToolTipWindow.ToolTipDestroy();
            }
        }
    }
}
