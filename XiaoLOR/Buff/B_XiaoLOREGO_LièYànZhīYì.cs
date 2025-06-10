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
namespace XiaoLOR
{
	/// <summary>
	/// Liè Yàn Zhī Yì
	/// </summary>
    public class B_XiaoLOREGO_LièYànZhīYì : Buff
    {
        public override void BuffStat()
        {
            PlusStat.PlusCriDmg = 15 * base.StackNum;
            PlusStat.cri = 15 * base.StackNum;
        }
    }
}