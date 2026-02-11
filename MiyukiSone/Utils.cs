using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.SpatialTracking;

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
			if (isStopOldBus)
			{
				MasterAudio.StopBus("SE");
			}
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
	}
}
