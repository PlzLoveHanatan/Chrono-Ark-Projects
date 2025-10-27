using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class DataStore
	{
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
	}
}
