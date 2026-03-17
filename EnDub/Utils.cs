using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using ChronoArkMod.ModData;
using DarkTonic.MasterAudio;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace EnDub
{
	public static class Utils
	{
		public static ModInfo ThisMod => ModManager.getModInfo("EnDub");
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

		public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = "endub";
			var address = ModManager.getModInfo("EnDub").assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
			T asset = AddressableLoadManager.LoadAddressableAsset<T>(address);
			return asset;
		}

		public static void PlaySoundFromAsset(string assetPath, bool isStopOldBus = true, int? volumePercent = null)
		{
			if (string.IsNullOrEmpty(assetPath)) return;

			AudioClip clip = GetAssets<AudioClip>(assetPath);
			if (clip == null) return;

			if (isStopOldBus && _currentTempGO != null)
			{
				MasterAudio.StopBus("SE");
				GameObject.Destroy(_currentTempGO);
				_currentTempGO = null;
			}

			GameObject tempGO = new GameObject("TempAudio");
			AudioSource audioSource = tempGO.AddComponent<AudioSource>();

			//float finalVolume = volumePercent.HasValue ? volumePercent.Value / 100f : 1f;

			audioSource.PlayOneShot(clip, Settings.AudioVolume);

			_currentTempGO = tempGO;
			UnityEngine.Object.Destroy(tempGO, clip.length);
		}

		public static void PlayCharacterAudio(string character, string skin, string audioFile, string text = "", int volumePercent = 100)
		{
			if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(audioFile)) return;

			if (!Data.HasSkin(character, skin)) skin = "Normal";

			string audioPath = $"Assets/{character}/{skin}/{audioFile}.wav";
			AudioClip clip = GetAssets<AudioClip>(audioPath);

			if (clip == null)
			{
				audioPath = $"Assets/{character}/{skin}/{audioFile}.mp3";
				clip = GetAssets<AudioClip>(audioPath);

				if (clip == null) return;
				
					//if (skin != "Normal")
					//{
					//	audioPath = $"Assets/{character}/Normal/{audioFile}.mp3";
					//	clip = GetAssets<AudioClip>(audioPath);

					//	if (clip == null)
					//	{
					//		audioPath = $"Assets/{character}/Normal/{audioFile}.wav";
					//		clip = GetAssets<AudioClip>(audioPath);
					//	}

					//	if (clip == null)
					//	{
					//		Debug.LogError($"No audio found for {character}/{audioFile}");
					//		return;
					//	}
					//}
					//else
					//{
					//	Debug.LogError($"No audio found for {character}/{audioFile}");
					//	return;
					//}
				
			}

			PlaySoundFromAsset(audioPath, true, volumePercent);
		}

		public static void PlayCharacterAudioById(string characterId, string audioFile, string characterName = null, string text = "")
		{
			if (string.IsNullOrEmpty(characterId) || string.IsNullOrEmpty(audioFile)) return;

			if (string.IsNullOrEmpty(characterName))
			{
				characterName = Data.GetCharacterNameByGameId(characterId);
				if (string.IsNullOrEmpty(characterName))
				{
					Debug.LogWarning($"Unknown character ID: {characterId}");
					return;
				}
			}

			string skin = GetCharacterSkin(characterId);
			PlayCharacterAudio(characterName, skin, audioFile, text);
		}

		public static string GetCharacterSkin(string characterId)
		{
			var enableSkins = SaveManager.NowData.EnableSkins;
			var skinData = enableSkins.FirstOrDefault(s => s.charKey == characterId);
			if (!string.IsNullOrEmpty(skinData.skinKey)) return Data.GetSkinNameByKey(skinData.skinKey);
			return "Normal";
		}
	}
}