using System.Collections.Generic;
using GameDataEditor;
using EmotionalSystem;
using UnityEngine;
using System.Collections;
using System;
using I2.Loc;

namespace EmotionalSystem
{ 
	public class DataStore
	{
		private static DataStore _Instance;

		public static DataStore Instance
		{
			get
			{
				if (_Instance == null)
				{
					_Instance = new DataStore();
				}
				return _Instance;
			}
		}

		public Synchronize Synchronization { get; } = new Synchronize();
		public AbnormalityData Abnormalities { get; } = new AbnormalityData();
		public EGOData EGO { get; } = new EGOData();
		public EnemyData Enemies { get; } = new EnemyData();
		public VisualUi Visual { get; } = new VisualUi();

		public class Synchronize
		{
			public Dictionary<BattleChar, CharacterSkills> SavedSkills = new Dictionary<BattleChar, CharacterSkills>();

			public class CharacterSkills
			{
				public List<Skill> Hand { get; } = new List<Skill>();
				public List<Skill> DrawPile { get; } = new List<Skill>();
				public List<Skill> DiscardPile { get; } = new List<Skill>();
				public List<string> FixedAbility { get; } = new List<string>();
			}

			public readonly List<string> DerSkills = new List<string>
			{
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_FloodingBullets,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_FloodingBullets,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_InevitableBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_InevitableBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_MagicBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_MagicBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_SilentBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_SilentBullet
			};
		}

		public class AbnormalityData
		{
			public readonly List<Abnormality> History = new List<Abnormality>
			{
				// Lv1 — Positive
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_Ashes, AbnoType.Pos, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_HappyMemories, AbnoType.Pos, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_NostalgicEmbrace, AbnoType.Pos, 1),

				// Lv1 — Negative
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_DisplayAffection, AbnoType.Neg, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_FairiesCare, AbnoType.Neg, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv1_Matchlight, AbnoType.Neg, 1),

				// Lv2 — Positive
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Gluttony, AbnoType.Pos, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Vines, AbnoType.Pos, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Spores, AbnoType.Pos, 2),

