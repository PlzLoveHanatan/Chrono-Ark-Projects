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
	/// <color=#ffc500>Regret</color>
	/// </summary>
	public class S_LucyEGO_Technological_Regret : Ex_EmotionalSystem_EGO
	{
		public override void Init()
		{
			base.Init();
			Cooldown = 3;
		}

		public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
		{
			Utils.PlaySound("Floor_Technological_Violence");

			if (Targets[0].BuffReturn(GDEItemKeys.Buff_B_Common_Rest) != null)
			{
				EmotionalSystem_Scripts.DestroyActions(Targets[0]);
			}
			BattleSystem.DelayInput(EmotionalSystem_Scripts.RecastSkill(Targets[0], BChar, MySkill.MySkill.KeyID, 2));
		}
	}
}