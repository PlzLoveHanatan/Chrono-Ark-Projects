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
using UnityEngine.Experimental.UIElements.StyleEnums;
namespace SuperHero
{
    public class S_SuperHero_TestSkill : Skill_Extended
    {
        public override bool Terms()
        {
            if (BChar.Info.KeyData == ModItemKeys.Character_SuperHero)
                return true; // any checks you need to return to be able play your fixed ability

            return false;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var buff1 = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var heroComplex = BChar.BuffReturn(buff1, false) as B_SuperHero_HeroComplex; // return buff
            var buff2 = ModItemKeys.Buff_B_SuperHero_HeroComplex_0;
            var skillJustice = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice;
            var skillJustice1 = ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1;

            Skill skill = null;

            if (BChar.BuffReturn(buff1, false) != null && BChar.BuffReturn(buff2, false) != null)
            {
                skill = Skill.TempSkill(skillJustice, BChar, BChar.MyTeam);
            }
            else if (heroComplex != null && heroComplex.StackNum >= 2)
            {
                skill = Skill.TempSkill(skillJustice1, BChar, BChar.MyTeam);
                heroComplex.SelfDestroy();
                heroComplex.SelfStackDestroy();
            }

            if (skill != null)
            {
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, Targets[0], false, false, true, null));
            }
            else
            {
                Debug.LogWarning("[SkillUseSingle] No valid skill to cast.");
            }
        }
    }
}