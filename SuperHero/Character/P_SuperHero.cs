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
using System.Diagnostics.Contracts;
using static B_LBossFirst_Summons_0;
using Spine;
namespace SuperHero
{
    /// <summary>
    /// Super Hero
    /// Passive:
    /// </summary>
    public class P_SuperHero : Passive_Char, IP_PlayerTurn, IP_Kill, IP_BattleStart_Ones, IP_SkillUse_User, IP_BattleEndRewardChange
    {
        public bool PlotArmor;
        public bool Relentless;
        public bool SecondAct;

        public bool OverPowered;
        public bool JusticeHero;
        public bool JusticeAscention;

        public int Complex;
        public bool SuperHeroPassive;

        public override void Init()
        {
            OnePassive = true;
        }

        public void BattleStart(BattleSystem Ins)
        {
            Utils.RemovePainSharingBuffsFromAllAllies();
            Utils.RemovePainSharingBuffsFromLucy();

            //if (!Utils.Timer)
            //{
            //    var TimerObj = new GameObject("GlobalTimerManager");
            //    TimerObj.AddComponent<GlobalTimerManager>();
            //    GlobalTimerManager.Instance?.StartTimer();
            //}

            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_GloryofJustice, BChar, false, 0, false, -1, false);
            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_IntheNameofJustice, BChar, false, 0, false, -1, false);
            PlotArmor = false;
            Relentless = false;
            SecondAct = false;
            OverPowered = false;
            JusticeHero = false;
            JusticeAscention = false;
            Complex = 0;
        }
        public void BattleEndRewardChange()
        {
            if (Utils.Music)
            {
                BattleSystem.DelayInputAfter(Utils.StopSong());
            }
        }

        //public override void FixedUpdate()
        //{
        //    PlusStat.atk = Utils.Attack;
        //}

        public void KillEffect(SkillParticle SP)
        {
            if (SP.UseStatus == BChar)
            {
                //Utils.Attack++;
                for (int i = 0; i < 2; i++)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
                }
            }
        }

        public void Turn()
        {
            var plotArmor = ModItemKeys.Buff_B_SuperHero_PlotArmor;
            var relentless = ModItemKeys.Buff_B_SuperHero_RelentlessRecovery;
            var secondAct = ModItemKeys.Buff_B_SuperHero_SecondAct;

            var overPowered = ModItemKeys.Buff_B_SuperHero_OverpoweredProtagonist;
            var justiceHero = ModItemKeys.Buff_B_SuperHero_JusticeHero;
            var justiceAscention = ModItemKeys.Buff_B_SuperHero_JusticeAscension;

            var gloryOfJustice = ModItemKeys.Buff_B_SuperHero_GloryofJustice;
            var heroComplex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var complex = BChar.BuffReturn(heroComplex, false) as B_SuperHero_HeroComplex;

            var inTheName = ModItemKeys.Buff_B_SuperHero_IntheNameofJustice;

            if (BattleSystem.instance.TurnNum >= 3 && heroComplex != null && complex.StackNum >= 25 && !SuperHeroPassive)
            {
                Skill skill;
                if (BattleSystem.instance.TurnNum >= 5)
                {
                    skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_JusticeFinale, BChar, BChar.MyTeam);
                }
                else
                {
                    skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_JusticePatience, BChar, BChar.MyTeam);
                }
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }

            if (SecondAct && BChar.BuffReturn(secondAct) == null)
            {
                BChar.BuffAdd(secondAct, BChar, false, 0, false, -1, false);
            }
            if (Relentless && BChar.BuffReturn(relentless) == null)
            {
                BChar.BuffAdd(relentless, BChar, false, 0, false, -1, false);
            }
            if (PlotArmor && BChar.BuffReturn(plotArmor) == null)
            {
                BChar.BuffAdd(plotArmor, BChar, false, 0, false, -1, false);
            }
            if (OverPowered && BChar.BuffReturn(overPowered) == null)
            {
                BChar.BuffAdd(overPowered, BChar, false, 0, false, -1, false);
            }
            if (JusticeHero && BChar.BuffReturn(justiceHero) == null)
            {
                BChar.BuffAdd(justiceHero, BChar, false, 0, false, -1, false);
            }
            if (JusticeAscention && BChar.BuffReturn(justiceAscention) == null)
            {
                BChar.BuffAdd(justiceAscention, BChar, false, 0, false, -1, false);
            }
            if (BChar.BuffReturn(gloryOfJustice) == null)
            {
                BChar.BuffAdd(gloryOfJustice, BChar, false, 0, false, -1, false);
            }
            if (BChar.BuffReturn(heroComplex) == null)
            {
                for (int i = 0; i < Complex; i++)
                {
                    BChar.BuffAdd(heroComplex, BChar, false, 0, false, -1, false);
                }
            }
            if (BChar.BuffReturn(inTheName) == null)
            {
                BChar.BuffAdd(inTheName, BChar, false, 0, false, -1, false);
            }

            BChar.BuffAdd(heroComplex, BChar, false, 0, false, -1, false);
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && !Utils.HeroAttacks.Contains(SkillD.MySkill.KeyID))
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
            }
        }
    }
}