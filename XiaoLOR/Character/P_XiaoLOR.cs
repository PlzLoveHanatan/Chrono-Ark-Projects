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
using System.Windows.Markup;
using EmotionSystem;
using Steamworks;
using UnityEngine.Analytics;
using Spine;
using DG.Tweening;
using NLog.Targets;
using ChronoArkMod.ModData.Settings;
using static CharacterDocument;
using static UnityEngine.UI.Image;
using static DarkTonic.MasterAudio.MasterAudio;
namespace XiaoLOR
{
	/// <summary>
	/// XiaoLOR
	/// Passive:
	/// </summary>
	public class P_XiaoLOR : Passive_Char, IP_PlayerTurn, IP_EmotionLvUpBefore, IP_Draw, IP_SkillUse_BasicSkill, IP_DamageChange_sumoperation, IP_SkillUse_Target, IP_LevelUp, IP_BattleEndRewardChange
	{
		public override void Init()
		{
			OnePassive = true;
		}

		public void BattleEndRewardChange()
		{
			XiaoUtils.StopXiaoSong();
		}

		public void Turn()
		{
			if (BChar.EmotionLevel() >= 2)
			{
				foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
				{
					Utils.ApplyBurn(battleEnemy, BChar);
				}
			}

			if (BChar.EmotionLevel() >= 3)
			{
				Utils.ApplyExtended(Utils.AllyTeam.Skills, ModItemKeys.SkillExtended_Ex_XiaoLOR_FuriousFireRendingtheSkies, null, true, true, 2, true);
			}
		}

		public void SkillUseBasicSkill(Skill skill)
		{
			if (skill.Master == BChar)
			{
				BattleSystem.instance.StartCoroutine(XiaoLOR_Scripts.FixXiaoFixedAbillity(BChar));
			}
		}

		public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
		{
			if (BChar.EmotionLevel() >= 3 && SkillD.IsDamage && SkillD.Master == BChar && Target != null && !Target.Info.Ally && !Target.Dummy && !Target.IsDead)
			{
				PlusDamage += (int)(Damage * 0.15f);
			}
			else
			{
				PlusDamage = 0;
			}
		}

		// Note: this is before level up
		public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
		{
			if (charEmotion.BChar == BChar && charEmotion.Level + 1 == 2)
			{
				XiaoUtils.PlaySound("RoarEmotionalLevel");
				XiaoLOR_Scripts.StartChangeSkills(BChar);
			}

			if (charEmotion.BChar == BChar && charEmotion.Level + 1 == 4)
			{
				XiaoUtils.PlaySound("Roar");
				XiaoUtils.StartXiaoSong();
			}
		}

		public IEnumerator Draw(Skill Drawskill, bool NotDraw)
		{
			XiaoLOR_Scripts.CheckDrawSkill(BChar, Drawskill);
			yield break;
		}

		public void LevelUp()
		{
			XiaoLOR_Scripts.XiaoLevelUp(MyChar, MyChar.LV);
		}

		public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
		{
			if (BChar.EmotionLevel() >= 1 && SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
			{
				Utils.ApplyBurn(hit, BChar);

				if (BChar.EmotionLevel() >= 5)
				{
					Utils.ApplyBurn(hit, BChar);
				}
			}
		}
	}
}