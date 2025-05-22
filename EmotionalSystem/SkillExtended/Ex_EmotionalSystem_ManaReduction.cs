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
using EmotionalSystem;
using Spine;
using System.EnterpriseServices;
namespace EmotionalSystem
{
    /// <summary>
    /// Cost 1 less
    /// </summary>
    public class Ex_EmotionalSystem_ManaReduction : BuffSkillExHand
    {
        public override void Init()
        {
            base.Init();
            APChange = -1;
        }
    }
}
