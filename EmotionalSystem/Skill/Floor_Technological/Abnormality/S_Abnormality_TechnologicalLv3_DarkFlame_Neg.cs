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
namespace EmotionalSystem
{
	/// <summary>
	/// <color=#DC143C>Dark Flame</color>
	/// Apply 'Gebrochener Pakt' to all targets on a field.
	/// <color=#919191>One day, the marksman realized the Devil no longer followed him. He pondered why, then realized that his soul had already fallen to Hell from the beginning.</color>
	/// </summary>
    public class S_Abnormality_TechnologicalLv3_DarkFlame_Neg : Skill_Extended
    {
		public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
		{
			Utils.PlaySound("Floor_Technological_DarkFlame");
		}
	}
}