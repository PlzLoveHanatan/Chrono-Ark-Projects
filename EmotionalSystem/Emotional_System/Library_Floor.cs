using System;
using System.Collections.Generic;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using UnityEngine;
using EmotionalSystem;

namespace EmotionalSystem
{
	public static class LibraryFloor
	{
		public static LibraryFloorType CurrentFloorType => (LibraryFloorType)ModManager.getModInfo("EmotionalSystem").GetSetting<DropdownSetting>("Library Floor").Value;

		public static LibraryFloorData CurrentFloor
		{
			get { return Floors[CurrentFloorType]; }
		}

		public static readonly Dictionary<LibraryFloorType, LibraryFloorData> Floors = new Dictionary<LibraryFloorType, LibraryFloorData>
		{
			{
				LibraryFloorType.History, new LibraryFloorData
				{
					Abnormalities = DataStore.AbnormalityData.History,
					Egos = DataStore.EGOData.HistoryKeyList,
				}
			},

			{
				LibraryFloorType.Technological, new LibraryFloorData
				{
					Abnormalities = DataStore.AbnormalityData.Technological,
					Egos = DataStore.EGOData.TechnologicalKeyList,
				}
			},
		};
	}

	public enum LibraryFloorType
	{
		History = 0,
		Technological = 1
	}

	public class LibraryFloorData
	{
		public List<Abnormality> Abnormalities;
		public List<string> Egos;
	}
}
