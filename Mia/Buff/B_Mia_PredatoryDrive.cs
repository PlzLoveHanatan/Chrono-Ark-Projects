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
namespace Mia
{
	/// <summary>
	/// Predatory Drive
	/// </summary>
    public class B_Mia_PredatoryDrive : Buff
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 3 * StackNum;
        }
    }
}