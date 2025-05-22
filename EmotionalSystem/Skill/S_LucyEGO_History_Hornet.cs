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
    /// <color=#ffc500>Hornet</color>
    /// </summary>
    public class S_LucyEGO_History_Hornet : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Countdown = 3;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Hornet", 100f, null, 0f, null, null, false, false);
        }
    }
}