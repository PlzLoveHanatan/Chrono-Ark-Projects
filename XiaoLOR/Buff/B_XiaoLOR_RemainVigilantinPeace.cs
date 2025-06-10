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
namespace XiaoLOR
{
	/// <summary>
	/// Remain Vigilant in Peace
	/// </summary>
    public class B_XiaoLOR_RemainVigilantinPeace : Buff, IP_DamageTake
    {
        private int MaxArmor;
        public override void Init()
        {
            OnePassive = true;
            PlusStat.Strength = true;
            PlusStat.AggroPer = 20;
            PlusStat.def = 5;
        }

        public override void BuffStat()
        {
            PlusStat.Strength = true;
            PlusStat.AggroPer = 20;
            PlusStat.def = 5 + Math.Min(MaxArmor, 2) * 5;
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg > 0 && MaxArmor < 2)
            {
                PlusStat.def += 5;
                MaxArmor++;
            }
        }
    }
}