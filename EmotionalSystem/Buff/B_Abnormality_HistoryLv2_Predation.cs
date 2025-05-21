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
	/// Predation
	/// </summary>
    public class B_Abnormality_HistoryLv2_Predation : Buff
    {
        public override void BuffStat()
        {
            PlusStat.atk = 3;
            PlusStat.reg = 3;
            PlusStat.cri = 25;
        }
    }
}