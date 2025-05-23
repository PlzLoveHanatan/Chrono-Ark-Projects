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
using DG.Tweening;
namespace Aqua
{
	/// <summary>
	/// Aqua Veil
	/// </summary>
    public class B_Aqua_AquaVeil : Buff
    {
        public override void BuffStat()
        {
            PlusStat.hit = -10 * StackNum;
        }
    }
}