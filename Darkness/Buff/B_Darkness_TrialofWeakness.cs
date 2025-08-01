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
namespace Darkness
{
	/// <summary>
	/// Trial of Weakness
	/// </summary>
    public class B_Darkness_TrialofWeakness : Buff
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = -15 * StackNum;
        }
    }
}