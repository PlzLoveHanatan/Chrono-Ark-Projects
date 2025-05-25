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
namespace Aqua
{
    public class Aqua_ModDefinition : ModDefinition
    {
        public override Type ModItemKeysType => typeof(ModItemKeys);
        public override List<object> BattleSystem_ModIReturn()
        {
            var list = base.BattleSystem_ModIReturn();

            list.Add(new ModIReturn());

            return list;
        }
    }
    public class ModIReturn : IP_BattleStart_UIOnBefore
    {
        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            if (Utils.MoreAquaVoice)
            {
                createIconButton("Aqua_Chibi",BattleSystem.instance.ActWindow.transform,"AquaChibi.png",
                    new Vector2(160f, 160f),
                    new Vector2(-441.9141f, -297.3823f)
                );
            }
        }

        private void createIconButton(string name, Transform trans, string spriteNormal, Vector2 size, Vector2 pos)
        {
            GameObject aquaChibiButtton = Utils.creatGameObject(name, trans);

            if (aquaChibiButtton == null) return;
            
            aquaChibiButtton.transform.SetParent(trans);
            aquaChibiButtton.transform.localPosition = pos;

            Image image = aquaChibiButtton.AddComponent<Image>();
            Sprite sprite = Utils.getSprite(spriteNormal);

            if (sprite == null) return;

            image.sprite = sprite;
            Utils.ImageResize(image, size, pos);

            Aqua_ChibiButton aquaChibi = aquaChibiButtton.AddComponent<Aqua_ChibiButton>();
            aquaChibiButtton.AddComponent<Aqua_ChibiButton_Script>();

            Aqua_ChibiButton.instance = aquaChibi;

            aquaChibiButtton.SetActive(true);
        }
    }
}