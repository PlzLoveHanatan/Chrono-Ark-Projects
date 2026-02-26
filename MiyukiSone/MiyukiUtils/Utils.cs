using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using ChronoArkMod.ModData;
using DarkTonic.MasterAudio;
using I2.Loc;
using UnityEngine;
using UnityEngine.SpatialTracking;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class Utils
	{
		public static ModInfo ThisMod => ModManager.getModInfo("MiyukiSone");
		public static TempSaveData Pd => PlayData.TSavedata;
		public static BattleSystem Bs => BattleSystem.instance;
		public static BattleTeam AllyTeam => Bs?.AllyTeam;
		public static BattleTeam EnemyTeam => Bs?.EnemyTeam;
		public static BattleChar DummyChar => AllyTeam?.DummyChar;
		public static BattleChar MiyukiBchar => AllyTeam.AliveChars?.FirstOrDefault(c => c.Info.KeyData == ModItemKeys.Character_Miyuki);
		public static MiyukiCV MiyukiData => GetOrCreateMiyukiData();
		public static bool MiyukiInParty => PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_Miyuki);
		private static GameObject _currentTempGO;

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

			float finalVolume = volumePercent.HasValue ? Mathf.Clamp(volumePercent.Value / 100f, 0f, 2f) : MasterAudio.MasterVolumeLevel;

			audioSource.volume = finalVolume;
			audioSource.PlayOneShot(clip);

			_currentTempGO = tempGO;
			UnityEngine.Object.Destroy(tempGO, clip.length);

			Debug.Log($"🎯 Playing audio: {audioPath}");
		}

		public static void ShowText(string text, bool isEvent)
		{
			bool isSoftText = IsDere || IsKuudere;
			if (string.IsNullOrEmpty(text) || MiyukiBchar.IsDead || MiyukiBchar == null) return;
			var position = MiyukiBchar.GetTopPos();

			if (isSoftText)
			{
				MiyukiBchar.StartCoroutine(TextSoft(position, text));
			}
			else
			{
				BattleSystem.DelayInput(TextHard(position, text, isEvent));
			}
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

		
	}
}
