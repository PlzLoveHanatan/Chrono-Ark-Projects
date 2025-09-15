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
namespace Mikure
{
	/// <summary>
	/// Are You OK!?
	/// </summary>
    public class B_Mikure_AreYouOK : Buff
    {
        public int OverHealed;

        public override void BuffStat()
        {
            PlusStat.maxhp = OverHealed;
        }
    }
}