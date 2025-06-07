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
    public class B_Xiao_StifledAssault : Buff
    {
        public override void BuffStat()
        {
            this.PlusPerStat.Damage = -8 * base.StackNum;
        }
    }
}