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
    /// Draw 3 skills.
    /// </summary>
    public class S_SuperHero_LucyDraw : Skill_Extended
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            Skills_Deck.Remove(this.MySkill);
            Skills_Deck.Insert(0, this.MySkill);
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var aliveHero = allies.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_SuperHero);

            if (aliveHero != null)
            {
                Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_DummyHeal, BChar, BChar.MyTeam);
                healingParticle.PlusHit = true;
                healingParticle.FreeUse = true;

                aliveHero.ParticleOut(healingParticle, aliveHero);

                BattleSystem.instance.AllyTeam.Draw(2);

                for (int i = 0; i < 2; i++)
                {
                    aliveHero.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
                }
            }
            else
            {
                BattleSystem.instance.AllyTeam.Draw();
                MySkill.isExcept = true;
            }
        }
    }
}