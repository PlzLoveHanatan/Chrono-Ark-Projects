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
	/// Pulse!
	/// </summary>
    public class B_Mikure_IKnowWhatYouNeed_0 : Buff
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 10 * StackNum;
            PlusStat.PlusCriDmg = 10 * StackNum;
        }
    }
}