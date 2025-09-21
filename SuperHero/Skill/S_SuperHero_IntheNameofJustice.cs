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

        public override string DescExtended(string desc)
        {
            string text = Utils.SuperHeroMod(BChar) ? ModLocalization.InTheNameOfJustice_1 : ModLocalization.InTheNameOfJustice_0;
            return base.DescExtended(desc).Replace("Description", text);
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.CreateSkill(BChar, ModItemKeys.Skill_S_SuperHero_IntheNameofJustice, true, true, 1, 1, true, true);
            BattleSystem.DelayInput(RestoreMana(Targets[0]));
            Utils.ApplyJusticeMark(BChar);
        }   

        public IEnumerator RestoreMana(BattleChar target)
        {
            if (!target.IsDead) yield break;

            BattleSystem.instance.AllyTeam.AP += 2;
        }
    }
}