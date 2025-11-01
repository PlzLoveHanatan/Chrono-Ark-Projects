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
using EmotionSystem;
namespace XiaoLOR
{
    /// <summary>
    /// Bù Miè Xīn Lián
    /// </summary>
    public class B_XiaoLOR_BùMièXīnLián : Buff, IP_DamageTake
    {
        public override void BuffStat()
        {
            PlusStat.def = 15f;
            PlusStat.DMGTaken = -15;
            PlusStat.AggroPer = 50;
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
    }
}