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
    public class B_Abnormality_HistoryLv1_NostalgicEmbraceoftheOldDays : Buff, IP_SkillUse_Target, IP_PlayerTurn
    {
        private bool OncePerTurn;

		public override void Init()
		{
			PlusStat.hit = 10;
		}

		public void Turn()
		{
			OncePerTurn = false;
		}

		public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
		{
			if (SP.SkillData.Master == BChar && DMG >= 1)
            {
                Utils.PlaySound("Floor_History_Embrace");

                bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 25);

                if (alwaysLucky && !OncePerTurn)
                {
					Utils.AddDebuff(hit, BChar, GDEItemKeys.Buff_B_Common_Rest, (int)(BChar.GetStat.HIT_CC + 100));
                    OncePerTurn = true;
				}
			}
		}
	}
}