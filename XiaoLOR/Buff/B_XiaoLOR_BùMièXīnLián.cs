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
using EmotionalSystem;
namespace XiaoLOR
{
    /// <summary>
    /// Bù Miè Xīn Lián
    /// </summary>
    public class B_XiaoLOR_BùMièXīnLián : Buff, IP_DamageTake, IP_DamageTakeChange
    {
        public override void BuffStat()
        {
            PlusStat.def = 15f;
        }
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg > 0)
            {
                if (BattleSystem.instance.EnemyList.Count >= 1)
                {
                    Utils.ApplyBurn(BattleSystem.instance.EnemyList.Random(this.BChar.GetRandomClass().Main), this.BChar, 2);
                }
            }
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (!Preview && Dmg >= 1)
            {
                Dmg = (int)(Dmg * 0.85f);
            }
            if (Dmg <= 1)
            {
                Dmg = 1;
            }
            return Dmg;
        }
    }
}