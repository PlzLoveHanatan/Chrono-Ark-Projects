using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DarkTonic.MasterAudio;
using GameDataEditor;
using UnityEngine;

namespace EmotionSystem
{
	public partial class InvitationManager
	{
		private void CleanSkill(string skillKey, bool isExclude = true)
		{
			if (string.IsNullOrEmpty(skillKey)) return;

			// --- Deck ---
			Skill skillDeck = Utils.AllyTeam.Skills_Deck.Find(s => s.MySkill.KeyID == skillKey);
			while (skillDeck != null)
			{
				Utils.RemoveSkill(skillDeck);
				skillDeck = Utils.AllyTeam.Skills_Deck.Find(s => s.MySkill.KeyID == skillKey);
			}

			// --- Used Deck ---
			Skill skillDiscard = Utils.AllyTeam.Skills_UsedDeck.Find(s => s.MySkill.KeyID == skillKey);
			while (skillDiscard != null)
			{
				Utils.RemoveSkill(skillDiscard);
				skillDiscard = Utils.AllyTeam.Skills_UsedDeck.Find(s => s.MySkill.KeyID == skillKey);
			}

			// --- Hand ---
			Skill skillHand = Utils.AllyTeam.Skills.Find(s => s.MySkill.KeyID == skillKey);
			while (skillHand != null)
			{
				Utils.RemoveSkill(skillHand, isExclude);
				skillHand = Utils.AllyTeam.Skills.Find(s => s.MySkill.KeyID == skillKey);
			}
		}

		private void RemoveExtended(string extendedKey, bool isExclude = true, bool isDraw = false)
		{
			var skillHand = Utils.AllyTeam.Skills.Where(s => s?.ExtendedFind_DataName(extendedKey) != null).ToList();

			foreach (var skill in skillHand)
			{
				Utils.RemoveSkill(skill, isExclude);

				if (isDraw)
				{
					Utils.AllyTeam.Draw();
				}
			}
		}

		private void CleanDebuff(string debuffKey)
		{
			foreach (var ally in Utils.AllyTeam.AliveChars)
			{
				Utils.RemoveBuff(ally, debuffKey, true);
			}
		}

		private void CleanAfterWitch()
		{
			CleanSkill(GDEItemKeys.Skill_S_Witch_P_0);
			CleanSkill(GDEItemKeys.Skill_S_Witch_2);
			CleanDebuff(GDEItemKeys.Buff_B_Witch_P_0_T);
			CleanDebuff(GDEItemKeys.Buff_B_Witch_2_T);
			CleanDebuff(GDEItemKeys.Buff_B_Maid_T_1);
			CleanMusic();
		}

		private void CleanAfterGolem()
		{
			RemoveExtended(GDEItemKeys.SkillExtended_Golem_Ex_0, false, true);
			RemoveExtended(GDEItemKeys.SkillExtended_Golem_Ex_1, false, true);
			CleanMusic();
		}

		private void CleanAfterDorchi()
		{
			CleanDebuff(GDEItemKeys.Buff_B_DorchiX_0_T);
			CleanMusic();
		}

		private void CleanAfterTank()
		{
			CleanDebuff(GDEItemKeys.Buff_B_MBoss2_0_2_T);
			CleanDebuff(GDEItemKeys.Buff_B_MBoss2_0_3_T);
			CleanMinions();
			CleanMusic();
		}

		private void CleanAfterJoker()
		{
			CleanSkill(GDEItemKeys.Skill_S_Joker_0);
			CleanDebuff(GDEItemKeys.Buff_B_Joker_1_T);
			CleanDebuff(GDEItemKeys.Buff_B_Pierrot_Bat_0_T);
			CleanDebuff(GDEItemKeys.Buff_B_Pierrot_Bat_1_T);
			CleanDebuff(GDEItemKeys.Buff_B_Pierrot_Bat_2_T);
			CleanMinions();
			CleanMusic();
		}

		public bool CanKillBurningStake = false;

		private void CleanAfterShiranui()
		{
			CleanSkill(GDEItemKeys.Skill_S_Shiranui_Bonus_1);
			CleanSkill(GDEItemKeys.Skill_S_Shiranui_Bonus_2);
			CleanSkill(GDEItemKeys.Skill_S_Shiranui_Bonus_3);
			CleanDebuff(GDEItemKeys.Buff_B_Shiranui_3_T);
			CleanMusic();

			var ally = Utils.AllyTeam.AliveChars.FirstOrDefault(a => a.Info.KeyData == GDEItemKeys.Character_AllyDoll);

			if (ally != null)
			{
				CanKillBurningStake = true;
				ally.HP = 0;

				int safetyCounter = 0;
				while (!ally.IsDead && safetyCounter < 10)
				{
					ally.Info.G_get_stat.DeadImmune = -999;
					ally.Info.ForceGetStat();
					ally.Dead(true, true);
					safetyCounter++;
				}

				CanKillBurningStake = false;

				if (safetyCounter >= 10)
				{
					Debug.LogWarning($"[BURNING_STAKE] {ally.Info?.KeyData} не умер после 10 попыток!");
				}
			}

		}

