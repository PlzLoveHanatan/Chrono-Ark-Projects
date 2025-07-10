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
namespace SuperHero
{
    /// <summary>
    /// Super Hero
    /// Passive:
    /// </summary>
    public class P_SuperHero : Passive_Char, IP_PlayerTurn, IP_Kill, IP_BattleStart_Ones, IP_SkillUse_User
    {
        public bool PlotArmor;
        public bool Relentless;
        public bool SecondAct;
        public bool OverPowered;
        public int Complex;

        public override void Init()
        {
            OnePassive = true;
        }

        public void BattleStart(BattleSystem Ins)
        {
            Utils.CreateHeroIcon(BChar, "HeroMascot", "Ui/HeroMascot.png", new Vector3(-0.2f, 0.4f, 0f), new Vector3(150f, 150f));

            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_GloryofJustice, BChar, false, 0, false, -1, false);
            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_IntheNameofJustice, BChar, false, 0, false, -1, false);
            PlotArmor = false;
            Relentless = false;
            SecondAct = false;
            OverPowered = false;
            Complex = 0;
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
            var buff = ModItemKeys.Buff_B_SuperHero_SecondAct;
            var buff2 = ModItemKeys.Buff_B_SuperHero_RelentlessRecovery;
            var buff3 = ModItemKeys.Buff_B_SuperHero_PlotArmor;
            var buff4 = ModItemKeys.Buff_B_SuperHero_GloryofJustice;
            var buff5 = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var heroComplex = BChar.BuffReturn(buff5, false) as B_SuperHero_HeroComplex;
            var buff6 = ModItemKeys.Buff_B_SuperHero_IntheNameofJustice;
            var buff7 = ModItemKeys.Buff_B_SuperHero_OverpoweredProtagonist;

            if (BattleSystem.instance.TurnNum >= 3 && heroComplex != null)
            {
                if (heroComplex.StackNum >= 25)
                {
                    if (BattleSystem.instance.TurnNum >= 5)
                    {
                        Skill skil3 = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_JusticeFinale, BChar, BChar.MyTeam);
                        BattleSystem.instance.AllyTeam.Add(skil3, true);
                    }
                    else
                    {
                        Skill skil2 = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_JusticePatience, BChar, BChar.MyTeam);
                        BattleSystem.instance.AllyTeam.Add(skil2, true);
                    }
                }
            }

            BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
            //Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0, BChar, BChar.MyTeam);
            //BattleSystem.instance.AllyTeam.Add(skill, true);

            if (SecondAct && BChar.BuffReturn(buff) == null)
                BChar.BuffAdd(buff, BChar, false, 0, false, -1, false);

            if (Relentless && BChar.BuffReturn(buff2) == null)
                BChar.BuffAdd(buff2, BChar, false, 0, false, -1, false);

            if (PlotArmor && BChar.BuffReturn(buff3) == null)
                BChar.BuffAdd(buff3, BChar, false, 0, false, -1, false);

            if (BChar.BuffReturn(buff4) == null)
                BChar.BuffAdd(buff4, BChar, false, 0, false, -1, false);

            if (BChar.BuffReturn(buff5) == null)
                BChar.BuffAdd(buff5, BChar, false, Complex, false, -1, false);

            if (BChar.BuffReturn(buff6) == null)
                BChar.BuffAdd(buff6, BChar, false, 0, false, -1, false);

            if (OverPowered && BChar.BuffReturn(buff7) == null)
                BChar.BuffAdd(buff7, BChar, false, 0, false, -1, false);
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