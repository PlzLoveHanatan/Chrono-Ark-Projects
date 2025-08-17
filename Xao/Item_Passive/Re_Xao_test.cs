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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace Xao
{
    public class Re_Xao_test : PassiveItemBase, IP_DrawNumChange, IP_TurnEnd
    {
        public void DrawNumChange(int DrawNum, out int OutNum)
        {
            DrawNum = DrawNum >= 5 ? 5 : 5;

            //if (PlayData.TSavedata.Passive_Itembase.Exists(item => item != null && item.itemkey == GDEItemKeys.Item_Passive_PokerDeck_CasinoDLC))
            //{
            //    DrawNum -= 2;
            //}

            if (BattleSystem.instance.AllyTeam.LucyAlly.BuffReturn(GDEItemKeys.Buff_B_LucyD_1_T, false) != null)
            {
                DrawNum -= 1;
            }

            OutNum = DrawNum;
        }

        public void TurnEnd()
        {
            var team = BattleSystem.instance.AllyTeam;

            for (int i = team.Skills.Count - 1; i >= 0; i--)
            {
                Skill skill = team.Skills[i];
                skill.Remove();
            }
        }
    }
}