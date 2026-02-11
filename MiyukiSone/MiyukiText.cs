using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using static MiyukiSone.DialogueBox;

namespace MiyukiSone
{
	public static class Text
	{
		public enum MiyukiTextState
		{
			heal,
			damage,
			other
		}

		public enum MiyukiEvent
		{
			draw,
			mana,
			gold,
			fetch,
			soulstones,
			random
		}

		private static List<string> MiyukiDialogue = new List<string>()
		{

		};

		public static readonly Dictionary<BoxState, string[]> MiyukiDialogueBoxYes = new Dictionary<BoxState, string[]>()
		{
			{
				BoxState.love, new string[]
				{
					"I love you too!",
					"You make me so happy!",
					"Forever and ever!",
					"Promise me...",
					"My heart is yours!",
					"I'll never let you go!",
					"Thank you for loving me...",
					"You're my hero!"
				}
			},

			{
				BoxState.kiss, new string[]
				{
					"Can we do that again?",
					"One more? Please?",
					"My heart is racing...",
				}
			}
		};

		public static readonly Dictionary<BoxState, string[]> MiyukiDialogueBoxNo = new Dictionary<BoxState, string[]>()
		{
			{
				BoxState.love, new string[]
				{
					"Oh... I understand...",
					"Maybe someday...",
					"I'll wait for you...",
					"It's okay, I still love you.",
					"Did I do something wrong?",
					"I hope you change your mind...",
					"Even if you don't love me back, I'll stay by your side.",
					"..."
				}
			},

			{
				BoxState.kiss, new string[]
				{
					"It's okay, I'll wait.",
					"Maybe next time?",
					"Did I make you uncomfortable?",
				}
			}
		};

		private static readonly Dictionary<MiyukiEvent, string> MiyukiEventLove = new Dictionary<MiyukiEvent, string>()
		{
			{ MiyukiEvent.draw, "Draw"},
			{ MiyukiEvent.mana, "Mana"},
			{ MiyukiEvent.gold, "Gold"},
			{ MiyukiEvent.fetch, "Fetch"},
			{ MiyukiEvent.random, "I love you!"},
		};

		private static readonly Dictionary<MiyukiEvent, string> MiyukiEventHate = new Dictionary<MiyukiEvent, string>()
		{
			{ MiyukiEvent.mana, "Mana"},
			{ MiyukiEvent.gold, "Gold"},
		};

		private readonly static Dictionary<MiyukiTextState, string> MiyukiDialogueBattle = new Dictionary<MiyukiTextState, string>()
		{
			{ MiyukiTextState.heal, "How dare you heal other people !?" },
		};

		public static void MiyukiTextBox(BoxState stateBox, bool isYes, bool isEvent = true)
		{
			string text = MiyukiTextBox(stateBox, isYes);
			MiyukiText(text, SoftText(), isEvent);
		}

		public static void MiyukiTextEvent(MiyukiEvent eventText, bool isEvent = true)
		{
			string text = MiyukiTextEvent(eventText);
			MiyukiText(text, SoftText(), isEvent);
		}

		private static bool SoftText()
		{
			bool isSoftText;
			switch (CurrentAffection)
			{
				case MiyukiAffectionState.love: isSoftText = true; break;
				case MiyukiAffectionState.hate: isSoftText = false; break;
				default: isSoftText = true; break;
			}
			return isSoftText;
		}

		public static void MiyukiText(string text, bool isSoftText, bool isEvent)
		{
			var position = MiyukiBchar.GetTopPos();
			if (string.IsNullOrEmpty(text) || MiyukiBchar.IsDead) return;

			if (isSoftText)
			{
				MiyukiBchar.StartCoroutine(TextSoft(position, text));
			}
			else
			{
				BattleSystem.DelayInput(TextHard(position, text, isEvent));
			}
		}

		private static string MiyukiTextBox(BoxState state, bool isYes)
		{
			var dic = isYes ? MiyukiDialogueBoxYes : MiyukiDialogueBoxNo;
			if (dic.TryGetValue(state, out string[] lines) && lines != null && lines.Length > 0)
			{
				return lines[RandomManager.RandomInt("MiyukiRandomBox", 0, lines.Length)];
			}
			return "I'm Error";
		}

		private static string MiyukiTextEvent(MiyukiEvent state)
		{
			var dic = CurrentAffection == MiyukiAffectionState.love ? MiyukiEventLove : MiyukiEventHate;
			return dic.TryGetValue(state, out string line) && !string.IsNullOrEmpty(line) ? line : "I'm Error";
		}

		private static IEnumerator TextSoft(Vector3 position, string text)
		{
			var topText = BattleText.CustomText(position, text);
			yield return new WaitForSecondsRealtime(3f);
			topText?.End();
		}

		private static IEnumerator TextHard(Vector3 position, string text, bool isEvent)
		{
			yield return BattleText.InstBattleTextAlly_Co(position, text, isEvent);
			yield break;
		}
	}
}
