using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using ChronoArkMod.ModData;
using DarkTonic.MasterAudio;
using GameDataEditor;
using I2.Loc;
using Spine;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.UI;
using static MiyukiSone.Affection;
using static MiyukiSone.Skills;
using static MiyukiSone.UtilsUI;

namespace MiyukiSone
{
	public static class Utils
	{
		public static ModInfo ThisMod => ModManager.getModInfo("MiyukiSone");
		public static TempSaveData Pd => PlayData.TSavedata;
		public static FieldSystem Fs => FieldSystem.instance;
		public static BattleSystem Bs => BattleSystem.instance;
		public static BattleTeam AllyTeam => Bs?.AllyTeam;
		public static BattleTeam EnemyTeam => Bs?.EnemyTeam;
		public static BattleChar DummyChar => AllyTeam?.DummyChar;
		public static BattleChar MiyukiBchar => AllyTeam.AliveChars?.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Miyuki);
		public static MiyukCV MiyukiData => GetOrCreateMiyukiData();
		public static bool MiyukiInParty => PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Miyuki);
		private static GameObject _currentTempGO;

		public static MiyukCV GetOrCreateMiyukiData()
		{
			var data = PlayData.TSavedata.GetCustomValue<MiyukCV>();
			if (data == null)
			{
				data = new MiyukCV();
				PlayData.TSavedata.AddCustomValue(data);
			}
			return data;
		}

		public static void PlaySound(string sound, bool isStopOldBus = false, float? volume = null)
		{
			if (string.IsNullOrEmpty(sound)) return;

			if (isStopOldBus) MasterAudio.StopBus("SE");

			float finalVolume = volume ?? MasterAudio.MasterVolumeLevel;
			MasterAudio.PlaySound(sound, finalVolume, null, 0f, null, null, false, false);
		}

		public static void PlaySound(string sound)
		{
			PlaySound(sound, false, null);
		}

		public static void PlaySound(string sound, bool isStopOldBus)
		{
			PlaySound(sound, isStopOldBus, null);
		}

		public static void PlaySound(string sound, float volume)
		{
			PlaySound(sound, false, volume);
		}


		public static void PlaySong()
		{
			MasterAudio.FadeBusToVolume("FieldBGM", 0f, 1f, null, false, false);
			MasterAudio.FadeBusToVolume("BattleBGM", 0f, 1f, null, false, false);
			MasterAudio.StopBus("BGM");
			MasterAudio.StopBus("BGM");
			MasterAudio.StopBus("StoryBGM");

			List<string> songKeys = new List<string>()
			{
				"DreamLove",
				"YouAndHer",
				"Monochrome",
				"MerryGoRound",
			};

			if (SaveManager.NowSaveSlot.SoundBGMVolume == 0 || SaveManager.NowSaveSlot.SoundMainVolume == 0)
			{
				MiyukiData.BGMVolumeIncreased = true;
				ChangeSettingsVolume(40);
			}

			var availableSongs = songKeys.ToList();
			if (MiyukiData.LastSong != -1 && availableSongs.Count > 1) availableSongs.RemoveAt(MiyukiData.LastSong);
			int randomIndex = RandomManager.RandomInt("MiyukiRandomSong", 0, availableSongs.Count);
			MiyukiData.LastSong = randomIndex;
			PlaySound(availableSongs[randomIndex], true);
		}

		public static void ChangeSettingsVolume(int volume)
		{
			SaveManager.NowSaveSlot.SoundMainVolume = volume;
			SaveManager.NowSaveSlot.SoundBGMVolume = volume;
			SaveManager.NowSaveSlot.SaveSoundData();
			SaveManager.savemanager.OptionApply(false, false);
		}

		// fix the audio volume
		public static void PlaySoundFromAsset(string audioPath, bool isStopOldBus = true, int? volumePercent = null)
		{
			if (string.IsNullOrEmpty(audioPath)) return;

			AudioClip clip = UtilsUI.GetAssets<AudioClip>(audioPath);
			if (clip == null)
			{
				Debug.LogWarning($"AudioClip not found: {audioPath}");
				return;
			}

			if (isStopOldBus && _currentTempGO != null)
			{
				MasterAudio.StopBus("SE");
				GameObject.Destroy(_currentTempGO);
				_currentTempGO = null;
			}

			GameObject tempGO = new GameObject("TempAudio");
			AudioSource audioSource = tempGO.AddComponent<AudioSource>();

			//float finalVolume = volumePercent.HasValue ? Mathf.Clamp(volumePercent.Value / 100f, 0f, 2f) : MasterAudio.MasterVolumeLevel;
			float volume = SaveManager.NowSaveSlot.SoundEffectVolume == 0 ? 1f : SaveManager.NowSaveSlot.SoundEffectVolume / 100;
			audioSource.PlayOneShot(clip, volume);

			_currentTempGO = tempGO;
			UnityEngine.Object.Destroy(tempGO, clip.length);

			Debug.Log($"🎯 Playing audio: {audioPath}");
		}

		public static void ShowText(string text, bool isEvent)
		{
			bool isSoftText = IsDere || IsKuudere;
			if (string.IsNullOrEmpty(text) || MiyukiBchar.IsDead || MiyukiBchar == null) return;
			var position = MiyukiBchar.GetTopPos();
			if (isSoftText) MiyukiBchar.StartCoroutine(TextSoft(position, text));
			else BattleSystem.DelayInput(TextHard(position, text, isEvent));
		}

		private static IEnumerator TextSoft(Vector3 position, string text)
		{
			var topText = BattleText.CustomText(position, text);
			yield return new WaitForSecondsRealtime(3.5f);
			topText?.End();
		}

		private static IEnumerator TextHard(Vector3 position, string text, bool isEvent)
		{
			yield return BattleText.InstBattleTextAlly_Co(position, text, isEvent);
			yield break;
		}

		public static void ChangeSkillImage(this Skill skill, string skillSpritePath = null, string buttonSpritePath = null, string basicSpritePath = null, string defaultSkillKey = null, bool isRestoreImg = false, bool isGlicthEffect = false)
		{
			if (isRestoreImg && !string.IsNullOrEmpty(defaultSkillKey))
			{
				var defaultSkill = Skill.TempSkill(defaultSkillKey, skill.Master, skill.Master.MyTeam);
				skill.Image_Skill = defaultSkill.Image_Skill;
				skill.Image_Button = defaultSkill.Image_Button;
				skill.Image_Basic = defaultSkill.Image_Basic;
			}
			else
			{
				if (!string.IsNullOrEmpty(skillSpritePath))
				{
					string address = GetSpriteAddress(skillSpritePath + ".png");
					skill.Image_Skill = address;
				}

				if (!string.IsNullOrEmpty(buttonSpritePath))
				{
					string address = GetSpriteAddress(buttonSpritePath + ".png");
					skill.Image_Button = address;
				}

				if (!string.IsNullOrEmpty(basicSpritePath))
				{
					string address = GetSpriteAddress(basicSpritePath + ".png");
					skill.Image_Basic = address;
				}

				if (Bs != null)
				{
					BattleSystem.instance.StartCoroutine(BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));
					if (isGlicthEffect) GlitchEffect(skill);
					foreach (var ex in skill.AllExtendeds)
					{
						if (ex is MiyukiSkill miyukiSkill) miyukiSkill.Init();
					}
				}
			}
		}

		public static void SkillChange(this Skill changeFrom, Skill changeTo, bool keepID = true, bool keepExtended = true, bool isGlitchEffect = false)
		{
			if (changeFrom.MyButton != null && isGlitchEffect) GlitchEffect(changeFrom);

			List<Skill_Extended> ExtendedToKeep = new List<Skill_Extended>();
			ExtendedToKeep.AddRange(changeTo.AllExtendeds.Select(ex => ex.Clone() as Skill_Extended));
			foreach (Skill_Extended skill_Extended in changeFrom.AllExtendeds)
			{
				foreach (string text in changeFrom.MySkill.SkillExtended)
				{
					if (keepExtended && !text.Contains(skill_Extended.Name))
					{
						ExtendedToKeep.Add(skill_Extended.Clone() as Skill_Extended);
					}
					skill_Extended.SelfDestroy();
				}
			}

			bool createExcept = keepExtended && changeFrom.isExcept;
			changeFrom.Init(changeTo.MySkill, changeFrom.Master, changeFrom.Master.MyTeam);
			if (createExcept) changeFrom.isExcept = true;

			foreach (var skill_Extended in ExtendedToKeep)
			{
				if (skill_Extended.BattleExtended)
				{
					changeFrom.ExtendedAdd_Battle(skill_Extended);
				}
				else
				{
					changeFrom.ExtendedAdd(skill_Extended);
				}
			}

			changeFrom.Image_Skill = changeTo.Image_Skill;
			changeFrom.Image_Button = changeTo.Image_Button;
			changeFrom.Image_Basic = changeTo.Image_Basic;

			if (changeFrom.CharinfoSkilldata == null) changeFrom.CharinfoSkilldata = new CharInfoSkillData(changeFrom.MySkill);

			changeFrom.CharinfoSkilldata.SkillInfo = changeFrom.MySkill;
			Skill_Extended oldUpgrade = changeFrom.CharinfoSkilldata.SKillExtended;
			if (!keepID)
			{
				changeFrom.CharinfoSkilldata.CopyData(changeTo.CharinfoSkilldata);
			}
			if (keepExtended)
			{
				changeFrom.CharinfoSkilldata.SKillExtended = oldUpgrade;
			}
			else
			{
				changeFrom.CharinfoSkilldata.SKillExtended = changeTo.CharinfoSkilldata.SKillExtended;
			}
			BattleSystem.instance.StartCoroutine(BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));
		}

		public static void GlitchEffect(this Skill changeFrom, float time = 0)
		{
			if (changeFrom.MyButton != null)
			{
				time = time > 0 ? time : 0.3f;
				UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("StoryGlitch/GlitchSkillEffect"), changeFrom.MyButton.transform);
				UnityEngine.Object.Destroy(obj, time);
			}
		}

		public static void CelestialUpgrade(this Skill skill)
		{
			if (skill?.Master == null || skill.CharinfoSkilldata?.SKillExtended != null) return;

			List<string> allKeys = new List<string>();
			GDEDataManager.GetAllDataKeysBySchema(GDESchemaKeys.SkillExtended, out allKeys);

			List<Skill_Extended> validForParty = new List<Skill_Extended>();

			foreach (string key in allKeys)
			{
				GDESkillExtendedData data = new GDESkillExtendedData(key);

				if (!data.Drop || data.Debuff) continue;
				if (PlayData.TSavedata.Party.Find(c => c.KeyData == data.NeedCharacter) == null) continue;

				Skill_Extended ex = Skill_Extended.DataToExtended(data);

				if (PlayData.Battleallys.SelectMany(bc => bc.Skills).Any(s => ex.CanEnforce(s))) validForParty.Add(ex);
			}

			if (validForParty.Count == 0) return;

			List<Skill_Extended> validForThisSkill = validForParty.Where(ex => ex.CanEnforce(skill)).ToList();

			if (validForThisSkill.Count == 0) return;

			Skill_Extended selected = validForThisSkill.Random("CelestialUpgrade");
			skill.CharinfoSkilldata.SKillExtended = selected;
			skill.ExtendedAdd_Battle(selected);
		}
	}
}
