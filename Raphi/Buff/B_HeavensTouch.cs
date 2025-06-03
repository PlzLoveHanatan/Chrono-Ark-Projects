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
	/// Heaven's Touch
	/// </summary>
    public class B_HeavensTouch : Buff
    {
        public override void BuffStat()
        {
            PlusStat.cri = 5 * StackNum;
            PlusStat.PlusCriDmg = 10 * StackNum;
        }
    }
}