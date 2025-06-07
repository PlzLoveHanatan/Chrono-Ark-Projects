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
namespace Unica
{
	/// <summary>
	/// In Times Like These!
	/// If 3 or fewer pages are in-hand at the end of the Scene, gain 1 Strength next Scene
	/// </summary>
    public class InTimesLikeThese_0 : Buff
    {
        public override void Init()
        {
            base.Init();
        }
        public override void BuffStat()
        {
            this.PlusPerStat.Damage = 15;
        }
    }
}