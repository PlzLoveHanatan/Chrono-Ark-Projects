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
    public class S_SuperHero_Rare_JusticeHero : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Priest_Ex_P).Particle_Path;
        }

        public override void FixedUpdate()
        {
            base.SkillParticleOn();
        }
        public override bool Terms()
        {
            if (BChar.Info.KeyData == ModItemKeys.Character_SuperHero)
                return true;

            return false;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.PlaySong(MySkill.MySkill.KeyID);
            var heroComplex = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var justiceHero = ModItemKeys.Buff_B_SuperHero_JusticeHero;
            var gloryOfJustice = ModItemKeys.Buff_B_SuperHero_GloryofJustice;
            var markOfJustice = ModItemKeys.Buff_B_SuperHero_MarkofJustice;
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveHero = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_SuperHero);
            var complex = aliveHero.BuffReturn(heroComplex, false) as B_SuperHero_HeroComplex;
            var justice = aliveHero.BuffReturn(gloryOfJustice, false) as B_SuperHero_GloryofJustice;
            var allyTeam = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null && x.Info.KeyData != ModItemKeys.Character_SuperHero);

            if (aliveHero != null)
            {
                Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_DummyHeal, BChar, BChar.MyTeam);
                healingParticle.PlusHit = true;
                healingParticle.FreeUse = true;

                aliveHero.ParticleOut(healingParticle, aliveHero);

                if (complex == null)
                {
                    aliveHero.BuffAdd(heroComplex, BChar, false, 0, false, -1, false);
                }

                if (complex != null)
                {
                    complex.SuperHero = true;
                    if (complex.StackNum >= 20)
                    {
                        complex.VillainObjectsCheck();
                    }
                }

                if (justice != null)
                {
                    justice.SuperHeroGlory = true;
                }
                else
                {
                    aliveHero.BuffAdd(gloryOfJustice, BChar, false, 0, false, -1, false);
                    justice.SuperHeroGlory = true;
                }

                if (BChar.Info.Passive is P_SuperHero Hero)
                {
                    if (Hero != null)
                    {
                        Hero.SuperHeroPassive = true;
                    }
                }

                foreach (var ally in allyTeam)
                {
                    var buff2 = ally.BuffReturn(markOfJustice, false) as B_SuperHero_MarkofJustice;
                    if (ally != null && buff2 != null)
                    {
                        buff2.SelfDestroy();
                    }
                }
                SuperHero_FaceChange.ChooseFace(aliveHero, false);
            }
        }
    }
}