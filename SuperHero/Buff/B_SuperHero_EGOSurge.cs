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
using Steamworks;
namespace SuperHero
{
	/// <summary>
	/// EGO Surge
	/// </summary>
    public class B_SuperHero_EGOSurge : Buff, IP_DealDamage, IP_BuffAddAfter
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 15 * StackNum;
        }

        public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
        {
            if (Damage >= 1)
            {
                BChar.Heal(BChar, (int)(Damage * 0.15f), false, false, null);
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_EGOSurge;
            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker.Info.KeyData != ModItemKeys.Character_SuperHero)
                {
                    BuffTaker.BuffRemove(buff);
                }
            }
        }
    }
}