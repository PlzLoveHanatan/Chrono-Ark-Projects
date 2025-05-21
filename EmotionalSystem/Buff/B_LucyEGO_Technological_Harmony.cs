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
    public class B_LucyEGO_Technological_Harmony : Buff
    {
        public override void BuffStat()
        {
            PlusStat.def = -25f;
            PlusStat.crihit = 25;
            PlusStat.CRIGetDMG = 25;
        }
    }
}