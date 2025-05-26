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
namespace Aqua
{
	/// <summary>
	/// Type 100 Recovery Mode
	/// </summary>
    public class B_Aqua_Type100RecoveryMode : Buff
    {
        public override void BuffStat()
        {
            PlusStat.HEALTaken = 30;
            PlusStat.def = -30;
        }
    }
}