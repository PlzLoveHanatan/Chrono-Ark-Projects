using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using UnityEngine;

namespace EmotionSystem
{
	public partial class DataStore
	{
		public class VisualUi
		{
			public LookDayUi LookDay { get; } = new LookDayUi();
			public EGOUi EGOButton { get; } = new EGOUi();

			public class EGOUi
			{
				public enum SpriteSetType
				{
					Normal,
					Angela,
				}

				public enum SpriteTypeEGOButton
				{
					EGO_Normal_Empty,
					EGO_Normal_Open,
					EGO_Normal_Active
				}

				[Serializable]
				public class EGOButtonVisualData
				{
					public string Path;
					public Vector2 Position;
					public Vector2 Size;
					public bool IsDefault;

					public EGOButtonVisualData(string path, Vector2 pos, Vector2 size, bool isDefault = false)
					{
						Path = path;
						Position = pos;
						Size = size;
						IsDefault = isDefault;
					}
				}

				public readonly Dictionary<SpriteSetType, Dictionary<SpriteTypeEGOButton, EGOButtonVisualData>> SpriteSets = new Dictionary<SpriteSetType, Dictionary<SpriteTypeEGOButton, EGOButtonVisualData>>()
					{
						// NORMAL
						{
							SpriteSetType.Normal, new Dictionary<SpriteTypeEGOButton, EGOButtonVisualData>()
							{
								{ SpriteTypeEGOButton.EGO_Normal_Empty,  new EGOButtonVisualData("Visual/EGO/Normal/Empty.png",  new Vector2(-325, 300), new Vector2(200, 200), true) },
								{ SpriteTypeEGOButton.EGO_Normal_Open,   new EGOButtonVisualData("Visual/EGO/Normal/Open.png",   new Vector2(-325, 300), new Vector2(200, 200)) },
								{ SpriteTypeEGOButton.EGO_Normal_Active, new EGOButtonVisualData("Visual/EGO/Normal/Active.png", new Vector2(-325, 300), new Vector2(200, 200)) }
							}
						},

						// ANGELA
						{
							SpriteSetType.Angela, new Dictionary<SpriteTypeEGOButton, EGOButtonVisualData>()
							{
								{ SpriteTypeEGOButton.EGO_Normal_Empty,  new EGOButtonVisualData("Visual/EGO/Angela/Empty.png",  new Vector2(-290, 280), new Vector2(200, 200), true) },
								{ SpriteTypeEGOButton.EGO_Normal_Open,   new EGOButtonVisualData("Visual/EGO/Angela/Open.png",   new Vector2(-290, 280), new Vector2(200, 200)) },
								{ SpriteTypeEGOButton.EGO_Normal_Active, new EGOButtonVisualData("Visual/EGO/Angela/Active.png", new Vector2(-290, 280), new Vector2(200, 200)) }
							}
						},
					};

				public readonly Dictionary<DataStore.LibraryFloorType, SpriteSetType> FloorToSetMap = new Dictionary<DataStore.LibraryFloorType, SpriteSetType>()
				{
					{ DataStore.LibraryFloorType.History, SpriteSetType.Normal },
					{ DataStore.LibraryFloorType.Technological, SpriteSetType.Normal },
					{ DataStore.LibraryFloorType.Literature, SpriteSetType.Normal },
					{ DataStore.LibraryFloorType.Art, SpriteSetType.Normal },
				};

				public EGOButtonVisualData GetData(SpriteSetType set, SpriteTypeEGOButton type)
				{
					if (SpriteSets.TryGetValue(set, out var dict) && dict.TryGetValue(type, out var data))
					{
						return data;
					}

					return null;
				}

				public string GetPath(SpriteSetType set, SpriteTypeEGOButton type) => GetData(set, type)?.Path;

				public SpriteTypeEGOButton? GetDefault(SpriteSetType set)
				{
					if (SpriteSets.TryGetValue(set, out var dict))
					{
						return dict.FirstOrDefault(kvp => kvp.Value.IsDefault).Key;
					}

					return null;
				}

				public SpriteSetType GetSetForFloor(DataStore.LibraryFloorType floor)
				{
					if (FloorToSetMap.TryGetValue(floor, out var setType))
					{
						return setType;
					}

					return SpriteSetType.Normal;
				}
			}


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
