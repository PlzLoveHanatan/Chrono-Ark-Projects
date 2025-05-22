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
	/// The Fairies' Care
	/// </summary>
    public class B_Abnormality_HistoryLv1_TheFairiesCare : Buff
    {
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        public override void BuffStat()
        {
            PlusStat.DMGTaken = 10f;
            PlusStat.RES_DOT = -10f;
            PlusStat.RES_CC = -10f;
            PlusStat.RES_DEBUFF = -10f;
        }
    }
}