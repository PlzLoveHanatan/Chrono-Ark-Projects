using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class DataStore
	{
		public class VisualUi
		{
			public class LookDayUi
			{
				public enum SpriteTypeFace
				{
					Face_VeryHappy,
					Face_Happy,
					Face_Normal,
					Face_Angry,
					Face_VeryAngry,
				}

				public readonly Dictionary<SpriteTypeFace, string> SpritePathsFace = new Dictionary<SpriteTypeFace, string>()
				{
					{ SpriteTypeFace.Face_VeryHappy, "Visual/Face/VeryHappy.png" },
					{ SpriteTypeFace.Face_Happy, "Visual/Face/Happy.png" },
					{ SpriteTypeFace.Face_Normal, "Visual/Face/Normal.png" },
					{ SpriteTypeFace.Face_Angry, "Visual/Face/Angry.png" },
					{ SpriteTypeFace.Face_VeryAngry, "Visual/Face/VeryAngry.png" },
				};

				public readonly List<string> FaceKeys = new List<string>
				{
					"Face_VeryHappy",
					"Face_Happy",
					"Face_Normal",
					"Face_Angry",
					"Face_VeryAngry",
				};
			}
		}
	}
}
