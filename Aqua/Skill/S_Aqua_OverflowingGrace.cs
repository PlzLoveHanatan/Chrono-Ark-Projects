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
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.KeyData == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            var targets = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Concat(BattleSystem.instance.AllyTeam.AliveChars);

            foreach (var target in targets)
            {
                target.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, BChar, false, 0, false, -1, false);

                if (target is BattleEnemy enemy)
                {
                    int enemyHeal = (int)(BChar.GetStat.reg * 0.85f);

                    enemy.Heal(BattleSystem.instance.DummyChar, enemyHeal, false, true, null);

                    Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_DummyHeal, BChar, BChar.MyTeam);
                    healingParticle.FreeUse = true;

                    BChar.ParticleOut(healingParticle, enemy);
                }
            }
        }
    }
}