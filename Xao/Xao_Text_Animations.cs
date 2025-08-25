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

    public class Xao_Text_Animations : MonoBehaviour
    {
        public Image Img;
        private SpriteRenderer spriteRenderer;

        public float scaleDuration = 1f;
        public float scaleMultiplier = 1.2f;

        private float scaleTimer = 0f;

        private Vector3 initialScale;
        private Vector3 initialPos;
        public bool FadeOutOn = false;
        public float fadeOutSpeed = 1f;

        // флаги для случайного эффекта
        private bool useTilt = false;
        private bool useShake = false;
        private bool usePulse = false;

        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Img = GetComponent<Image>();
            }

            SetAlpha(1f); // появляется сразу

            initialScale = transform.localScale;
            initialPos = transform.localPosition;
        }

        public void Update()
        {
            // SCALE-UP + эффекты
            if (scaleTimer < scaleDuration)
            {
                scaleTimer += Time.deltaTime;
                float t = Mathf.Clamp01(scaleTimer / scaleDuration);

                // базовый scale
                Vector3 baseScale = initialScale * Mathf.Lerp(1f, scaleMultiplier, t);

                // применяем выбранный эффект к transform
                Vector3 newScale = baseScale;
                Vector3 newPos = initialPos;
                float newAngle = 0f;

                if (usePulse)
                    newScale *= 1f + Mathf.Sin(Time.time * 6f) * 0.05f;

                if (useShake)
                    newPos += new Vector3(Mathf.Sin(Time.time * 25f) * 3f, 0f, 0f);

                if (useTilt)
                    newAngle = Mathf.Sin(Time.time * 5f) * 5f;

                transform.localScale = newScale;
                transform.localPosition = newPos;
                transform.rotation = Quaternion.Euler(0f, 0f, newAngle);

                SetAlpha(1f);
            }
            else if (!FadeOutOn)
            {
                StartFadeOut();
            }

            // FADE-OUT
            if (FadeOutOn)
            {
                float a = GetAlpha();
                a -= Time.deltaTime * fadeOutSpeed;
                SetAlpha(a);

                // эффекты продолжают работать при fade-out
                Vector3 baseScale = transform.localScale; // используем текущий scale как базу
                Vector3 newScale = baseScale;
                Vector3 newPos = initialPos;
                float newAngle = 0f;

                if (usePulse)
                    newScale *= 1f + Mathf.Sin(Time.time * 6f) * 0.05f;

                if (useShake)
                    newPos += new Vector3(Mathf.Sin(Time.time * 25f) * 3f, 0f, 0f);

                if (useTilt)
                    newAngle = Mathf.Sin(Time.time * 5f) * 5f;

                transform.localScale = newScale;
                transform.localPosition = newPos;
                transform.rotation = Quaternion.Euler(0f, 0f, newAngle);

                // Уничтожение объекта
                if (a <= 0f)
                {
                    Utils.DestroyObjects(gameObject);
                    FadeOutOn = false;
                }
            }
        }

        private void SetAlpha(float a)
        {
            if (spriteRenderer != null)
            {
                var color = spriteRenderer.color;
                spriteRenderer.color = new Color(color.r, color.g, color.b, a);
            }
            else if (Img != null)
            {
                var color = Img.color;
                Img.color = new Color(color.r, color.g, color.b, a);
            }
        }

        private float GetAlpha()
        {
            if (spriteRenderer != null)
            {
                return spriteRenderer.color.a;
            }
            else if (Img != null)
            {
                return Img.color.a;
            }
            return 1f;
        }

        public void StartScaleUp()
        {
            scaleTimer = 0f;
            FadeOutOn = false;
            SetAlpha(1f);
            transform.localScale = initialScale;
            transform.localPosition = initialPos;
            transform.rotation = Quaternion.identity;

            // случайный выбор одного эффекта
            int r = UnityEngine.Random.Range(0, 4); // 0 = none, 1 = tilt, 2 = shake, 3 = pulse
            useTilt = (r == 1);
            useShake = (r == 2);
            usePulse = (r == 3);
        }

        public void StartFadeOut()
        {
            FadeOutOn = true;
        }
    }
}