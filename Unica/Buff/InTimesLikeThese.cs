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
namespace Unica
{
    /// <summary>
    /// In Times Like These!
    /// If 3 or fewer pages are in-hand at the end of the Scene, gain 1 Strength next Scene
    /// </summary>
    public class InTimesLikeThese : Buff, IP_TurnEnd, IP_PlayerTurn_1
    {
        private bool grantAttackPowerNextTurn = false;
        public void TurnEnd()
        {
            if (BattleSystem.instance.AllyTeam.Skills.Count <= 3)
            {
                grantAttackPowerNextTurn = true;
            }
        }
        public void Turn1()
        {
            if (grantAttackPowerNextTurn)
            {
                this.BChar.BuffAdd(ModItemKeys.Buff_InTimesLikeThese_0, this.BChar, false, 0, false, 1, false);
                grantAttackPowerNextTurn = false;
            }
        }        
    }
}