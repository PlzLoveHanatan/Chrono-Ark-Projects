using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using TMPro;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using UnityEngine.UI;
using GameDataEditor;

namespace MiyukiSone
{
	public static class MiyukiUI
	{
		public static void GetSprite(string pathAddress, Image img)
		{
			string path = ModManager.getModInfo("MiyukiSone").assetInfo.ImageFromFile(pathAddress);
			AddressableLoadManager.LoadAsyncAction(path, AddressableLoadManager.ManageType.None, img);
		}

		public static Sprite GetSprite(string pathAddress)
		{
			string path = ModManager.getModInfo("MiyukiSone").assetInfo.ImageFromFile(pathAddress);
			return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path, AddressableLoadManager.ManageType.None);
		}
		public static void GetSpriteByAddress(this Image img, string address, AddressableLoadManager.ManageType type = AddressableLoadManager.ManageType.Stage)
		{
			AddressableLoadManager.LoadAsyncAction(address, type, img);
		}

		public static void GetSpriteByPath(this Image img, string path)
		{
			var address = ModManager.getModInfo("MiyukiSone").assetInfo.ImageFromFile(path);
			img.GetSpriteByAddress(address);
		}

		public static void GetSpriteAsync(string pathAddress, Action<AsyncOperationHandle> collback)
		{
			string path = ModManager.getModInfo("MiyukiSone").assetInfo.ImageFromFile(pathAddress);
			AddressableLoadManager.LoadAsyncAction(path, AddressableLoadManager.ManageType.None, collback);
		}

		public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = ModManager.getModInfo("MiyukiSone").DefaultAssetBundlePath;
			var address = ModManager.getModInfo("MiyukiSone").assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
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

		public static void DestroyObjects(IEnumerable<string> objectNames)
		{
			foreach (var objName in objectNames)
			{
				var existing = GameObject.Find(objName);
				if (existing != null)
				{
					UnityEngine.Object.Destroy(existing);
				}
			}
		}

		public static void DestroyObject(GameObject obj)
		{
			if (obj != null)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
	}
}
