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
	/// <color=#DC143C>Music</color>
	/// <color=#919191>But nothing could compare to the music it makes when it eats a human.</color>
	/// </summary>
    public class S_Abnormality_TechnologicalLv3_Music_Neg : Skill_Extended
    {
		public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
		{
			Utils.PlaySound("Floor_Technological_Music");
		}

	}
}