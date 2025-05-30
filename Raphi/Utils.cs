using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using TMPro;
using UnityEngine.ResourceManagement.AsyncOperations;
using ChronoArkMod.ModData.Settings;

namespace Raphi
{
    public static class Utils
    {
        public static bool RaphiButton => ModManager.getModInfo("Raphi").GetSetting<ToggleSetting>("Raphi Button").Value;

        public static bool RaphiButtonHotkey => ModManager.getModInfo("Raphi").GetSetting<ToggleSetting>("Raphi Button Hotkey").Value;

        public static void AddExHeavenlyGrace(Skill skill)
        {
            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Raphi_1) == null && skill.ExtendedFind(GDEItemKeys.SkillExtended_SkillEn_Ilya_0) == null)
            {
                skill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_Raphi_1);
            }
        }

        public static void RemoveExHeavenlyGrace(Skill skill)
        {
            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Raphi_1) != null)
            {
                skill.ExtendedDelete_Dataname(ModItemKeys.SkillExtended_Ex_Raphi_1);
            }
        }

        public static void AddExHeavenlyWrath(Skill skill)
        {

            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Raphi_2) == null && skill.ExtendedFind(GDEItemKeys.SkillExtended_SkillEn_Ilya_0) == null)
            {
                skill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_Raphi_2);
            }
        }

        public static void RemoveExHeavenlyWrath(Skill skill)
        {
            if (skill.ExtendedFind(ModItemKeys.SkillExtended_Ex_Raphi_2) != null)
            {
                skill.ExtendedDelete_Dataname(ModItemKeys.SkillExtended_Ex_Raphi_2);
            }
        }
        public static void getSprite(string path, Image img)
        {
            string path2 = ModManager.getModInfo("Raphi").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
        }

        public static Sprite getSprite(string path)
        {
            string path2 = ModManager.getModInfo("Raphi").assetInfo.ImageFromFile(path);
            return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
        }

        public static void getSpriteAsync(string path, Action<AsyncOperationHandle> collback)
        {
            string path2 = ModManager.getModInfo("Raphi").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
        }
        public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("Raphi");
            if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
            var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
            return AddressableLoadManager.LoadAddressableAsset<T>(address);
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

        public static void ImageResize(Image img, Vector2 size)
        {
            img.rectTransform.anchorMin = new Vector2(0f, 1f);
            img.rectTransform.anchorMax = new Vector2(0f, 1f);
            img.rectTransform.sizeDelta = size;
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
