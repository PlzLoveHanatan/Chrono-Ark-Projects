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
	public class B_Abnormality_TechnologicalLv2_Recharge : Buff, IP_Kill
	{
		public override void Init()
		{
			PlusStat.cri = 20;
			PlusStat.hit = 20;
		}

		public void KillEffect(SkillParticle SP)
		{
			foreach (var target in SP.TargetChar)
			{
				if (target is BattleEnemy enemy && enemy.IsDead)
				{
					Utils.PlaySound("Floor_Technological_Recharge");
					Utils.AllyTeam.AP += 2;
				}
			}
		}
	}
}