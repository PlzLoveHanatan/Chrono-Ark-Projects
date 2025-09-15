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
	/// Restrained
	/// Stunned.
	/// Whenever you are healed, the ally with the lowest health is also healed for half the amount.
	/// </summary>
    public class B_Mikure_Restrained : Buff
    {
        public override void Init()
        {
            PlusStat.Strength = true;
            PlusStat.DMGTaken = 30;
            PlusPerStat.Damage = 30;
            PlusStat.HEALTaken = 30;
            PlusStat.AggroPer = 100;
        }
    }
}