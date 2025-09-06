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
namespace ImaSuguRinne
{
	/// <summary>
	/// Blooming Agony
	/// </summary>
    public class B_Rinne_BloomingAgony : Buff, IP_BuffAdd
    {
        public override void BuffStat()
        {
            PlusStat.RES_CC = -10 * StackNum;
            PlusStat.RES_DEBUFF = -10 * StackNum;
            PlusStat.RES_DOT = -10 * StackNum;
            PlusStat.DMGTaken = 3 * StackNum;
        }

        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if (BuffTaker == BChar && !BuffTaker.Info.Ally && addedbuff.BuffData.Debuff)
            {
                BattleSystem.DelayInput(this.Del(BuffUser, BuffTaker));
            }
        }

        public IEnumerator Del(BattleChar BuffUser, BattleChar BuffTaker)
        {
            if (BuffTaker != null && !BuffTaker.IsDead)
            {
                BuffTaker.Damage(BuffUser, 2, false, true, false, 0, false, false, false);
            }
            yield break;
        }
    }
}