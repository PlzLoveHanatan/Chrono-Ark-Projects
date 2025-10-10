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
	/// Gain Ignore Taunt on all skills.
	/// Deal 30% increased damage to enemies with only 1 or 2 Actions Counts.
	/// Otherwise, deal 15% less.
	/// </summary>
	public class B_Abnormality_HistoryLv1_DisplayofAffection : Buff, IP_SkillUse_User, IP_DamageChange
	{
		public override void Init()
		{
			PlusStat.IgnoreTaunt = true;
			PlusStat.hit = 20;
		}

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
			if (SkillD.IsDamage && SkillD.Master == BChar)
			{
				Utils.PlaySound("Floor_History_DisplayAffection");
			}
		}

		public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
		{
			if (SkillD.IsDamage && SkillD.Master == BChar && Target is BattleEnemy enemy)
			{
				bool additionalDamage = enemy.SkillQueue[0].CastSpeed == 0 || enemy.SkillQueue[0].CastSpeed == 1 || enemy.SkillQueue[0].CastSpeed >= 9;

				if (enemy.SkillQueue.Count > 0 && additionalDamage)
				{
					Damage += (int)(Damage * 0.2);
				}
				else
				{
					Damage -= (int)(Damage * 0.4);
				}
			}
			return Damage;
		}
	}
}