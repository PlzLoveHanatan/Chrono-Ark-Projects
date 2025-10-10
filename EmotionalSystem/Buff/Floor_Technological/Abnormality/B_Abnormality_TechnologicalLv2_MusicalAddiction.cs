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
	/// Musical Addiction
	/// </summary>
    public class B_Abnormality_TechnologicalLv2_MusicalAddiction : Buff, IP_SkillUse_User
    {
        public override void BuffStat()
        {
            PlusPerStat.Damage = 40;
            PlusStat.DMGTaken = 40;
        }

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
			if (SkillD.Master == BChar && SkillD.IsDamage)
			{
				Utils.PlaySound("Floor_Technological_Musical");
			}
		}
	}
}