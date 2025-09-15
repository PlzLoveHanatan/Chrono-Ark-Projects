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
    public class B_Mikure_IKnowWhatYouNeed_1 : Buff
    {
        public override void BuffStat()
        {
            PlusStat.DMGTaken = -10 * StackNum;
            PlusStat.HEALTaken = 10 * StackNum;
            PlusStat.Strength = true;
        }
    }
}