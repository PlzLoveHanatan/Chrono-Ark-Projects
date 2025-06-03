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
using ChronoArkMod.ModData.Settings;
using DarkTonic.MasterAudio;
using ChronoArkMod.ModEditor;
using HarmonyLib;

namespace Mia
{
    public static class Utils
    {
        public static int FestivalFang;

        public static bool MiaButton => ModManager.getModInfo("Mia").GetSetting<ToggleSetting>("Mia Button").Value;

        public static bool MiaButtonHotkey => ModManager.getModInfo("Mia").GetSetting<ToggleSetting>("Mia Button Hotkey").Value;

        public static bool MiaVoiceSkills => ModManager.getModInfo("Mia").GetSetting<ToggleSetting>("Mia Voice Skills").Value;

        public static bool MiaVoice => ModManager.getModInfo("Mia").GetSetting<ToggleSetting>("Mia Voice").Value;


        private static readonly Dictionary<string, string> MiaSkillSounds = new Dictionary<string, string>
        {
            { ModItemKeys.Skill_S_Mia_BeastsPunchline, "BeastsPunchline" },
            { ModItemKeys.Skill_S_Mia_BurstofFlavor, "BurstofFlavor" },
            { ModItemKeys.Skill_S_Mia_CamelSprint, "CamelSprint" },
            { ModItemKeys.Skill_S_Mia_Rare_ChaoticHarvest, "ChaoticHarvest" },
            { ModItemKeys.Skill_S_Mia_FestivalFang, "FestivalFang" }, // 2 sounds
            { ModItemKeys.Skill_S_Mia_FluffyStrike, "FluffyStrike" },
            { ModItemKeys.Skill_S_Mia_Rare_HarvestDance, "HarvestDance" },
            { ModItemKeys.Skill_S_Mia_ImpulsiveHarvest, "ImpulsiveHarvest" },
            { ModItemKeys.Skill_S_Mia_MeowsteryMomentum, "MeowsteryMomentum" }, // 2 sounds
            { ModItemKeys.Skill_S_Mia_LucyDraw_0, "MessyNotes" },
            { ModItemKeys.Skill_S_Mia_LucyDraw_1, "MiasDreamland" },
            { ModItemKeys.Skill_S_Mia_PlayfulMasquerade, "PlayfulMasquerade" },
            { ModItemKeys.Skill_S_Mia_Scrollfang, "Scrollfang" },
            { ModItemKeys.Skill_S_Mia_Snowver, "Snowver" }, // 3 sounds
            { ModItemKeys.Skill_S_Mia_VortexChores, "VortexChores" },
        };

        public static void PlayMiaSound(string skillId)
        {
            if (!Utils.MiaVoiceSkills) return;

            if (!MiaSkillSounds.TryGetValue(skillId, out string baseSound)) return;

            string soundToPlay = baseSound;

            if (skillId == ModItemKeys.Skill_S_Mia_FestivalFang ||
                skillId == ModItemKeys.Skill_S_Mia_MeowsteryMomentum)
            {
                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, 2);

                if (skillId == ModItemKeys.Skill_S_Mia_FestivalFang)
                    soundToPlay = $"FestivalFang_{index}";
                if (skillId == ModItemKeys.Skill_S_Mia_MeowsteryMomentum)
                    soundToPlay = $"MeowsteryMomentum_{index}";

            }
            if (skillId == ModItemKeys.Skill_S_Mia_Snowver)
            {
                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, 3);
                if (skillId == ModItemKeys.Skill_S_Mia_Snowver)
                    soundToPlay = $"Snowver_{index}";
            }

            MasterAudio.StopBus("SE");
            MasterAudio.PlaySound(soundToPlay, 100f);
        }

        public static void TryPlayMiaSound(Skill skill, BattleChar bChar)
        {
            if (skill == null) return;
            if (bChar.Info.KeyData != ModItemKeys.Character_Mia) return;

            PlayMiaSound(skill.MySkill.KeyID);
        }

        public static void getSprite(string path, Image img)
        {
            string path2 = ModManager.getModInfo("Mia").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
        }

        public static Sprite getSprite(string path)
        {
            string path2 = ModManager.getModInfo("Mia").assetInfo.ImageFromFile(path);
            return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
        }

        public static void getSpriteAsync(string path, Action<AsyncOperationHandle> collback)
        {
            string path2 = ModManager.getModInfo("Mia").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
        }
        public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("Mia");
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
