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
    /// <color=#DC143C>Loyalty</color>
    /// <color=#919191>The loyalty of bees is a naturally developed instinct. If we discover a way to draw forth that instinct, many things could change.</color>
    /// </summary>
    public class S_Abnormality_HistoryLv3_Loyalty_Neg : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Loyality", 100f, null, 0f, null, null, false, false);

            foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
            {
                if (ally != BChar)
                {
                    ally.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv3_Loyalty, this.BChar, false, 0, false, -1, false);
                }
            }
            Targets[0].Dead(false, false);
        }
    }
}