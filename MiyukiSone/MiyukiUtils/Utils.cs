using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using ChronoArkMod.ModData;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.SpatialTracking;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class Utils
	{
		public static TempSaveData Pd => PlayData.TSavedata;
		public static BattleSystem Bs => BattleSystem.instance;
		public static BattleTeam AllyTeam => Bs?.AllyTeam;
		public static BattleChar DummyChar => AllyTeam?.DummyChar;
		public static BattleChar MiyukiBchar => AllyTeam.AliveChars?.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Miyuki);
		public static MiyukiCV MiyukiData => GetOrCreateMiyukiData();
		public static ModInfo ThisMod => ModManager.getModInfo("MiyukiSone");
		public static bool MiyukiInParty()
		{
			return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Miyuki);
		}

		public static MiyukiCV GetOrCreateMiyukiData()
		{
			var data = PlayData.TSavedata.GetCustomValue<MiyukiCV>();
			if (data == null)
			{
				data = new MiyukiCV();
				PlayData.TSavedata.AddCustomValue(data);
			}
			return data;
		}

		public static void PlaySound(string sound)
		{
			if (string.IsNullOrEmpty(sound)) return;

			float volume = MasterAudio.MasterVolumeLevel;
			MasterAudio.PlaySound(sound, volume, null, 0f, null, null, false, false);
		}

		public static void PlaySound(string sound, bool isStopOldBus = true)
		{
			if (string.IsNullOrEmpty(sound)) return;

			if (isStopOldBus) MasterAudio.StopBus("SE");
			PlaySound(sound);
		}

		public static void RemoveSkill(Skill skill, bool isExclude = false)
		{
			if (skill == null) return;

			if (isExclude)
			{
				skill.Except();
			}
			else
			{
				BattleSystem.instance?.StartCoroutine(RemoveSkillCo(skill));
			}
		}

		private static IEnumerator RemoveSkillCo(Skill skill)
		{
			DeckList(AllyTeam.Skills, skill);
			DeckList(AllyTeam.Skills_Deck, skill);
			DeckList(AllyTeam.Skills_UsedDeck, skill);
			BattleSystem.instance?.ActWindow.Draw(AllyTeam, false);
			yield break;
		}

		private static void DeckList(List<Skill> list, Skill skill)
		{
			int index = list.FindIndex(s => s.CharinfoSkilldata == skill.CharinfoSkilldata);

			if (index != -1)
			{
				list.RemoveAt(index);
			}
		}

		public static void ShowText(string text, bool isEvent)
		{
			var position = MiyukiBchar.GetTopPos();
			bool isSoftText = SoftText();
			if (string.IsNullOrEmpty(text) || MiyukiBchar.IsDead) return;

			if (isSoftText)
			{
				MiyukiBchar.StartCoroutine(TextSoft(position, text));
			}
			else
			{
				BattleSystem.DelayInput(TextHard(position, text, isEvent));
			}
		}

		private static bool SoftText()
		{
			bool isSoftText;
			switch (CurrentAffection)
			{
				case MiyukiAffectionState.adoration: isSoftText = true; break;
				case MiyukiAffectionState.eradication: isSoftText = false; break;
				default: isSoftText = true; break;
			}
			return isSoftText;
		}

		private static IEnumerator TextSoft(Vector3 position, string text)
		{
			var topText = BattleText.CustomText(position, text);
			yield return new WaitForSecondsRealtime(3f);
			topText?.End();
		}

		private static IEnumerator TextHard(Vector3 position, string text, bool isEvent)
		{
			yield return BattleText.InstBattleTextAlly_Co(position, text, isEvent);
			yield break;
		}

		public static void BattleFaceChange(string path)
		{
			ModInfo modInfo = ModManager.getModInfo("MiyukiSone");
			string facePath = modInfo.assetInfo.ImageFromFile("MiyukiVisual/" + path);

			MiyukiBchar.Info.GetData.face_Path = facePath;
			var imageComponent = MiyukiBchar.UI.CharImage.GetComponent<UnityEngine.UI.Image>();
			if (imageComponent != null)
			{
				AddressableLoadManager.LoadAsyncAction(facePath, AddressableLoadManager.ManageType.Character, imageComponent);
			}
		}

		public static string GetLocalizedText<T>(T line) where T : class
		{
			if (line == null) return "I'm Error";

			string lang = I2.Loc.LocalizationManager.CurrentLanguage;

			var type = typeof(T);

			string english = (string)type.GetProperty("English")?.GetValue(line);
			string korean = (string)type.GetProperty("Korean")?.GetValue(line);
			string japanese = (string)type.GetProperty("Japanese")?.GetValue(line);
			string chinese = (string)type.GetProperty("Chinese")?.GetValue(line);
			string chineseTW = (string)type.GetProperty("Chinese_TW")?.GetValue(line);

			switch (lang)
			{
				case "Korean": return string.IsNullOrEmpty(korean) ? english : korean;
				case "Japanese": return string.IsNullOrEmpty(japanese) ? english : japanese;
				case "Chinese": return string.IsNullOrEmpty(chinese) ? english : chinese;
				case "Chinese-TW": return string.IsNullOrEmpty(chineseTW) ? english : chineseTW;
				default: return english;
			}
		}
	}
}
