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
    public class B_EnemyAbnormality_BehaviorAdjustment : Buff
    {
        public override void BuffStat()
        {
            PlusStat.cri = 20f;
            PlusStat.hit = 20f;
        }
    }
}