using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiyukiSone
{
	public static class MiyukiPatchesHelpers
	{
		private static readonly string RelativePath = "Assets/Images/Ui/";
		private static readonly Color MiyukiPink = new Color(1f, 0.5f, 0.8f);
		private static readonly Color MiyukiButtonBg = new Color(0.8f, 0.3f, 0.6f, 0.3f);
		private static readonly Color MiyukiMaskBg = new Color(0.8f, 0.3f, 0.6f, 0.2f);

		public static bool SetSprite(Transform root, string path, string spritePath, string customPath = null)
		{
			Transform t = root.Find(path);
			if (t == null) return false;

			Image img = t.GetComponent<Image>();
			if (img == null) return false;

			Sprite sprite = UtilsUI.GetSpriteFromAsset(customPath ?? RelativePath + spritePath + ".png");
			if (sprite == null) return false;

			img.sprite = sprite;
			img.color = Color.white;
			return true;
		}

		public static bool SetImageColor(Transform root, string path, Color color)
		{
			Transform t = root.Find(path);
			if (t == null) return false;

			Image img = t.GetComponent<Image>();
			if (img == null) return false;

			img.color = color;
			return true;
		}

		public static bool MakeTransparent(Transform root, string path)
		{
			return SetImageColor(root, path, new Color(0, 0, 0, 0));
		}

		public static bool SetText(Transform root, string path, string text = null, float fontSize = 0, bool onlyColor = false)
		{
			Transform t = root.Find(path);
			if (t == null) return false;

			TextMeshProUGUI tmp = t.GetComponent<TextMeshProUGUI>();
			if (tmp == null) return false;

			tmp.color = MiyukiPink;
			if (!onlyColor && text != null) tmp.text = text;
			if (fontSize > 0) tmp.fontSize = fontSize;
			return true;
		}

		public static void ProcessButtons(Transform root, string layoutPath)
		{
			Transform layout = root.Find(layoutPath);
			if (layout == null) return;

			foreach (Transform btn in layout)
			{
				if (btn.name.StartsWith("OptionButton"))
				{
					SetText(btn, "Content/TextMeshPro Text", null, 0, true);
					SetImageColor(btn, "Mask", MiyukiButtonBg);
				}
			}
		}

		public static void ProcessToggleOptions(Transform root, string layoutPath)
		{
			Transform layout = root.Find(layoutPath);
			if (layout == null) return;

			foreach (Transform toggle in layout)
			{
				if (toggle.name.StartsWith("ToggleOption"))
				{
					SetText(toggle, "Content/TextMeshPro Text", null, 0, true);

					Transform valueText = toggle.Find("Content/TextMeshPro Text (1)");
					if (valueText != null)
					{
						TextMeshProUGUI text = valueText.GetComponent<TextMeshProUGUI>();
						if (text != null)
						{
							text.color = Color.white;
							text.fontStyle = FontStyles.Bold;
						}

						Image[] arrows = valueText.GetComponentsInChildren<Image>();
						foreach (Image arrow in arrows)
						{
							if (arrow.sprite != null && arrow.sprite.name == "SimpleArrow")
								arrow.color = MiyukiPink;
						}
					}

					SetImageColor(toggle, "Mask", MiyukiMaskBg);
				}
			}
		}

		public static void ProcessKeyGroup(Transform group)
		{
			if (group == null) return;

			foreach (Transform keyItem in group)
			{
				MakeTransparent(keyItem, "");
				SetText(keyItem, "Content/TextMeshPro Text", null, 0, true);

				Transform valueText = keyItem.Find("Content/TextMeshPro Text (1)");
				if (valueText != null)
				{
					TextMeshProUGUI text = valueText.GetComponent<TextMeshProUGUI>();
					if (text != null)
					{
						text.color = Color.white;
						text.fontStyle = FontStyles.Bold;
					}
				}

				SetImageColor(keyItem, "Mask", MiyukiMaskBg);
			}
		}

		public static void ProcessRemainArea(Transform root)
		{
			Transform remain = root.Find("Main/Remain");
			if (remain == null) return;

			SetText(remain, "RemainText", null, 0, true);
			SetText(remain, "RemainDesc", null, 0, true);

			foreach (Transform line in FindAll(remain, "Line"))
			{
				SetImageColor(line, "", MiyukiPink);
			}	

			SetImageColor(remain, "Grid", new Color(1f, 0.5f, 0.8f, 0.3f));

			foreach (string name in new[] { "Chance1", "Chance2", "Chance3" })
			{
				SetImageColor(remain, name, MiyukiPink);
			}	
		}

		public static void ReplaceAllBackSprites(string spriteName)
		{
			Sprite sprite = UtilsUI.GetSpriteFromAsset(spriteName + ".png");
			if (sprite == null) return;

			foreach (Image img in GameObject.FindObjectsOfType<Image>())
			{
				if (img.name == "Back")
				{
					img.sprite = sprite;
					img.color = Color.white;
				}
			}
		}

		public static Transform[] FindAll(Transform parent, string name)
		{
			List<Transform> results = new List<Transform>();
			FindAllRecursive(parent, name, results);
			return results.ToArray();
		}

		private static void FindAllRecursive(Transform current, string name, List<Transform> results)
		{
			if (current.name == name) results.Add(current);
			foreach (Transform child in current) FindAllRecursive(child, name, results);
		}

		public static void SetMenuButton(Transform parent, string childName, string buttonText)
		{
			Transform t = parent.Find(childName);
			if (t == null) return;

			TextMeshProUGUI tmp = t.GetComponent<TextMeshProUGUI>();
			if (tmp == null) return;

			tmp.color = new Color(1f, 0.5f, 0.8f);
			tmp.fontStyle = FontStyles.Bold;

			// Не меняем текст, только цвет (можно раскомментировать если хочешь)
			// if (!string.IsNullOrEmpty(buttonText)) tmp.text = buttonText;
		}

		public static void DumpTransform(Transform transform, int depth)
		{
			string indent = new string(' ', depth * 2);
			string path = GetPath(transform);

			Debug.Log($"{indent}[{transform.name}] Path: {path}");

			Image img = transform.GetComponent<Image>();
			if (img != null)
			{
				string spriteName = img.sprite != null ? img.sprite.name : "NULL";
				Debug.Log($"{indent}  └─ IMAGE: enabled={img.enabled}, color={img.color}, sprite={spriteName}");
				Debug.Log($"{indent}     Size: {img.rectTransform.rect.width:F1} x {img.rectTransform.rect.height:F1}");
				Debug.Log($"{indent}     Position: {img.rectTransform.anchoredPosition}");
				Debug.Log($"{indent}     Anchors: min={img.rectTransform.anchorMin}, max={img.rectTransform.anchorMax}");
			}

			TextMeshProUGUI text = transform.GetComponent<TextMeshProUGUI>();
			if (text != null)
			{
				Debug.Log($"{indent}  └─ TEXT: \"{text.text}\", color={text.color}, size={text.fontSize}");
			}

			ToggleOption toggle = transform.GetComponent<ToggleOption>();
			if (toggle != null)
			{
				Debug.Log($"{indent}  └─ TOGGLE: state={toggle.NowState}");
			}

			foreach (Transform child in transform)
			{
				DumpTransform(child, depth + 1);
			}
		}

		private static string GetPath(Transform transform)
		{
			if (transform.parent == null) return transform.name;
			return GetPath(transform.parent) + "/" + transform.name;
		}
	}
}
