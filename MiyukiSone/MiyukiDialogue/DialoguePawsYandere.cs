using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModEditor;
using GameDataEditor;
using I2.Loc;
using MiyukiSone;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsScripts;

namespace MiyukiSone
{
	public partial class DialoguePaws
	{
		private static readonly List<Action> YandereDialoguePawsAction = new List<Action>()
		{
			CreateSkill,
			AppyEx,
			ShuffleDeck,
			ApplyBuffEnemy,
			ApplyBuffAlly,
		};

		public static void YanderePaws()
		{
			List<Action> paws = YandereDialoguePawsAction.ToList();
			if (MiyukiData.LastYandereDialoguePaw != -1 && paws.Count > 1) paws.RemoveAt(MiyukiData.LastYandereDialoguePaw);
			int randomIndex = RandomManager.RandomInt("MiyukiYanderePaw", 0, paws.Count);
			paws[randomIndex].Invoke();
			MiyukiData.LastYandereDialoguePaw = randomIndex;
		}

		#region Yandere Paws Functions
		private static void CreateSkill()
		{
			string skillKey = YanderePawSkillKeys.Random("MiyukiRandomSkill");
			if (string.IsNullOrEmpty(skillKey)) return;
			Skill skill = Skill.TempSkill(skillKey, AllyTeam.LucyAlly, AllyTeam);
			if (skill != null) AllyTeam.Add(skill, false);
		}

		private static void AppyEx()
		{
			string exKey = YanderePawExKeys.Random("MiyukiRandomExKey");
			if (string.IsNullOrEmpty(exKey) || AllyTeam.Skills.Count == 0) return;
			var skill = AllyTeam.Skills.Where(s => s != null && s.ExtendedFind_DataName(exKey) == null).ToList().Random("MiyukiRandomEx").Let(s => ApplyExtended(s, exKey));
			//if (skill != null) ApplyExtended(skill, exKey);
			//AllyTeam.Skills.Where(s => s.ExtendedFind_DataName(exKey) == null && s != null).Select(s => { ApplyExtended(s, exKey); return s; }).ToList();
		}

		private static void ShuffleDeck()
		{
			if (AllyTeam.Skills_Deck.Count > 0) BattleSystem.DelayInput(ShuffleCo());
		}

		// Shuffle draw pile into discard pile and apply negative Ex
		private static IEnumerator ShuffleCo()
		{
			while (AllyTeam.Skills_Deck.Count > 0)
			{
				Skill skill = AllyTeam.Skills_Deck[0];
				skill.ExtendedAdd_Battle(NegExtendedKeys.Random("RandomNegativeEx"));
				yield return BattleSystem.instance.StartCoroutine(SkillShuffleCo(skill));
			}

			BattleSystem.instance.AllyTeam.ShuffleDeck();
			BattleSystem.instance.AllyTeam.Draw();
			yield break;
		}

		private static IEnumerator SkillShuffleCo(Skill ToSkill)
		{
			AllyTeam.Skills_Deck.Remove(ToSkill);
			AllyTeam.Skills_UsedDeck.Add(ToSkill);
			AllyTeam.DeckInputAni(ToSkill);
			yield return new WaitForSeconds(0.04f);
			yield break;
		}


		public static void ApplyBuffEnemy()
		{
			string buffKey = YanderePawBuffKeysEnemies.Random("RandomBuff");
			if (string.IsNullOrEmpty(buffKey) || Bs.EnemyTeam.AliveChars.Count == 0) return;
			Bs.EnemyTeam.AliveChars.Where(e => e.BuffReturn(buffKey, false) == null).ToList().Random("RandomEnemy")?.AddBuff(buffKey);
		}

		private static void ApplyBuffAlly()
		{
			string buffKey = YanderePawBuffKeysAllies.Random("RandomBuff");
			if (string.IsNullOrEmpty(buffKey) || Bs.EnemyTeam.AliveChars.Count == 0) return;
			Bs.EnemyTeam.AliveChars.Where(e => e.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("RandomAlly")?.AddBuff(buffKey);
		}

		private static void ChangeHand()
		{

		}

		//private void PawsWithAllies(bool isPositive)
		//{
		//	if (isPositive) HealLowestAlly(BChar, (int)BChar.GetStat.reg);
		//	else AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("MiyukiRandom").Damage(MiyukiBchar, PlayData.TSavedata.StageNum * 10, false, true);

		//}


	}
	#endregion
}
