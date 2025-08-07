using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.U2D;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Xao
{
    public class Xao_Hearts_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        public Image Img;
        public bool interactable = true;
        private bool wasInteractable = false;

        public Color normalColor = Color.white;
        public Color hoverColor = new Color(0.8f, 0.8f, 0.8f);
        public Color pressedColor = new Color(0.9f, 0.9f, 0.9f);
        public Color disabledColor = new Color(1f, 1f, 1f, 0.5f);

        public void Awake()
        {
            Img = gameObject.GetComponent<Image>();
            //XaoHeartButton = gameObject.GetComponent<Xao_Hearts>();
            //gameObject.AddComponent<Mia_Button_Tooltip>();
        }

        public void Update()
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveXao = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Xao);
            var buffKey = ModItemKeys.Buff_B_Xao_Affection;
            var buff = aliveXao?.BuffReturn(buffKey, false);

            if (aliveXao == null && this != null)
            {
                Destroy(this);
            }

            interactable = aliveXao != null && Img != null && buff != null;

            if (!interactable && wasInteractable)
            {
                Img.rectTransform.localScale = Vector3.one;
                Img.color = disabledColor;
            }
            if (interactable && wasInteractable == false)
            {
                Img.color = normalColor;
            }

            wasInteractable = interactable;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;

            Img.rectTransform.localScale = 1.1f * Vector3.one;
            Img.color = hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;

            Img.rectTransform.localScale = Vector3.one;
            Img.color = normalColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;

            Img.rectTransform.localScale = Vector3.one;
            Img.color = pressedColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;

            Img.rectTransform.localScale = 1.1f * Vector3.one;
            Img.color = pressedColor;
            RemoveOverload();
        }

        public void RemoveOverload()
        {
            string buffKey = ModItemKeys.Buff_B_Xao_Affection;
            string xao = ModItemKeys.Character_Xao;
            var aliveXao = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(c => c.Info.KeyData == xao && c.BuffReturn(buffKey, false) != null);
            var affection = aliveXao.BuffReturn(buffKey, false) as B_Xao_Affection;
            affection?.XaoCall();
        }
    }
}
