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
    public class B_Abnormality_TechnologicalLv3_DarkFlame_0 : Buff
    {
        public override void BuffStat()
        {
            PlusStat.RES_CC = -300;
            PlusStat.RES_DEBUFF = -300;
            PlusStat.RES_DOT = -300;
        }
    }
}