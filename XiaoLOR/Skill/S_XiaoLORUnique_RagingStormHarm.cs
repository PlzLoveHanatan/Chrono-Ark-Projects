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
namespace XiaoLOR
{
    /// <summary>
    /// Raging Storm Harm
    /// </summary>
    public class S_XiaoLORUnique_RagingStormHarm : Skill_Extended
    {
        public override void FixedUpdate()
        {
            if (BChar.EmotionLevel() >= 5)
            {
                var RagingStormHarm = BattleSystem.instance.AllyTeam.Skills
                    .Where(skill => skill?.MySkill?.KeyID == ModItemKeys.Skill_S_XiaoLORUnique_RagingStormHarm)
                    .ToList();

                foreach (Skill skill in RagingStormHarm)
                {
                    if (RagingStormHarm != null)
                    {
                        var changeTo = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLORUnique_RagingStormLove, skill.Master, skill.Master.MyTeam);
                        skill.SkillChange(changeTo);
                    }
                }
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("Storm");

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 4);
                }

                BattleSystem.DelayInput(this.SecondCast(Targets));
            }
        }

        public IEnumerator SecondCast(List<BattleChar> Targets)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLORUnique_RagingStormHarm, this.BChar, this.BChar.MyTeam);
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