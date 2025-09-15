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
	/// Please Don't Collapse!
	/// </summary>
    public class B_Mikure_PleaseDontCollapse : Buff
    {
        public override void Init()
        {
            PlusStat.Strength = true;
            PlusStat.HEALTaken = 15;
        }
    }
}