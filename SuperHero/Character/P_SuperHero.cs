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
    /// Super Hero
    /// Passive:
    /// </summary>
    public class P_SuperHero : Passive_Char, IP_PlayerTurn, IP_Kill
    {
        public override void FixedUpdate()
        {
            PlusStat.atk = Utils.Attack;
        }
        public void KillEffect(SkillParticle SP)
        {
            if (SP.UseStatus == BChar)
                Utils.Attack++;
        }

        public void Turn()
        {
            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_IntheNameofJustice, BChar, BChar.MyTeam);
            skill.isExcept = true;
            BattleSystem.instance.AllyTeam.Add(skill, true);
        }
    }
}