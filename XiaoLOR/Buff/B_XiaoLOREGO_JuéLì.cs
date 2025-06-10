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
	/// JuéLì
	/// Jué Lì
	/// </summary>
    public class B_XiaoLOREGO_JuéLì : Buff
    {
        public override void BuffStat()
        {
            this.PlusPerStat.Damage = -15 * StackNum;
        }
    }
}