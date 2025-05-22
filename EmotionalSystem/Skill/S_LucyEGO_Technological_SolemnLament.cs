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
namespace EmotionalSystem
{
    /// <summary>
    /// <color=#ffc500>Solemn Lament</color>
    /// Recast this skill 8 times.
    /// </summary>
    public class S_LucyEGO_Technological_SolemnLament : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Countdown = 3;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.DelayInput(this.Damage(Targets[0]));
        }

        public IEnumerator Damage(BattleChar Target)
        {
            for (int i = 0; i < 6; i++)
            {
                yield return new WaitForSecondsRealtime(0.25f);

                Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_S_LucyEGO_Technological_SolemnLament, this.BChar, this.BChar.MyTeam);
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