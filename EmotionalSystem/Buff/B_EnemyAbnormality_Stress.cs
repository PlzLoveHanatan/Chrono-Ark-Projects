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
    /// Stress
    /// </summary>
    public class B_EnemyAbnormality_Stress : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            PlusPerStat.Damage = 15;
            PlusStat.HIT_DOT = 15f;
            PlusStat.HIT_CC = 15f;
            PlusStat.HIT_DEBUFF = 15f;
        }
    }
}