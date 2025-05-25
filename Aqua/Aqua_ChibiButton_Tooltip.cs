using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Aqua
{
    public class Aqua_ChibiButton_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Aqua_ChibiButton AquaButton;
        public int Width = 210;
        public int TextSize = 20;

        // change these to translatable texts if available
        public string Text_AquaPhrases => "Click to hear Aqua talk!";
        public string Text_AquaDead => "Aqua is unconscious... She can't talk right now.";

        public void Awake()
        {
            AquaButton = gameObject.GetComponent<Aqua_ChibiButton>();
        }

        public string TooltipText
        {
            get
            {
                Aqua_ChibiButton_Script chibiButtonScript = GetComponent<Aqua_ChibiButton_Script>();

                if (AquaButton != null && chibiButtonScript.interactable)
                {
                    return Text_AquaPhrases;

                }
                else
                {
                    return Text_AquaDead;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (AquaButton != null)
            {
                var tooltip = ToolTipWindow.NewToolTip(transform, TooltipText, Width,
                    new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0.5f, 0.5f));
                tooltip.GetComponent<ToolTipWindow>().Description.fontSize = TextSize;
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
