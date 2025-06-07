using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using ChronoArkMod.ModData;
using UnityEngine.Events;
namespace projectEGO
{
    public class projectEGO_ModDefinition : ModDefinition
    {
        public override Type ModItemKeysType => typeof(ModItemKeys);
        public override List<object> BattleSystem_ModIReturn()
        {
            var list = base.BattleSystem_ModIReturn();
            list.Add(new ModIReturn());

            return list;
        }
    }
    public class ModIReturn : IP_BattleStart_Ones
    {/// <summary>
     /// Need to create a button at the battle start for UI.
     /// 0. Find inside the BattleSystem ActWindow for button place. ++
     /// 1. Create a new Game Object.
     /// 2. Load the image for the Mod sprite.
     /// 3. Create a script for a Click (add listener).
     /// 4.
     /// </summary>
     /// <param name="Ins"></param>
        public void BattleStart(BattleSystem Ins)
        {
            createIconButton(
                "EGO_Button",
                BattleSystem.instance.ActWindow.transform,
                "EGO_Active.png",
                new Vector2(160f, 160f),
                new Vector2(-324.6328f, 300.5991f),
                () => Debug.Log("EGO Button Clicked")
            );
        }

        private void createIconButton(string name, Transform trans, string spriteNormal, Vector2 size, Vector2 pos, UnityAction onClick)
        {
            Debug.Log("Creating icon button: " + name);

            GameObject egoButton = Utils.creatGameObject(name, trans);
            if (egoButton != null)
            {
                Debug.Log("EGO button created successfully.");
            }

            egoButton.transform.SetParent(trans);
            Debug.Log("Set parent transform.");

            egoButton.transform.localPosition = new Vector3(-324.6328f, 300.5991f);
            Debug.Log("Set button local position to: " + egoButton.transform.localPosition);

            Image image = egoButton.AddComponent<Image>();
            if (image != null)
            {
                Debug.Log("Image component added to EGO button.");
            }

            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite != null)
            {
                image.sprite = sprite;
                Debug.Log("Sprite loaded successfully: " + spriteNormal);
            }
            else
            {
                Debug.LogError("Failed to load sprite: " + spriteNormal);
                return;
            }

            Utils.ImageResize(image, size, pos);
            Debug.Log("Image resized and positioned.");

            Button button = egoButton.AddComponent<Button>();
            if (button != null)
            {
                Debug.Log("Button component added to EGO button.");
            }

            button.targetGraphic = image;

            EGO_System egoSystem = egoButton.AddComponent<EGO_System>();
            button.onClick.AddListener(egoSystem.Call);

            egoButton.SetActive(true);
            Debug.Log("EGO button is now active.");
        }
    }
}