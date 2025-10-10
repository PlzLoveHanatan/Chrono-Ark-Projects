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
    /// Clean
    /// </summary>
    public class B_Abnormality_TechnologicalLv2_Clean : Buff, IP_DamageChange, IP_SkillUse_User
    {
		public override void Init()
		{
			PlusStat.cri = 20;
			PlusStat.PlusCriDmg = 20;
		}

		public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
		{
			if (SkillD.IsDamage && SkillD.Master == BChar && Target is BattleEnemy enemy)
			{
				BattleEnemy battleEnemy = Target as BattleEnemy;

				if (enemy.SkillQueue.Count == 0 || battleEnemy.SkillQueue[0].CastSpeed >= 9)
				{
					Damage += (int)(Damage * 0.2);
				}
			}
			return Damage;
		}

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
			if (SkillD.Master == BChar && SkillD.IsDamage)
			{
				Utils.PlaySound("Floor_Technological_Clean");
			}
		}
	}
}