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
    /// Loyalty
    /// </summary>
    public class B_Abnormality_HistoryLv3_Loyalty : Buff
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 40;
            PlusPerStat.Heal = 40;
        }
    }
}