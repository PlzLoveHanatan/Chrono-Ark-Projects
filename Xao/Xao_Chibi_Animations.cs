using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Xao
{
    public class Xao_Chibi_Animations : MonoBehaviour
    {
        // Bounce
        public float bounceHeight = 40f;
        public float bounceDuration = 0.7f;
        private bool bounceActive = false;

        // Spin
        public float spinDuration = 2f;
        public float rotationAmplitude = 5f;
        public float rotationFrequency = 1.2f;
        private bool spinActive = false;

        // Fade
        public float fadeOutDuration = 0.4f;
        private bool fadeActive = false;

        private float timer = 0f;
        private float fadeTimer = 0f;

        private Vector3 startPos;
        private float initialRotationZ;
        private Vector3 baseScale;

        private CanvasGroup canvasGroup;

        public void Awake()
        {
            startPos = transform.localPosition;
            initialRotationZ = transform.localRotation.eulerAngles.z;
            baseScale = transform.localScale;

            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void Update()
        {
            if (bounceActive)
            {
                timer += Time.deltaTime;
                float t = timer / bounceDuration;

                if (t < 1f)
                {
                    float yOffset = Mathf.Sin(t * Mathf.PI) * bounceHeight;
                    transform.localPosition = startPos + new Vector3(0, yOffset, 0);
                }
                else
                {
                    bounceActive = false;
                    transform.localPosition = startPos;
                    StartFadeInternal();
                }
            }

            if (spinActive)
            {
                timer += Time.deltaTime;

                float angle = Mathf.Sin(Time.time * rotationFrequency * Mathf.PI * 2) * rotationAmplitude;
                transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ + angle);

                if (timer >= spinDuration)
                {
                    spinActive = false;
                    transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ);
                    StartFadeInternal();
                }
            }

            if (fadeActive)
            {
                fadeTimer += Time.deltaTime;

                // float t = fadeTimer / fadeOutDuration;
                // float alpha = Mathf.Lerp(1f, 0f, t);
                // canvasGroup.alpha = alpha;

                if (fadeTimer >= fadeOutDuration)
                {
                    fadeActive = false;
                    Utils.DestroyAndCreateChibi(gameObject);
                }
            }
        }

        // === PUBLIC API ===
        public void StartBounce(float durationOverride = -1f)
        {
            if (durationOverride > 0f)
                bounceDuration = durationOverride;

            ResetVisuals();
            timer = 0f;
            bounceActive = true;
        }

        public void StartSpin(float durationOverride = -1f)
        {
            if (durationOverride > 0f)
                spinDuration = durationOverride;

            ResetVisuals();
            timer = 0f;
            spinActive = true;
        }

        public void StartFade(float durationOverride = -1f)
        {
            if (durationOverride > 0f)
                fadeOutDuration = durationOverride;

            StartFadeInternal();
        }

        // === INTERNAL ===
        private void StartFadeInternal()
        {
            fadeTimer = 0f;
            fadeActive = true;
        }

        private void ResetVisuals()
        {
            transform.localPosition = startPos;
            transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ);
            transform.localScale = baseScale;
            canvasGroup.alpha = 1f;

            // Сбросить предыдущие состояния
            bounceActive = false;
            spinActive = false;
            fadeActive = false;
        }
    }
}
