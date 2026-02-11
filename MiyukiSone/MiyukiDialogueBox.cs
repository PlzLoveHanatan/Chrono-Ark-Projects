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
			//sex,
			//help
		}

		//public static readonly Dictionary<BoxState, string> DialogueSprite = new Dictionary<BoxState, string>()
		//{
		//	{ BoxState.love, "box_love_0.png" },
		//	{ BoxState.love, "box_love_1.png" },
		//	{ BoxState.love, "box_love_2.png" },
		//	{ BoxState.love, "box_love_3.png" },
		//	{ BoxState.love, "box_love_4.png" },
		//	{ BoxState.love, "box_love_5.png" },
		//	{ BoxState.love, "box_love_6.png" },
		//	//{ BoxState.kiss, "*kisses you*" },
		//	//{ BoxState.sex, "💕" },
		//	//{ BoxState.help, "How can I help you?" }
		//};

		public static readonly Dictionary<BoxState, string[]> DialogueSprites = new Dictionary<BoxState, string[]>()
		{
			{
				BoxState.love, new string[]
				{
					"box_love_0.png",
					"box_love_1.png",
					"box_love_2.png",
					"box_love_3.png",
					"box_love_4.png",
					"box_love_5.png",
					"box_love_6.png"
				}
			},

			{
				BoxState.kiss, new string[]
				{
					"box_kiss_0.png",
					"box_kiss_1.png",
					"box_kiss_2.png",
				}
			},
		};

		public static readonly Dictionary<BoxState, Vector2> DialogueSize= new Dictionary<BoxState, Vector2>()
		{
			{ BoxState.love, new Vector3(700, 130, 0) },
			{ BoxState.kiss, new Vector3(700, 130, 0) },
			//{ BoxState.sex, new Vector3(0, 120, 0) },
			//{ BoxState.help, new Vector3(0, 90, 0) }
		};

		public static readonly Dictionary<BoxState, Vector3> Dialogueposition = new Dictionary<BoxState, Vector3>()
		{
			{ BoxState.love, new Vector3(170, 170, 0) },
			{ BoxState.kiss, new Vector3(170, 170, 0) },
			//{ BoxState.sex, new Vector3(0, 120, 0) },
			//{ BoxState.help, new Vector3(0, 90, 0) }
		};
	}
}
