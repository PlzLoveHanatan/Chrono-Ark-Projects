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
namespace Darkness
{
	/// <summary>
	/// Ecstatic Endurance
	/// </summary>
    public class B_Darkness_EcstaticEndurance : Buff, IP_HPChange
    {
        public override void Init()
        {
            PlusStat.DeadImmune = 100;
            PlusStat.AggroPer = 100;
        }
        public void HPChange(BattleChar Char, bool Healed)
        {
            if (BChar.HP <= 0)
            {
                BChar.HP = 1;
            }
        }
        public void Awake()
        {
            if (BChar.HP <= 0)
            {
                BChar.HP = 1;
            }
        }
    }
}