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
	/// Overflowing Grace
	/// </summary>
    public class S_Aqua_OverflowingGrace : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
            {
                if (ally != null)
                {
                    ally.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, this.BChar, false, 0, false, -1, false);
                }
            }

            foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if (enemy != null)
                {
                    enemy.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, this.BChar, false, 0, false, -1, false);
                }
            }

            bool neverLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 20);

            if (neverLucky)
            {
                foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    int enemyHeal = (int)(BChar.GetStat.reg * 0.75f);

                    enemy.Heal(BattleSystem.instance.DummyChar, enemyHeal, false, true, null);

                    Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_DummyHeal, this.BChar, this.BChar.MyTeam);
                    healingParticle.PlusHit = true;
                    healingParticle.FreeUse = true;

                    this.BChar.ParticleOut(healingParticle, enemy);
                }
            }
        }
    }
}