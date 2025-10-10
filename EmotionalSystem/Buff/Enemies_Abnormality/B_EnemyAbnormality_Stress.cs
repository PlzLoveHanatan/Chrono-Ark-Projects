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
		public override void Init()
		{
			PlusPerStat.Damage = 20;
			PlusStat.HIT_DOT = 20f;
			PlusStat.HIT_CC = 20f;
			PlusStat.HIT_DEBUFF = 20f;
		}
    }
}