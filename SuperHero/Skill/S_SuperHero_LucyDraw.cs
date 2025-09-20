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
            if (Utils.SuperHero)
            {
                BattleSystem.DelayInput(Utils.HealingParticle(Utils.SuperHero, BattleSystem.instance.DummyChar));
                Utils.AddBuff(Utils.SuperHero, BattleSystem.instance.DummyChar, ModItemKeys.Buff_B_SuperHero_HeroComplex, 3);
                BattleSystem.instance.AllyTeam.Draw(3);
            }
            else
            {
                Utils.AllyTeam.Draw();
                MySkill.isExcept = true;
            }
        }
    }
}