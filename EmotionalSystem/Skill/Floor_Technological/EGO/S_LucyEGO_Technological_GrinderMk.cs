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
    /// <color=#ffc500>Grinder Mk. 5-2</color>
    /// Inflict 10 <color=#5e0000>Bleed</color>.
    /// If this skill defeat an enemy, restore 1 Mana.
    /// </summary>
    public class S_LucyEGO_Technological_GrinderMk : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Cooldown = 3;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.PlaySound("Floor_Technological_Clean");
            Utils.ApplyBleed(Targets[0], BChar, 15);
		}
    }
}