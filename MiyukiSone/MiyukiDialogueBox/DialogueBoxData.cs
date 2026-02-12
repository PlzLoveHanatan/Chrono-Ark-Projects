using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2.Loc;
using UnityEngine;
using ChronoArkMod;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public static class DialogueBoxData
	{
		public enum DialogueBoxState
		{
			love,
			kiss,
			//sex,
			//help
		}

		public static readonly Dictionary<DialogueBoxState, List<string>> DialogueSprites = new Dictionary<DialogueBoxState, List<string>>()
		{
			{ DialogueBoxState.love, new List<string> {"box_love_0.png", "box_love_1.png", "box_love_2.png", "box_love_3.png", "box_love_4.png",
				"box_love_5.png", "box_love_6.png", "box_love_7.png", "box_love_8.png", "box_love_9.png", "box_love_10.png" } },
			{ DialogueBoxState.kiss, new List<string> { "box_kiss_0.png", "box_kiss_1.png", "box_kiss_2.png", "box_kiss_3.png" } },
		};


		public static readonly Dictionary<DialogueBoxState, Vector2> DialogueSize = new Dictionary<DialogueBoxState, Vector2>()
		{
			{ DialogueBoxState.love, new Vector3(700, 130, 0) },
			{ DialogueBoxState.kiss, new Vector3(700, 130, 0) },
			//{ DialogueBoxState.sex, new Vector3(0, 120, 0) },
			//{ DialogueBoxState.help, new Vector3(0, 90, 0) }
		};

		public static readonly Dictionary<DialogueBoxState, Vector3> Dialogueposition = new Dictionary<DialogueBoxState, Vector3>()
		{
			{ DialogueBoxState.love, new Vector3(170, 170, 0) },
			{ DialogueBoxState.kiss, new Vector3(170, 170, 0) },
			//{ DialogueBoxState.sex, new Vector3(0, 120, 0) },
			//{ DialogueBoxState.help, new Vector3(0, 90, 0) }
		};
	}
}
