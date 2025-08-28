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
using System.Diagnostics;
namespace Xao
{
	/// <summary>
	/// Swimsuit Day ♡♡♡
	/// </summary>
    public class S_Xao_SwimsuitDay_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.CopyAndExtendDebuffs(Targets[0]);
            Utils.PlayXaoVoice(BChar, true);
        }
    }
}