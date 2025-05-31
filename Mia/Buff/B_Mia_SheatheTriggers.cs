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
    public class B_Mia_SheatheTriggers : Buff, IP_PlayerTurn
    {
        public int MiaSheathe;
        private bool MiaMana;
        private bool MiaDraw;

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", MiaSheathe.ToString());
        }

        public void Turn()
        {
            MiaSheathe = 0;
            MiaMana = true;
            MiaDraw = true;
        }

        public override void Init()
        {
            OnePassive = true;

            if (BattleSystem.instance != null)
            {
                if (MiaSheathe >= 2 && MiaMana)
                {
                    BattleSystem.instance.AllyTeam.AP += 1;
                    MiaMana = false;
                }

                if (MiaSheathe >= 4 && MiaDraw && BChar.Info.LV >= 3)
                {
                    BattleSystem.DelayInputAfter(MiaDrawHand());
                    MiaDraw = false;
                }
            }
        }

        public IEnumerator MiaDrawHand()
        {
            BattleSystem.instance.AllyTeam.Draw();
            yield break;
        }
    }
}