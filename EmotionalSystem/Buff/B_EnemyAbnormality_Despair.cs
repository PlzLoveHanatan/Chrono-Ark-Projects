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
	/// Despair
	/// </summary>
    public class B_EnemyAbnormality_Despair : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            PlusStat.cri = 30f;
            PlusStat.hit = -15;
        }
    }
}