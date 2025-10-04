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
using UnityEngine.Analytics;
using ChronoArkMod.ModEditor;
namespace Xao
{
    /// <summary>
    /// Love Egg
    /// </summary>
    public class Equip_Xao_LoveEgg : EquipBase, IP_PlayerTurn, IP_SomeOneDead, IP_BattleStart_Ones
    {
        public void BattleStart(BattleSystem Ins)
        {
            Utils.AddBuff(BChar, ModItemKeys.Buff_B_Xao_E_LoveEgg);
        }

        public override void Init()
        {
            OnePassive = true;
            PlusStat.atk = 3;
            PlusStat.reg = 3;
            PlusStat.dod = 3;
            PlusStat.cri = 3;
        }     

        public void SomeOneDead(BattleChar DeadChar)
        {
            if (DeadChar == BChar)
            {
                Xao_Hearts_Ally.DestroyHearts(BChar);
            }
        }

        public void Turn()
        {
            string buff = Utils.GetAffectionBuff(BChar.Info);
            Utils.AddBuff(BChar, buff, 1);
        }
    }
}