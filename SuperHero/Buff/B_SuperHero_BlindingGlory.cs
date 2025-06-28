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
	/// Blinding Glory
	/// </summary>
    public class B_SuperHero_BlindingGlory : Buff
    {
        public override void BuffStat()
        {
            PlusStat.hit = -30 * StackNum;
            PlusStat.dod = -30 * StackNum;
            PlusStat.DMGTaken = 15;
        }
    }
}