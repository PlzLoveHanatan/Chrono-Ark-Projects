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
namespace Akari
{
    /// <summary>
    /// Flame Ammunition
    /// If this skill is discarded by any Akari attack, apply the 'Bleeding' to the target.
    /// </summary>
    public class FlameAmmunition : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            MasterAudio.PlaySound("Gun_Normal1", 100f, null, 0f, null, null, false, false);
        }
    }
}