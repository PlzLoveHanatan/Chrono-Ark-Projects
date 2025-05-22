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
namespace EmotionalSystem
{
    /// <summary>
    /// Loyalty
    /// </summary>
    public class B_Abnormality_HistoryLv3_Loyalty : Buff /*IP_DamageTake*/
    {
        public override void BuffStat()
        {
            PlusStat.atk = 5;
            PlusStat.reg = 5;
        }
        //public override void Init()
        //{
        //    this.OnePassive = true;
        //}
        //public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        //{
        //    if (Dmg > 0)
        //    {
        //        foreach (BattleAlly battleAlly in BattleSystem.instance.AllyList)
        //        {
        //            battleAlly.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv3_Loyalty_0, this.BChar, false, 0, false, -1, false);
        //        }
        //    }
        //}
    }
}