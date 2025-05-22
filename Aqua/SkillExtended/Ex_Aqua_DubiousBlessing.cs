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
namespace Aqua
{
	/// <summary>
	/// Dubious Blessing
	/// </summary>
    public class Ex_Aqua_DubiousBlessing : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1 && MainSkill.IsDamage;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            PlusSkillPerFinal.Damage = 25;
            BChar.BuffAdd(ModItemKeys.Buff_B_Aqua_DubiousBlessing, BChar, false, 0, false, -1, false);
        }
    }
}