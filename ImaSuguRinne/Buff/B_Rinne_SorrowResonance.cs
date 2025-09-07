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
namespace ImaSuguRinne
{
	/// <summary>
	/// Sorrow Resonance
	/// </summary>
    public class B_Rinne_SorrowResonance : Buff
    {
        public override void BuffStat()
        {
            //PlusStat.def = -5 * StackNum;
            PlusStat.hit = -5 * StackNum;
            PlusStat.dod = -5 * StackNum;
        }
    }
}