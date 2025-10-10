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
    /// Coffin
    /// </summary>
    public class B_Abnormality_TechnologicalLv3_Coffin : Buff, IP_SkillUse_User, IP_PlayerTurn
    {
        private bool OncePerTurn;

		public override string DescExtended()
		{
			string text = OncePerTurn ? "Inactive" : "Active";
			return base.DescExtended().Replace("&a", text.ToString());
		}

        public void Turn()
        {
            OncePerTurn = false;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && !OncePerTurn)
            {
                EmotionalSystem_Scripts.DestroyActions(Targets[0]);
                Utils.PlaySound("Floor_Technological_Coffin");
                OncePerTurn = true;
			}
        }
    }
}