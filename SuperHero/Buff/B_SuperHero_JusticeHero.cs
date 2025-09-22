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
	/// Justice Hero
	/// </summary>
    public class B_SuperHero_JusticeHero : Buff, IP_BuffAddAfter, IP_Awake
    {
        public override void Init()
        {
            OnePassive = true;
            PlusPerStat.Damage = 30;
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
            {
                superHero.JusticeHero = true;
                superHero.SuperVillain = false;
                superHero.SuperHero = true;
                BattleSystem.DelayInput(Utils.CheckModSkills(true));
            } 
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff.BuffData.Key == ModItemKeys.Buff_B_SuperHero_JusticeHero && BuffTaker != Utils.SuperHero)
            {
                BuffTaker.BuffRemove(ModItemKeys.Buff_B_SuperHero_JusticeHero);
            }
        }
    }
}