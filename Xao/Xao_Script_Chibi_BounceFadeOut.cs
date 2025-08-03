using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using static NLog.LayoutRenderers.Wrappers.ReplaceLayoutRendererWrapper;

namespace Xao
{
    public class Xao_Script_Chibi_BounceFadeOut : MonoBehaviour
    {
        public float height = 40f;
        public float duration = 0.7f;
        public float fadeDuration = 0.5f;

        private float timer = 0f;
        private Vector3 startPos;
        private bool BounceOn = false;
        private bool FadeOn = false;

        private CanvasGroup canvasGroup; // Если ты используешь UI
                                         // Или SpriteRenderer spriteRenderer; // Если спрайт

        public void Awake()
        {
            startPos = transform.localPosition;
            canvasGroup = GetComponent<CanvasGroup>();
            // spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            if (BounceOn)
            {
                timer += Time.deltaTime;
                float t = timer / duration;

                if (t < 1f)
                {
                    float yOffset = Mathf.Sin(t * Mathf.PI) * height;
                    transform.localPosition = startPos + new Vector3(0, yOffset, 0);
                }
                else
                {
                    BounceOn = false;
                    timer = 0f; // сбрасываем таймер для фейда
                    transform.localPosition = startPos;
                    StartFade();
                }
            }

            if (FadeOn)
            {
                timer += Time.deltaTime;
                float t = timer / fadeDuration;
                float alpha = Mathf.Lerp(1f, 0f, t);

                if (canvasGroup != null)
                {
                    canvasGroup.alpha = alpha;
                }
                // else if (spriteRenderer != null)
                // {
                //     Color c = spriteRenderer.color;
                //     c.a = alpha;
                //     spriteRenderer.color = c;
                // }

                if (t >= 1f)
                {
                    FadeOn = false;
                    Utils.DestroyAndCreateChibi(gameObject);
                }
            }
        }

        // Вызывается для старта эффекта — сразу прыжок + пульс
        public void StartBounce(float durationTime = 0.5f)
        {
            duration = durationTime;
            timer = 0f;
            BounceOn = true;
        }

        public void StartFade(float duration = 0.4f)
        {
            fadeDuration = duration;
            FadeOn = true;
            timer = 0f;
        }
    }
}