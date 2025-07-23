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
namespace Urunhilda
{
    /// <summary>
    /// Milked Dry
    /// </summary>
    public class B_Urunhilda_MilkedDry_1 : Buff, IP_BuffAddAfter, IP_PlayerTurn
    {
        public override void BuffStat()
        {
            PlusStat.atk = -1 * StackNum;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            string debuff = ModItemKeys.Buff_B_Urunhilda_MilkedDry_1;
            string buff = ModItemKeys.Buff_B_Urunhilda_MilkedDry_0;

            Utils.ReverseDebuffs(BChar, BuffTaker, debuff, buff);
        }

        public void Turn()
        {
            //SelfStackDestroy();
        }
    }
}