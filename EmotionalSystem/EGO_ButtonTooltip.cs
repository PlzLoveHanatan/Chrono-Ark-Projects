using UnityEngine.EventSystems;
using UnityEngine;
using EmotionalSystem;

namespace EmotionalSystem
{
    public class EGO_ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private EGO_System EGO;
        public int Width = 250;
        public int TextSize = 20;

        // change these to translatable texts if available
        public string Text_NoEGO => "No EGO skill is available";
        public string Text_SwitchEGO => "Switch to EGO skills";
        public string Text_SwitchHand => "Switch back to hand";

        public void Awake()
        {
            EGO = gameObject.GetComponent<EGO_System>();
        }

        public string TooltipText
        {
            get
            {
                if (EGO != null)
                {
                    if (EGO.egoActive)
                    {
                        return Text_SwitchHand;
                    }
                    else
                    {
                        if (EGO.hasEGOSkill)
                        {
                            return Text_SwitchEGO;
                        }
                        else
                        {
                            return Text_NoEGO;
                        }
                    }
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
                    new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0.5f, 0.5f));
                tooltip.GetComponent<ToolTipWindow>().Description.fontSize = TextSize;
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
