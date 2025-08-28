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
namespace Xao
{
	/// <summary>
	/// Cowgirl Pleasure ♥♥♥
	/// </summary>
    public class S_Xao_CowGirl_Love_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Xao_Combo.CurrentCombo >= 5)
            {
                foreach (var target in Targets)
                {
                    if (target != null)
                    {
                        BattleSystem.DelayInputAfter(Utils.ApplyPleasureLock(target, BChar));
                    }
                }
            }

            if (Xao_Combo.CurrentCombo >= 7)
            {
                if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;

                CastingSkill targetSkill = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(skill => skill.skill.Master == Targets[0]);

                if (targetSkill != null)
                {
                    BattleSystem.instance.EnemyCastSkills.Remove(targetSkill);
                    BattleSystem.instance.ActWindow.CastingWasteFixed(targetSkill);
                }
            }
            Utils.PlayXaoVoice(BChar, true);
        }
    }
}