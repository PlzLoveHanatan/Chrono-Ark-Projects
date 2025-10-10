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
	/// Rhythm 
	/// </summary>
    public class B_Abnormality_TechnologicalLv1_Rhythm : Buff, IP_SkillUse_User
    {
        public override void Init()
        {
            PlusPerStat.Damage = 20;
            PlusStat.DMGTaken = 20;
        }

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
			if (SkillD.Master == BChar && SkillD.IsDamage)
			{
				Utils.PlaySound("Floor_Technological_Rhythm");
			}
		}
	}
}