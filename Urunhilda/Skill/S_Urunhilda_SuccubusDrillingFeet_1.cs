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
namespace Urunhilda
{
	/// <summary>
	/// Succubus Caress Feet
	/// Create a <color=#d78fe9>Succubus Squeeze Feet</color> in hand.
	/// </summary>
    public class S_Urunhilda_SuccubusDrillingFeet_1 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.CreateSkill(ModItemKeys.Skill_S_Urunhilda_SuccubusDrillingFeet_2, BChar);
        }
    }
}