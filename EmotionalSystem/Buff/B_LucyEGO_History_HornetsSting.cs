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
	/// Hornets Sting
	/// </summary>
    public class B_LucyEGO_History_HornetsSting : Buff
    {
        public override void BuffStat()
        {
            this.PlusStat.def = -40;
        }
    }
}