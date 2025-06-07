using System;
using ChronoArkMod;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace projectEGO
{
    public static class Utils
    {
        public static IEnumerator AddSkillNoDrawEffect(this BattleTeam team, Skill skill, int position = -1)
        {
            if (position == -1)
            {
                team.Skills.Add(skill);
            }
            else
            {
                team.Skills.Insert(position, skill);
            }

            BattleSystem.instance.ActWindow.Draw(team, false);
            yield break;
        }

        public static void getSprite(string path, Image img)
        {
            string path2 = ModManager.getModInfo("projectEGO").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
        }

        public static Sprite getSprite(string path)
        {
            string path2 = ModManager.getModInfo("projectEGO").assetInfo.ImageFromFile(path);
            return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
        }

        public static GameObject creatGameObject(string name, Transform parent)
        {
            GameObject gameObject = new GameObject(name);
            gameObject.SetActive(false);
            gameObject.transform.SetParent(parent, false);
            gameObject.transform.localScale = Vector3.one;
            gameObject.layer = 8;
            return gameObject;
        }

        //public static string getTranslation(string key)
        //{
        //    try
        //    {
        //        return ModManager.getModInfo("RandomCharacter").localizationInfo.SyetemLocalizationUpdate("RandomCharacter/UI/" + key);
        //    }
        //    catch
        //    {
        //    }
        //    return key;
        //}

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
    }
}
