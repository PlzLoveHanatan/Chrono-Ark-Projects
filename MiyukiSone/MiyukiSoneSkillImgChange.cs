using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class MiyukiSkillImgChange
	{
		[Serializable]
		public class SkillImageSet
		{
			public string skillPath;
			public string buttonPath;
			public string basicPath;
			public bool useDefault;
			public List<PointsThreshold> pointsThresholds;

			public SkillImageSet() { }
		}

		[Serializable]
		public class PointsThreshold
		{
			public int minPoints;
			public string skillPath;
			public string buttonPath;
			public string basicPath;
			public bool useDefault;
		}

		private static Dictionary<string, Dictionary<MiyukiAffection, SkillImageSet>> _skillImages;
		private static Dictionary<string, MiyukiAffection> _currentState = new Dictionary<string, MiyukiAffection>();
		private static Dictionary<string, int> _currentPoints = new Dictionary<string, int>();

		public static Dictionary<string, Dictionary<MiyukiAffection, SkillImageSet>> SkillImages
		{
			get
			{
				if (_skillImages == null) LoadSkillImages();
				return _skillImages;
			}
		}

		private static void LoadSkillImages()
		{
			string jsonContent = MiyukiJsonReader.LoadJson("ImgPaths.json");

			if (jsonContent == null)
			{
				Debug.LogError("[Miyuki] Failed to load ImgPaths.json");
				_skillImages = new Dictionary<string, Dictionary<MiyukiAffection, SkillImageSet>>();
				return;
			}

			try
			{
				_skillImages = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<MiyukiAffection, SkillImageSet>>>(jsonContent);
				if (_skillImages == null) _skillImages = new Dictionary<string, Dictionary<MiyukiAffection, SkillImageSet>>();
			}
			catch (Exception e)
			{
				Debug.LogError($"[Miyuki] Error loading skill images: {e.Message}");
				_skillImages = new Dictionary<string, Dictionary<MiyukiAffection, SkillImageSet>>();
			}
		}

		private static SkillImageSet FindBestImageSet(Dictionary<MiyukiAffection, SkillImageSet> stateMap, MiyukiAffection targetState)
		{
			if (stateMap.TryGetValue(targetState, out var exactSet)) return exactSet;
			if (stateMap.TryGetValue(MiyukiAffection.Kuudere, out var kuudereSet)) return kuudereSet;
			return stateMap.Values.FirstOrDefault();
		}

		public static void ChangeSkillImg(this Skill skill)
		{
			if (skill == null) return;

			string skillID = skill.MySkill.KeyID;
			MiyukiAffection newState = CurrentAffection;

			if (!SkillImages.TryGetValue(skillID, out var stateMap) || stateMap == null || stateMap.Count == 0) return;

			// Для скилов с thresholds в DereDere
			if (newState == MiyukiAffection.DereDere && stateMap.TryGetValue(MiyukiAffection.DereDere, out var deredereSet)
				&& deredereSet.pointsThresholds != null && deredereSet.pointsThresholds.Count > 0) 
			{
				int currentPoints = 0;
				var threshold = deredereSet.pointsThresholds.Where(t => currentPoints >= t.minPoints).OrderByDescending(t => t.minPoints).FirstOrDefault();

				if (threshold != null)
				{
					// Проверяем изменился ли threshold
					if (_currentPoints.TryGetValue(skillID, out var lastPoints) && lastPoints == threshold.minPoints) return;

					_currentPoints[skillID] = threshold.minPoints;

					if (threshold.useDefault) skill.ChangeSkillImage(isRestoreImg: true, defaultSkillKey: skillID);
					else skill.ChangeSkillImage(threshold.skillPath, threshold.buttonPath, threshold.basicPath);
					return;
				}
			}

			// Для всех остальных случаев (включая DereDere без thresholds)
			if (_currentState.TryGetValue(skillID, out var currentState) && currentState == newState) return;

			var imageSet = FindBestImageSet(stateMap, newState);
			if (imageSet == null) return;

			_currentState[skillID] = newState;

			if (imageSet.useDefault) skill.ChangeSkillImage(isRestoreImg: true, defaultSkillKey: skillID);
			else if (!string.IsNullOrEmpty(imageSet.skillPath)) skill.ChangeSkillImage(imageSet.skillPath, imageSet.buttonPath, imageSet.basicPath);
		}

		public static void ReloadConfig()
		{
			_skillImages = null;
			_currentState.Clear();
			_currentPoints.Clear();
		}
	}
}