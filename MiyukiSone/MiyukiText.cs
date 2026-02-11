using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;

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
			{ MiyukiEvent.draw, "Draw"},
			{ MiyukiEvent.mana, "Mana"},
			{ MiyukiEvent.gold, "Gold"},
			{ MiyukiEvent.fetch, "Fetch"},
			{ MiyukiEvent.random, "I love you!"},
		};

		private readonly static Dictionary<MiyukiTextState, string> MiyukiBattleDialogue = new Dictionary<MiyukiTextState, string>()
		{
			{ MiyukiTextState.heal, "How dare you heal other people !?" },
		};

		public static void MiyukiText(BattleChar bchar, MiyukiTextState state, bool isEvent = true)
		{
			string text = MiyukiText(state);
			MiyukiText(bchar, text, SoftText(), isEvent);
		}

		public static void MiyukiTextEvent(BattleChar bchar, MiyukiEvent eventText, bool isEvent = true)
		{
			string text = MiyukiTextEvent(eventText);
			MiyukiText(bchar, text, SoftText(), isEvent);
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

		public static void MiyukiText(BattleChar bchar, string text, bool isSoftText, bool isEvent)
		{
			var position = bchar.GetTopPos();
			if (string.IsNullOrEmpty(text) || bchar.IsDead || bchar.Info.KeyData != ModItemKeys.Character_Miyuki) return;

			if (isSoftText)
			{
				bchar.StartCoroutine(TextSoft(position, text));
			}
			else
			{
				BattleSystem.DelayInput(TextHard(position, text, isEvent));
			}
		}

		private static string MiyukiText(MiyukiTextState state)
		{
			return MiyukiBattleDialogue.TryGetValue(state, out string line) && !string.IsNullOrEmpty(line) ? line : "I'm Error";
		}

		private static string MiyukiTextEvent(MiyukiEvent state)
		{
			var dic = CurrentAffection == MiyukiAffectionState.love ? MiyukiEventLove : MiyukiEventHate;
			return dic.TryGetValue(state, out string line) && !string.IsNullOrEmpty(line) ? line : "I'm Error";
		}

		private static IEnumerator TextSoft(Vector3 position, string text)
		{
			var topText = BattleText.CustomText(position, text);
			yield return new WaitForSecondsRealtime(2f);
			topText?.End();
		}

		private static IEnumerator TextHard(Vector3 position, string text, bool isEvent)
		{
			yield return BattleText.InstBattleTextAlly_Co(position, text, isEvent);
			yield break;
		}
	}
}
