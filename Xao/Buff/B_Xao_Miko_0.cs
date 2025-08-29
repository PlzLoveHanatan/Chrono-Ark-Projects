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
	/// Forbidden Ritual
	/// </summary>
    public class B_Xao_Miko_0 : Buff
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = -5 * StackNum;
            PlusStat.hit = -5 * StackNum;
        }
    }
}