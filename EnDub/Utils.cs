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
		public static string Path_CSV = Path.Combine(Application.dataPath, "StreamingAssets", "LangDataDB.csv");
		public static string Path_Json = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ThisMod.DirectoryName, "Assets/Dialogues.json");
		private static GameObject _currentTempGO;

		public static Sprite GetSpriteFromMod(string path)
		{
			var adress = ModManager.getModInfo("EnDub").assetInfo.ImageFromFile(path);
			var sprite = AddressableLoadManager.LoadAsyncCompletion<Sprite>(adress, AddressableLoadManager.ManageType.None);
			return sprite;
		}

		public static T GetAssets<T>(string path) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(path)) return null;
			var address = ModManager.getModInfo("EnDub").assetInfo.ObjectFromAsset<T>("endub", path);
			T asset = AddressableLoadManager.LoadAddressableAsset<T>(address);
			return asset;
		}

		public static T Let<T>(this T obj, Action<T> action)
		{
			if (obj != null) action(obj);
			return obj;
		}

		public static void PlayCharacterAudio(string character, string skin, string audioFile)
		{
			if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(skin) || string.IsNullOrEmpty(audioFile)) return;
			string audioPath = $"Assets/{character}/{skin}/{audioFile}";
			PlayAudio(audioPath, character);
		}

		public static void PlayAudio(string audioPath, string character)
		{
			if (string.IsNullOrEmpty(audioPath)) return;
			AudioClip clip = GetAssets<AudioClip>(audioPath + ".wav") ?? GetAssets<AudioClip>(audioPath + ".mp3");
			PlayClip(clip, character);
		}

		public static void PlayClip(AudioClip clip, string character)
		{
			if (clip == null) return;

			MasterAudio.StopBus("SE");

			if (_currentTempGO != null)
			{
				GameObject.Destroy(_currentTempGO);
				_currentTempGO = null;
			}

			GameObject tempGO = new GameObject("TempAudio");
			AudioSource audioSource = tempGO.AddComponent<AudioSource>();

			//float finalVolume = Settings.AudioVolume > 0 ? Settings.AudioVolume : SaveManager.NowSaveSlot.SoundEffectVolume / 100f;
			audioSource.PlayOneShot(clip, Mathf.Clamp(SaveManager.Instance.FinalVolume(character), 0f, 6f));

			_currentTempGO = tempGO;
			UnityEngine.Object.Destroy(tempGO, clip.length);
		}
	}
}