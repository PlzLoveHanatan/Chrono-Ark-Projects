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
using NLog.Targets;
namespace EmotionalSystem
{
	/// <summary>
	/// Violence
	/// </summary>
    public class B_Abnormality_TechnologicalLv1_Violence : Buff, IP_DamageChange, IP_SkillUse_User
    {
		public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
		{
			if (SkillD.IsDamage && SkillD.Master == BChar)
			{
				int randomNum = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, 4);

				switch (randomNum)
				{
					case 0: Damage = (int)(Damage * 0.5f); break;
					case 1: Damage = (int)(Damage * 1.5f); break;
					case 2: Damage = (int)(Damage * 2f); break;
					default:
						// No damage change
						break;
				}
			}
			return Damage;
		}

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
			if (SkillD.Master == BChar && SkillD.IsDamage)
			{
				Utils.PlaySound("Floor_Technological_Violence");
			}
		}
	}
}