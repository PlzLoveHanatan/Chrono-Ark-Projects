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
using NLog.Targets;
namespace EmotionalSystem
{
    /// <summary>
    /// <color=#ffc500>Regret</color>
    /// </summary>
    public class S_LucyEGO_Technological_Regret : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Countdown = 3;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var stun = GDEItemKeys.Buff_B_Common_Rest;

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    if (target.BuffReturn(stun, false) != null)
                    {
                        var skillToRemove = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(skill => skill.Usestate == target);

                        if (skillToRemove != null)
                        {
                            BattleSystem.instance.EnemyCastSkills.Remove(skillToRemove);
                            BattleSystem.instance.ActWindow.CastingWasteFixed(skillToRemove);
                        }
                    }
                    else
                    {
                        target.BuffAdd(stun, this.BChar, false, 100, false, -1, false);
                    }
                }
            }

            BattleSystem.DelayInput(AdditionalHits(Targets[0]));
        }
        public IEnumerator AdditionalHits(BattleChar target)
        {
            string stun = GDEItemKeys.Buff_B_Common_Rest;

            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSecondsRealtime(0.5f);

                if (this.BChar != null && !this.BChar.Dummy && !this.BChar.IsDead)
                {
                    Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_LucyEGO_Technological_Regret, this.BChar, this.BChar.MyTeam);
                    skill.PlusHit = true;
                    skill.FreeUse = true;

                    if (target.BuffReturn(stun, false) != null)
                    {
                        var skillToRemove = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(s => s.Usestate == target);
                        if (skillToRemove != null)
                        {
                            BattleSystem.instance.EnemyCastSkills.Remove(skillToRemove);
                            BattleSystem.instance.ActWindow.CastingWasteFixed(skillToRemove);
                        }
                    }
                    else
                    {
                        target.BuffAdd(stun, this.BChar, false, 100, false, -1, false);
                    }

                    if (target.IsDead)
                    {
                        this.BChar.ParticleOut(this.MySkill, skill, this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main));
                    }
                    else
                    {
                        this.BChar.ParticleOut(this.MySkill, skill, target);
                    }
                }
            }
        }
    }
}