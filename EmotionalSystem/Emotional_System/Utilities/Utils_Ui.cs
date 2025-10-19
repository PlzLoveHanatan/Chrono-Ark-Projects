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

namespace EmotionalSystem
{
	public class Utils_Ui
	{
		public static void GetSprite(string path, Image img)
		{
			string path2 = ModManager.getModInfo("EmotionalSystem").assetInfo.ImageFromFile(path);
			AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
		}

		public static Sprite GetSprite(string path)
		{
			string path2 = ModManager.getModInfo("EmotionalSystem").assetInfo.ImageFromFile(path);
			return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
		}

		public static void GetSpriteAsync(string path, Action<AsyncOperationHandle> collback)
		{
			string path2 = ModManager.getModInfo("EmotionalSystem").assetInfo.ImageFromFile(path);
			AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
		}

		public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
		{
			var mod = ModManager.getModInfo("EmotionalSystem");
			if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
			var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
			//Debug.Log($"[EmotionalSystem] Getting asset address: {address}");
			return AddressableLoadManager.LoadAddressableAsset<T>(address);
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
			txt.rectTransform.sizeDelta = size;
			txt.rectTransform.transform.localPosition = pos;
			txt.text = text;
			txt.fontSize = fontSize;
			txt.color = Color.white;
			txt.alignment = TextAlignmentOptions.Left;
		}

		public static void FitRectTransformToTarget(RectTransform toFit, RectTransform target, Vector3 localPositionOffset)
		{
			if (toFit == null || target == null)
			{
				Debug.LogWarning("RectTransform is null!");
				return;
			}

			// Stretch across the parent (full width and height)
			toFit.anchorMin = new Vector2(0f, 0f);
			toFit.anchorMax = new Vector2(1f, 1f);
			toFit.pivot = target.pivot;

			// Add padding of 20 pixels on all sides
			toFit.offsetMin = new Vector2(10f, 80f);   // Left and bottom
			toFit.offsetMax = new Vector2(-10f, -35f); // Right and top

			// Additional manual offset (if you want to move the image further)
			toFit.localPosition += localPositionOffset;
		}
	}
}
