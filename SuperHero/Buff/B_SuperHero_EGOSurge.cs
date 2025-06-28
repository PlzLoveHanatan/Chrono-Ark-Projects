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
using Steamworks;
namespace SuperHero
{
	/// <summary>
	/// EGO Surge
	/// </summary>
    public class B_SuperHero_EGOSurge : Buff
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 15 * StackNum;
        }
    }
}