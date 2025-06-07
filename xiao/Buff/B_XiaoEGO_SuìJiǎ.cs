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
namespace Xiao
{
	/// <summary>
	/// Suì Jiǎ
	/// </summary>
    public class B_XiaoEGO_SuìJiǎ : Buff
    {
        public override void BuffStat()
        {
            this.PlusStat.def = -15 * StackNum;
        }
    }
}