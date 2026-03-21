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

		private static readonly List<string> DereScrollKeys = new List<string>
		{
			GDEItemKeys.Item_Scroll_Scroll_Enchant,
			GDEItemKeys.Item_Scroll_Scroll_Identify,
			GDEItemKeys.Item_Scroll_Scroll_Item,
			GDEItemKeys.Item_Scroll_Scroll_Mapping,
			GDEItemKeys.Item_Scroll_Scroll_Midas,
			GDEItemKeys.Item_Scroll_Scroll_Purification,
			GDEItemKeys.Item_Scroll_Scroll_Quick,
			GDEItemKeys.Item_Scroll_Scroll_Teleport,
			GDEItemKeys.Item_Scroll_Scroll_Transfer,
			GDEItemKeys.Item_Scroll_Scroll_Uncurse,
			GDEItemKeys.Item_Scroll_Scroll_Vitality,
		};

		// Yandere data
		private static readonly List<string> YanderePawExKeys = new List<string>()
		{
			//ModItemKeys.SkillExtended_Ex_Miyuki_Glitch,
			GDEItemKeys.SkillExtended_Golem_Ex_0,
			//GDEItemKeys.SkillExtended_Golem_Ex_1,
			//GDEItemKeys.SkillExtended_LBossFirst_Sword,
			GDEItemKeys.SkillExtended_Pope_Ex_0,
		};

		private static readonly List<string> YanderePawSkillKeys = new List<string>()
		{
			GDEItemKeys.Skill_S_Witch_P_0, // Crucifying Curse
			GDEItemKeys.Skill_S_Witch_2, // Weakening Curse
			GDEItemKeys.Skill_S_Joker_0, // Joker
			//GDEItemKeys.Skill_S_ProgramMaster_LucyUpdate_Main,
			GDEItemKeys.Skill_S_FanaticBoss_Phase1AllyCard, // Sentenced to Death
			GDEItemKeys.Skill_S_BombClown_B_0, // Time Bomb
		};

		private static readonly List<string> YanderePawBuffKeysEnemies = new List<string>()
		{
			GDEItemKeys.Buff_B_Outlaw_P_0,
			GDEItemKeys.Buff_B_DuelistWill,
			GDEItemKeys.Buff_B_LBossFirst_Phase3_Summon_T_HealStun_T,
			GDEItemKeys.Buff_B_S3_Boss_Pope_P_0,
			ModItemKeys.Buff_B_Miyuki_Enemy_ExtraAction,
		};

		private static readonly List<string> YanderePawBuffKeysAllies = new List<string>()
		{
			GDEItemKeys.Buff_B_S3_Pope_P_2, // Complete Obedience
			GDEItemKeys.Buff_B_Enemy_Boss_Reaper_P_0, // Mark of Death
			GDEItemKeys.Buff_TheLight_P_0, // Sacred Brand
			GDEItemKeys.Buff_B_S2_Mainboss_1_LeftDebuff, // Ruby Stigma
			GDEItemKeys.Buff_B_S2_Mainboss_1_RightDebuf, // Saphire Stigma
		};
		#endregion

		public static void ChoosePaws()
		{
			if (!Affection.MiyukiDecides || BattleSystem.instance == null || Affection.IsKuudere) return;

			(Affection.IsDere ? (Action)DerePaws : YanderePaws)();
		}
	}
}
