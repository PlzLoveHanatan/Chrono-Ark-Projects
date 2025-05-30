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
namespace Raphi
{
    /// <summary>
    /// Passive:
    /// </summary>
    public class P_Raphi : Passive_Char, IP_LevelUp, IP_PlayerTurn, IP_BattleStart_UIOnBefore
    {
        public bool ItemTake;

        public override void Init()
        {
            base.Init();
            OnePassive = true;
        }

        public void Turn()
        {
            BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection, BChar, false, 0, false, -1, false);
        }

        public void LevelUp()
        {
            FieldSystem.DelayInput(Delay());
        }

        public IEnumerator Delay()
        {
            if (!ItemTake)
            {
                ItemTake = true;
                InventoryManager.Reward(ItemBase.GetItem(ModItemKeys.Item_Consume_Raphi_Consume, 3));
            }
            yield return null;
            yield break;
        }
        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            if (Utils.RaphiButton && BChar is BattleAlly raphi)
            {
                Vector3 basePos = raphi.GetTopPos();

                Vector3 offset = new Vector3(1.25f, 0.85f, 0f);
                Vector3 finalPos = basePos + offset;

                createIconButton("Raphi_Button", raphi.transform, "RaphiButton.png", new Vector3(128f, 128f), finalPos);
            }
        }

        private void createIconButton(string name, Transform parent, string spriteNormal, Vector3 size, Vector3 worldPos)
        {
            GameObject raphiButton = Utils.creatGameObject(name, parent);
            if (raphiButton == null) return;

            raphiButton.transform.SetParent(parent);

            raphiButton.transform.position = worldPos;

            Image image = raphiButton.AddComponent<Image>();
            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite == null) return;
            image.sprite = sprite;

            Utils.ImageResize(image, size);

            Raphi_Button button = raphiButton.AddComponent<Raphi_Button>();
            raphiButton.AddComponent<Raphi_Button_Script>();

            Raphi_Button.instance = button;

            raphiButton.SetActive(true);
        }
    }
}
