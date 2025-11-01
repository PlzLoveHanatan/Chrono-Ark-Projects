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
using EmotionSystem;
namespace XiaoLOR
{
	/// <summary>
	/// Fiery Dragon Slash
	/// </summary>
    public class S_XiaoLORLv1_FieryDragonSlash : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
			XiaoUtils.PlaySound("NormalHit2");
            Utils.ApplyBurn(Targets[0], this.BChar);
        }
    }
}