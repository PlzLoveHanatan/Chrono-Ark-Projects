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
	/// Vines of Despair
	/// </summary>
    public class B_Abnormality_HistoryLv2_Vines_2 : Buff
    {
        public override void BuffStat()
        {
            this.PlusPerStat.Damage = -20;
            this.PlusStat.def = -20;
        }
    }
}