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
    /// Flooding Bullets
    /// </summary>
    public class S_Synchronize_Technological_FloodingBullets : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.DelayInput(EmotionalSystem_Scripts.RecastSkill(Targets[0], BChar, MySkill.MySkill.KeyID, 2));
        }
    }
}