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
namespace SuperHero
{
    /// <summary>
    /// Mark of Justice
    /// </summary>
    public class B_SuperHero_MarkofJustice : Buff, IP_BuffAddAfter
    {
        public int MarkStacks;
        public override void Init()
        {
            OnePassive = true;
            PlusPerStat.Damage = -5;
            PlusStat.def = -5;
        }

        public override void BuffStat()
        {
            PlusPerStat.Damage = -5 * StackNum;
            PlusStat.def = -5 * StackNum;
            BuffData.MaxStack = MarkStacks + 5;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker.Info.KeyData == ModItemKeys.Character_SuperHero)
                {
                    BuffTaker.BuffRemove(buff, true);
                }
            }
        }
    }
}