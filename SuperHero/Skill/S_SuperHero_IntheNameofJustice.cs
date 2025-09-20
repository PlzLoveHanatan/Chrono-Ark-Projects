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
        public override bool Terms()
        {
            return BChar.Info.KeyData == ModItemKeys.Character_SuperHero;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_IntheNameofJustice, this.BChar, this.BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);
            skill.AutoDelete = 1;
            skill.isExcept = true;

            var target = Targets[0];

            BattleSystem.DelayInput(RestoreMana(target));

        }

        public IEnumerator RestoreMana(BattleChar target)
        {
            if (!target.IsDead) yield break;

            BattleSystem.instance.AllyTeam.AP += 2;

            yield return null;
        }
    }
}