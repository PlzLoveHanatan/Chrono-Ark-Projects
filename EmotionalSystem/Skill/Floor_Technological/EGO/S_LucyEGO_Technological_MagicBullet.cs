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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace EmotionalSystem
{
    /// <summary>
    /// <color=#ffc500>Magic Bullet</color>
    /// </summary>
    public class S_LucyEGO_Technological_MagicBullet : Ex_EmotionalSystem_EGO
    {
        public override void Init()
        {
            base.Init();
            Cooldown = 3;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            EmotionalSystem_Scripts.DeSynchronize(BChar);
            Utils.AllyTeam.Draw(2);
        }
    }
}