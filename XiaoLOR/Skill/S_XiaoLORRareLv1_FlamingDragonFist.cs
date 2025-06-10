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
using EmotionalSystem;
namespace XiaoLOR
{
	/// <summary>
	/// Flaming Dragon Fist
	/// Inflict 3 <color=#f8181c>Burn</color>.
	/// </summary>
    public class S_XiaoLORRareLv1_FlamingDragonFist : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("RushDown", 100f, null, 0f, null, null, false, false);

            Utils.ApplyBurn(Targets[0], this.BChar, 3);
        }
    }
}