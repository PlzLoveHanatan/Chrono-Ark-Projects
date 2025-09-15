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
	/// Don't Push Yourself!
	/// </summary>
    public class B_Mikure_DontPushYourself : Buff
    {
        public override void Init()
        {
            PlusStat.DMGTaken = -10;
        }
    }
}