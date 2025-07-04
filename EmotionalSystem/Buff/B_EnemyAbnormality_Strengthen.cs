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
	/// Strengthen
	/// </summary>
    public class B_EnemyAbnormality_Strengthen : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            PlusStat.RES_DEBUFF = 15f;
            PlusStat.RES_DOT = 15f;
            PlusStat.RES_CC = 15f;
            PlusStat.def = 15;
        }
    }
}