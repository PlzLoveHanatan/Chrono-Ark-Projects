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
namespace Urunhilda
{
    /// <summary>
    /// Rutting Instinct
    /// </summary>
    public class B_Urunhilda_RuttingInstinct : Buff, IP_BuffAddAfter, IP_BuffAdd
    {
        private bool IsBuffAdding;

        public override string DescExtended()
        {
            if (BattleSystem.instance != null)
            {
                return base.DescExtended().Replace("&a", ((int)(StackNum * 5)).ToString());
            }
            return base.DescExtended().Replace("&a", 0.ToString());
        }

        public override void Init()
        {
            OnePassive = true;
            PlusStat.AggroPer = 20 * StackNum;
            PlusStat.reg = 1 * StackNum;
            PlusStat.atk = 1 * StackNum;
        }

        public override void BuffStat()
        {
            PlusStat.AggroPer = 20 * StackNum;
            PlusStat.reg = 1 * StackNum;
            PlusStat.atk = 1 * StackNum;
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_Urunhilda passive)
            {
                passive.RuttingInstinct = true;
            }
        }

        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if (BuffUser != BChar || IsBuffAdding) return;
            int chance = StackNum * 5;
            IsBuffAdding = true;
            if (RandomManager.RandomPer(BChar.GetRandomClass().Main, 100, chance))
            {
                BuffTaker.BuffAdd(addedbuff.BuffData.Key, BChar, false, 0, false, -1, false);
            }
            IsBuffAdding = false;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            string buff = ModItemKeys.Buff_B_Urunhilda_RuttingInstinct;
            string buff2 = ModItemKeys.Buff_B_Urunhilda_BeastkinInstinct;
            string buff3 = ModItemKeys.Buff_B_Urunhilda_GentleViolence;
            string urunhilda = ModItemKeys.Character_Urunhilda;
            var aliveUrunhilda = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == urunhilda);

            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker.Info.KeyData == urunhilda)
                {
                    if (urunhilda != null)
                    {
                        if (aliveUrunhilda.Info.Passive is P_Urunhilda passive)
                        {
                            passive.RuttingStacks = StackNum;
                        }

                        Urunhilda_Visual.BuffCheck(BChar, 1);

                        if (addedbuff.StackNum >= 3 && aliveUrunhilda.BuffReturn(buff2, false) == null)
                        {
                            aliveUrunhilda.BuffAdd(buff2, BChar, false, 0, false, -1, false);
                        }
                        if (addedbuff.StackNum >= 5 && aliveUrunhilda.BuffReturn(buff3, false) == null)
                        {
                            aliveUrunhilda.BuffAdd(buff3, BChar, false, 0, false, -1, false);
                        }
                    }
                }
                else
                {
                    BuffTaker.BuffRemove(buff, true);
                }
            }
        }
    }
}