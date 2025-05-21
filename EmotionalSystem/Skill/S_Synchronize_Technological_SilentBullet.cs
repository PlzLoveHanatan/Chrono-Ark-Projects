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
    /// <color=#3c8dbc>Silent Bullet</color>
    /// </summary>
    public class S_Synchronize_Technological_SilentBullet : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    var skillToRemove = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(skill => skill.Usestate == target);

                    if (skillToRemove != null)
                    {
                        BattleSystem.instance.EnemyCastSkills.Remove(skillToRemove);
                        BattleSystem.instance.ActWindow.CastingWasteFixed(skillToRemove);
                    }
                }
            }
        }
    }
}