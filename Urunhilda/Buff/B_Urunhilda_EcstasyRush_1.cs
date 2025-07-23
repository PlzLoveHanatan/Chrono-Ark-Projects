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
	/// Ecxtasy Rush
	/// Only Male characters can have this buff.
	/// </summary>
    public class B_Urunhilda_EcstasyRush_1 : Buff, IP_PlayerTurn, IP_BuffAddAfter
    {
        public override void BuffStat()
        {
            PlusStat.CRIGetDMG = 5 * StackNum;
            PlusStat.PlusCriDmg = -5 * StackNum;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            string debuff = ModItemKeys.Buff_B_Urunhilda_EcstasyRush_1;
            string buff = ModItemKeys.Buff_B_Urunhilda_EcstasyRush_0;

            Utils.ReverseDebuffs(BChar, BuffTaker, debuff, buff);
        }

        public void Turn()
        {
            //SelfStackDestroy();
        }
    }
}