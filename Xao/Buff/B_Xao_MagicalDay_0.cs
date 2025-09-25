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
    /// Magical Day
    /// </summary>
    public class B_Xao_MagicalDay_0 : Buff
    {
        public override void BuffStat()
        {
            PlusStat.dod = 3 * StackNum;
            PlusStat.cri = 3 * StackNum;

        }
    }
}