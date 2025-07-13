using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace SuperHero
{
    public class SuperHero_Script : MonoBehaviour
    {
        public Sprite HeroMascotOn;
        public const string HeroMascotOnPath = "Ui/HeroMascot.png";

        public Sprite StarOn1;
        public const string StarOnPath1 = "Ui/Star1.png";

        public Sprite StarOn2;
        public const string StarOnPath2 = "Ui/Star2.png";

        public Sprite StarOn3;
        public const string StarOnPath3 = "Ui/Star3.png";

        public Sprite VillainMascotOn;
        public const string VillainMascotOnPath = "Ui/VillainMascot.png";

        public Sprite StarOnVillian1;
        public const string StarOnVillianPath1 = "Ui/StarVillian1.png";

        public Sprite StarOnVillian2;
        public const string StarOnVillianPath2 = "Ui/StarVillian2.png";

        public Sprite StarOnVillian3;
        public const string StarOnVillianPath3 = "Ui/StarVillian3.png";

        public Image Img;

        public float rotationAmplitude = 10f;
        public float rotationFrequency = 1.5f;
        private float initialRotationZ;
        public bool RotationOn = false;

        public void Awake()
        {
            initialRotationZ = 0f;

            Utils.getSpriteAsync(HeroMascotOnPath, delegate (AsyncOperationHandle handle)
            {
                HeroMascotOn = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(StarOnPath1, delegate (AsyncOperationHandle handle)
            {
                StarOn1 = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(StarOnPath2, delegate (AsyncOperationHandle handle)
            {
                StarOn2 = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(StarOnPath3, delegate (AsyncOperationHandle handle)
            {
                StarOn3 = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(VillainMascotOnPath, delegate (AsyncOperationHandle handle)
            {
                VillainMascotOn = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(StarOnVillianPath1, delegate (AsyncOperationHandle handle)
            {
                StarOnVillian1 = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(StarOnVillianPath2, delegate (AsyncOperationHandle handle)
            {
                StarOnVillian2 = (Sprite)handle.Result;
            });
            Utils.getSpriteAsync(StarOnVillianPath3, delegate (AsyncOperationHandle handle)
            {
                StarOnVillian3 = (Sprite)handle.Result;
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
