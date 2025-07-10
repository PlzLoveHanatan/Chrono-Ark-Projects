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
    /// Relentless Recovery
    /// </summary>
    public class B_SuperHero_RelentlessRecovery : Buff, IP_PlayerTurn, IP_Awake, IP_BuffAddAfter
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(BChar.GetStat.maxhp * 0.4f)).ToString());
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
                superHero.Relentless = true;
        }

        public void Turn()
        {
            int barrierValue = (int)(BChar.GetStat.maxhp * 0.4f);
            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_EgoShield, BChar, false, 0, false, -1, false).BarrierHP += barrierValue;

            var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Random(BChar.GetRandomClass().Main, 3);
            foreach (var debuff in debuffs)
            {
                debuff.SelfDestroy();
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_RelentlessRecovery;
            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker.Info.KeyData != ModItemKeys.Character_SuperHero)
                {
                    BuffTaker.BuffRemove(buff, true);
                }
            }
        }
    }
}