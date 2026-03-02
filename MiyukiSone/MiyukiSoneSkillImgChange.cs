using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace MiyukiSone
{
	public static class MiyukiSoneSkillImgChange
	{
		[Serializable]
		public class SkillImageSet
		{
			public string skillPath;
			public string buttonPath;
			public string basicPath;
			public bool useDefault;

			public SkillImageSet() { }
		}

		private static Dictionary<string, Dictionary<MiyukiAffectionState, SkillImageSet>> _skillImages;
		private static Dictionary<string, MiyukiAffectionState> _currentState = new Dictionary<string, MiyukiAffectionState>();

		public static Dictionary<string, Dictionary<MiyukiAffectionState, SkillImageSet>> SkillImages
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
				_skillImages = new Dictionary<string, Dictionary<MiyukiAffectionState, SkillImageSet>>();
				return;
			}

			try
			{
				_skillImages = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<MiyukiAffectionState, SkillImageSet>>>(jsonContent);
				if (_skillImages == null) _skillImages = new Dictionary<string, Dictionary<MiyukiAffectionState, SkillImageSet>>();
			}
			catch (Exception e)
			{
				Debug.LogError($"[Miyuki] Error loading skill images: {e.Message}");
				_skillImages = new Dictionary<string, Dictionary<MiyukiAffectionState, SkillImageSet>>();
			}
		}

		private static SkillImageSet FindBestImageSet(Dictionary<MiyukiAffectionState, SkillImageSet> stateMap, MiyukiAffectionState targetState)
		{
			if (stateMap.TryGetValue(targetState, out var exactSet)) return exactSet;
			if (stateMap.TryGetValue(MiyukiAffectionState.Kuudere, out var kuudereSet)) return kuudereSet;
			return stateMap.Values.FirstOrDefault();
		}

		public static void ChangeImg(this Skill skill)
		{
			if (skill == null) return;

			string skillID = skill.MySkill.KeyID;
			MiyukiAffectionState newState = Affection.CurrentAffectionState;

			if (!SkillImages.TryGetValue(skillID, out var stateMap) || stateMap == null || stateMap.Count == 0) return;

			if (_currentState.TryGetValue(skillID, out var currentState) && currentState == newState) return;

			var imageSet = FindBestImageSet(stateMap, newState);
			if (imageSet == null) return;

			_currentState[skillID] = newState;

			if (imageSet.useDefault)
			{
				skill.ChangeSkillImages(isRestoreImg: true, defaultSkillKey: skillID);
			}
			else if (!string.IsNullOrEmpty(imageSet.skillPath))
			{
				skill.ChangeSkillImages(imageSet.skillPath, imageSet.buttonPath, imageSet.basicPath);
			}
		}

		public static void ReloadConfig()
		{
			_skillImages = null;
			_currentState.Clear();
		}
	}
}