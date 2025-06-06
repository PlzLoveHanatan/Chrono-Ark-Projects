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
    /// Armor-piercing Ammunition
    /// If this skill is discarded by any Range Akari attack, apply the 'Armor Reduced!'  to the target.
    /// </summary>
    public class Armor_piercingAmmunition : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            MasterAudio.PlaySound("Gun_Normal1", 100f, null, 0f, null, null, false, false);
        }
    }
}