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
using EmotionSystem;
namespace EmotionalSystem
{
    /// <summary>
    /// Colorless Depth
    /// </summary>
    public class B_EmotionalSystem_ColorlessDepth : Buff, IP_EmotionLvUpBefore, IP_Awake
    {
        public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
        {
            if (charEmotion.BChar == BChar && nextLevel > 0)
                SelfDestroy();
        }


        public override void Init()
        {
            PlusPerStat.Damage = -20;
            PlusPerStat.Heal = -20;
        }

        public override void BuffStat()
        {
            PlusPerStat.Damage = -20 * StackNum;
            PlusPerStat.Heal = -20 * StackNum;
        }

        public void Awake()
        {
            if (BChar.EmotionLevel() >= 5)
                SelfDestroy();
        }
    }
}