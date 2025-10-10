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
    /// E.G.O. Cooldwon
    /// Something &a
    /// </summary>
    public class Ex_EmotionalSystem_CoolDown : Skill_Extended
    {
        public Ex_EmotionalSystem_EGO EGO_Extended;

        public override void FixedUpdate()
        {
            if (EGO_Extended == null || EGO_Extended.Cooldown == 0)
            {
                SelfDestroy();
            }
            else if (EGO_Extended != null)
            {
                BuffIconStackNum = EGO_Extended.NowCooldown;
            }
        }
    }
}