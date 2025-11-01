using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;

namespace EmotionSystem
{
	public partial class InvitationManager
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
		public bool SpecialReward = false;

		public bool FirstGuestInvitation = false;
		public bool SecondGuestInvitation = false;

		public string FirstGuestTheme;
		public string SecondGuestTheme;

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

		public readonly Dictionary<Type, Action> ReceptionCleaning = new Dictionary<Type, Action>
		{
			{ typeof(B_Witch_P_0), () => { InvitationManager.Instance.CleanAfterWitch(); } },
			{ typeof(P_Golem_0), () => { InvitationManager.Instance.CleanAfterGolem(); } },
			{ typeof(P_DorchiX), () => { InvitationManager.Instance.CleanAfterDorchi(); } },
			{ typeof(B_S2_Tank_P ), () => { InvitationManager.Instance.CleanAfterTank(); } },
			{ typeof(B_Joker_P_0 ), () => { InvitationManager.Instance.CleanAfterJoker(); } },
			{ typeof(P_Shiranui ), () => { InvitationManager.Instance.CleanAfterShiranui(); } },
			{ typeof(P_TheDealer ), () => { InvitationManager.Instance.CleanAfterDealer(); } },
			{ typeof(P_S2_MainBoss_1_Left ), () => { InvitationManager.Instance.CleanAfterTwins(); } },
			{ typeof(P_BombClown_0 ), () => { InvitationManager.Instance.CleanAfterCLown(); } },
			{ typeof(B_MBoss2_1_P ), () => { InvitationManager.Instance.CleanAfterTimeEater(); } },
			{ typeof(B_S3_Boss_Pope_P_0 ), () => { InvitationManager.Instance.CleanAfterLeader(); } },
			{ typeof(TheLight_P_1 ), () => { InvitationManager.Instance.CleanAfterKaraela(); } },
			{ typeof(B_Enemy_Boss_Reaper_P ), () => { InvitationManager.Instance.CleanAfterReaper(); } },
		};

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

		public void StartNewReception(string nextQueue)
		{
			FieldSystem.instance.BattleStart(new GDEEnemyQueueData(nextQueue), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
		}

		public void DoubleBattle()
		{
			Debug.Log("[Invitation] DoubleBattle triggered — starting next reception (Reaper).");

			StartNewReception(GDEItemKeys.EnemyQueue_Queue_S3_Reaper);
			FieldSystem.instance.BattleAfterDelegate = null;
		}
	}
}
