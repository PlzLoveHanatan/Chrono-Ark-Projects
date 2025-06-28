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
    /// Relentless Recovery
    /// </summary>
    public class B_SuperHero_RelentlessRecovery : Buff, IP_PlayerTurn
    {
        public void Turn()
        {
            var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Random(BChar.GetRandomClass().Main, 2);
            foreach (var debuff in debuffs)
            {
                debuff.SelfDestroy();
            }
        }
    }
}