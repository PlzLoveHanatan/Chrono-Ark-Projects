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
	/// Paralysis
	/// </summary>
    public class B_EmotionalSystem_Paralysis : Buff, IP_SkillUse_User_After
	{
		public override void Init()
		{
			PlusPerStat.Damage = -20;
		}

		public void SkillUseAfter(Skill SkillD)
		{
			if (SkillD.IsDamage)
			{
				SelfDestroy();
			}
		}
	}
}