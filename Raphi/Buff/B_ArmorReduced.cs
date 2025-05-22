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
namespace Raphi
{
	/// <summary>
	/// Armor Reduced!
	/// </summary>
    public class B_ArmorReduced : Buff
    {
        public override void BuffStat()
        {
            PlusStat.def = -10f * StackNum;
        }
    }
}