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
	/// Insurance Plan
	/// </summary>
    public class InsurancePlan : Buff
    {
        public override void Init()
        {
            base.Init();            
        }
        public override void BuffStat()
        {
            this.PlusPerStat.MaxHP = 15;
            this.PlusStat.Strength = true;
        }
    }
}