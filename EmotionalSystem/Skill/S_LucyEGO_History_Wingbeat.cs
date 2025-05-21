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
    /// Wingbeat
    /// </summary>
    public class S_LucyEGO_History_Wingbeat : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Countdown = 3;
        }        
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            HealAllyWithParticle();
            BattleSystem.DelayInput(this.Damage(Targets[0]));
            MasterAudio.PlaySound("Wingbeat", 100f, null, 0f, null, null, false, false);
        }

        public IEnumerator Damage(BattleChar Target)
        {
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSecondsRealtime(0.5f);

                MasterAudio.PlaySound("Wingbeat", 100f, null, 0f, null, null, false, false);

                Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_S_LucyEGO_History_Wingbeat, this.BChar, this.BChar.MyTeam);
                skill2.PlusHit = true;
                skill2.FreeUse = true;

                if (this.BChar != null && !this.BChar.Dummy && !this.BChar.IsDead)
                {
                    if (this.BChar != null && !this.BChar.Dummy && !this.BChar.IsDead)
                    {
                        if (Target.IsDead)
                        {
                            this.BChar.ParticleOut(this.MySkill, skill2, this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main));
                            HealAllyWithParticle();
                        }
                        else
                        {
                            this.BChar.ParticleOut(this.MySkill, skill2, Target);
                            HealAllyWithParticle();
                        }
                    }
                }
            }
            yield break;
        }
        private void HealAllyWithParticle()
        {
            var target = this.BChar.MyTeam.AliveChars.OrderBy(x => x.HP).FirstOrDefault();
            if (target != null)
            {
                target.Heal(BattleSystem.instance.DummyChar, 6f, false, true, null);

                Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Buff_Unity, this.BChar, this.BChar.MyTeam);
                healingParticle.PlusHit = true;
                healingParticle.FreeUse = true;

                this.BChar.ParticleOut(healingParticle, target);
            }
        }
    }
}