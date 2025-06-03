using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Mia
{
    public class Mia_Button_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        /// <summary>
        /// Sprite when Mia Chibi Button is available
        /// </summary>
        public Sprite SpriteOn;
        public const string SpriteOnPath = "MiaButton.png";

        public Image Img;
        public Mia_Button MiaButton;
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
            MiaButton = gameObject.GetComponent<Mia_Button>();
            gameObject.AddComponent<Mia_Button_Tooltip>();
        }

        public void Update()
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveMia = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Mia);
            var buffKey = ModItemKeys.Buff_B_Mia_SavageImpulse;
            var buff = aliveMia?.BuffReturn(buffKey, false);

            if (aliveMia == null && Mia_Button.instance != null)
            {
                Destroy(Mia_Button.instance.gameObject);
            }

            interactable = MiaButton != null && Img != null && buff != null;

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

            if (interactable && BattleSystem.instance.ActWindow.CanAnyMove && Utils.MiaButtonHotkey && Input.GetKeyDown(KeyCode.V))
            {
                MiaButton?.SavageImpulse();

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

            MiaButton?.SavageImpulse();
            Img.rectTransform.localScale = 1.1f * Vector3.one;
            Img.color = pressedColor;
        }
    }
}
