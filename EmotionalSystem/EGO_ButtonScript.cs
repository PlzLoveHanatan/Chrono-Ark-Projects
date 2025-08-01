﻿using System.Diagnostics.Contracts;
using EmotionalSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace EmotionalSystem
{
    public class EGO_ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        /// <summary>
        /// Sprite when no EGO is available
        /// </summary>
        public Sprite SpriteOff;
        public const string SpriteOffPath = "EGO_Button.png";

        /// <summary>
        /// Sprite when EGO is available
        /// </summary>
        public Sprite SpriteOn;
        public const string SpriteOnPath = "EGO_Active.png";

        /// <summary>
        /// Sprite when EGO hand is active
        /// </summary>
        public Sprite SpriteActive;
        public const string SpriteActivePath = "BattleIcon_EgoOn.png";

        public Image Img;
        public EGO_System EGO;
        public bool interactable;

        public float rotationAmplitude = 10f;
        public float rotationFrequency = 2f;
        private float initialRotationZ;
        public bool RotationOn;

        public void Awake()
        {
            initialRotationZ = transform.eulerAngles.z;

            Utils.getSpriteAsync(SpriteOffPath, delegate (AsyncOperationHandle handle)
            {
                SpriteOff = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(SpriteOnPath, delegate (AsyncOperationHandle handle)
            {
                SpriteOn = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(SpriteActivePath, delegate (AsyncOperationHandle handle)
            {
                SpriteActive = (Sprite)handle.Result;
            });
            Img = gameObject.GetComponent<Image>();
            EGO = gameObject.GetComponent<EGO_System>();
            gameObject.AddComponent<EGO_ButtonTooltip>();
        }

        public void Update()
        {
            if (EGO != null && Img != null)
            {
                if (EGO.egoActive)
                {
                    Img.sprite = SpriteActive;
                    interactable = true;
                }
                else if (EGO.hasEGOSkill)
                {
                    Img.sprite = SpriteOn;
                    interactable = true;
                }
                else
                {
                    Img.sprite = SpriteOff;
                    interactable = false;
                }

                if (RotationOn)
                {
                    float angle = Mathf.Sin(Time.time * rotationFrequency * Mathf.PI * 2) * rotationAmplitude;
                    transform.rotation = Quaternion.Euler(0f, 0f, initialRotationZ + angle);
                }

                if (interactable && BattleSystem.instance.ActWindow.CanAnyMove && Utils.EGOButtonHotkey && Input.GetKeyDown(KeyCode.S))
                {
                    if (EGO.egoActive)
                    {
                        EGO.SwitchToNormal();
                    }
                    else
                    {
                        EGO.SwitchToEGO();
                    }
                }
            }
        }

        public void StartRotation()
        {
            RotationOn = true;
        }

        public void ResetRotation()
        {
            RotationOn = false;
            transform.rotation = Quaternion.Euler(0f, 0f, initialRotationZ);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ResetRotation();
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
            if (EGO != null)
            {
                if (EGO.egoActive)
                { // clicked when EGO hand is active, switch back to normal hand
                    EGO.SwitchToNormal();
                }
                else
                { // clicked when EGO hand is not active, switch to EGO hand
                    EGO.SwitchToEGO();
                }
            }
            Img.rectTransform.localScale = 1.1f * Vector3.one;
            Img.color = new Color(1f, 1f, 1f, 0.8f);
        }
    }
}
