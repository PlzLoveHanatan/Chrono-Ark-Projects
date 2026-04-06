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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static CharacterDocument;
using static MiyukiSone.Affection;
using static MiyukiSone.Buffs;
using static MiyukiSone.EventsData;
using static MiyukiSone.Skills;
using static MiyukiSone.UtilsUI;

namespace MiyukiSone
{
	public static class Utils
	{
		public static ModInfo ThisMod => ModManager.getModInfo("MiyukiSone");
		public static int SameSkillNum => SaveManager.Difficalty == 2 ? 4 : 3;
		public static int MinSkillNum => SaveManager.Difficalty == 2 ? 7 : 6;
		public static TempSaveData Pd => PlayData.TSavedata;
		public static BattleChar DummyChar => BattleSystem.instance?.DummyChar;

		public static BattleChar MiyukiBchar
		{
			get
			{
				if (BattleSystem.instance.AllyTeam.AliveChars == null) return null;
				return BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(c => c.Info?.KeyData == ModItemKeys.Character_Miyuki);
			}
		}

		public static BattleAlly MiyukiChar
		{
			get
			{
				if (PlayData.TSavedata?.Party == null) return null;
				return PlayData.TSavedata.Party.FirstOrDefault(c => c.KeyData == ModItemKeys.Character_Miyuki)?.GetBattleChar as BattleAlly;
			}
		}

		public static MiyukCV MiyukiData => GetOrCreateMiyukiData();

		public static bool MiyukiInParty
		{
			get
			{
				if (PlayData.TSavedata?.Party == null) return false;
				return PlayData.TSavedata.Party.Any(c => c.KeyData == ModItemKeys.Character_Miyuki);
			}
		}

		public static MiyukiPassive GetMiyukiPassive
		{
			get
			{
				var miyuki = MiyukiBchar;
				if (miyuki == null) return null;
				return miyuki.Info?.Passive as MiyukiPassive;
			}
		}

		public static AffectionOverflow MiyukiBuff
		{
			get
			{
				if (MiyukiBchar == null) return null;
				return MiyukiBchar.BuffReturn(ModItemKeys.Buff_B_Miyuki_Passive, false) as AffectionOverflow;
			}
		}

		public static void CheckMiyukiDraw(bool createCharDraw = false, bool changeIcon = false)
		{
			if (createCharDraw) MiyukiPassive.CreateCharacterDraw = createCharDraw;
			MiyukiBuff?.Init();
			if (changeIcon) MiyukiBuff?.ChangeIcon();
		}

		public static void RefreshMiyukiCharacterDraw()
		{
			var miyukiSkills = BattleSystem.instance.AllyTeam.Skills.Concat(BattleSystem.instance.AllyTeam.Skills_Deck).Concat(BattleSystem.instance.AllyTeam.Skills_UsedDeck).Where(s => s.Master == MiyukiBchar)?.Select(s => s.MySkill.KeyID).ToList();
			var shouldHaveDraws = miyukiSkills.Where(MiyukiPassive.CharacterDrawList.ContainsKey).Select(s => MiyukiPassive.CharacterDrawList[s]).Distinct().ToList();
			MiyukiPassive.AvaliableCharacterDraw.RemoveAll(draw => !shouldHaveDraws.Contains(draw));
			shouldHaveDraws.ForEach(s => { if (!MiyukiPassive.AvaliableCharacterDraw.Contains(s)) MiyukiPassive.AvaliableCharacterDraw.Add(s); });
			if (MiyukiPassive.AvaliableCharacterDraw.Count == 0) CheckMiyukiDraw(false, true);
		}

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

		private static GameObject _currentTempGO;

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
				"AboutYou",
			};

