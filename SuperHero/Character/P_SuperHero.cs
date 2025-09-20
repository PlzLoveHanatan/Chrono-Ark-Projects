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
using Spine.Unity.Examples;
namespace SuperHero
{
    /// <summary>
    /// Super Hero
    /// Passive:
    /// </summary>
    public class P_SuperHero : Passive_Char, IP_PlayerTurn, IP_Kill, IP_BattleStart_Ones, IP_SkillUse_User, IP_Discard
    {
        public bool PlotArmor;
        public bool Relentless;
        public bool SecondAct;

        public bool OverPowered;
        public bool JusticeHero;
        public bool JusticeAscention;

        public int Complex;
        public bool SuperHero;
        public bool SuperVillain;

        public override void Init()
        {
            OnePassive = true;
        }

        public void BattleStart(BattleSystem Ins)
        {
            ResetFlags();
            //Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, ModItemKeys.Buff_B_SuperHero_GloryofJustice);
        }

        public void KillEffect(SkillParticle SP)
        {
            if (SP.UseStatus == BChar)
            {
                for (int i = 0; i < 2; i++)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
                }
            }
        }

        public void Turn()
        {
            var buffMap = new Dictionary<string, bool>
            {
                { ModItemKeys.Buff_B_SuperHero_SecondAct, SecondAct },
                { ModItemKeys.Buff_B_SuperHero_RelentlessRecovery, Relentless },
                { ModItemKeys.Buff_B_SuperHero_PlotArmor, PlotArmor },
                //{ ModItemKeys.Buff_B_SuperHero_OverpoweredProtagonist, OverPowered },
                //{ ModItemKeys.Buff_B_SuperHero_JusticeHero, JusticeHero },
                //{ ModItemKeys.Buff_B_SuperHero_JusticeAscension, JusticeAscention }
            };

            foreach (var kvp in buffMap)
            {
                if (kvp.Value)
                {
                    EnsureBuff(BChar, kvp.Key);
                }
            }

            //EnsureBuff(BChar, ModItemKeys.Buff_B_SuperHero_GloryofJustice);
            EnsureBuff(BChar, ModItemKeys.Buff_B_SuperHero_HeroComplex, Complex);
            EnsureBuff(BChar, ModItemKeys.Buff_B_SuperHero_HeroComplex, Complex);

            Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, ModItemKeys.Buff_B_SuperHero_HeroComplex);

            string skillKey = "";

            if (!SuperHero && BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) is B_SuperHero_HeroComplex complex && complex.StackNum >= 25)
            {
                if (BattleSystem.instance.TurnNum >= 5)
                {
                    skillKey = ModItemKeys.Skill_S_SuperHero_JusticeFinale;

                    var skillFixed = Skill.TempSkill(skillKey, BChar, BChar.MyTeam);
                    (BChar as BattleAlly).MyBasicSkill.SkillInput(skillFixed);
                }
                else if (BattleSystem.instance.TurnNum >= 3)
                {
                    skillKey = ModItemKeys.Skill_S_SuperHero_JusticePatience;
                }
            }
            else if (BattleSystem.instance.TurnNum >= 3)
            {
                skillKey = ModItemKeys.Skill_S_SuperHero_JusticePatience_0;
            }

            if (!string.IsNullOrEmpty(skillKey))
            {
                var skill = Skill.TempSkill(skillKey, BChar, BChar.MyTeam);
                if (skill != null)
                {
                    BattleSystem.instance.AllyTeam.Add(skill, true);
                }
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && !Utils.HeroAttacks.Contains(SkillD.MySkill.KeyID))
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
            }
        }

        public void EnsureBuff(BattleChar target, string buffKey, int buffNum = 1, bool addIfMissing = true)
        {
            if (target.BuffReturn(buffKey) != null) return;

            if (addIfMissing)
            {
                Utils.AddBuff(target, BattleSystem.instance.DummyChar, buffKey, buffNum);
            }
        }

        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            var justice = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0;
            var justice1 = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1;

            if (skill.MySkill.KeyID == justice || skill.MySkill.KeyID == justice1)
            {
                if (!HandFullWaste)
                {
                    Utils.AllyTeam.Draw();
                }
            }
        }

        public void BecomeJusticeHero()
        {
            if (PlotArmor && Relentless && SecondAct)
            {
                BattleSystem.DelayInput(Utils.SuperHeroModCheck(BChar, ModItemKeys.Buff_B_SuperHero_JusticeAscension, true, false));
            }
        }

        public void ResetFlags()
        {
            PlotArmor = false;
            Relentless = false;
            SecondAct = false;
            OverPowered = false;
            JusticeHero = false;
            JusticeAscention = false;
            SuperHero = false;
            SuperVillain = false;
            Complex = 0;
        }
    }
}