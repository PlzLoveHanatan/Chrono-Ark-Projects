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
	/// Miko Experience Pussy ♡♡
	/// Create a <color=#d78fe9>Miko Experience Pussy ♡♡♡</color> in hand.
	/// </summary>
    public class S_Xao_MikoExperiencePussy_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.PlayXaoVoice(BChar, true);
        }
    }
}