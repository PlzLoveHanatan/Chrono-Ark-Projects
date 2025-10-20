using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Curse;
using DarkTonic.MasterAudio;
using GameDataEditor;
using UnityEngine;
using static EmotionalSystem.DataStore;

namespace EmotionalSystem
{
	public class InvitationManager
	{
		private static InvitationManager _Instance;

		public static InvitationManager Instance
		{
			get
			{
				if (_Instance == null)
				{
					_Instance = new InvitationManager();
				}
				return _Instance;
			}
		}

		public bool InvitationActive = false;
		public int RewardMultiplier = 1;
		public bool SpecialCase = false;

		public bool FirstGuestInvitation = false;
		public bool SecondGuestInvitation = false;

		public string FirstGuestTheme;
		public string SecondGuestTheme;

		public class GuestSequence
		{
			public string FirstGuest;
			public string SecondGuest;
			public int FogTurn;

			public GuestSequence(string guest1, string guest2, int fogTurn)
			{
				this.FirstGuest = guest1;
				this.SecondGuest = guest2;
				this.FogTurn = fogTurn;
			}
		}

		private readonly Dictionary<string, GuestSequence> ReceptionChains = new Dictionary<string, GuestSequence>()
		{
			// stage 1
			{ GDEItemKeys.EnemyQueue_Garden_Midboss, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_MBoss_0, null, 12) },
			{ GDEItemKeys.EnemyQueue_Queue_MBoss_0, new GuestSequence(GDEItemKeys.EnemyQueue_Garden_Midboss, null, 12) },
			
