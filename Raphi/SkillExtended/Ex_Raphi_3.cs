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
using Spine;

namespace Raphi
{
    /// <summary>
    /// Cast this skill
    /// </summary>
    public class Ex_Raphi_3 : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_0
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_9_Rare
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_8_Rare
                && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_5;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BChar.BuffAdd(ModItemKeys.Buff_B_CelestialConnection_0, BChar, false, 0, false, -1, false);
        }
    }
}