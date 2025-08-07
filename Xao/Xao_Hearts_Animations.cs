using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Xao
{
    public class Xao_Hearts_Animations : MonoBehaviour
    {
        public bool PlayPopIn = false;
        public bool PlayFadeOut = false;
        public bool PlayGreyIn = false;

        private float popInTime = 0f;
        private float fadeOutAlpha = 1f;

        public float popInDuration = 0.2f;
        public float greyInDuration = 0.3f;
        public float fadeOutSpeed = 2f;

        public float popScale = 1.2f;
        public float greyInStartScale = 0.8f;

        private Vector3 initialScale;
        private CanvasGroup cg;

        public void Awake()
        {
            initialScale = transform.localScale;
            cg = GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void OnEnable()
        {
            if (PlayPopIn)
            {
                popInTime = 0f;
                transform.localScale = Vector3.zero;
                cg.alpha = 0f;
            }
            if (PlayFadeOut)
            {
                fadeOutAlpha = 1f;
                cg.alpha = 1f;
            }
        }

        public void Update()
        {
            if (PlayPopIn)
            {
                popInTime += Time.deltaTime;
                float t = Mathf.Clamp01(popInTime / popInDuration);

                transform.localScale = Vector3.Lerp(Vector3.zero, initialScale * popScale, t);
                cg.alpha = Mathf.Lerp(0f, 1f, t);

                if (t >= 1f)
                {
                    transform.localScale = initialScale;
                    PlayPopIn = false;
                }
            }

            if (PlayFadeOut)
            {
                fadeOutAlpha -= Time.deltaTime * fadeOutSpeed;
                cg.alpha = fadeOutAlpha;

                if (fadeOutAlpha <= 0f)
                {
                    PlayFadeOut = false;
                    Utils.DestroyObjects(gameObject);
                }
            }

            if (PlayGreyIn)
            {
                popInTime += Time.deltaTime;
                float t = Mathf.Clamp01(popInTime / greyInDuration);

                transform.localScale = Vector3.Lerp(Vector3.one * greyInStartScale, initialScale, t);
                cg.alpha = Mathf.Lerp(0f, 1f, t);

                if (t >= 1f)
                {
                    transform.localScale = initialScale;
                    PlayGreyIn = false;
                }
            }
        }
    }
}

