using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileTypes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Xao
{
    public class Xao_Chibi_Animations : MonoBehaviour
    {
        public enum ChibiEntranceSide { Left, Right, Top }

        // === SETTINGS ===
        public float jumpHeight = 50f;

        // Время каждой анимации
        public float slideDuration = 1.0f;
        public float bounceDuration = 0.9f;
        public float spinDuration = 1.5f;
        public float squashDuration = 0.7f;

        // Bounce
        public float bounceHeight = 40f;

        // Spin
        public float rotationAmplitude = 5f;
        public float rotationFrequency = 1.2f;

        // === STATES ===
        private bool slideActive = false;
        private bool bounceActive = false;
        private bool spinActive = false;
        private bool squashActive = false;
        private bool fadeActive = false;

        // === TIMERS ===
        private float timer = 0f;
        private float fadeTimer = 0f;

        // === POS / SCALE ===
        private Vector3 slideStartPos;
        private Vector3 slideEndPos;
        private Vector3 startPos;
        private float initialRotationZ;
        private Vector3 baseScale;

        // === SPIN + JUMP ===
        private bool spinJumpActive = false;
        private float spinJumpTimer = 0f;
        private float spinJumpDuration = 1.2f;
        private float spinJumpHeight = 20f;
        private Vector3 spinJumpStartPos;
        private float spinJumpStartRotation;

        // === Fade
        public float fadeOutDuration = 0.4f;
        private CanvasGroup canvasGroup;

        public void Awake()
        {
            startPos = transform.localPosition;
            initialRotationZ = transform.localRotation.eulerAngles.z;
            baseScale = transform.localScale;

            canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
        }

        public void Update()
        {
            // === ARC JUMP / Slide ===
            if (slideActive)
            {
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / slideDuration);
                float yOffset = Mathf.Sin(t * Mathf.PI) * jumpHeight;
                transform.localPosition = Vector3.Lerp(slideStartPos, slideEndPos, t) + new Vector3(0, yOffset, 0);

                if (t >= 1f)
                {
                    slideActive = false;
                    transform.localPosition = slideEndPos;

                    int effect = UnityEngine.Random.Range(0, 2);
                    switch (effect)
                    {
                        case 0:
                            StartBounce();
                            break;
                        case 1: StartSpinJump();
                            break;
                    }
                }
            }

            // === Bounce ===
            if (bounceActive)
            {
                timer += Time.deltaTime;
                float tBounce = timer / bounceDuration;
                float yOffsetBounce = Mathf.Sin(tBounce * Mathf.PI) * bounceHeight;

                if (tBounce < 1f)
                {
                    transform.localPosition = startPos + new Vector3(0, yOffsetBounce, 0);
                }
                else
                {
                    bounceActive = false;
                    transform.localPosition = startPos;
                    StartFade();
                }
            }

            // === Spin ===
            if (spinActive)
            {
                timer += Time.deltaTime;
                float tSpin = timer / spinDuration;
                float angle = Mathf.Sin(Time.time * rotationFrequency * Mathf.PI * 2) * rotationAmplitude;
                transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ + angle);

                if (tSpin >= 1f)
                {
                    spinActive = false;
                    transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ);
                    StartFade();
                }
            }

            // === Squash & Stretch ===
            if (squashActive)
            {
                timer += Time.deltaTime;
                float tS = Mathf.Clamp01(timer / squashDuration);

                // плавный "волнообразный" squash с одной волной
                float wave = Mathf.Sin(tS * Mathf.PI); // 0 -> 1 -> 0
                float scaleY = 1 + wave * 0.3f; // растяжение по Y
                float scaleX = 1 - wave * 0.2f; // сжатие по X
                transform.localScale = new Vector3(baseScale.x * scaleX, baseScale.y * scaleY, baseScale.z);

                if (tS >= 1f)
                {
                    squashActive = false;
                    StartFade();
                }
            }

            if (spinJumpActive)
            {
                spinJumpTimer += Time.deltaTime;
                float t = Mathf.Clamp01(spinJumpTimer / spinJumpDuration);

                // прыжок
                float yOffset = Mathf.Sin(t * Mathf.PI) * spinJumpHeight;
                transform.localPosition = spinJumpStartPos + new Vector3(0, yOffset, 0);

                // вращение на 360°
                float angle = Mathf.Lerp(0f, 360f, t);
                transform.localRotation = Quaternion.Euler(0f, 0f, spinJumpStartRotation + angle);

                if (t >= 1f)
                {
                    spinJumpActive = false;
                    transform.localPosition = spinJumpStartPos;
                    transform.localRotation = Quaternion.Euler(0f, 0f, spinJumpStartRotation);

                    // удаляем чиби
                    StartFade();
                }
            }

            // === Fade (удаление без плавного исчезновения) ===
            if (fadeActive)
            {
                // Старый фейд оставил закомментированным для справки
                // fadeTimer += Time.deltaTime;
                // float t = Mathf.Clamp01(fadeTimer / fadeOutDuration);
                // canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
                //
                // if (t >= 1f)
                // {
                //     fadeActive = false;
                //     Utils.DestroyAndCreateChibi(gameObject);
                // }

                fadeActive = false;
                Utils.DestroyAndCreateChibi(gameObject);
            }
        }

        // === PUBLIC METHODS ===
        public void StartRandomEntrance()
        {
            ResetVisuals();
            timer = 0f;

            int side = UnityEngine.Random.Range(1, 3); // 0=left,1=right,2=top
            float offsetX = 50f;
            float offsetY = 50f;

            switch ((ChibiEntranceSide)side)
            {
                case ChibiEntranceSide.Left:
                    slideStartPos = startPos + new Vector3(-offsetX, 0, 0);
                    break;
                case ChibiEntranceSide.Right:
                    slideStartPos = startPos + new Vector3(offsetX, 0, 0);
                    break;
                case ChibiEntranceSide.Top:
                    slideStartPos = startPos + new Vector3(0, offsetY, 0);
                    break;
            }

            slideEndPos = startPos;
            transform.localPosition = slideStartPos;
            slideActive = true;
        }

        public void StartBounce()
        {
            ResetVisuals();
            timer = 0f;
            bounceActive = true;
        }

        public void StartSpin()
        {
            ResetVisuals();
            timer = 0f;
            spinActive = true;
        }

        public void StartSquashAndStretch()
        {
            ResetVisuals();
            timer = 0f;
            squashActive = true;
        }

        public void StartFade()
        {
            fadeTimer = 0f;
            fadeActive = true;
        }
        public void StartSpinJump(float duration = 1f, float jumpHeightOverride = 20f)
        {
            ResetVisuals(); // сброс всех других анимаций
            spinJumpActive = true;
            spinJumpTimer = 0f;
            spinJumpDuration = duration;
            spinJumpHeight = jumpHeightOverride;
            spinJumpStartPos = transform.localPosition;
            spinJumpStartRotation = transform.localRotation.eulerAngles.z;
        }

        // === INTERNAL ===
        private void ResetVisuals()
        {
            transform.localPosition = startPos;
            transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ);
            transform.localScale = baseScale;
            canvasGroup.alpha = 1f;

            bounceActive = false;
            spinActive = false;
            squashActive = false;
            slideActive = false;
            fadeActive = false;
        }
    }
}
