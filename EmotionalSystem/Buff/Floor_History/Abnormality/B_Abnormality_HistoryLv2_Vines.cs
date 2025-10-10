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
    /// Vines
    /// </summary>
    public class B_Abnormality_HistoryLv2_Vines : Buff, IP_Awake, IP_PlayerTurn_1
    {
		public override void Init()
		{
			PlusStat.DMGTaken = -20;
			PlusStat.RES_CC = 20f;
			PlusStat.RES_DEBUFF = 20f;
			PlusStat.RES_DOT = 20f;
		}

		public void Awake()
        {
			ApplyVines();
		}

		public void Turn1()
		{
            ApplyVines();
		}

        public void ApplyVines()
        {
			var enemies = Utils.EnemyTeam.AliveChars;
			int index = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, enemies.Count);
			var randomEnemy = enemies[index];

            Utils.PlaySound("Floor_History_Vines");
			Utils.AddDebuff(randomEnemy, BChar, ModItemKeys.Buff_B_Abnormality_HistoryLv2_Vines_0);
		}
	}
}