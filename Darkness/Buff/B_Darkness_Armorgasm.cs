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
    /// Armorgasm ♡
    /// </summary>
    public class B_Darkness_Armorgasm : Buff, IP_DamageTake
    {
        private int StatGain;
        public override void Init()
        {
            if (BChar.Info.LV >= 2)
                PlusStat.AggroPer = 40;
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg > 0 && StatGain < 5)
            {
                StatGain++;
                PlusStat.def += 5;
                PlusStat.DeadImmune += 5;
            }
        }
    }
}