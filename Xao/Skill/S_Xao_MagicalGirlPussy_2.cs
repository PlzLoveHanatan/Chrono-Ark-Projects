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
namespace Xao
{
	/// <summary>
	/// Magical Girl Pussy ♡♡
	/// Create a <color=#d78fe9>Magical Girl Pussy ♡♡♡</color> in hand.
	/// </summary>
    public class S_Xao_MagicalGirlPussy_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.CreateSkill(ModItemKeys.Skill_S_Xao_MagicalGirlPussy_3, BChar, true, true, 2, 1);
        }
    }
}