using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using UnityEngine.UI;

namespace Aqua
{
    public class Aqua_ChibiButton_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        /// <summary>
        /// Sprite when Aqua Chibi Button is available
        /// </summary>
        public Sprite SpriteOn;
        public const string SpriteOnPath = "AquaChibi.png";

        public Image Img;
        public Aqua_ChibiButton Aqua;
        public bool interactable;

        public void Awake()
        {
            Utils.getSpriteAsync(SpriteOnPath, delegate (AsyncOperationHandle handle)
            {
                SpriteOn = (Sprite)handle.Result;
            });

            Img = gameObject.GetComponent<Image>();
            Aqua = gameObject.GetComponent<Aqua_ChibiButton>();
            gameObject.AddComponent<Aqua_ChibiButton_Tooltip>();
        }

        public void Update()
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveAqua = allies.FirstOrDefault(c => c.Info.Name == ModItemKeys.Character_Aqua);

            if (Aqua != null && Img != null && aliveAqua != null)
            {
                interactable = true;
            }
            else
            {
                interactable = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;
            Img.rectTransform.localScale = 1.1f * Vector3.one;
            Img.color = new Color(1f, 1f, 1f, 0.8f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;
            Img.rectTransform.localScale = Vector3.one;
            Img.color = new Color(1f, 1f, 1f, 1f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;
            Img.rectTransform.localScale = Vector3.one;
            Img.color = new Color(1f, 1f, 1f, 1f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable || !BattleSystem.instance.ActWindow.CanAnyMove) return;
            if (Aqua != null)
            {
                Aqua.AquaYapping();
            }

            Img.rectTransform.localScale = 1.1f * Vector3.one;
            Img.color = new Color(1f, 1f, 1f, 0.8f);
        }
    }
}
