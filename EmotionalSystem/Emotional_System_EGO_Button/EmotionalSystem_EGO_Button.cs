using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using Dialogical;
using EmotionalSystem;
using GameDataEditor;
using Spine;
using UnityEngine;
using static EmotionalSystemSkillExtended.ExEGO;

namespace EmotionalSystem
{
	public class EmotionalSystem_EGO_Button : MonoBehaviour
	{
		public static EmotionalSystem_EGO_Button instance;

		public List<Skill> EGOHand = new List<Skill>();

		private List<Skill> Hand = new List<Skill>();
		private List<Skill> DrawPile = new List<Skill>();
		private List<Skill> DiscardPile = new List<Skill>();

		private int ExchangeNum = 0;

		public bool ActiveEGOHand = false;

		public bool HasEGOSkill => EGOHand.Count > 0;

		public void Awake()
		{
			instance = this;
		}

		public void OnDestroy()
		{
			if (instance == this)
			{
				instance = null;
			}
		}

		public void TurnUpdate()
		{
			UpdateEGOCountdown();
		}

		public void AddEGOSkill(Skill skill)
		{
			EGOHand.Add(skill);
			GetComponent<EmotionalSystem_EGO_Button_Script>()?.StartRotation();
		}

		public void RemoveEGOSkill(Skill skill)
		{
			EGOHand.Remove(skill);
		}

		public void ChangeHand(bool changeToEGO = false)
		{
			GetComponent<EmotionalSystem_EGO_Button_Script>()?.ResetRotation();

			if (changeToEGO)
			{
				ChangeToEGO();
			}
			else
			{
				ChangeToNormal();
			}
		}

		private void ChangeToEGO()
		{
			Hand.Clear();
			DrawPile.Clear();
			DiscardPile.Clear();

			Hand.AddRange(Utils.AllyTeam.Skills);
			DrawPile.AddRange(Utils.AllyTeam.Skills_Deck);
			DiscardPile.AddRange(Utils.AllyTeam.Skills_UsedDeck);

			for (int i = Utils.AllyTeam.Skills.Count - 1; i >= 0; i--)
			{
				var skill = Utils.AllyTeam.Skills[i];
				skill.Remove();

				if (DataStore.Instance.ExceptSkills.Contains(skill.MySkill.KeyID))
				{
					EGOHand.Add(skill);
				}
			}

			Utils.AllyTeam.Skills_Deck.Clear();
			Utils.AllyTeam.Skills_UsedDeck.Clear();

			foreach (var skill in EGOHand.Where(sk => !sk.Master.IsDead))
			{
				BattleSystem.instance.StartCoroutine(Utils.AllyTeam.AddSkillNoDrawEffect(skill, -1));
			}

			ActiveEGOHand = true;

			// don't allow exchange in EGO skills
			ExchangeNum = Utils.AllyTeam.DiscardCount;
			Utils.AllyTeam.DiscardCount = 0;
		}

		private void ChangeToNormal()
		{
			for (int j = Utils.AllyTeam.Skills.Count - 1; j >= 0; j--)
			{
				var skill = Utils.AllyTeam.Skills[j];

				if (!EGOHand.Contains(skill))
				{
					Hand.Add(skill);
				}

				if (DataStore.Instance.ExceptSkills.Contains(skill.MySkill.KeyID))
				{
					EGOHand.Add(skill);
				}

				skill.Remove();
			}

			Utils.AllyTeam.Skills.Clear();
			Utils.AllyTeam.Skills_Deck.Clear();
			Utils.AllyTeam.Skills_UsedDeck.Clear();

			Utils.AllyTeam.Skills_Deck.AddRange(DrawPile);
			Utils.AllyTeam.Skills_UsedDeck.AddRange(DiscardPile);

			foreach (var skill in Hand)
			{
				BattleSystem.instance.StartCoroutine(Utils.AllyTeam.AddSkillNoDrawEffect(skill, -1));
			}

			ActiveEGOHand = false;

			// return back the exchange count
			Utils.AllyTeam.DiscardCount = ExchangeNum;
		}

		public void UpdateEGOCountdown()
		{
			foreach (var skill in EGOHand)
			{
				var ex = skill.ExtendedFind<Ex_EGO>();
				ex?.TurnUpdate();
			}
		}
	}
}
