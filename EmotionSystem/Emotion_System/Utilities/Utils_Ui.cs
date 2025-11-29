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

namespace EmotionSystem
{
	public static class Utils_Ui
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
		public static string GetDescription(this ItemEnchant enchant)
		{
			bool flag = string.IsNullOrEmpty(enchant.Name);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string hexcode = enchant.CurseEnchant ? "FFDD09" : "97D8FFFF";
				string text = enchant.Name + Misc.StatString(enchant.EnchantData.PlusStat, enchant.EnchantData.PlusPerStat);
				bool flag2 = !string.IsNullOrEmpty(enchant.Key);
				if (flag2)
				{
					string text2 = new GDESpecialKeyData("SPK_EnchantDesc_" + enchant.Key).Name;
					bool flag3 = !string.IsNullOrEmpty(text2);
					if (flag3)
					{
						text2 = enchant.EnchantData.DescExtended(text2);
						text = text + "\n" + text2;
					}
				}
				text = Misc.InputColor(text, hexcode);
				result = text;
			}
			return result;
		}

		public static Type GetEnchantType(string prefix, string className)
		{
			return ModManager.GetType(prefix, className) ?? ModManager.GetType(className);
		}

		public static void CopyEquipEnchantCurse(Item_Equip equipNew, Item_Equip equipOld)
		{
			equipNew.Enchant = equipOld.Enchant;
			equipNew.Curse = equipOld.Curse;
		}
	}
}
