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
    public class B_Abnormality_TechnologicalLv1_RepetitivePatternRecognition : Buff, IP_SkillUse_User, IP_PlayerTurn
    {
        private bool ManaNextTurn;

		public override void Init()
		{
            PlusStat.cri = 10;
		}

        public void Turn()
        {
            if (ManaNextTurn)
            {
                Utils.AllyTeam.AP += 1;
            }
			ManaNextTurn = false;
		}

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar && SkillD.IsDamage)
            {
                Utils.PlaySound("Floor_Technological_Repetitive");
                ManaNextTurn = true;
            }
        }
    }
}