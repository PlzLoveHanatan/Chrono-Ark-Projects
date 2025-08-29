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
namespace Xao
{
	/// <summary>
	/// Mistress' Touch
	/// </summary>
    public class B_Xao_MistressTouch : Buff
    {
        public override void BuffStat()
        {
            PlusStat.DMGTaken = 3 * StackNum;
            PlusStat.crihit = 3 * StackNum;
            //PlusStat.hit = -3 * StackNum;
        }
    }
}