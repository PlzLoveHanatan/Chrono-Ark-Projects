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
	/// Chained Wrath
	/// </summary>
    public class B_Abnormality_TechnologicalLv2_ChainedWrath : Buff
    {
        public override void BuffStat()
        {
            PlusStat.atk = 2f;
            PlusStat.spd = -2;
        }
    }
}