using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Xao
{
    public class Xao_Script : MonoBehaviour
    {
        public Image Img;
        private SpriteRenderer spriteRenderer;

        public float rotationAmplitude = 10f;
        public float rotationFrequency = 1.5f;
        private float initialRotationZ;
        public bool RotationOn = false;

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
            initialRotationZ = transform.localRotation.eulerAngles.z;
        }

        public void Update()
        {
            if (RotationOn)
            {
                float angle = Mathf.Sin(Time.time * rotationFrequency * Mathf.PI * 2) * rotationAmplitude;
                transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ + angle);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ);
            }

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
                    Debug.Log($"[{name}] Fade out complete, destroying");
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
            Debug.Log($"[{name}] Scale up started");
        }

        public void StartRotation()
        {
            RotationOn = true;
            Debug.Log($"[{name}] Rotation started");
        }

        public void StopRotation()
        {
            RotationOn = false;
            transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ);
            Debug.Log($"[{name}] Rotation stopped");
        }

        public void StartFadeOut()
        {
            FadeOutOn = true;
            Debug.Log($"[{name}] Fade out started");
        }
    }
}

