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

namespace EmotionSystem
{
	public class Utils_Ui
	{
		public static void GetSprite(string path, Image img)
		{
			string path2 = ModManager.getModInfo("EmotionSystem").assetInfo.ImageFromFile(path);
			AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
		}

		public static Sprite GetSprite(string path)
		{
			string path2 = ModManager.getModInfo("EmotionSystem").assetInfo.ImageFromFile(path);
			return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
		}

		public static void GetSpriteAsync(string path, Action<AsyncOperationHandle> collback)
		{
			string path2 = ModManager.getModInfo("EmotionSystem").assetInfo.ImageFromFile(path);
			AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
		}

		public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
		{
			var mod = ModManager.getModInfo("EmotionSystem");
			if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
			var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
			//Debug.Log($"[EmotionSystem] Getting asset address: {address}");
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

		//public static void ImageResize(Image img, Vector2 size)
		//{
		//	img.rectTransform.anchorMin = new Vector2(0f, 1f);
		//	img.rectTransform.anchorMax = new Vector2(0f, 1f);
		//	img.rectTransform.sizeDelta = size;
		//}

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

		//public static GameObject CreateUIImage(string name, Transform parent, string spritePath, Vector2 size, Vector3 localPos, bool setAsFirstSibling = true)
		//{
		//	// Создаём объект
		//	GameObject go = new GameObject(name);
		//	go.SetActive(false);
		//	if (parent != null) go.transform.SetParent(parent, false);

		//	go.transform.localPosition = localPos;
		//	go.transform.localScale = Vector3.one;
		//	go.layer = 8;

		//	// Добавляем Image
		//	Image img = go.AddComponent<Image>();
		//	if (img == null)
		//	{
		//		Debug.LogWarning($"[CreateUIImage] Не удалось добавить компонент Image на {name}");
		//		return go;
		//	}

		//	// Загружаем спрайт через ваш метод
		//	try
		//	{
		//		GetSprite(spritePath, img);
		//		Debug.Log($"[CreateUIImage] Спрайт '{spritePath}' подгружен на {name}");
		//	}
		//	catch (Exception ex)
		//	{
		//		Debug.LogWarning($"[CreateUIImage] Ошибка при подгрузке спрайта '{spritePath}': {ex.Message}");
		//	}

		//	// Размер
		//	RectTransform rt = go.GetComponent<RectTransform>();
		//	if (rt != null) rt.sizeDelta = size;

		//	// Ставим первым в иерархии, если нужно
		//	if (setAsFirstSibling) go.transform.SetAsFirstSibling();

		//	go.SetActive(true);
		//	return go;
		//}

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
