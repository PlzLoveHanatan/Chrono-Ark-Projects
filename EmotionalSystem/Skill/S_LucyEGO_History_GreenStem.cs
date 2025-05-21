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
    /// Green Stem
    /// Apply "Vines of Despair" if the target is affected by the "Entangled" debuff.
    /// Otherwise apply "Entagled".
    /// </summary>
    public class S_LucyEGO_History_GreenStem : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Countdown = 3;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("GreenStem", 100f, null, 0f, null, null, false, false);

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    if (target.BuffReturn(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_0, false) != null)
                    {
                        target.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_2, this.BChar, false, 0, false, -1, false);
                    }
                    else
                    {
                        target.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_0, this.BChar, false, 0, false, -1, false);
                    }
                }
            }            
        }
    }
}