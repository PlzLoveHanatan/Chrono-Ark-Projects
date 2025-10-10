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
    /// <color=#ffc500>Solemn Lament</color>
    /// Recast this skill 8 times.
    /// </summary>
    public class S_LucyEGO_Technological_SolemnLament : Skill_Extended
    {
        public override void Init()
        {
            base.Init();
			//Cooldown = 3;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.DelayInput(EmotionalSystem_Scripts.RecastSkill(Targets[0], BChar, MySkill.MySkill.KeyID, 8));
        }
    }
}