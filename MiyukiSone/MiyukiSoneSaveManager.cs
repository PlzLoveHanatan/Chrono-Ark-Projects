using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public class MiyukiSaveManager
	{
		[Serializable]
		public class MiyukiSaveData
		{
			public int AffectionPoints;
			public int? LockedState; // Храним как int? для сериализации
			public bool GameUpdated;
			public bool GameRestarted;
			public bool EternalPromise;
			//public Dictionary<string, bool> UnlockedSkills;
			//public List<string> CompletedEvents;
		}

		public MiyukiSaveData CurrentData => _currentData;
		private MiyukiSaveData _currentData;
		private static MiyukiSaveManager _instance;

		public static MiyukiSaveManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new MiyukiSaveManager();
					_instance.Load();
				}
				return _instance;
			}
		}

		private MiyukiSaveManager()
		{
			_currentData = new MiyukiSaveData();
		}

		private static string SavePath => Path.Combine(Application.persistentDataPath, "MiyukiSone", "save.json");

		public void Save()
		{
			string json = JsonConvert.SerializeObject(_currentData, Formatting.Indented);
			Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
			File.WriteAllText(SavePath, json);
			Debug.Log($"[Miyuki] Game saved to: {SavePath}");
		}

		public void Load()
		{
			if (!File.Exists(SavePath))
			{
				Debug.Log("[Miyuki] No save file found, using defaults");
				return;
			}

			try
			{
				string json = File.ReadAllText(SavePath);
				_currentData = JsonConvert.DeserializeObject<MiyukiSaveData>(json) ?? new MiyukiSaveData();
				Debug.Log("[Miyuki] Game loaded successfully!");
			}
			catch (Exception e)
			{
				Debug.LogError($"[Miyuki] Error loading save: {e.Message}");
				_currentData = new MiyukiSaveData();
			}
		}

		public void ResetSave()
		{
			_currentData = new MiyukiSaveData();
			if (File.Exists(SavePath)) File.Delete(SavePath);
			Debug.Log("[Miyuki] Save reset");
		}
	}
}