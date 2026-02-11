using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.Text;
using static MiyukiSone.DialogueBox;

namespace MiyukiSone
{
	public static class Affection
	{
		private static GameObject testWindow;
		public enum MiyukiAffectionState
		{
			love, // 5 -> 10
			neutral, // between -5 and 5
			hate, // -5 -> -10
		}

		private const string MiyukiRandom = "MiyukiRandom";

		private const int Love_Threshold = 5;
		private const int Hate_Threshold = -5;
		private const int Max_Affection = 10;
		private const int Min_Affection = -10;
		private const int Random_Chance = 5;

		public static int MiyukiPoints => MiyukiData.MiyukiAffectionPoints;

		public static MiyukiAffectionState CurrentAffection
		{
			get
			{
				if (MiyukiPoints >= Love_Threshold) return MiyukiAffectionState.love;
				else if (MiyukiPoints <= Hate_Threshold) return MiyukiAffectionState.hate;
				else return MiyukiAffectionState.neutral;
			}
		}

		public static void ChangePoints()
		{
			int amount = RandomManager.RandomInt(MiyukiRandom, 0, 2);
			ChangePoints(-amount);
		}

		public static void ChangePoints(int amount)
		{
			MiyukiData.MiyukiAffectionPoints = Mathf.Clamp(MiyukiData.MiyukiAffectionPoints + amount, Min_Affection, Max_Affection);
		}

		public static void MiyukiTurn()
		{
			//MiyukiDialogueBox(); return;

			if (RandomManager.RandomPer(MiyukiRandom, 0, 50) == true && CurrentAffection != MiyukiAffectionState.neutral)
			{
				switch (CurrentAffection)
				{
					case MiyukiAffectionState.love: LoveAction(5); break;
					case MiyukiAffectionState.hate: HateAction(3); break;
					default: break;
				}
			}
			else
			{
				MiyukiDialogueBox();
			}
		}

		private static void CreateDialogueBox(BoxState state)
		{
			MiyukiDialogueBox(state);
		}

		private static void MiyukiDialogueBox(BoxState? state = null)
		{
			var allStates = Enum.GetValues(typeof(BoxState));
			BoxState currentState = state ?? (BoxState)allStates.GetValue(UnityEngine.Random.Range(0, allStates.Length));
			string[] spriteVariants = DialogueSprites[currentState];
			string randomSprite = spriteVariants[RandomManager.RandomInt("MiyukiRandomBox", 0, spriteVariants.Length)];
			Sprite sprite = MiyukiUI.GetSprite("MiyukiVisual/DialogueBox/" + randomSprite);
			Vector2 size = DialogueSize[currentState];
			Vector3 position = Dialogueposition[currentState];

			testWindow = MiyukiUI.CreateUIImage($"dialogue_{currentState}", BattleSystem.instance.ActWindow.transform, sprite, size, position, true);
			testWindow.AddComponent<MiyukiWindow>();
			testWindow.AddComponent<MiyukiWindowDragHandler>();
			testWindow.GetComponent<MiyukiWindow>().currentBoxState = currentState;
		}

		private static void LoveAction(int actions)
		{
			if (RandomManager.RandomPer(MiyukiRandom, 100, MiyukiPoints * Random_Chance) == false) return;
			int actionIndex = RandomManager.RandomInt(MiyukiRandom, 0, actions);
			MiyukiEvent selectedEvent = MiyukiEvent.random;
			switch (actionIndex)
			{
				case 0: AllyTeam.Draw(); selectedEvent = MiyukiEvent.draw; break;
				case 1: AllyTeam.AP += 1; selectedEvent = MiyukiEvent.mana; break;
				case 2: FetchSkill(); selectedEvent = MiyukiEvent.fetch; break;
				case 3: Pd._Gold += 250; selectedEvent = MiyukiEvent.gold; break;
				case 4: Pd._Soul += 1; selectedEvent = MiyukiEvent.soulstones; break;
			}
			MiyukiTextEvent(selectedEvent, true);
		}

		private static void FetchSkill()
		{
			BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(AllyTeam.Skills_Deck,
				new SkillButton.SkillClickDel(b => b.Myskill.Master.MyTeam.ForceDraw(b.Myskill)), ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));
		}

		private static void HateAction(int actions)
		{
			if (RandomManager.RandomPer(MiyukiRandom, 100, MiyukiPoints * Random_Chance) == false) return;
			if (Pd._Gold < 250) actions--;
			if (Pd._Soul < 1) actions--;
			int actionIndex = RandomManager.RandomInt(MiyukiRandom, 0, actions);
			MiyukiEvent selectedEvent = MiyukiEvent.random;
			switch (actionIndex)
			{
				case 0: AllyTeam.AP -= 1; selectedEvent = MiyukiEvent.mana; break;
				case 1: Pd._Gold -= 250; selectedEvent = MiyukiEvent.gold; break;
				case 2: Pd._Soul -= 1; selectedEvent = MiyukiEvent.soulstones; break;
			}
			// remove skill from hand or deck for the current battle
			MiyukiTextEvent(selectedEvent, true);
		}
	}
}
