using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using UnityEngine.UI;
using Raphi;

namespace Raphi
{
    public class Raphi_Button_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        /// <summary>
        /// Sprite when Raphi Chibi Button is available
        /// </summary>
        public Sprite SpriteOn;
        public const string SpriteOnPath = "RaphiButton.png";

        public Image Img;
        public Raphi_Button RaphiButton;
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
            RaphiButton = gameObject.GetComponent<Raphi_Button>();
            gameObject.AddComponent<Raphi_Button_Tooltip>();
        }

        public void Update()
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveRaphi = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Raphi);
            var buffKey = ModItemKeys.Buff_B_CelestialConnection;
            var buff = aliveRaphi?.BuffReturn(buffKey, false);

            if (aliveRaphi == null && Raphi_Button.instance != null)
            {
                Destroy(Raphi_Button.instance.gameObject);
            }

            interactable = RaphiButton != null && Img != null && buff != null;

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

            if (interactable && BattleSystem.instance.ActWindow.CanAnyMove && Utils.RaphiButtonHotkey && Input.GetKeyDown(KeyCode.C))
            {
                RaphiButton?.CelestialConnection();

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

            RaphiButton?.CelestialConnection();
            Img.rectTransform.localScale = 1.1f * Vector3.one;
            Img.color = pressedColor;
        }

        private void OnDisable()
        {
            if (Img != null)
            {
                Img.rectTransform.localScale = Vector3.one;
                Img.color = disabledColor;
            }
        }
    }
}
