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

            if (B_EnemyEmotionalLevel.EnemyEmotionOn)
            {
                list.Add(B_EnemyTeamEmotionalLevel.Instance);
            }

            return list;
        }
    }

    public class ModIReturn : IP_BattleStart_UIOnBefore, IP_BattleStart_Ones, IP_EnemyAwake
    {
        public void BattleStart(BattleSystem Ins)
        {
            BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_LucyEmotionalLevel, BattleSystem.instance.AllyTeam.LucyAlly, false, 0, false, -1, false);
            foreach (BattleChar battleChar in Ins.AllyTeam.AliveChars)
            {
                battleChar.BuffAdd(ModItemKeys.Buff_B_EmotionalLevel, battleChar, false, 0, false, -1, false);
            }
        }

        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            createIconButton(
                "EGO_Button",
                BattleSystem.instance.ActWindow.transform,
                "EGO_Active.png",
                new Vector2(160f, 160f),
                new Vector2(-324.6328f, 300.5991f)
            );
        }

        public void EnemyAwake(BattleChar Enemy)
        {
            if (B_EnemyEmotionalLevel.EnemyEmotionOn)
            {
                Enemy.BuffAdd(ModItemKeys.Buff_B_EnemyEmotionalLevel, Enemy);
            }
        }

        private void createIconButton(string name, Transform trans, string spriteNormal, Vector2 size, Vector2 pos)
        {
            GameObject egoButton = Utils.creatGameObject(name, trans);
            if (egoButton == null)
            {
                return;
            }

            egoButton.transform.SetParent(trans);
            egoButton.transform.localPosition = pos;

            Image image = egoButton.AddComponent<Image>();
            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite == null)
            {
                return;
            }

            image.sprite = sprite;
            Utils.ImageResize(image, size, pos);

            EGO_System egoSystem = egoButton.AddComponent<EGO_System>();
            egoButton.AddComponent<EGO_ButtonScript>();

            EGO_System.instance = egoSystem;

            egoButton.SetActive(true);
        }
    }
}