			// stage 1-1
			{ GDEItemKeys.EnemyQueue_Queue_Witch, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_Golem, null, 14) },
			{ GDEItemKeys.EnemyQueue_Queue_Golem, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_Witch, null, 14) },
			{ GDEItemKeys.EnemyQueue_Queue_DorchiX, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_Witch, GDEItemKeys.EnemyQueue_Queue_Golem, 21) },
			
			// stage 2-1
			{ GDEItemKeys.EnemyQueue_Queue_S2_Joker, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_MBoss2_0, null, 14) },
			{ GDEItemKeys.EnemyQueue_Queue_MBoss2_0, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S2_Joker, null, 14) },
			{ GDEItemKeys.EnemyQueue_Casino_Queue, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S2_Joker, GDEItemKeys.EnemyQueue_Queue_MBoss2_0, 17) },
			{ GDEItemKeys.EnemyQueue_Shiranui_Queue, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S2_Joker, GDEItemKeys.EnemyQueue_Queue_MBoss2_0, 15) },
			
			// stage 2-2
			{ GDEItemKeys.EnemyQueue_Queue_S2_TimeEater, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S2_BombClown, GDEItemKeys.EnemyQueue_Queue_S2_MainBoss_Luby, 19) },
			{ GDEItemKeys.EnemyQueue_Queue_S2_BombClown, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S2_MainBoss_Luby, GDEItemKeys.EnemyQueue_Queue_S2_TimeEater, 19) },
			{ GDEItemKeys.EnemyQueue_Queue_S2_MainBoss_Luby, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S2_BombClown, GDEItemKeys.EnemyQueue_Queue_S2_TimeEater, 19) },
			
			//stage 3-1
			{ GDEItemKeys.EnemyQueue_Queue_S3_PharosLeader, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S3_Reaper, GDEItemKeys.EnemyQueue_Queue_S3_TheLight, 21) },
			{ GDEItemKeys.EnemyQueue_Queue_S3_Reaper, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S3_PharosLeader, GDEItemKeys.EnemyQueue_Queue_S3_TheLight, 21) },
			{ GDEItemKeys.EnemyQueue_Queue_S3_TheLight, new GuestSequence(GDEItemKeys.EnemyQueue_Queue_S3_PharosLeader, GDEItemKeys.EnemyQueue_Queue_S3_Reaper, 21) },
		};

		public void ApplyReception(BattleSystem system)
		{
			if (system == null || system.MainQueueData == null)
			{
				return;
			}

			string currentBoss = system.MainQueueData.Key;

			if (currentBoss == GDEItemKeys.EnemyQueue_Queue_FanaticBoss)
			{
				Debug.Log("[ApplyReception] Fighting Fanatic.");
				SpecialCase = true;

				system.EnemyWaveData = new WaveData();
				system.MainQueueData.Wave2 = null;
				system.MainQueueData.Wave3 = null;
				RewardMultiplier = 1;

				return;
			}


			if (string.IsNullOrEmpty(currentBoss))
			{
				Debug.LogWarning("[Invitation] Current boss key is null or empty.");
				return;
			}

			if (!ReceptionChains.ContainsKey(currentBoss))
			{
				Debug.LogWarning("[Invitation] No reception data found for boss: " + currentBoss);
				return;
			}

			GuestSequence sequence = ReceptionChains[currentBoss];

			if (!string.IsNullOrEmpty(sequence.FirstGuest))
			{
				FirstGuestInvitation = true;

				if (!string.IsNullOrEmpty(sequence.SecondGuest))
				{
					SecondGuestInvitation = true;

					if (RandomManager.RandomInt(RandomClassKey.Boss, 0, 2) == 0)
					{
						(sequence.SecondGuest, sequence.FirstGuest) = (sequence.FirstGuest, sequence.SecondGuest);
					}
				}
			}

			FirstGuestTheme = sequence.FirstGuest;

			if (!string.IsNullOrEmpty(sequence.SecondGuest))
			{
				SecondGuestTheme = sequence.SecondGuest;
			}
			else
			{
				SecondGuestTheme = null;
			}

			if (system.EnemyWaveData == null)
			{
				system.EnemyWaveData = new WaveData();
			}

			if (system.EnemyWaveData != null && SpecialCase)
			{
				system.EnemyWaveData = new WaveData();
			}

			if (!string.IsNullOrEmpty(sequence.FirstGuest))
			{
				system.MainQueueData.Wave2 = LoadWave(sequence.FirstGuest);
				system.MainQueueData.Wave2Turn = 99;
				system.EnemyWaveData.wave2turn = 99;
				system.EnemyWaveData.wave2out = false;
				Debug.Log("[Invitation] Wave2 loaded: " + sequence.FirstGuest);
			}

			if (!string.IsNullOrEmpty(sequence.SecondGuest))
			{
				system.MainQueueData.Wave3 = LoadWave(sequence.SecondGuest);
				system.MainQueueData.Wave3Turn = 99;
				system.EnemyWaveData.wave3turn = 99;
				system.EnemyWaveData.wave3out = false;
				Debug.Log("[Invitation] Wave3 loaded: " + sequence.SecondGuest);
			}

			if (sequence.FogTurn > 0)
			{
				system.MainQueueData.CustomeFogTurn = sequence.FogTurn;
				system.FogTurn = sequence.FogTurn;
			}

			InvitationActive = false;
			RewardMultiplier = 1;

			if (!string.IsNullOrEmpty(sequence.FirstGuest))
			{
				InvitationActive = true;

				if (!string.IsNullOrEmpty(sequence.SecondGuest))
				{
					RewardMultiplier = 2;
				}
			}
		}

		public List<GDEEnemyData> LoadWave(string bossKey)
		{
			List<GDEEnemyData> result = new List<GDEEnemyData>();

			if (string.IsNullOrEmpty(bossKey) ||
				GDEDataManager.masterData == null ||
				!GDEDataManager.masterData.ContainsKey(bossKey) ||
				!(GDEDataManager.masterData[bossKey] is Dictionary<string, object> data) ||
				!data.ContainsKey("Enemys") ||
				!(data["Enemys"] is List<object> rawList))
			{
				return result;
			}

			foreach (object obj in rawList)
			{
				if (obj is string enemyKey)
				{
					result.Add(new GDEEnemyData(enemyKey));
				}
			}

			return result;
		}

		public void StartNewReception(string nextQueue)
		{
			Debug.Log($"[Invitation] Restarting battle immediately for next guest: {nextQueue}");
			FieldSystem.instance.BattleStart(new GDEEnemyQueueData(nextQueue), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
		}

		public readonly List<ItemBase> ReceptionRewards = new List<ItemBase>();

		public void PrepareReceptionRewards(int totalRewards = 1)
		{
			ReceptionRewards.Clear();

			int soulStones = 2;
			int timeMoney = PlayData.TSavedata.StageNum + 2;

			AddReward(GDEItemKeys.Item_Misc_TimeMoney, timeMoney, totalRewards);
			AddReward(GDEItemKeys.Item_Misc_Soul, soulStones, totalRewards);
			AddReward(GDEItemKeys.Item_Consume_SkillBookCharacter, 1, totalRewards);
			AddReward(ModItemKeys.Item_Consume_C_EmotionalSystem_DreamingCurrent, 1);

			if (PlayData.TSavedata.StageNum <= 4)
			{
				AddReward(GDEItemKeys.Item_Consume_FriendShipPouch, 1, totalRewards);
			}

			if (RandomManager.RandomPer(RandomClassKey.BattleClear, 100, 25))
			{
				AddReward(GDEItemKeys.Item_Misc_Item_Key, 1, totalRewards);
			}

			if (RandomManager.RandomPer(RandomClassKey.BattleClear, 100, 15))
			{
				AddReward(GDEItemKeys.Item_Consume_ArtifactPouch, 1, totalRewards);
			}
		}

		public void AddReward(string itemKey, int itemAmount = 1, int repeatCount = 1)
		{
			if (string.IsNullOrEmpty(itemKey)) return;

			for (int i = 0; i < repeatCount; i++)
			{
				ReceptionRewards.Add(ItemBase.GetItem(itemKey, itemAmount));
			}
		}

		public readonly Dictionary<Type, Action> ReceptionCleaning = new Dictionary<Type, Action>
		{
			{ typeof(B_Witch_P_0), () => { Instance.CleanAfterWitch(); } },
			{ typeof(P_Golem_0), () => { Instance.CleanAfterGolem(); } },
			{ typeof(P_DorchiX), () => { Instance.CleanAfterDorchi(); } },
			{ typeof(B_S2_Tank_P ), () => { Instance.CleanAfterTank(); } },
			{ typeof(B_Joker_P_0 ), () => { Instance.CleanAfterJoker(); } },
			{ typeof(P_Shiranui ), () => { Instance.CleanAfterShiranui(); } },
			{ typeof(P_TheDealer ), () => { Instance.CleanAfterDealer(); } },
			{ typeof(P_S2_MainBoss_1_Left ), () => { Instance.CleanAfterTwins(); } },
			{ typeof(P_BombClown_0 ), () => { Instance.CleanAfterCLown(); } },
			{ typeof(B_MBoss2_1_P ), () => { Instance.CleanAfterTimeEater(); } },
			{ typeof(B_S3_Boss_Pope_P_0 ), () => { Instance.CleanAfterLeader(); } },
			{ typeof(TheLight_P_1 ), () => { Instance.CleanAfterKaraela(); } },
			{ typeof(B_Enemy_Boss_Reaper_P ), () => { Instance.CleanAfterReaper(); } },
		};

		private void CleanSkill(string skillKey, bool isExclude = true)
		{
			if (string.IsNullOrEmpty(skillKey)) return;

			var skillDeck = Utils.AllyTeam.Skills_Deck.Where(s => s.MySkill.KeyID == skillKey).ToList();

			foreach (var skill in skillDeck)
			{
				Utils.RemoveSkill(skill);
			}

			var skillDiscard = Utils.AllyTeam.Skills_UsedDeck.Where(s => s.MySkill.KeyID == skillKey).ToList();

			foreach (var skill in skillDiscard)
			{
				Utils.RemoveSkill(skill);
			}

			var skillHand = Utils.AllyTeam.Skills.Where(s => s.MySkill.KeyID == skillKey).ToList();

			foreach (var skill in skillHand)
			{
				Utils.RemoveSkill(skill, isExclude);
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
				ally?.Dead(true, true);

				if (!ally.IsDead)
				{
					ally.Dead(true, true);
					CanKillBurningStake = false;
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
			bool hasEGO = EmotionalSystem_EGO_Button.instance.HasEGOSkill;

			//if (hasEGO)
			//{
			//	skillHand.Concat(EmotionalSystem_EGO_Button.instance.EGOHand);
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

		private readonly Dictionary<string, string> ReceptionMusic = new Dictionary<string, string>
		{
			{ GDEItemKeys.EnemyQueue_Queue_MBoss_0, "CA_Boss01" },
			{ GDEItemKeys.EnemyQueue_Garden_Midboss, "CA_Boss01" },
			{ GDEItemKeys.EnemyQueue_Queue_Witch, "04 Hope for Existence (Boss intro)" },
			{ GDEItemKeys.EnemyQueue_Queue_Golem, "CA_Boss01" },
			{ GDEItemKeys.EnemyQueue_Queue_S2_Joker, "06 Show Time (Boss Front)" },
			{ GDEItemKeys.EnemyQueue_Queue_MBoss2_0, "2st_Boss" },
			{ GDEItemKeys.EnemyQueue_Queue_S2_MainBoss_Luby, "2st_Boss" },
			{ GDEItemKeys.EnemyQueue_Queue_S2_TimeEater, "2st_Boss" },
		};
	}
}
