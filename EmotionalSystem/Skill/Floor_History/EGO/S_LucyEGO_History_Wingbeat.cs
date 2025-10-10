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
	/// Wingbeat
	/// </summary>
	public class S_LucyEGO_History_Wingbeat : Ex_EmotionalSystem_EGO
	{
		public override void Init()
		{
			base.Init();
			Countdown = 3;
		}

		public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
		{
			Utils.PlaySound("Floor_History_Wingbeat");
			BattleSystem.DelayInput(CallHeal());
			BattleSystem.DelayInput(RecastSkill(Targets[0]));
		}

		public IEnumerator RecastSkill(BattleChar Target)
		{
			for (int i = 0; i < 2; i++)
			{
				yield return new WaitForSecondsRealtime(0.3f);

				Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_LucyEGO_History_Wingbeat, BChar, BChar.MyTeam);
				skill.PlusHit = true;
				skill.FreeUse = true;

				if (Target.IsDead)
				{
					BChar.ParticleOut(MySkill, skill, BChar.BattleInfo.EnemyList.Random(BChar.GetRandomClass().Main));
				}
				else
				{
					BChar.ParticleOut(MySkill, skill, Target);
				}
				yield return CallHeal();
			}
			yield break;
		}

		public IEnumerator CallHeal()
		{
			yield return new WaitForSecondsRealtime(0.3f);
			Utils.PlaySound("Floor_History_Wingbeat");
			yield return Utils.HealingParticle(BChar, BattleSystem.instance.DummyChar, 8, true, true, true);
		}
	}
}