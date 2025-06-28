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
    public class B_SuperHero_SecondAct : Buff, IP_PlayerTurn
    {
        public void Turn()
        {
            var team = BattleSystem.instance.AllyTeam;
            team.Draw();
            team.AP += 1;
        }
    }
}