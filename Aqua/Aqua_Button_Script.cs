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
    public class Aqua_Button_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        /// <summary>
        /// Sprite when Aqua Chibi Button is available
        /// </summary>
        public Sprite SpriteOn;
        public const string SpriteOnPath = "AquaChibi.png";

        public Image Img;
        public Aqua_Button AquaButton;
        public bool interactable;
        private bool wasInteractable = false;

        public Color normalColor = Color.white;
        public Color hoverColor = new Color(0.8f, 0.8f, 0.8f);
        public Color pressedColor = new Color(0.9f, 0.9f, 0.9f);
        public Color disabledColor = new Color(1f, 1f, 1f, 0.5f);

        public void Awake()
        {
            Utils.getSpriteAsync(SpriteOnPath, delegate (AsyncOperationHandle handle)
            {
                SpriteOn = (Sprite)handle.Result;
            });

            Img = gameObject.GetComponent<Image>();
            AquaButton = gameObject.GetComponent<Aqua_Button>();
            gameObject.AddComponent<Aqua_Button_Tooltip>();
        }

        public void Update()
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveAqua = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Aqua);

            if (aliveAqua == null && Aqua_Button.instance != null)
            {
                Destroy(Aqua_Button.instance.gameObject);
            }

            interactable = AquaButton != null && Img != null && aliveAqua != null;

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

            if (interactable && BattleSystem.instance.ActWindow.CanAnyMove && Utils.AquaVoiceButtonHotkey && Input.GetKeyDown(KeyCode.N))
            {
                AquaButton?.AquaYapping();
                Img.color = normalColor;

                //Img.rectTransform.localScale = 1.1f * Vector3.one;
                //Img.color = pressedColor;
            }
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

            AquaButton?.AquaYapping();
            Img.rectTransform.localScale = 1.1f * Vector3.one;
            Img.color = pressedColor;
        }
    }
}
