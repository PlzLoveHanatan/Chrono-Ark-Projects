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
	/// You Must Be Happy
	/// </summary>
    public class B_EnemyAbnormality_YouMustBeHappy : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            PlusStat.DMGTaken = -15f;
        }
    }
}