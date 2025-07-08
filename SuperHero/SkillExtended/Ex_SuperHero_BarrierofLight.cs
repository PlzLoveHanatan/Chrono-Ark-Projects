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
    public class Ex_SuperHero_BarrierofLight : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveHero = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_SuperHero);

            if (aliveHero != null)
            {
                Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_DummyHeal, BChar, BChar.MyTeam);
                healingParticle.PlusHit = true;
                healingParticle.FreeUse = true;

                aliveHero.ParticleOut(healingParticle, aliveHero);

                for (int i = 0; i < 4; i++)
                {
                    aliveHero.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_Ex_SuperHero_BarrierofLight, BChar, false, 0, false, -1, false);
            }
            SelfDestroy();
        }
    }
}