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
namespace Xiao
{
	/// <summary>
	/// Flow of the Sword
	/// </summary>
    public class S_XiaoLv1_FlowoftheSword : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("NormalHit2", 100f, null, 0f, null, null, false, false);

            Utils.ApplyBurn(Targets[0], this.BChar, 2);
        }
    }
}