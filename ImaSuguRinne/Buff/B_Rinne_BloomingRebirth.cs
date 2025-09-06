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
	/// Blooming Rebirth
	/// </summary>
    public class B_Rinne_BloomingRebirth : Buff
    {
        public override void BuffStat()
        {
            PlusPerStat.MaxHP = 4 * StackNum;
            PlusStat.def = 4 * StackNum;
            PlusStat.DeadImmune = 4 * StackNum;
        }
    }
}