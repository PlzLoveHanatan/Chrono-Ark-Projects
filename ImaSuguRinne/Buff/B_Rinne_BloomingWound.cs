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
	/// Blooming Wound
	/// </summary>
    public class B_Rinne_BloomingWound : Buff
    {
        public override void BuffStat()
        {
            PlusStat.dod = -4 * StackNum;
        }
    }
}