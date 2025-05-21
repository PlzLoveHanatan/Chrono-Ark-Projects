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
namespace Akari
{
    public class B_LeRegole_NextTurn : Buff, IP_PlayerTurn
    {
        public void Turn()
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;

            foreach (var ally in allies)
            {
                ally.BuffAdd(ModItemKeys.Buff_B_LeRegole, BChar, false, 0, false, -1, false);
            }

            SelfDestroy();
        }
    }
}