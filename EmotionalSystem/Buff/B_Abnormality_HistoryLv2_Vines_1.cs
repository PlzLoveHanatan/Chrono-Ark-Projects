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
    public class B_Abnormality_HistoryLv2_Vines_1 : Buff, IP_SpecialEnemyTargetSelect
    {
        public bool SpecialEnemyTargetSelect(Skill skill, BattleEnemy Target)
        {
            return Target.BuffFind(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_0, false) || Target.GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0;
        }
    }
}