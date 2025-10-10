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
	/// Malice
	/// At the start of each turn, apply "Malicious Mark" to all enemies who do not already have it.
	/// </summary>
    public class B_Abnormality_HistoryLv3_Malice : Buff, IP_SkillUse_User
    {
		public override void Init()
		{
			PlusPerStat.Damage = 40;
			PlusStat.PlusCriDmg = 40;
			PlusStat.cri = 40;
		}

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
			if (SkillD.Master == BChar && !SkillD.FreeUse)
			{
				Utils.PlaySound("Floor_History_Malice");
			}
		}
	}
}