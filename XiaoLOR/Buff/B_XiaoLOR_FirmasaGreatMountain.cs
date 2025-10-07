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
namespace XiaoLOR
{
	/// <summary>
	/// Firm as a Great Mountain
	/// </summary>
    public class B_XiaoLOR_FirmasaGreatMountain : Buff
    {
        public override void Init()
        {
            PlusStat.def = 10;
            PlusStat.Strength = true;
            PlusStat.AggroPer = 40;
        }
    }
}