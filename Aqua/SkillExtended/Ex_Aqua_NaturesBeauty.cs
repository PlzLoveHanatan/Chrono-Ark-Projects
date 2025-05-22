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
namespace Aqua
{
	/// <summary>
	/// When played cast "Nature's Beauty" to all allies.
	/// </summary>
    public class Ex_Aqua_NaturesBeauty : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;

            foreach (var ally in allies)
            {
                ally.Heal(BattleSystem.instance.DummyChar, 5f, false, true, null);
                ally.BuffAdd("B_PopcornGirl_Lucy_1", BattleSystem.instance.AllyTeam.LucyChar, false, 0, false, -1, false);

                Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_DummyHeal, this.BChar, this.BChar.MyTeam);
                healingParticle.PlusHit = true;
                healingParticle.FreeUse = true;

                this.BChar.ParticleOut(healingParticle, ally);
            }
        }
    }
}