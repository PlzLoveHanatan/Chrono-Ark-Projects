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
	/// Remain Vigilant in Peace
	/// </summary>
    public class B_XiaoLOR_RemainVigilantinPeace : Buff
    {
        public override void Init()
        {
            PlusStat.Strength = true;
            PlusStat.AggroPer = 20;
            PlusStat.def = 5;
        }
    }
}