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
    public class B_Xao_ArousedBliss : Buff
    {
        public override void BuffStat()
        {
            PlusStat.PlusCriDmg = 3 * StackNum;
            PlusStat.dod = 3 * StackNum;
        }
    }
}