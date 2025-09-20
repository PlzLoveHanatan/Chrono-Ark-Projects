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
namespace SuperHero
{
    /// <summary>
    /// Second Act
    /// </summary>
    public class B_SuperHero_SecondAct : Buff, IP_PlayerTurn, IP_Awake, IP_BuffAddAfter
    {
        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
            {
                superHero.SecondAct = true;
                superHero.BecomeJusticeHero();
            } 
        }

        public void Turn()
        {
            Utils.AllyTeam.Draw(2);
            Utils.AllyTeam.AP += 2;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff.BuffData.Key == ModItemKeys.Buff_B_SuperHero_SecondAct && BuffTaker != Utils.SuperHero)
            {
                BuffTaker.BuffRemove(ModItemKeys.Buff_B_SuperHero_SecondAct);
            }
        }
    }
}