		private void CleanAfterDealer()
		{
			BattleSystem.instance.MapObject.GetComponentInChildren<Animator>().SetBool("Gamble", false);

			if (B_CasinoGame.instance != null)
			{
				var field = typeof(B_CasinoGame).GetField("ScoreUI", BindingFlags.NonPublic | BindingFlags.Instance);
				if (field != null)
				{
					var ui = field.GetValue(B_CasinoGame.instance) as CasinoBossUI;
					if (ui != null)
					{
						BattleSystem.instance.StartCoroutine(ui.Co_SetActive(false));
					}
					BattleSystem.instance.CantTurnEnd = false;
				}
			}

			CleanSkill(GDEItemKeys.Skill_S_BlackJack_CheckDeck);
			CleanDebuff(GDEItemKeys.Buff_B_TheDealer_0);
			CleanDebuff(GDEItemKeys.Buff_B_TheDealer_1);
			CleanDebuff(GDEItemKeys.Buff_B_TheDealer_2);
			CleanMusic();
		}

		private void CleanAfterTwins()
		{
			CleanSkill(GDEItemKeys.Skill_S_S2_MainBoss_1_Lucy_0);
			CleanDebuff(GDEItemKeys.Buff_B_S2_Mainboss_1_LeftDebuff);
			CleanDebuff(GDEItemKeys.Buff_B_S2_Mainboss_1_RightDebuf);
			CleanMusic();
		}

		private void CleanAfterCLown()
		{
			CleanSkill(GDEItemKeys.Skill_S_BombClown_B_0);
			CleanDebuff(GDEItemKeys.Buff_B_BombClown_1_T);
			CleanMinions();
			CleanMusic();
		}

		private void CleanAfterTimeEater()
		{
			CleanSkill(GDEItemKeys.Skill_S_MBoss2_1_5);
			CleanDebuff(GDEItemKeys.Buff_B_Mboss2_1_P2);
			CleanDebuff(GDEItemKeys.Buff_B_Mboss2_1_P3);
			CleanDebuff(GDEItemKeys.Buff_B_MBoss2_1_4_T);
			CleanMinions();
			CleanMusic();

			var skillHand = Utils.AllyTeam.Skills;
			bool hasEGO = EmotionSystem_EGO_Button.instance.HasEGOSkill;

			//if (hasEGO)
			//{
			//	skillHand.Concat(EmotionSystem_EGO_Button.instance.EGOHand);
			//}

			//foreach (var skill in skillHand) // need check after boss death
			//{
			//	if (skill.Counting >= 9)
			//	{
			//		skill.MySkill.Reset_Counting();

			//		skill.Counting = 0;
			//	}

			//	if (skill.ExtendedFind("Extended_Mboss2_Skill_P") != null)
			//	{
			//		skill.ExtendedDelete("Extended_Mboss2_Skill_P");
			//	}
			//}
		}

		private void CleanAfterLeader()
		{
			CleanDebuff(GDEItemKeys.Buff_B_S3_Pope_P_2);
			CleanDebuff(GDEItemKeys.Buff_B_S3_Pope_1_T);
			CleanDebuff(GDEItemKeys.Buff_B_S3_Pope_2_T);
			CleanDebuff(GDEItemKeys.Buff_B_S3_Pope_3_T);
			CleanMusic();
		}

		private void CleanAfterKaraela()
		{
			CleanSkill(GDEItemKeys.Skill_S_S_TheLight_P_1);
			CleanDebuff(GDEItemKeys.Buff_TheLight_P_0);
			CleanDebuff(GDEItemKeys.Buff_B_TheLight_1_T);
			CleanDebuff(GDEItemKeys.Buff_B_TheLight_2_T);
			CleanMinions();
			CleanMusic();
		}

		private void CleanAfterReaper()
		{
			var markTransform = BattleSystem.instance.MainUICanvas.transform.Find("ReaperBossUI(Clone)");
			if (markTransform != null)
			{
				GameObject.Destroy(markTransform.gameObject);
			}
			CleanDebuff(GDEItemKeys.Buff_B_Enemy_Boss_Reaper_P_0);
			CleanDebuff(GDEItemKeys.Buff_B_Boss_Reaper_1_T);
			CleanDebuff(GDEItemKeys.Buff_B_Boss_Reaper_2_T);
			CleanMinions();
			CleanMusic();
		}

		private void CleanMinions()
		{
			using (List<BattleEnemy>.Enumerator enumerator2 = BattleSystem.instance.EnemyList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					BattleEnemy battleEnemy3 = enumerator2.Current;
					battleEnemy3.Info.Hp = 0;
					battleEnemy3.Dead(false, false);
				}
				return;
			}
		}

		private void CleanMusic(bool isPheonixTheme = false)
		{
			if (!FirstGuestInvitation && !SecondGuestInvitation)
			{
				return;
			}

			string song = null;
			string key = null;

			if (FirstGuestInvitation)
			{
				key = FirstGuestTheme;
			}
			else if (SecondGuestInvitation)
			{
				key = SecondGuestTheme;
			}

			if (isPheonixTheme)
			{
				song = "pheonix_theme";
			}
			else if (!string.IsNullOrEmpty(key))
			{
				ReceptionMusic.TryGetValue(key, out song);
			}

			if (string.IsNullOrEmpty(song))
			{
				return;
			}

			float volume = MasterAudio.MasterVolumeLevel;

			MasterAudio.FadeBusToVolume("FieldBGM", 0f, 1f, null, false, false);
			MasterAudio.FadeBusToVolume("BattleBGM", 0f, 1f, null, false, false);
			MasterAudio.StopBus("BGM");
			MasterAudio.StopBus("BGM");
			MasterAudio.StopBus("StoryBGM");
			MasterAudio.PlaySound(song, volume, null, 0f, null, null, false, false);

			if (SecondGuestInvitation && !FirstGuestInvitation)
			{
				SecondGuestInvitation = false;
			}

			if (FirstGuestInvitation)
			{
				FirstGuestInvitation = false;
			}
		}
	}
}
