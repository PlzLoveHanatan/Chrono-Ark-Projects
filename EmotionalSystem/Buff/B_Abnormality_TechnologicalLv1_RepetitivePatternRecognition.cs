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
    /// Repetitive Pattern-Recognition
    /// </summary>
    public class B_Abnormality_TechnologicalLv1_RepetitivePatternRecognition : Buff, IP_SkillUse_User, IP_PlayerTurn, IP_Awake
    {
        private int ManaNextTurn;

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ManaNextTurn.ToString());
        }
        public void Awake()
        {
            BChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_0, this.BChar, false, 0, false, -1, false);
        }
        public override void BuffStat()
        {
            PlusStat.hit = 10;
        }
        public void Turn()
        {
            BChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_RepetitivePatternRecognition_0, this.BChar, false, 0, false, -1, false);

            if (ManaNextTurn >= 2)
            {
                BattleSystem.instance.AllyTeam.AP += 1;
                ManaNextTurn = 0;
            }
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && SkillD.IsDamage && !SkillD.FreeUse && !SkillD.PlusHit)
            {
                ManaNextTurn++;
            }
        }
    }
}