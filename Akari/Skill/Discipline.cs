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
using Spine;
using NLog.Targets;
namespace Akari
{
    /// <summary>
    /// Discipline (Melee)
    /// If this attack lands, recast this skill 3 times.
    /// </summary>
    public class Discipline : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Melee_Normal1", 100f, null, 0f, null, null, false, false);

            BattleSystem.DelayInput(this.Damage(Targets[0]));

            if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;
            
                CastingSkill targetSkill = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(skill => skill.skill.Master == Targets[0]);

                if (targetSkill != null)
                {
                    BattleSystem.instance.EnemyCastSkills.Remove(targetSkill);
                    BattleSystem.instance.ActWindow.AkariCastingWasteFixed(targetSkill);
                }
        }

        public IEnumerator Damage(BattleChar Target)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSecondsRealtime(0.3f);

                Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_Discipline, this.BChar, this.BChar.MyTeam);
                skill2.PlusHit = true;
                skill2.FreeUse = true;

                if (this.BChar != null && !this.BChar.Dummy && !this.BChar.IsDead)
                {
                    if (Target.IsDead)
                    {
                        this.BChar.ParticleOut(this.MySkill, skill2, this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main));
                    }
                    else
                    {
                        this.BChar.ParticleOut(this.MySkill, skill2, Target);
                    }
                }
            }

            yield break;
        }
    }
}