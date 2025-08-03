using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Xao
{
    public class Xao_Script_Chibi_SpinFadeOut : MonoBehaviour
    {
        public float rotationAmplitude = 5f;
        public float rotationFrequency = 1.2f;
        public float fadeOutDuration = 0.4f;
        public float spinDuration = 2f;

        private float timer = 0f;
        private float fadeTimer = 0f;
        private bool spinning = false;
        private bool fadingOut = false;

        private float initialRotationZ;
        private CanvasGroup canvasGroup;
        private Vector3 baseScale;

        public void Awake()
        {
            initialRotationZ = transform.localRotation.eulerAngles.z;
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            baseScale = transform.localScale;
        }

        public void Update()
        {
            if (spinning)
            {
                timer += Time.deltaTime;
                float angle = Mathf.Sin(Time.time * rotationFrequency * Mathf.PI * 2) * rotationAmplitude;
                transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ + angle);

                if (timer >= spinDuration)
                {
                    spinning = false;
                    fadingOut = true;
                    fadeTimer = 0f;
                }
            }

            if (fadingOut)
            {
                fadeTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeOutDuration);
                canvasGroup.alpha = alpha;

                if (fadeTimer >= fadeOutDuration)
                {
                    fadingOut = false;
                }
                Utils.DestroyAndCreateChibi(gameObject);
            }

            if (!spinning && !fadingOut)
            {
                // Вернуть в исходное состояние
                transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ);
                canvasGroup.alpha = 1f;
                transform.localScale = baseScale;
            }
        }

        public void StartSpinAndFadeOut()
        {
            spinning = true;
            fadingOut = false;
            timer = 0f;
            fadeTimer = 0f;
        }
    }
}
