using System;
using System.Collections.Generic;
using System.IO;
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

		public static T GetAssets<T>(string path) where T : UnityEngine.Object
		{
			var address = ModManager.getModInfo("EnDub").assetInfo.ObjectFromAsset<T>("endub", path);
			T asset = AddressableLoadManager.LoadAddressableAsset<T>(address);
			return asset;
		}

		public static void PlaySoundFromAsset(string assetPath, bool isStopOldBus = true)
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

			float finalVolume = Settings.AudioVolume > 0 ? Settings.AudioVolume : SaveManager.NowSaveSlot.SoundEffectVolume / 100f;

			//string path = Path.Combine(ThisMod.DirectoryName, "Assets", "AudioVolume.txt");
			//string content = File.ReadAllText(path).Trim();

			//if (!float.TryParse(content, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float finalVolume))
			//{
			//	finalVolume = 1f;
			//}

			audioSource.PlayOneShot(clip, finalVolume);

			_currentTempGO = tempGO;
			UnityEngine.Object.Destroy(tempGO, clip.length);
		}

		public static void PlayCharacterAudio(string character, string skin, string audioFile)
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

			PlaySoundFromAsset(audioPath, true);
		}

		public static void PlayCharacterAudioById(string characterId, string audioFile, string characterName = null, string text = "")
		{
			if (string.IsNullOrEmpty(characterId) || string.IsNullOrEmpty(audioFile)) return;

			if (string.IsNullOrEmpty(characterName))
			{
				characterName = Data.GetCharacterName(characterId);
				if (string.IsNullOrEmpty(characterName))
				{
					Debug.LogWarning($"Unknown character ID: {characterId}");
					return;
				}
			}

			string skin = GetCharacterSkin(characterId);
			PlayCharacterAudio(characterName, skin, audioFile);
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