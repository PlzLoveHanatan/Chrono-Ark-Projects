using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Xao
{
    public class Xao_Hearts_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public int Width = 180;
        public int TextSize = 20;

        public string TooltipText
        {
            get
            {
                string text = ModLocalization.Affection_0 ?? "";
                return text;
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this != null)
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
            if (this != null)
            {
                ToolTipWindow.ToolTipDestroy();
            }
        }
    }
}
