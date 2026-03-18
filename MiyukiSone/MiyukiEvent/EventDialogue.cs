using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using Spine;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsScripts;

namespace MiyukiSone
{
	public class EventDialogue
	{
		private static List<string> negExtendedKeys = new List<string>
		{
			GDEItemKeys.SkillExtended_SkillWe_NoExchange,
			GDEItemKeys.SkillExtended_SkillWe_AutoWaste,
			GDEItemKeys.SkillExtended_SkillWe_Count,
			GDEItemKeys.SkillExtended_SkillWe_Effect15,
			GDEItemKeys.SkillExtended_SkillWe_Mana1,
			GDEItemKeys.SkillExtended_SkillWe_Ones,
			GDEItemKeys.SkillExtended_SkillWe_SelfpainDMG
		};

		private static List<string> posExtendedKeys = new List<string>
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

		public static void PawsPrank()
		{
			try
			{
				List<int> availableIndexes = GetAvailableActions();
				var lastAction = MiyukiData.LastPrank;
				if (lastAction != -1 && availableIndexes.Count > 1) availableIndexes.Remove(lastAction);
				int randomIndex = availableIndexes[RandomManager.RandomInt("MiyukiPrank", 0, availableIndexes.Count)];
				MiyukiData.LastPrank = randomIndex;

				switch (randomIndex)
				{
					case 0: CreateCurse(); break;
					case 1: CreateJoker(); break;
					case 2: ApplySkillGlitch(); break;
					case 3: DiscardSkill(); break;
					//case 4: IncreaseSkillCost(); break;
					case 5: ApplyModule(); break;
					//case 6: ApplyExtendedSkill(); break;
					//case 7: RemoveSkillSwift(); break;
					case 8: ShuffleDeck(); break;
					default: break;
				}
			}
			catch (Exception e)
			{
				Debug.Log(e.ToString());
			}
		}

		private static List<int> GetAvailableActions()
		{
			List<int> actions = new List<int> { 0, 1 };
			if (AllyTeam.Skills?.Count > 0) actions.AddRange(new int[] { 2, 3, 4, 5, 6 });
			if (AllyTeam.Skills?.FindAll(s => s.NotCount).Count > 0) actions.Add(7);
			if (AllyTeam.Skills_Deck?.Count > AllyTeam.Skills_UsedDeck?.Count) actions.Add(8);
			return actions;
		}

		private static void CreateCurse()
		{
			var skillKey = RandomManager.RandomPer("MiyukiRandomCurse", 100, 25) ? GDEItemKeys.Skill_S_Witch_2 : GDEItemKeys.Skill_S_Witch_P_0;
			AllyTeam.Add(Skill.TempSkill(skillKey, AllyTeam.LucyAlly, AllyTeam), false);
		}

		private static void CreateJoker()
		{
			AllyTeam.Add(Skill.TempSkill(GDEItemKeys.Skill_S_Joker_0, AllyTeam.LucyAlly, AllyTeam), false);
		}

		private static void ApplySkillGlitch()
		{
			var skillsInHand = AllyTeam.Skills.Where(s => s.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_Miyuki_Glitch) == null).ToList();
			if (skillsInHand.Count > 0) ApplyExtended(skillsInHand.Random("MiyukiGlitch"), ModItemKeys.SkillExtended_Ex_Miyuki_Glitch, isBattleExtended: true);
		}

		private static void DiscardSkill()
		{
			if (AllyTeam.Skills.Count == 0) return;

			int index = RandomManager.RandomInt("MiyukiDiscard", 0, AllyTeam.Skills.Count);
			var skill = AllyTeam.Skills[index];
			if (skill != null && skill.Master.Info.KeyData == GDEItemKeys.Character_Ilya) skill.Remove();
			else skill?.Delete();
		}

		

		private static void ApplyModule()
		{
			if (AllyTeam.Skills.Count == 0) return;
			var exKey = RandomManager.RandomPer("MiyukiRandomModule", 100, 25) ? GDEItemKeys.SkillExtended_Golem_Ex_0 : GDEItemKeys.SkillExtended_Golem_Ex_1;
			ApplyExtended(AllyTeam.Skills.Random("RandomSkill"), exKey);
		}

		
		private static void ChangeHand()
		{

		}
		
		// sorts deck by mana cost
		private static void ShuffleDeck()
		{
			AllyTeam.ShuffleDeck();
		}

		private static void ApplyDebuffs()
		{
			HashSet<string> buffkeys = new HashSet<string>()
			{

			};
		}

		private void PawsWithHand(bool isPositive)
		{
			var skillList = isPositive ? AllyTeam.Skills_Deck : AllyTeam.Skills;
			var action = isPositive ? new SkillButton.SkillClickDel(b => b.Myskill.Master.MyTeam.ForceDraw(b.Myskill)) : new SkillButton.SkillClickDel(b => b.Waste());
			var title = isPositive ? ScriptLocalization.System_SkillSelect.DrawSkill : ScriptLocalization.System_SkillSelect.WasteSkill;
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(skillList, action, title, false, true, true, false, true));
		}


		//private void PawsWithAllies(bool isPositive)
		//{
		//	if (isPositive) HealLowestAlly(BChar, (int)BChar.GetStat.reg);
		//	else AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("MiyukiRandom").Damage(MiyukiBchar, PlayData.TSavedata.StageNum * 10, false, true);

		//}

		private void PawsWithEnemies(bool isPositive)
		{
			if (isPositive) RemoveActions(Bs.EnemyTeam.AliveChars_Vanish);
			else Bs.EnemyTeam.AliveChars_Vanish.ForEach(e => e.AddBuff(""));
		}
	}
}
