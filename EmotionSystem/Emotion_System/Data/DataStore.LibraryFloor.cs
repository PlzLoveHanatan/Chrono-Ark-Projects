using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;

namespace EmotionSystem
{
	public partial class DataStore
	{
		public class LibraryFloor
		{
			public static LibraryFloorType CurrentFloorType => (LibraryFloorType)ModManager.getModInfo("EmotionSystem").GetSetting<DropdownSetting>("Library Floor").Value;

			public static LibraryFloorData CurrentFloor
			{
				get { return Floors[CurrentFloorType]; }
			}

			public static readonly Dictionary<LibraryFloorType, LibraryFloorData> Floors = new Dictionary<LibraryFloorType, LibraryFloorData>
		{
			{
				LibraryFloorType.History, new LibraryFloorData
				{
					Abnormalities = DataStore.Instance.Abnormalities.History,
					Egos = DataStore.Instance.EGO.HistoryKeyList,
				}
			},

			{
				LibraryFloorType.Technological, new LibraryFloorData
				{
					Abnormalities = DataStore.Instance.Abnormalities.Technological,
					Egos = DataStore.Instance.EGO.TechnologicalKeyList,
				}
			},

			{
				LibraryFloorType.Literature, new LibraryFloorData
				{
					Abnormalities = DataStore.Instance.Abnormalities.Literature,
					Egos = DataStore.Instance.EGO.LiteratureKeyList,
				}
			},
		};
		}

		public enum LibraryFloorType
		{
			History = 0,
			Technological = 1,
			Literature = 2
		}

		public class LibraryFloorData
		{
			public List<DataStore.Abnormality> Abnormalities;
			public List<string> Egos;
		}
	}
}
