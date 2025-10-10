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
	/// Magic Bullet
	/// Synchronized with Der Freischütz.
	/// </summary>
	public class B_LucyEGO_Technological_MagicBullet : Buff, IP_Awake, IP_TurnEnd, IP_PlayerTurn
	{
		private int synchronizedTurns = 3;

		public void Awake()
		{
			synchronizedTurns = 3;
			EmotionalSystem_Scripts.SynchronizeWithEGO(BChar, ModItemKeys.Skill_S_Synchronize_Technological_Desynchronize, EmotionalSystem_DataStore.DerSkills);
		}

		public void Turn()
		{
			if (synchronizedTurns <= 0)
			{
				EmotionalSystem_Scripts.DeSynchronize(BChar);
			}
		}

		public void TurnEnd()
		{
			synchronizedTurns--;
		}
	}
}