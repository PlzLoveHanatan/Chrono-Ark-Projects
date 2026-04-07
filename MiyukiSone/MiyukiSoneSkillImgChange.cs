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

			public SkillImageSet() { }
		}

		private static Dictionary<string, Dictionary<MiyukiAffection, SkillImageSet>> _skillImages;
		private static Dictionary<string, MiyukiAffection> _currentState = new Dictionary<string, MiyukiAffection>();

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

			if (jsonContent != null)
			{
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
			else
			{
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

			bool stateChanged;
			if (_currentState.TryGetValue(skillID, out var oldState))
			{
				stateChanged = oldState != newState;
			}
			else
			{
				stateChanged = true;
			}

			var imageSet = FindBestImageSet(stateMap, newState);
			if (imageSet == null) return;

			_currentState[skillID] = newState;

			if (imageSet.useDefault)
			{
				skill.ChangeSkillImage(isRestoreImg: true, defaultSkillKey: skillID, isGlicthEffect: true);
			}
			else if (!string.IsNullOrEmpty(imageSet.skillPath))
			{
				skill.ChangeSkillImage(imageSet.skillPath, imageSet.buttonPath, imageSet.basicPath, isGlicthEffect: stateChanged);
			}
		}

		public static void ReloadConfig()
		{
			_skillImages = null;
			_currentState.Clear();
		}
	}
}