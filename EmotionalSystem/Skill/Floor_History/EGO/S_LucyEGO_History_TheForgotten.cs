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
            Utils.PlaySound("Floor_History_Forgotten");

            if (BattleSystem.instance.EnemyCastSkills.Count > 0)
            {
				var target = Targets[0];
                var targetSkills = BattleSystem.instance.EnemyCastSkills.Where(skill => skill.Usestate == target).FirstOrDefault();

				for (int i = 0; i < 2; i++)
				{
					BattleSystem.instance.EnemyCastSkills.Remove(targetSkills);
					BattleSystem.instance.ActWindow.CastingWasteFixed(targetSkills);
				}
			}
        }
    }
}