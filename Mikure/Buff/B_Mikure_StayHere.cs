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
using System.Runtime.InteropServices.WindowsRuntime;
namespace Mikure
{
	/// <summary>
	/// Stay With Me!
	/// </summary>
    public class B_Mikure_StayHere : Buff
    {
        public override void Init()
        {
            PlusStat.cri = 10;
            PlusStat.hit = 10;
        }
    }
}