			SetVolumeSettings();
			songKeys.RandomElement()?.Let(s => PlaySound(s, true));			
		}

		public static void StopSong(bool? isPauseBool = null)
		{
			MasterAudio.StopBus("BGM");
			MasterAudio.FadeBusToVolume("BattleBGM", 1f, 0.5f);
			MasterAudio.FadeBusToVolume("BGM", 1f, 0.5f);
			MasterAudio.FadeBusToVolume("FieldBGM", 1f, 0.5f);
			RestoreVolumeSettings();
			if (isPauseBool.HasValue) MiyukiData.PauseOpen = isPauseBool.Value;
		}

		public static void SetVolumeSettings()
		{
			if (SaveManager.NowSaveSlot.SoundBGMVolume != 0 && SaveManager.NowSaveSlot.SoundMainVolume != 0) return;

			if (MiyukiData.BGMVolumeIncreased) return;

			MiyukiSaveManager.Instance.CurrentData.SoundVolumeMain = SaveManager.NowSaveSlot.SoundMainVolume;
			MiyukiSaveManager.Instance.CurrentData.SoundVolumeBGM = SaveManager.NowSaveSlot.SoundBGMVolume;
			MiyukiSaveManager.Instance.CurrentData.SoundVolumeEffect = SaveManager.NowSaveSlot.SoundEffectVolume;
			SaveManager.NowSaveSlot.SoundMainVolume = 40;
			SaveManager.NowSaveSlot.SoundBGMVolume = 40;
			SaveManager.NowSaveSlot.SoundEffectVolume = 40;
			MiyukiData.BGMVolumeIncreased = true;
			SaveManager.NowSaveSlot.SaveSoundData();
			SaveManager.savemanager.OptionApply(false, false);
		}

		public static void RestoreVolumeSettings()
		{
			if (!MiyukiData.BGMVolumeIncreased) return;

			SaveManager.NowSaveSlot.SoundMainVolume = MiyukiSaveManager.Instance.CurrentData.SoundVolumeMain;
			SaveManager.NowSaveSlot.SoundBGMVolume = MiyukiSaveManager.Instance.CurrentData.SoundVolumeBGM;
			SaveManager.NowSaveSlot.SoundEffectVolume = MiyukiSaveManager.Instance.CurrentData.SoundVolumeEffect;
			MiyukiData.BGMVolumeIncreased = false;
			SaveManager.NowSaveSlot.SaveSoundData();
			SaveManager.savemanager.OptionApply(false, false);
		}

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

			float volume = SaveManager.NowSaveSlot.SoundEffectVolume == 0 ? 1f : SaveManager.NowSaveSlot.SoundEffectVolume / 100f;
			audioSource.PlayOneShot(clip, volume);

			_currentTempGO = tempGO;
			UnityEngine.Object.Destroy(tempGO, clip.length);

			Debug.Log($"🎯 Playing audio: {audioPath}");
		}

		public static void StartMiyukiText(string text)
		{
			if (BattleSystem.instance != null) MiyukiTextBattle(text);
			else if (FieldSystem.instance != null) MiyukiTextField(text);
		}

		private static void MiyukiTextBattle(string text)
		{
			if (string.IsNullOrEmpty(text) /*|| !MiyukiInParty*/) return;
			DummyChar.StartCoroutine(MiyukiTextBattle(Position(), text));
		}

		public static void MiyukiTextField(string text)
		{
			if (string.IsNullOrEmpty(text) /*|| !MiyukiInParty*/) return;
			FieldSystem.instance.StartCoroutine(MiyukiTextField(AllyWindow(), text));
			//if (window != null) BattleText.InstFieldText(window, text);
		}

		private static IEnumerator MiyukiTextField(AllyWindow window, string text)
		{
			if (string.IsNullOrEmpty(text) || window == null) yield break;
			BattleText component = Misc.UIInst(UIManager.inst.BattleTalkTextUI, window.TextPos).GetComponent<BattleText>();
			component.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			component.transform.rotation = GetRandomRotation();
			component.transform.localPosition -= GetRandomTextPosition();
			component.Ptext.TextInput(text);
			yield return component.BattleTextOut();
		}

		private static IEnumerator MiyukiTextBattle(Vector3 position, string text)
		{
			if (string.IsNullOrEmpty(text) || position == null) yield break;
			BattleText component = Misc.UIInst(UIManager.inst.BattleTalkTextUI, BattleSystem.instance.MainUICanvas.transform).GetComponent<BattleText>();
			component.transform.localScale = GetRandomScale();
			component.transform.rotation = GetRandomRotation();
			component.transform.position = position;
			component.transform.GetComponent<RectTransform>().localPosition -= GetRandomTextPosition();
			component.Ptext.TextInput(text);
			if (MiyukiDecides) BattleSystem.instance.BattleWaitList.Remove(component.gameObject);
			yield return component.BattleTextOut();
		}

		private static Vector3 Position()
		{
			return MiyukiBchar != null ? MiyukiBchar.GetTopPos() : BattleSystem.instance.AllyTeam.AliveChars.Random().GetPos();
		}

		private static AllyWindow AllyWindow()
		{
			return PlayData.TSavedata.Party.FirstOrDefault(c => c.KeyData == ModItemKeys.Character_Miyuki).GetAllyWindow ?? PlayData.TSavedata.Party.Random().GetAllyWindow;
		}

		private static Quaternion GetRandomRotation()
		{
			Quaternion baseRotation = Quaternion.identity;
			if (MiyukiDecides) return baseRotation;
			float angle = RandomManager.RandomInt("MiyukiTextRotation", -30, 30);
			return Quaternion.Euler(0, 0, angle);
		}

		private static Vector3 GetRandomTextPosition()
		{
			Vector3 basePos = new Vector3(150f, 0f, 0f);
			if (MiyukiDecides) return basePos;
			float xOffset = RandomManager.RandomInt("MiyukiTextX", -125, 125);
			float yOffset = RandomManager.RandomInt("MiyukiTextY", -125, 125);
			return new Vector3(basePos.x + xOffset, basePos.y + yOffset, basePos.z);
		}

		private static Vector3 GetRandomScale()
		{
			Vector3 baseScale = new Vector3(0.9f, 0.9f, 0.9f);
			if (MiyukiDecides) return baseScale;
			float xOffset = RandomManager.RandomFloat("MiyukiScaleX", 0.6f, 1.3f);
			float yOffset = RandomManager.RandomFloat("MiyukiScaleY", 0.6f, 1.3f);
			float zOffset = RandomManager.RandomFloat("MiyukiScaleZ", 0.6f, 1.3f);
			return new Vector3(xOffset, yOffset, zOffset);
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

				if (BattleSystem.instance != null)
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
			if (skill == null || skill?.Master == null || skill.CharinfoSkilldata?.SKillExtended != null) return;

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

			if (validForThisSkill.Count == 0)
			{
				skill.NormalUpgrade();
				Debug.Log($"Celestial upgrade is unavailable, adding normal upgrade.");
			}
			else
			{
				Skill_Extended selected = validForThisSkill.Random("MiyukiCelestialUpgrade");
				skill.ExtendedAdd_Battle(selected);

				if (skill.Master?.Info?.SkillDatas != null)
				{
					var skillData = skill.Master.Info.SkillDatas.FirstOrDefault(sd => sd == skill.CharinfoSkilldata);
					if (skillData != null && skillData.SKillExtended == null)
					{
						skillData.SKillExtended = selected;
						Debug.Log($"Saved Celestial upgrade to SkillData: {selected.Data.Key}");
					}
				}

				Debug.Log($"Applied Celestial upgrade: {selected.Data.Key}");
			}
		}

		public static void NormalUpgrade(this Skill skill)
		{
			if (skill == null) return;

			var upgradeList = PlayData.GetEnforce(!MiyukiResult(), skill);
			var ex = upgradeList?.RandomElement()?.Let(ext => skill.ExtendedAdd_Battle(ext));

			var skillData = skill.Master.Info.SkillDatas.FirstOrDefault(sd => sd == skill.CharinfoSkilldata);
			if (skillData != null && skillData.SKillExtended == null)
			{
				skillData.SKillExtended = ex;
				Debug.Log($"Saved Normal upgrade to SkillData: {ex.Data.Key}");
			}
			else
			{
				Debug.Log($"Cannot apply Normal upgrade: to {skill.MySkill.KeyID}");
			}
			Debug.Log($"Applied Normal upgrade: {ex.Data.Key}");
		}

		public static T Let<T>(this T obj, Action<T> action)
		{
			if (obj != null) action(obj);
			return obj;
		}

		public static T RandomElement<T>(this IEnumerable<T> source, string key = null)
		{
			var list = source.ToList();
			if (string.IsNullOrEmpty(key)) key = "MiyukiRandom";
			return list.Count == 0 ? default : list.Random(key);
		}

		public static void GainEquip(int rarity, int? selectFrom = null)
		{
			rarity = Math.Max(0, Math.Min(4, rarity));
			List<string> equipList = rarity == 4 ? PlayData.TSavedata.EquipList_Legendary : rarity == 3 ? PlayData.TSavedata.EquipList_Unique : null;
			List<ItemBase> Equip = Enumerable.Range(0, selectFrom ?? 3).Select(_ => ItemBase.GetItem(PlayData.GetEquipRandom(rarity, false, null))).ToList();
			UIManager.InstantiateActive(UIManager.inst.SelectItemUI).GetComponent<SelectItemUI>().Init(Equip, new RandomItemBtn.SelectItemClickDel(i => { equipList?.Add(i.itemkey); InventoryManager.Reward(i); }));
		}
	}
}
