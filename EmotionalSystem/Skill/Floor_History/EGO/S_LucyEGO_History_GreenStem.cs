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
using NLog.Targets;
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
			Utils.PlaySound("Floor_History_Malice");

            if (Targets[0].BuffReturn(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_0, false) == null)
            {
                Utils.AddDebuff(Targets[0], BChar, ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_0);
            }
            else
            {
				Utils.AddDebuff(Targets[0], BChar, ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_1);
			}
        }
    }
}