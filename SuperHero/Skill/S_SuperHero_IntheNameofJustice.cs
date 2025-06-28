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
namespace SuperHero
{
    /// <summary>
    /// In the Name of Justice
    /// </summary>
    public class S_SuperHero_IntheNameofJustice : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];
            if (target.IsDead)
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_IntheNameofJustice, BChar, BChar.MyTeam);
                skill.isExcept = true;
                skill.AutoDelete = 2;
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }
        }
    }
}