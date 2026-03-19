using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using MiyukiSone;
using UnityEngine;

namespace MiyukiSone
{
	public partial class DialoguePaws
	{
		#region Data & Constructors
		// Base game negative Ex
		private static readonly List<string> NegExtendedKeys = new List<string>
		{
			GDEItemKeys.SkillExtended_SkillWe_NoExchange,
			GDEItemKeys.SkillExtended_SkillWe_AutoWaste,
			GDEItemKeys.SkillExtended_SkillWe_Count,
			GDEItemKeys.SkillExtended_SkillWe_Effect15,
			GDEItemKeys.SkillExtended_SkillWe_Mana1,
			GDEItemKeys.SkillExtended_SkillWe_Ones,
			GDEItemKeys.SkillExtended_SkillWe_SelfpainDMG
		};

		// Base game positive Ex
		private static readonly List<string> PosExtendedKeys = new List<string>
		{
			GDEItemKeys.SkillExtended_SkillEn_AddDefDebuff,
			GDEItemKeys.SkillExtended_SkillEn_AddPainDebuff,
			GDEItemKeys.SkillExtended_SkillEn_BattleStartDraw,
			GDEItemKeys.SkillExtended_SkillEn_ChangePlus,
			GDEItemKeys.SkillExtended_SkillEn_Cri50,
			GDEItemKeys.SkillExtended_SkillEn_Draw,
			GDEItemKeys.SkillExtended_SkillEn_ExchangeMana,
			GDEItemKeys.SkillExtended_SkillEn_HealToAttack,
			GDEItemKeys.SkillExtended_SkillEn_Ignore,
			GDEItemKeys.SkillExtended_SkillEn_MPMinus1,
			GDEItemKeys.SkillExtended_SkillEn_MPMinus1NextTurn,
			GDEItemKeys.SkillExtended_SkillEn_TauntTargetDamage,
			GDEItemKeys.SkillExtended_SkillEn_TauntTargetDamage,
		};

		// Dere data
		private static readonly List<string> DereConsumeKeys = new List<string>
		{
			GDEItemKeys.Item_Consume_Bread,
			GDEItemKeys.Item_Consume_GoldenBread,
			GDEItemKeys.Item_Consume_GoldenApple,
			GDEItemKeys.Item_Consume_Celestial,
			GDEItemKeys.Item_Consume_Herb,
			GDEItemKeys.Item_Consume_SodaWater,
			GDEItemKeys.Item_Consume_RedHammer,
			GDEItemKeys.Item_Consume_RedHerb,
			GDEItemKeys.Item_Consume_Dinner,
			GDEItemKeys.Item_Consume_Ilya_PassiveConsume,
			GDEItemKeys.Item_Consume_FriendShipPouch,
			GDEItemKeys.Item_Consume_RedWing,
		};

		private static readonly List<string> DereBookKeys = new List<string>
		{
			GDEItemKeys.Item_Consume_SkillBookCharacter,
			GDEItemKeys.Item_Consume_SkillBookInfinity,
			GDEItemKeys.Item_Consume_SkillBookSuport,
			GDEItemKeys.Item_Consume_SkillBookCharacter_Rare,
			GDEItemKeys.Item_Consume_SkillBookLucy,
			GDEItemKeys.Item_Consume_SkillBookLucy_Rare,
		};

		private static readonly List<string> DereMiscKeys = new List<string>
		{
			GDEItemKeys.Item_Misc_ArtifactPlusInven,
			GDEItemKeys.Item_Misc_BlackironMoru,
			GDEItemKeys.Item_Misc_Item_Key,
			GDEItemKeys.Item_Misc_Scrap_0,
			GDEItemKeys.Item_Misc_Scrap_1,
			GDEItemKeys.Item_Misc_RWEnterItem,
			//GDEItemKeys.Item_Misc_TimeMoney,
		};

		// Yandere data
		private static readonly List<string> YanderePawExKeys = new List<string>()
		{
			ModItemKeys.SkillExtended_Ex_Miyuki_Glitch,
			GDEItemKeys.SkillExtended_Golem_Ex_0,
			GDEItemKeys.SkillExtended_Golem_Ex_1,
			GDEItemKeys.SkillExtended_LBossFirst_Sword,
			GDEItemKeys.SkillExtended_Pope_Ex_0,
		};

		private static readonly List<string> YanderePawSkillKeys = new List<string>()
		{
			GDEItemKeys.Skill_S_Witch_P_0,
			GDEItemKeys.Skill_S_Witch_2,
			GDEItemKeys.Skill_S_Joker_0,
			//GDEItemKeys.Skill_S_ProgramMaster_LucyUpdate_Main,
			GDEItemKeys.Skill_S_FanaticBoss_Phase1AllyCard,
		};

		private static readonly List<string> YanderePawBuffKeys = new List<string>()
		{
			GDEItemKeys.Buff_B_Outlaw_P_0,
			GDEItemKeys.Buff_B_DuelistWill,
			GDEItemKeys.Buff_B_LBossFirst_Phase3_Summon_T_HealStun_T,
		};
		#endregion

		public static void ChoosePaws()
		{
			if (Affection.IsKuudere) return;
			if (Affection.MiyukiDecides) (Affection.IsDere ? (Action)DerePaws : YanderePaws)();
		}
	}
}
