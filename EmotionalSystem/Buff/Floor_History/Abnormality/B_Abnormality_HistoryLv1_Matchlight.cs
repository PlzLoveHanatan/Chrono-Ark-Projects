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
    /// Matchlight
    /// </summary>
    public class B_Abnormality_HistoryLv1_Matchlight : Buff, IP_SkillUse_Target
	{
		public override string DescExtended()
        {
            int damage = (int)(BChar.GetStat.maxhp * 0.2f);
            return base.DescExtended().Replace("&a", damage.ToString());
        }
        
        public override void Init()
		{
			PlusPerStat.Damage = 20;
			PlusStat.HIT_CC = 20f;
			PlusStat.HIT_DEBUFF = 20f;
			PlusStat.HIT_DOT = 20f;
		}

		public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
		{
			if (SP.SkillData.Master == BChar && DMG >= 1 && !SP.SkillData.PlusHit)
			{
				Utils.PlaySound("Floor_History_Matchlight");
				Utils.ApplyBurn(hit, BChar, 2);

				bool neverLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 20);

				if (neverLucky)
				{
					int damage = (int)(BChar.GetStat.maxhp * 0.2f);
					BChar.Damage(BChar, damage, false, true, true);
					Utils.PlaySound("Floor_History_Explode");
				}
			}
		}
    }
}