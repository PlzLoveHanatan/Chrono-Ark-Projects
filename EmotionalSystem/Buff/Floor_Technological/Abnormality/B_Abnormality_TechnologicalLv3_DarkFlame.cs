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
	/// Tödlicher Akkord
	/// </summary>
    public class B_Abnormality_TechnologicalLv3_DarkFlame : Buff, IP_Awake
    {
		public override void Init()
		{
			PlusPerStat.Damage = 40;
			PlusStat.spd = 2;
		}

		public void Awake()
		{
			foreach (var target in Utils.AllyTeam.AliveChars.Concat(Utils.EnemyTeam.AliveChars_Vanish))
			{
                Utils.AddBuff(target, ModItemKeys.Buff_B_Abnormality_TechnologicalLv3_DarkFlame_0);
			}
		}
    }
}