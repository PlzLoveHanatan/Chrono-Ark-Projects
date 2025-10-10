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
    public class B_Abnormality_HistoryLv3_BarrierofThorns : Buff, IP_Hit, IP_Dodge
    {
        public override void BuffStat()
        {
            PlusStat.DMGTaken = -40;
            PlusStat.DeadImmune = 40;
			PlusStat.RES_CC = 40f;
			PlusStat.RES_DEBUFF = 40f;
			PlusStat.RES_DOT = 40f;
		}

		public void Hit(SkillParticle SP, int Dmg, bool Cri)
		{
			if (SP.SkillData.IsDamage && !SP.SkillData.PlusHit)
			{
				ApplyDebuffs(SP.UseStatus);
			}
		}

		public void Dodge(BattleChar Char, SkillParticle SP)
		{
			if (Char == BChar && SP.SkillData.IsDamage && !SP.SkillData.PlusHit)
			{
				ApplyDebuffs(SP.UseStatus);
			}
		}

		private void ApplyDebuffs(BattleChar enemy)
		{
			Utils.PlaySound("Floor_History_BarrierThorns");
			Utils.AddDebuff(enemy, BChar, "");
		}
	}
}