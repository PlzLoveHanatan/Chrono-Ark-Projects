using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace EmotionSystem
{
	public partial class DataStore
	{
		public class GuestData
		{
			public readonly List<Abnormality> Abnormality = new List<Abnormality>
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

			public readonly Dictionary<string, List<string>> BannedAbnormality = new Dictionary<string, List<string>>()
			{
				{ GDEItemKeys.Enemy_S2_Ballon, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } },
				{ GDEItemKeys.Enemy_S2_BoomBalloon, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } },
				{ GDEItemKeys.Enemy_S2_HealBallon, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } },
				{ GDEItemKeys.Enemy_S4_King_minion_0, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } },
				{ GDEItemKeys.Enemy_S4_King_minion_1, new List<string> { ModItemKeys.Buff_B_Abnormality_GuestLv2_Shelter } }
			};

			public readonly List<string> AbnormalityKeyList = new List<string>
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

			public readonly List<string> GuestCurses = new List<string>
			{
				GDEItemKeys.Buff_B_CursedMob_0,
				GDEItemKeys.Buff_B_CursedMob_1,
				GDEItemKeys.Buff_B_CursedMob_2,
				GDEItemKeys.Buff_B_CursedMob_3,
				GDEItemKeys.Buff_B_CursedMob_4,
				GDEItemKeys.Buff_B_CursedMob_5,
			};

			public readonly List<string> CurseSelectionList = new List<string>
			{
				ModItemKeys.Skill_S_CurseSelection_0,
				ModItemKeys.Skill_S_CurseSelection_1,
				ModItemKeys.Skill_S_CurseSelection_2,
				ModItemKeys.Skill_S_CurseSelection_3,
				ModItemKeys.Skill_S_CurseSelection_4,
				ModItemKeys.Skill_S_CurseSelection_5,
			};

			public static readonly Dictionary<string, string> CurseMap = new Dictionary<string, string>
			{
				{ ModItemKeys.Skill_S_CurseSelection_0, GDEItemKeys.Buff_B_CursedMob_0 },
				{ ModItemKeys.Skill_S_CurseSelection_1, GDEItemKeys.Buff_B_CursedMob_1 },
				{ ModItemKeys.Skill_S_CurseSelection_2, GDEItemKeys.Buff_B_CursedMob_2 },
				{ ModItemKeys.Skill_S_CurseSelection_3, GDEItemKeys.Buff_B_CursedMob_3 },
				{ ModItemKeys.Skill_S_CurseSelection_4, GDEItemKeys.Buff_B_CursedMob_4 },
				{ ModItemKeys.Skill_S_CurseSelection_5, GDEItemKeys.Buff_B_CursedMob_5 },
			};
		}
	}
}