				// Lv2 — Negative
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Footfalls, AbnoType.Neg, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_Predation, AbnoType.Neg, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv2_WorkerBee, AbnoType.Neg, 2),

				// Lv3
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv3_Malice, AbnoType.Pos, 3),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv3_BarrierThorns, AbnoType.Pos, 3),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_HistoryLv3_Loyalty, AbnoType.Neg, 3),
			};

			public readonly List<string> HistoryKeys = new List<string>
			{
				// Lv1 — Positive
				ModItemKeys.Skill_S_Abnormality_HistoryLv1_Ashes,
				ModItemKeys.Skill_S_Abnormality_HistoryLv1_HappyMemories,
				ModItemKeys.Skill_S_Abnormality_HistoryLv1_NostalgicEmbrace,

				// Lv1 — Negative
				ModItemKeys.Skill_S_Abnormality_HistoryLv1_DisplayAffection,
				ModItemKeys.Skill_S_Abnormality_HistoryLv1_FairiesCare,
				ModItemKeys.Skill_S_Abnormality_HistoryLv1_Matchlight,

				// Lv2 — Positive
				ModItemKeys.Skill_S_Abnormality_HistoryLv2_Gluttony,
				ModItemKeys.Skill_S_Abnormality_HistoryLv2_Vines,
				ModItemKeys.Skill_S_Abnormality_HistoryLv2_Spores,

				// Lv2 — Negative
				ModItemKeys.Skill_S_Abnormality_HistoryLv2_Footfalls,
				ModItemKeys.Skill_S_Abnormality_HistoryLv2_Predation,
				ModItemKeys.Skill_S_Abnormality_HistoryLv2_WorkerBee,

				// Lv3
				ModItemKeys.Skill_S_Abnormality_HistoryLv3_Malice,
				ModItemKeys.Skill_S_Abnormality_HistoryLv3_BarrierThorns,
				ModItemKeys.Skill_S_Abnormality_HistoryLv3_Loyalty,
			};

			public readonly List<Abnormality> Technological = new List<Abnormality>
			{
				// Lv1 — Positive
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_MetallicRinging, AbnoType.Pos, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_RepetitivePattern, AbnoType.Pos, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Request, AbnoType.Pos, 1),

				// Lv1 — Negative
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Lament, AbnoType.Neg, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Rhythm, AbnoType.Neg, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Violence, AbnoType.Neg, 1),

				// Lv2 — Positive
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_Clean, AbnoType.Pos, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_EternalRest, AbnoType.Pos, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_Recharge, AbnoType.Pos, 2),

				// Lv2 — Negative
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_ChainedWrath, AbnoType.Neg, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_MusicalAddiction, AbnoType.Neg, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_SeventhBullet, AbnoType.Neg, 2),
				
				// Lv3
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Coffin, AbnoType.Pos, 3),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Music, AbnoType.Neg, 3),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_DarkFlame, AbnoType.Neg, 3),
			};

			public readonly List<string> TechnologicalKeys = new List<string>
			{
				// Lv1 — Positive
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_MetallicRinging,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_RepetitivePattern,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Request,

				// Lv1 — Negative
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Lament,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Rhythm,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv1_Violence,
				
				// Lv2 — Positive
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_Clean,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_EternalRest,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_Recharge,

				// Lv2 — Negative
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_ChainedWrath,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_MusicalAddiction,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv2_SeventhBullet,
				
				// Lv3
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Coffin,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_Music,
				ModItemKeys.Skill_S_Abnormality_TechnologicalLv3_DarkFlame,
			};

			public readonly List<Abnormality> Literature = new List<Abnormality>
			{
				// Lv1 — Positive
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv1_Cocoon, AbnoType.Pos, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv1_SocialDistancing, AbnoType.Pos, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv1_SurpriseGift, AbnoType.Pos, 1),

				// Lv1 — Negative
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv1_Axe, AbnoType.Neg, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv1_Glitter, AbnoType.Neg, 1),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv1_LookDay, AbnoType.Neg, 1),

				// Lv2 — Positive
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Alertness, AbnoType.Pos, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Meal, AbnoType.Pos, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Shyness, AbnoType.Pos, 2),

				// Lv2 — Negative
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Friend, AbnoType.Neg, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv2_FunnyPrank, AbnoType.Neg, 2),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Obsession, AbnoType.Neg, 2),
				
				// Lv3
				new Abnormality(ModItemKeys.Buff_B_Abnormality_LiteratureLv3_GooeyWaste, AbnoType.Pos, 3),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv3_LovingFamily, AbnoType.Pos, 3),
				new Abnormality(ModItemKeys.Skill_S_Abnormality_Literature_Lv3_WornParasol, AbnoType.Neg, 3),
			};

			public readonly List<string> LiteratureKeys = new List<string>
			{
				// Lv1 — Positive
				ModItemKeys.Skill_S_Abnormality_Literature_Lv1_Cocoon,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv1_SocialDistancing,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv1_SurpriseGift,

				// Lv1 — Negative
				ModItemKeys.Skill_S_Abnormality_Literature_Lv1_Axe,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv1_Glitter,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv1_LookDay,
				
				// Lv2 — Positive
				ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Alertness,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Meal,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Shyness,

				// Lv2 — Negative
				ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Friend,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv2_FunnyPrank,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv2_Obsession,
				
				// Lv3
				ModItemKeys.Skill_S_Abnormality_Literature_Lv3_GooeyWaste,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv3_LovingFamily,
				ModItemKeys.Skill_S_Abnormality_Literature_Lv3_WornParasol,
			};
		}

		public class EGOData
		{
			public readonly List<string> HistoryKeyList = new List<string>
			{
				ModItemKeys.Skill_S_EGO_History_FourthMatchFlame,
				ModItemKeys.Skill_S_EGO_History_GreenStem,
				ModItemKeys.Skill_S_EGO_History_Hornet,
				ModItemKeys.Skill_S_EGO_History_TheForgotten,
				ModItemKeys.Skill_S_EGO_History_Wingbeat,
			};

			public readonly List<string> TechnologicalKeyList = new List<string>
			{
				ModItemKeys.Skill_S_EGO_Technological_GrinderMk,
				ModItemKeys.Skill_S_EGO_Technological_Harmony,
				ModItemKeys.Skill_S_EGO_Technological_MagicBullet,
				ModItemKeys.Skill_S_EGO_Technological_Regret,
				ModItemKeys.Skill_S_EGO_Technological_SolemnLament,
			};

			public readonly List<string> LiteratureKeyList = new List<string>
			{
				ModItemKeys.Skill_S_EGO_Literature_BlackSwan,
				ModItemKeys.Skill_S_EGO_Literature_Laetitia,
				ModItemKeys.Skill_S_EGO_Literature_RedEyes,
				ModItemKeys.Skill_S_EGO_Literature_SanguineDesire,
				ModItemKeys.Skill_S_EGO_Literature_TodayExpression,
			};
		}


		public class EnemyData
		{
			public readonly List<Abnormality> AbnormalityEnemy = new List<Abnormality>
			{
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv1_Despair, AbnoType.Neg, 1),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv1_Stress, AbnoType.Neg, 1),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv1_YouMustbeHappy, AbnoType.Neg, 1),

				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv1_GiantMushroom, AbnoType.Pos, 1),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv1_Strengthen, AbnoType.Pos, 1),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity, AbnoType.Pos, 1),

				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv2_MirrorAdjustment, AbnoType.Pos, 2),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv2_Present, AbnoType.Pos, 2),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter, AbnoType.Pos, 2),

				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv2_BehaviourAdjustment, AbnoType.Neg, 2),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv2_EnergyConversion, AbnoType.Neg, 2),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv2_Storytime, AbnoType.Neg, 2),

				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv3_Bait, AbnoType.Pos, 3),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv3_DimensionalRefraction, AbnoType.Neg, 3),
				new Abnormality(ModItemKeys.Buff_B_Abnormality_GuestLv3_CycleCurse, AbnoType.Neg, 3),
			};

			public readonly Dictionary<string, List<string>> BossActions = new Dictionary<string, List<string>>()
			{
				{ GDEItemKeys.Enemy_MBoss_0, new List<string> { GDEItemKeys.Skill_S_MBoss_0, GDEItemKeys.Skill_S_MBoss_1, GDEItemKeys.Skill_S_MBoss_2 } },
				{ GDEItemKeys.Enemy_S1_ArmorBoss, new List<string> { GDEItemKeys.Skill_S_Armor_0, GDEItemKeys.Skill_S_Armor_2, GDEItemKeys.Skill_S_Armor_3 } },
				{ GDEItemKeys.Enemy_S1_WitchBoss, new List<string> { ModItemKeys.Skill_S_Guest_CursePain, ModItemKeys.Skill_S_Guest_CurseWeak} },
				{ GDEItemKeys.Enemy_Boss_Golem, new List<string> { GDEItemKeys.Skill_S_Golem_1, GDEItemKeys.Skill_S_Golem_2 } },
				{ GDEItemKeys.Enemy_S1_BossDorchiX, new List<string> { GDEItemKeys.Skill_S_DorchiX_1, GDEItemKeys.Skill_S_DorchiX_2 } },
				{ GDEItemKeys.Enemy_MBoss2_0, new List<string> { GDEItemKeys.Skill_S_MBoss2_0_2, GDEItemKeys.Skill_S_MBoss2_0_3 } },
				{ GDEItemKeys.Enemy_S2_Joker, new List<string> { GDEItemKeys.Skill_S_Joker_1, GDEItemKeys.Skill_S_Joker_2 } },
				{ GDEItemKeys.Enemy_S2_Shiranui, new List<string> { GDEItemKeys.Skill_S_Shiranui_1, GDEItemKeys.Skill_S_Shiranui_3 } },
				{ GDEItemKeys.Enemy_TheDealer, new List<string> { GDEItemKeys.Skill_S_TheDealer_1, GDEItemKeys.Skill_S_TheDealer_2, GDEItemKeys.Skill_S_TheDealer_3} },
				{ GDEItemKeys.Enemy_S2_MainBoss_1_0, new List<string> { GDEItemKeys.Skill_S_S2_Mainboss_1_Left_0, GDEItemKeys.Skill_S_S2_Mainboss_1_Left_1, GDEItemKeys.Skill_S_S2_Mainboss_1_Left_2 } },
				{ GDEItemKeys.Enemy_S2_MainBoss_1_1, new List<string> { GDEItemKeys.Skill_S_S2_Mainboss_1_Right_0, GDEItemKeys.Skill_S_S2_Mainboss_1_Right_1, GDEItemKeys.Skill_S_S2_Mainboss_1_Right_2 } },
				{ GDEItemKeys.Enemy_S2_BombClownBoss, new List<string> { GDEItemKeys.Skill_S_BombClown_P, GDEItemKeys.Skill_S_BombClown_0, GDEItemKeys.Skill_S_BombClown_1 } },
				{ GDEItemKeys.Enemy_MBoss2_1, new List<string> { GDEItemKeys.Skill_S_MBoss2_1_1, GDEItemKeys.Skill_S_MBoss2_1_2, GDEItemKeys.Skill_S_MBoss2_1_3 } },
				{ GDEItemKeys.Enemy_SR_GunManBoss, new List<string> { GDEItemKeys.Skill_S_Gunman_0, GDEItemKeys.Skill_S_Gunman_1 } },
				{ GDEItemKeys.Enemy_S3_Boss_Pope, new List<string> { GDEItemKeys.Skill_S_S3_Pope_1, GDEItemKeys.Skill_S_S3_Pope_2, GDEItemKeys.Skill_S_S3_Pope_3} },
				{ GDEItemKeys.Enemy_S3_Boss_TheLight, new List<string> { GDEItemKeys.Skill_S_TheLight_1, GDEItemKeys.Skill_S_TheLight_2, GDEItemKeys.Skill_S_TheLight_3 } },
				{ GDEItemKeys.Enemy_S3_Boss_Reaper, new List<string> { GDEItemKeys.Skill_S_Boss_Reaper_1, GDEItemKeys.Skill_S_Boss_Reaper_2 } },
				{ GDEItemKeys.Enemy_S3_FanaticBoss, new List<string> { GDEItemKeys.Skill_S_FanaticBoss_2, GDEItemKeys.Skill_S_FanaticBoss_3 } },
				{ GDEItemKeys.Enemy_LBossFirst, new List<string> { GDEItemKeys.Skill_S_LBossFirst_0, GDEItemKeys.Skill_S_LBossFirst_2, GDEItemKeys.Skill_S_LBossFirst_3 } },
				{ GDEItemKeys.Enemy_S4_King_0, new List<string> { GDEItemKeys.Skill_S_S4_King_P1_1, GDEItemKeys.Skill_S_S4_King_P1_2, GDEItemKeys.Skill_S_S4_King_P2_0, GDEItemKeys.Skill_S_S4_King_P2_1 } },
				{ GDEItemKeys.Enemy_ProgramMaster, new List<string> { GDEItemKeys.Skill_S_ProgramMaster_0, GDEItemKeys.Skill_S_ProgramMaster_1 } },
				{ GDEItemKeys.Enemy_ProgramMaster_0, new List<string> { GDEItemKeys.Skill_S_ProgramMaster_0_Bomb } },
			};

			public readonly Dictionary<string, List<string>> BannedBossAbnormality = new Dictionary<string, List<string>>()
			{
				{ GDEItemKeys.Enemy_MBoss_0, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
				{ GDEItemKeys.Enemy_S1_ArmorBoss, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
				{ GDEItemKeys.Enemy_S1_WitchBoss, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Strengthen, ModItemKeys.Buff_B_Abnormality_GuestLv1_Despair, ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity} },
				{ GDEItemKeys.Enemy_Boss_Golem, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
				{ GDEItemKeys.Enemy_S1_BossDorchiX, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
				{ GDEItemKeys.Enemy_MBoss2_0, new List<string> { } },
				{ GDEItemKeys.Enemy_S2_Joker, new List<string> { } },
				{ GDEItemKeys.Enemy_S2_Shiranui, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
				{ GDEItemKeys.Enemy_TheDealer, new List<string> { } },
				{ GDEItemKeys.Enemy_S2_MainBoss_1_0, new List<string> { } },
				{ GDEItemKeys.Enemy_S2_MainBoss_1_1, new List<string> { } },
				{ GDEItemKeys.Enemy_S2_BombClownBoss, new List<string> { } },
				{ GDEItemKeys.Enemy_MBoss2_1, new List<string> { } },
				{ GDEItemKeys.Enemy_SR_GunManBoss, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
				{ GDEItemKeys.Enemy_S3_Boss_Pope, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
				{ GDEItemKeys.Enemy_S3_Boss_TheLight, new List<string> { } },
				{ GDEItemKeys.Enemy_S3_Boss_Reaper, new List<string> { } },
				{ GDEItemKeys.Enemy_S3_FanaticBoss, new List<string> { } },
				{ GDEItemKeys.Enemy_LBossFirst, new List<string> { } },
				{ GDEItemKeys.Enemy_S4_King_0, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
				{ GDEItemKeys.Enemy_ProgramMaster, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity } },
			};

			public readonly Dictionary<string, List<string>> BannedEnemyAbnormality = new Dictionary<string, List<string>>()
			{
				{ GDEItemKeys.Enemy_S2_Ballon, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } },
				{ GDEItemKeys.Enemy_S2_BoomBalloon, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } },
				{ GDEItemKeys.Enemy_S2_HealBallon, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } },
				{ GDEItemKeys.Enemy_S4_King_minion_0, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } },
				{ GDEItemKeys.Enemy_S4_King_minion_1, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } }
			};

			//public static List<string> AbnormalityEnemyBuff = new List<string>
			//{
			//	ModItemKeys.Buff_B_Abnormality_GuestLv1_Despair,
			//	ModItemKeys.Buff_B_Abnormality_GuestLv1_Stress,
			//	ModItemKeys.Buff_B_Abnormality_GuestLv1_YouMustbeHappy,

			//	ModItemKeys.Skill_S_Abnormality_GuestLv1_GiantMushroom,
			//	ModItemKeys.Buff_B_Abnormality_GuestLv1_Strengthen,
			//	ModItemKeys.Buff_B_Abnormality_GuestLv1_Unity,
			//};

			public readonly List<string> EnemyAbnormalityKeyList = new List<string>
			{
				// Lv1 — Positive
				ModItemKeys.Skill_S_Abnormality_GuestLv1_GiantMushroom,
				ModItemKeys.Skill_S_Abnormality_GuestLv1_Strengthen,
				ModItemKeys.Skill_S_Abnormality_GuestLv1_Unity,

				// Lv1 — Negative
				ModItemKeys.Skill_S_Abnormality_GuestLv1_Despair,
				ModItemKeys.Skill_S_Abnormality_GuestLv1_Stress,
				ModItemKeys.Skill_S_Abnormality_GuestLv1_YouMustbeHappy,

				// Lv2 — Positive
				ModItemKeys.Skill_S_Abnormality_GuestLv2_Shelter,
				ModItemKeys.Skill_S_Abnormality_GuestLv2_MirrorAdjustment,
				ModItemKeys.Skill_S_Abnormality_GuestLv2_Present,

				// Lv2 — Negative
				ModItemKeys.Skill_S_Abnormality_GuestLv2_BehaviourAdjustment,
				ModItemKeys.Skill_S_Abnormality_GuestLv2_EnergyConversion,
				ModItemKeys.Skill_S_Abnormality_GuestLv2_Storytime,

				// Lv 3
				ModItemKeys.Skill_S_Abnormality_GuestLv3_Bait,
				ModItemKeys.Skill_S_Abnormality_GuestLv3_CycleCurse,
				ModItemKeys.Skill_S_Abnormality_GuestLv3_DimensionalRefraction,

			};

		}

		public class VisualUi
		{
			public enum SpriteTypeFace
			{
				Face_VeryHappy,
				Face_Happy,
				Face_Normal,
				Face_Angry,
				Face_VeryAngry,
			}

			public readonly Dictionary<SpriteTypeFace, string> SpritePathsFace = new Dictionary<SpriteTypeFace, string>()
			{
				{ SpriteTypeFace.Face_VeryHappy, "Visual/Face/VeryHappy.png" },
				{ SpriteTypeFace.Face_Happy, "Visual/Face/Happy.png" },
				{ SpriteTypeFace.Face_Normal, "Visual/Face/Normal.png" },
				{ SpriteTypeFace.Face_Angry, "Visual/Face/Angry.png" },
				{ SpriteTypeFace.Face_VeryAngry, "Visual/Face/VeryAngry.png" },
			};

			public readonly List<string> FaceKeys = new List<string>
			{
				"Face_VeryHappy",
				"Face_Happy",
				"Face_Normal",
				"Face_Angry",
				"Face_VeryAngry",
			};
		}

		public readonly List<string> ExceptSkills = new List<string>
		{
			GDEItemKeys.Skill_S_FanaticBoss_Phase1AllyCard,
			//GDEItemKeys.Skill_S_Momori_P
		};

		public readonly List<string> LucyNecklace = new List<string>
		{
			GDEItemKeys.Item_Active_LucysNecklace,
			GDEItemKeys.Item_Active_LucysNecklace2,
			GDEItemKeys.Item_Active_LucysNecklace3,
			GDEItemKeys.Item_Active_LucysNecklace4
		};
	}
}
