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
namespace Mia
{
    /// <summary>
    /// Passive:
    /// </summary>
    public class P_Mia : Passive_Char, IP_Discard, IP_BattleStart_Ones, IP_BattleStart_UIOnBefore, IP_PlayerTurn, IP_LevelUp
    {
        public bool MiaItemTake;

        public override void Init()
        {
            OnePassive = true;
        }
        public void LevelUp()
        {
            FieldSystem.DelayInput(Delay());
        }

        public IEnumerator Delay()
        {
            if (!MiaItemTake)
            {
                MiaItemTake = true;
                InventoryManager.Reward(ItemBase.GetItem(ModItemKeys.Item_Consume_Mia_InstinctTonic, 3));
            }
            yield return null;
            yield break;
        }

        public void BattleStart(BattleSystem Ins)
        {
            if (BChar.Info.LV >= 2)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_Mia_SheatheTriggers, BChar, false, 0, false, -1, false);
            }
        }

        public void Turn()
        {
            BChar.BuffAdd(ModItemKeys.Buff_B_Mia_SavageImpulse, BChar, false, 0, false, -1, false);

            if (BChar.Info.LV >= 5)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_Mia_BurstofFlavor_0, BChar, false, 0, false, -1, false);
            }
        }

        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            var sheathe = BChar.BuffReturn(ModItemKeys.Buff_B_Mia_SheatheTriggers) as B_Mia_SheatheTriggers;

            if (!Click && !HandFullWaste)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_Mia_PredatoryDrive, BChar, false, 0, false, -1, false);

                if (BChar.Info.LV >= 2 && sheathe?.StackNum >= 1)
                {
                    sheathe.MiaSheathe++;
                }

                if (BChar.Info.LV >= 4)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_InstinctivePrecision, BChar, false, 0, false, -1, false);
                }
            }
        }

        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            if (BChar is BattleAlly mia)
            {
                Vector3 basePos = mia.GetTopPos();

                Vector3 offset = new Vector3(1.25f, 0.85f, 0f);
                Vector3 finalPos = basePos + offset;

                createIconButton("Mia_Button", mia.transform, "MiaButton.png", new Vector3(139f, 144f), finalPos);
            }
        }

        private void createIconButton(string name, Transform parent, string spriteNormal, Vector3 size, Vector3 worldPos)
        {
            GameObject miaButton = Utils.creatGameObject(name, parent);
            if (miaButton == null)
                return;

            miaButton.transform.SetParent(parent);
            miaButton.transform.position = worldPos;

            Image image = miaButton.AddComponent<Image>();
            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite == null)
                return;

            image.sprite = sprite;
            Utils.ImageResize(image, size);

            Mia_Button button = miaButton.AddComponent<Mia_Button>();
            miaButton.AddComponent<Mia_Button_Script>();

            Mia_Button.instance = button;

            miaButton.SetActive(true);
        }
    }
}