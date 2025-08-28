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
namespace Xao
{
	/// <summary>
	/// Soaked Lust
	/// </summary>
    public class B_Xao_SoakedLust : Buff
    {
        public override void Init()
        {
            PlusStat.RES_CC = -8 * StackNum;
            PlusStat.RES_DEBUFF = -8 * StackNum;
            PlusStat.RES_DOT = -8 * StackNum;
            PlusStat.def = -4 * StackNum;
            PlusStat.hit = -4 * StackNum;
        }
    }
}