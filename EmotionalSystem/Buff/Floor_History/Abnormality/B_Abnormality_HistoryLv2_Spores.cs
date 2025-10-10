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
    /// Spores
    /// </summary>
    public class B_Abnormality_HistoryLv2_Spores : Buff, IP_Hit, IP_Dodge
    {
        public override void BuffStat()
        {
            PlusStat.Strength = true;
            PlusStat.AggroPer = 40;
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
            Utils.PlaySound("Floor_History_Spores");
            Utils.ApplyBleed(enemy, BChar, 4);
            Utils.ApplyBurn(enemy, BChar, 4);
        }
    }
}