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
	/// Echo Distortion
	/// </summary>
    public class B_Abnormality_TechnologicalLv1_MetallicRinging_0 : Buff
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = -4 * StackNum;
            PlusStat.RES_CC = -4f * StackNum;
        }
    }
}