using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc.SimpleJSON;
using Newtonsoft.Json;
using Steamworks;
using UnityEngine;

namespace EnDub
{
	public class EnDubSaveManager
	{
		[Serializable]
		public class EnDubSaveData
		{
			public float MainAudioVolume = 3;
			public float Azar;
			public float Charon;
			public float Johan;
			public float Huz;
			public float Lian;
			public float Narhan;
			public float Silverstein;
			public float Sizz;
		}

		private EnDubSaveManager()
		{
			_currentData = new EnDubSaveData();
		}

		public EnDubSaveData CurrentData => _currentData;
		private EnDubSaveData _currentData;

		private static EnDubSaveManager _instance;

		public static EnDubSaveManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new EnDubSaveManager();
					_instance.Load();
				}
				return _instance;
			}
		}
		private static string SavePath => Path.Combine(Application.persistentDataPath, "Endub", "Settings.json");

		private static readonly Dictionary<string, Func<EnDubSaveData, float>> CharacterVolumeMap = new Dictionary<string, Func<EnDubSaveData, float>>
		{
			{ "Azar", data => data.Azar },
			{ "Charon", data => data.Charon },
			{ "Johan", data => data.Johan},
			{ "Huz", data => data.Huz },
			{ "Lian", data => data.Lian },
			{ "Narhan", data => data.Narhan },
			{ "Silverstein", data => data.Silverstein },
			{ "Sizz", data => data.Sizz }
		};

		public void Save()
		{
			string json = JsonConvert.SerializeObject(_currentData, Formatting.Indented);
			Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
			File.WriteAllText(SavePath, json);
		}

		public void Load()
		{
			if (File.Exists(SavePath))
			{
				try
				{
					string json = File.ReadAllText(SavePath);
					_currentData = JsonConvert.DeserializeObject<EnDubSaveData>(json) ?? new EnDubSaveData();
				}
				catch (Exception e)
				{
					Debug.Log(e);
				}
			}
			else
			{
				Save();
				Debug.Log($"Json file is not found in the {SavePath} path");
			}
		}

		public float GetCharacterVolume(string character)
		{
			if (CharacterVolumeMap.TryGetValue(character, out var getter))
			{
				return getter(_currentData);
			}
			return 0;
		}

		public float FinalVolume(string character)
		{
			float charVolume = GetCharacterVolume(character);
			return charVolume > 0 ? charVolume : CurrentData.MainAudioVolume;
		}
	}
}