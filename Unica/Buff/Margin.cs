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
    /// Margin
    /// At the end of each Scene, restore 1 Light and draw 1 page if the hand is empty
    /// </summary>
    public class Margin : Buff, IP_TurnEnd, IP_PlayerTurn_1
    {
        private bool grantManaNextTurn = false;

        public void TurnEnd()
        {
            if (BattleSystem.instance.AllyTeam.Skills.Count == 0)
            {
                BattleSystem.instance.AllyTeam.Draw();
                grantManaNextTurn = true;
            }
        }
        public void Turn1()
        {
            if (grantManaNextTurn)
            {
                BattleSystem.instance.AllyTeam.AP += 1;
                grantManaNextTurn = false;
            }
        }        
    }
}