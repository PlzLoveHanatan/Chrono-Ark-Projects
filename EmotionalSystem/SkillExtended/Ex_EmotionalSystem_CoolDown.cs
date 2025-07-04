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
        public Ex_EmotionalSystem_EGO MainEx;

        public override void FixedUpdate()
        {
            if (MainEx == null || MainEx.NowCountdown == 0)
            {
                SelfDestroy();
            }
            else if (MainEx != null)
            {
                BuffIconStackNum = MainEx.NowCountdown;
            }
        }

        //public void CoolDownUpdate()
        //{
        //    if (MainEx != null)
        //    {
        //        BuffIconStackNum = MainEx.NowCountdown;
        //    }
        //}
    }
}