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
        public bool FadeOutOn = false;
        public float fadeOutSpeed = 1f;

        public void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Img = GetComponent<Image>();
            }

            SetAlpha(1f); // Появляется сразу

            initialScale = transform.localScale;
        }

        public void Update()
        {
            if (scaleTimer < scaleDuration)
            {
                scaleTimer += Time.deltaTime;
                float t = Mathf.Clamp01(scaleTimer / scaleDuration);

                float scaleFactor = Mathf.Lerp(1f, scaleMultiplier, t);
                transform.localScale = initialScale * scaleFactor;

                SetAlpha(1f);
            }
            else if (!FadeOutOn)
            {
                StartFadeOut();
            }

            if (FadeOutOn)
            {
                float a = GetAlpha();
                a -= Time.deltaTime * fadeOutSpeed;
                SetAlpha(a);

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
        }

        public void StartFadeOut()
        {
            FadeOutOn = true;
        }
    }
}