using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using GameDataEditor;
using TMPro;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using UnityEngine.UI;
using ChronoArkMod.ModData;

namespace Kazuma
{
    public static class Utils
    {
        public static int Luck;

		public const string modname = "Kazuma";
		public static ModInfo ThisMod => ModManager.getModInfo(modname);

		public static readonly List<string> FemaleEnemy = new List<string>
        {
            GDEItemKeys.Enemy_Sandbag,
            GDEItemKeys.Enemy_S1_Pharos_Healer,
            GDEItemKeys.Enemy_S1_Maid,
            GDEItemKeys.Enemy_S1_LittleMaid,
            GDEItemKeys.Enemy_S1_Kitchenmaid,
            GDEItemKeys.Enemy_S1_WitchBoss,
            GDEItemKeys.Enemy_Story_Witch,
            GDEItemKeys.Enemy_Story_Maid,
            GDEItemKeys.Enemy_S2_Pierrot_Bat,
            GDEItemKeys.Enemy_S2_MainBoss_0_Left,
            GDEItemKeys.Enemy_S2_MainBoss_0_Right,
            GDEItemKeys.Enemy_S2_Pharos_Healer,
            GDEItemKeys.Enemy_S2_PharosWitch,
            GDEItemKeys.Enemy_S2_Ghost,
            GDEItemKeys.Enemy_S2_PopcornGirl,
            GDEItemKeys.Enemy_S2_Shiranui,
            GDEItemKeys.Enemy_SR_Blade,
            GDEItemKeys.Enemy_SR_GuitarList,
            GDEItemKeys.Enemy_SR_Sniper,
            GDEItemKeys.Enemy_S3_Boss_Pope,
            GDEItemKeys.Enemy_S3_Boss_TheLight,
            GDEItemKeys.Enemy_S4_Summoner,
        };

		public static void GetSprite(string path, Image img)
		{
			string path2 = ModManager.getModInfo("Kazuma").assetInfo.ImageFromFile(path);
			AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
		}

		public static Sprite GetSprite(string path)
		{
			string path2 = ModManager.getModInfo("Kazuma").assetInfo.ImageFromFile(path);
			return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
		}
		public static void GetSpriteByAddress(this Image img, string address,
			AddressableLoadManager.ManageType type = AddressableLoadManager.ManageType.Stage)
		{
			AddressableLoadManager.LoadAsyncAction(address, type, img);
		}

		public static void GetSpriteByPath(this Image img, string path)
		{
			var address = ThisMod.assetInfo.ImageFromFile(path);
			img.GetSpriteByAddress(address);
		}

		public static void GetSpriteAsync(string path, Action<AsyncOperationHandle> collback)
		{
			string path2 = ModManager.getModInfo("Kazuma").assetInfo.ImageFromFile(path);
			AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
		}

		public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
		{
			var mod = ModManager.getModInfo("Kazuma");
			if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
			var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
			return AddressableLoadManager.LoadAddressableAsset<T>(address);
		}

		public static void LoadSpriteAsync(string path, Action<Sprite> onLoaded)
		{
			GetSpriteAsync(path, handle =>
			{
				Sprite sprite = (Sprite)handle.Result;
				onLoaded?.Invoke(sprite);
			});
		}

		public static GameObject CreatGameObject(string name, Transform parent)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.SetActive(false);
			gameObject.transform.SetParent(parent, false);
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = 8;
			return gameObject;
		}

		public static GameObject GetChildByName(GameObject obj, string name)
		{
			Transform transform = obj.transform.Find(name);
			bool flag = transform != null;
			GameObject result;
			if (flag)
			{
				result = transform.gameObject;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static void ImageResize(Image img, Vector2 size, Vector2 pos)
		{
			img.rectTransform.anchorMin = new Vector2(0f, 1f);
			img.rectTransform.anchorMax = new Vector2(0f, 1f);
			img.rectTransform.sizeDelta = size;
			img.rectTransform.transform.localPosition = pos;
		}

		public static void TextResize(TextMeshProUGUI txt, Vector2 size, Vector2 pos, string text, float fontSize)
		{
			txt.rectTransform.anchorMin = new Vector2(0f, 1f);
			txt.rectTransform.anchorMax = new Vector2(0f, 1f);
			txt.rectTransform.sizeDelta = size; txt.rectTransform.transform.localPosition = pos;
			txt.text = text;
			txt.fontSize = fontSize;
			txt.color = Color.white;
			txt.alignment = TextAlignmentOptions.Left;
		}

		public static GameObject CreateUIImage(string name, Transform parent, Sprite sprite, Vector2 size, Vector3 localPos, bool setAsFirstSibling = true)
		{
			GameObject go = new GameObject(name);
			go.SetActive(false);
			if (parent != null) go.transform.SetParent(parent, false);

			go.transform.localPosition = localPos;
			go.transform.localScale = Vector3.one;
			go.layer = 8;

			Image img = go.AddComponent<Image>();
			if (img == null) return go;

			img.sprite = sprite;
			img.preserveAspect = true;

			RectTransform rt = go.GetComponent<RectTransform>();
			if (rt != null) rt.sizeDelta = size;

			if (setAsFirstSibling) go.transform.SetAsFirstSibling();
			go.SetActive(true);
			return go;
		}

		public static void UnlockSkillPreview(string key)
		{
			if (!SaveManager.NowData.unlockList.SkillPreView.Contains(key))
			{
				SaveManager.NowData.unlockList.SkillPreView.Add(key);
			}
		}
	}
}
