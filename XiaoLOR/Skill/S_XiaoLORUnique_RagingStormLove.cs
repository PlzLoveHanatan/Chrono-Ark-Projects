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
using EmotionalSystem;
namespace XiaoLOR
{
    /// <summary>
    /// Raging Storm Love
    /// Inflict 3 <color=#f8181c>Burn</color>.
    /// Destroy the target's Action Point.
    /// Cast this skill again.
    /// </summary>
    public class S_XiaoLORUnique_RagingStormLove : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("StormStrong", 100f, null, 0f, null, null, false, false);

            BattleSystem.DelayInput(this.Damage(Targets));

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 5);
                }

                if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;

                var targetSkills = BattleSystem.instance.EnemyCastSkills
                    .Where(skill => skill.Usestate == target)
                    .ToList();

                for (int i = 0; i < Math.Min(2, targetSkills.Count); i++)
                {   
                    var skill = targetSkills[i];
                    BattleSystem.instance.EnemyCastSkills.Remove(skill);
                    BattleSystem.instance.ActWindow.CastingWasteFixed(skill);
                }
            }
        }

        public IEnumerator Damage(List<BattleChar> Targets)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLORUnique_RagingStormLove, this.BChar, this.BChar.MyTeam);
            skill2.PlusHit = true;
            skill2.FreeUse = true;

            this.BChar.ParticleOut(this.MySkill, skill2, Targets);

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 5);
                }
            }
        }
    }
}