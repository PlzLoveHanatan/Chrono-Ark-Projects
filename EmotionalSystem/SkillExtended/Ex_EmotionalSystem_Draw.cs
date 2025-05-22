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
    public class Ex_EmotionalSystem_Draw : BuffSkillExHand
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            var mainBuff = (B_EmotionalSystem_Draw)MainBuff;
            if (mainBuff != null) BuffIconStackNum = ((B_EmotionalSystem_Draw)MainBuff).skillUse;
        }
    }
}