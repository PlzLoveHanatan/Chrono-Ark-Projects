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
	/// <summary>
	/// Firm as a Great Mountain
	/// </summary>
    public class B_Xiao_FirmasaGreatMountain : Buff, IP_DamageTake
    {
        private int MaxArmor;
        public override void Init()
        {
            OnePassive = true;
            PlusStat.def = 10;
            PlusStat.Strength = true;
            PlusStat.AggroPer = 40;
        }
        public override void BuffStat()
        {
            PlusStat.Strength = true;
            PlusStat.AggroPer = 40;
            PlusStat.def = 10 + Math.Min(MaxArmor, 4) * 5;
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg > 0 && MaxArmor < 4)
            {
                MaxArmor++;
                PlusStat.def += 5;
            }
        }
    }
}