using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MiyukiSone
{
	public static class DialogueBox
	{
		public enum BoxState
		{
			love,
			kiss,
			sex,
			help
		}

		public static readonly Dictionary<BoxState, string> DialogueSprite = new Dictionary<BoxState, string>()
		{
			{ BoxState.love, "I love you!" },
			{ BoxState.kiss, "*kisses you*" },
			{ BoxState.sex, "💕" },
			{ BoxState.help, "How can I help you?" }
		};

		public static readonly Dictionary<BoxState, Vector2> DialoguePositions = new Dictionary<BoxState, Vector2>()
		{
			{ BoxState.love, new Vector3(0, 100, 0) },
			{ BoxState.kiss, new Vector3(0, 80, 0) },
			{ BoxState.sex, new Vector3(0, 120, 0) },
			{ BoxState.help, new Vector3(0, 90, 0) }
		};

		public static readonly Dictionary<BoxState, Vector3> DialogueSize = new Dictionary<BoxState, Vector3>()
		{
			{ BoxState.love, new Vector3(0, 100, 0) },
			{ BoxState.kiss, new Vector3(0, 80, 0) },
			{ BoxState.sex, new Vector3(0, 120, 0) },
			{ BoxState.help, new Vector3(0, 90, 0) }
		};
	}
}
