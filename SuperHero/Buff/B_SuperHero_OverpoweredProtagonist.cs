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
	/// Overpowered Protagonist
	/// </summary>
    public class B_SuperHero_OverpoweredProtagonist : Buff, IP_HPChange, IP_Awake, IP_BuffAddAfter
    {
        public override void Init()
        {
            PlusStat.atk = 5;
        }

        public void HPChange(BattleChar Char, bool Healed)
        {
            if (BChar.HP <= 0)
            {
                BChar.HP = 1;
            }
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
                superHero.OverPowered = true;

            if (BChar.HP <= 0)
            {
                BChar.HP = 1;
            }
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_OverpoweredProtagonist;
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