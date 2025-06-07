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
using EmotionalSystem;
namespace Xiao
{
    /// <summary>
    /// Raging Storm Harm
    /// </summary>
    public class S_XiaoUnique_RagingStormHarm : Skill_Extended
    {
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 5)
            {
                var RagingStormHarm = BattleSystem.instance.AllyTeam.Skills
                    .Where(skill => skill?.MySkill?.KeyID == ModItemKeys.Skill_S_XiaoUnique_RagingStormHarm)
                    .ToList();

                foreach (Skill skill in RagingStormHarm)
                {
                    if (RagingStormHarm != null)
                    {
                        var changeTo = Skill.TempSkill(ModItemKeys.Skill_S_XiaoUnique_RagingStormLove, skill.Master, skill.Master.MyTeam);
                        skill.SkillChange(changeTo);
                    }
                }
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Storm", 100f, null, 0f, null, null, false, false);

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 4);
                }

                BattleSystem.DelayInput(this.SecondCast(Targets));


                //if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;

                //var targetSkill = BattleSystem.instance.EnemyCastSkills
                //    .FirstOrDefault(skill => skill.Usestate == target);

                //if (targetSkill != null)
                //{
                //    BattleSystem.instance.EnemyCastSkills.Remove(targetSkill);
                //    BattleSystem.instance.ActWindow.CastingWasteFixed(targetSkill);
                //}
            }
        }
        public IEnumerator SecondCast(List<BattleChar> Targets)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_S_XiaoUnique_RagingStormHarm, this.BChar, this.BChar.MyTeam);
            skill2.PlusHit = true;
            skill2.FreeUse = true;

            this.BChar.ParticleOut(this.MySkill, skill2, Targets);

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 4);
                }
            }
        }
    }
}