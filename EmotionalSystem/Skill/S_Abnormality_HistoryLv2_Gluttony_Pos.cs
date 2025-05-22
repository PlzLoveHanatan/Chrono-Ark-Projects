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
    /// <color=#3CB371>Gluttony</color>
    /// <color=#919191>The fairies were no more than carnivorous monsters, and their "protection" was their method to keep the meat fresh.</color>
    /// </summary>
    public class S_Abnormality_HistoryLv2_Gluttony_Pos : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("TheFairiesCare", 100f, null, 0f, null, null, false, false);

            foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
            {
                if (ally != BChar)
                {
                    ally.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Gluttony_1, this.BChar, false, 0, false, -1, false);
                }
            }
        }
    }
}