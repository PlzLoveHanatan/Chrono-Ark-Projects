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
using EmotionalSystem;
namespace EmotionalSystem
{
    public class EmotionalSystem_ModDefinition : ModDefinition
    {
        public override Type ModItemKeysType => typeof(ModItemKeys);
        public override List<object> BattleSystem_ModIReturn()
        {
            var list = base.BattleSystem_ModIReturn();

            list.Add(new ModIReturn());

            if (Utils.EnemyEmotions)
            {
                list.Add(B_EnemyTeamEmotionalLevel.Instance);
            }

            return list;
        }

        public class ModIReturn : IP_BattleStart_UIOnBefore, IP_BattleStart_Ones, IP_EnemyAwake
        {
            public void BattleStart(BattleSystem Ins)
            {
                if (Utils.EmotionalSystemTutorial)
                {
                    Utils.EmotionalSystemTutorial = false;
                    InitTutorial("Assets/ModAssets/EmotionalSystemTutorial.asset");
                }
                if (Utils.AllyEmotions)
                {
                    BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_LucyEmotionalLevel, BattleSystem.instance.AllyTeam.LucyAlly, false, 0, false, -1, false);
                    foreach (BattleChar battleChar in Ins.AllyTeam.AliveChars)
                    {
                        battleChar.BuffAdd(ModItemKeys.Buff_B_EmotionalLevel, battleChar, false, 0, false, -1, false);
                    }
                }
            }

            public static void InitTutorial(string tutorialPath)
            {
                var tutorial = Utils.GetAssets<Tutorial>(tutorialPath, "tutorial");
                if (tutorial == null)
                {
                    Debug.Log("Tutorial not found");
                    return;
                }

                var obj = UIManager.InstantiateActiveAddressable(UIManager.inst.AR_TutorialUI, AddressableLoadManager.ManageType.Stage).GetComponent<TutorialObject>();
                obj.transform.Find("Window").localPosition += new Vector3(0, 100, 0);
                obj.transform.Find("Window/VideoImage").localPosition += new Vector3(0, 60, 0);
                obj.transform.Find("Window/TextPos").localPosition += new Vector3(0, 120, 0);
                obj.gameObject.AddComponent<TutorialLocalizer>().Init(obj);
                obj.Init(tutorial);


                //var obj = UIManager.InstantiateActiveAddressable(
                //    UIManager.inst.AR_TutorialUI,
                //    AddressableLoadManager.ManageType.Stage
                //).GetComponent<TutorialObject>();

                //obj.transform.Find("Window").localPosition += new Vector3(0, 100, 0);

                //var windowRect = obj.transform.Find("Window").GetComponent<RectTransform>();
                //var videoImageRect = obj.transform.Find("Window/VideoImage").GetComponent<RectTransform>();
                //Utils.FitRectTransformToTarget(videoImageRect, windowRect, new Vector3());
                //obj.gameObject.AddComponent<TutorialLocalizer>().Init(obj);
                //obj.Init(tutorial);
            }

            public void BattleStartUIOnBefore(BattleSystem Ins)
            {
                Utils.EmotionsCheck();

                if (Utils.AllyEmotions)
                {
                    CreateIconButton(
                    "EGO_Button",
                    BattleSystem.instance.ActWindow.transform,
                    "EGO_Active.png",
                    new Vector2(160f, 160f),
                    new Vector2(-324.6328f, 300.5991f));
                }
            }

            public void EnemyAwake(BattleChar Enemy)
            {
                if (Utils.EnemyEmotions)
                {
                    Enemy.BuffAdd(ModItemKeys.Buff_B_EnemyEmotionalLevel, Enemy);
                }
            }

            private void CreateIconButton(string name, Transform trans, string spriteNormal, Vector2 size, Vector2 pos)
            {
                GameObject egoButton = Utils.CreatGameObject(name, trans);
                if (egoButton == null)
                {
                    return;
                }

                egoButton.transform.SetParent(trans);
                egoButton.transform.localPosition = pos;

                Image image = egoButton.AddComponent<Image>();
                Sprite sprite = Utils.GetSprite(spriteNormal);
                if (sprite == null)
                {
                    return;
                }

                image.sprite = sprite;
                Utils.ImageResize(image, size, pos);

                EmotionalSystem_EGO_Button egoSystem = egoButton.AddComponent<EmotionalSystem_EGO_Button>();
                egoButton.AddComponent<EmotionalSystem_EGO_Button_Script>();

                EmotionalSystem_EGO_Button.instance = egoSystem;

                egoButton.SetActive(true);
            }
        }
    }
}
