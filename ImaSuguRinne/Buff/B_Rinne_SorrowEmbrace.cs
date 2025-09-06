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
	/// Sorrow Embrace
	/// </summary>
    public class B_Rinne_SorrowEmbrace : Buff
    {
        public override void BuffStat()
        {
            PlusStat.dod = -5 * StackNum;
            PlusPerStat.Damage = -5 * StackNum;
        }
    }
}