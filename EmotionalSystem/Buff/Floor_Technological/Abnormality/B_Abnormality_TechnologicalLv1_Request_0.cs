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
	/// Requested Target
	/// </summary>
    public class B_Abnormality_TechnologicalLv1_Request_0 : Buff, IP_PlayerTurn
    {
		public override void Init()
		{
			PlusStat.IgnoreTaunt_EnemySelf = true;
			PlusStat.dod = -10;
		}

		public void Turn()
		{
			SelfDestroy();
		}
	}
}