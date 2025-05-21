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
namespace EmotionalSystem
{
	/// <summary>
	/// Entangled
	/// Removed at the start of the next turn.
	/// Can be targeted regardless of Taunt status. 
	/// </summary>
    public class B_Abnormality_HistoryLv2_Vines_0 : Buff, IP_PlayerTurn
    {
        public override void BuffStat()
        {
            base.BuffStat();
            PlusStat.dod = -15f;
            PlusStat.crihit = 15;
            PlusStat.CRIGetDMG = 15;
        }
        public void Turn()
        {
            SelfDestroy();
        }
    }
}