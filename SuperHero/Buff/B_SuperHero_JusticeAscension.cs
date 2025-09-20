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
    public class B_SuperHero_JusticeAscension : Buff, IP_BuffAddAfter, IP_Awake, IP_SkillUse_User
    {
        public override void Init()
        {
            PlusStat.atk = 5;
            //PlusStat.PlusCriDmg = 25;
        }

        public void Awake()
        {
            if (BChar.Info.Passive is P_SuperHero superHero)
            {
                superHero.JusticeAscention = true;
                superHero.SuperVillain = true;
                superHero.SuperHero = false;
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff.BuffData.Key == ModItemKeys.Buff_B_SuperHero_JusticeAscension && BuffTaker != Utils.SuperHero)
            {
                BuffTaker.BuffRemove(ModItemKeys.Buff_B_SuperHero_JusticeAscension);
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.AttackRedirect(BChar, SkillD, Targets);
        }
    }
}