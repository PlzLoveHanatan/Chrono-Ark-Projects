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
    public class Xao_Combo_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public int Width = 180;
        public int TextSize = 20;

        public string TooltipText
        {
            get
            {
                string text;

                if (Xao_Combo.CurrentCombo >= 8)
                {
                    if (Xao_Combo.SaveComboBetweenTurns)
                    {
                        text = ModLocalization.Combo_3.Replace("&a", Xao_Combo.CurrentCombo.ToString());
                    }
                    else
                    {
                        text = ModLocalization.Combo_1.Replace("&a", Xao_Combo.CurrentCombo.ToString());
                    }
                }
                else if (Xao_Combo.SaveComboBetweenTurns)
                {
                    text = ModLocalization.Combo_2.Replace("&a", Xao_Combo.CurrentCombo.ToString());
                }
                else
                {
                    text = ModLocalization.Combo_0.Replace("&a", Xao_Combo.CurrentCombo.ToString());
                }
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

                // Фиксируем ширину
                float fixedWidth = Width;

                // Получаем высоту текста при фиксированной ширине
                Vector2 preferredValues = desc.GetPreferredValues(TooltipText, fixedWidth, 1000f);

                var rect = tooltip.GetComponent<RectTransform>();

                // Устанавливаем фиксированную ширину (ширина не меняется)
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fixedWidth);

                // Устанавливаем высоту по вычисленному значению
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredValues.y);
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
