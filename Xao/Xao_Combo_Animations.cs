using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Xao
{
    public class Xao_Combo_Animations : MonoBehaviour
    {
        public float popInDuration = 0.2f;
        public float popScale = 1.2f;

        private float popInTime = 0f;
        private Vector3 initialScale;
        private CanvasGroup cg;

        private bool isPlayingPopIn = false;

        public void Awake()
        {
            initialScale = transform.localScale;
            cg = GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void PlayPopIn()
        {
            isPlayingPopIn = true;
            popInTime = 0f;
            transform.localScale = Vector3.zero;
            cg.alpha = 0f;
        }

        public void Update()
        {
            if (!isPlayingPopIn) return;

            popInTime += Time.deltaTime;
            float t = Mathf.Clamp01(popInTime / popInDuration);

            transform.localScale = Vector3.Lerp(Vector3.zero, initialScale * popScale, t);
            cg.alpha = Mathf.Lerp(0f, 1f, t);

            if (t >= 1f)
            {
                transform.localScale = initialScale;
                isPlayingPopIn = false;
            }
        }
    }
}
