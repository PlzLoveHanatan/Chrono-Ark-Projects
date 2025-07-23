using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Urunhilda
{
    public class SuperHero_Script : MonoBehaviour
    {
        public Sprite HeartPinkOn;
        public const string HeartPinkPath = "Ui/HeartPink.png";

        public Sprite HeartPink2On;
        public const string HeartPink2Path = "Ui/HeartPink2.png";

        public Sprite HeartGreyOn;
        public const string HeartGreyPath = "Ui/HeartGrey.png";

        public Sprite HeartGrey2On;
        public const string HeartGrey2Path = "Ui/HeartGrey2.png";

        public Sprite LockOn;
        public const string LockPath = "Ui/Lock.png";

        public Sprite WhipOn;
        public const string WhipPath = "Ui/Whip.png";

        public Sprite WhipGreyOn;
        public const string WhipGreyPath = "Ui/WhipGrey.png";

        public Image Img;

        public float rotationAmplitude = 10f;
        public float rotationFrequency = 1.5f;
        private float initialRotationZ;
        public bool RotationOn = false;

        public void Awake()
        {
            initialRotationZ = 0f;

            
            Utils.getSpriteAsync(HeartPink2Path, delegate (AsyncOperationHandle handle)
            {
                HeartPink2On = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(HeartPinkPath, delegate (AsyncOperationHandle handle)
            {
                HeartPinkOn = (Sprite)handle.Result;
            });

            Utils.getSpriteAsync(HeartGreyPath, delegate (AsyncOperationHandle handle)
            {
                HeartGreyOn = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(HeartGrey2Path, delegate (AsyncOperationHandle handle)
            {
                HeartGrey2On = (Sprite)handle.Result;
            });

            Utils.getSpriteAsync(LockPath, delegate (AsyncOperationHandle handle)
            {
                LockOn = (Sprite)handle.Result;
            });

            Utils.getSpriteAsync(WhipPath, delegate (AsyncOperationHandle handle)
            {
                WhipOn = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(WhipGreyPath, delegate (AsyncOperationHandle handle)
            {
                WhipGreyOn = (Sprite)handle.Result;
            });
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
        }

        public void StartRotation()
        {
            RotationOn = true;
        }

        public void StopRotation()
        {
            RotationOn = false;
            transform.localRotation = Quaternion.Euler(0f, 0f, initialRotationZ);
        }
    }
}