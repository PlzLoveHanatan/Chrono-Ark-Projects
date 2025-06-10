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
    /// Inner Ardor
    /// If the target doesn't have Taunt status :
    /// Apply Taunt status on the target, and 
    /// Gain 2 random Emotional Coins.
    /// If the target has Taunt status : All allies gain 2 random Emotonal Coins.
    /// </summary>
    public class S_XiaoLORLv1_InnerArdor : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("NormalHit", 100f, null, 0f, null, null, false, false);

            var target = Targets[0];
            target.BuffAdd(GDEItemKeys.Buff_B_EnemyTaunt, this.BChar, false, 0, false, -1, false);
            Utils.GiveEmotionsToChar(this.BChar, 2, SkillD.GetPosUI());
        }
    }
}