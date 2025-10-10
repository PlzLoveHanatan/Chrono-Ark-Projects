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
using static CharacterDocument;
using NLog.Targets;
using Steamworks;
namespace EmotionalSystem
{
    public class B_Abnormality_TechnologicalLv1_Request : Buff, IP_PlayerTurn_1, IP_Awake
    {
		public override void Init()
		{
			PlusPerStat.Damage = 10;
		}

		public void Awake()
		{
			ApplyRequestedTarget();
		}

		public void Turn1()
		{
			ApplyRequestedTarget();
		}

		public void ApplyRequestedTarget()
		{
			var enemies = Utils.EnemyTeam.AliveChars;
			int index = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, enemies.Count);
			var randomEnemy = enemies[index];

			Utils.PlaySound("Floor_Technological_Request");
			Utils.AddDebuff(randomEnemy, BChar, ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Request_0);
		}
	}
}