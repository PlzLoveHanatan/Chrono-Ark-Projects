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
	/// The Forgotten
	/// </summary>
    public class S_LucyEGO_History_TheForgotten : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Countdown = 3;
        }
        
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("TheForgotten", 100f, null, 0f, null, null, false, false);

            if (BattleSystem.instance.EnemyCastSkills.Count == 0) return;

            var target = Targets[0];

            var targetSkills = BattleSystem.instance.EnemyCastSkills
                .Where(skill => skill.Usestate == target)
                .ToList();

            foreach (var targetSkill in targetSkills)
            {
                BattleSystem.instance.EnemyCastSkills.Remove(targetSkill);
                BattleSystem.instance.ActWindow.CastingWasteFixed(targetSkill);
            }
        }
    }
}