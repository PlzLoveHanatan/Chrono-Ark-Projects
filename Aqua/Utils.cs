using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData.Settings;
using ChronoArkMod;
using DarkTonic.MasterAudio;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GameDataEditor;

namespace Aqua
{
    public static class Utils
    {
        public static bool AquaVoice => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Aqua Voice").Value;

        public static bool AquaVoiceSkills => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Aqua Voice Skills").Value;

        public static bool AquaVoiceButton => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Aqua Voice Button").Value;

        public static bool CleanseAllCurses => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Cleanse All Curses").Value;

        public static bool AquaVoiceButtonHotkey => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Aqua Voice Button Hotkey").Value;

        public static bool CleanseAllDebuffs => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Cleanse All Debuffs").Value;

        public static bool AquaHealingButton => ModManager.getModInfo("Aqua").GetSetting<ToggleSetting>("Aqua Healing Button").Value;


        public static List<GDESkillData> CachedSkills;
        public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
        public static BattleChar Aqua => AllyTeam.AliveChars.FirstOrDefault(x => x?.Info.KeyData == ModItemKeys.Character_Aqua);


        private static readonly Dictionary<string, string> AquaSkillsSounds = new Dictionary<string, string>
        { 
            { ModItemKeys.Skill_S_Aqua_AquaGradePurification, "AquaGradePurification" },
            { ModItemKeys.Skill_S_Aqua_BlessingoftheAxisCult, "BlessingoftheAxisCult" },
            { ModItemKeys.Skill_S_Aqua_DivineLottery, "DivineLottery" },
            { ModItemKeys.Skill_S_Aqua_FogofBlessings, "FogofBlessings" },
            { ModItemKeys.Skill_S_Aqua_LucyDraw, "GoddessSecretWeapon" },
            { ModItemKeys.Skill_S_Aqua_OverflowingGrace, "OverflowingGrace" },
            { ModItemKeys.Skill_S_Aqua_PartyDrunkard, "PartyDrunkard" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick, "PartyTrick" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty, "NaturesBeauty" }, // 2 sounds
            { ModItemKeys.Skill_S_Aqua_PartyTrick_PhantasmalBeauty, "PhantasmalBeauty" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_VanishTrick, "VanishTrick" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_TelekinesisTrick, "TelekinesisTrick" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_Certainkill, "Certainkillpartytrick" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_UnusualPlant, "UnusualPlant" },
            { ModItemKeys.Skill_S_Aqua_PartyTrick_Minorpocket, "Minorpocketdimension" },
            { ModItemKeys.Skill_S_Aqua_Rare_AxisCultRecruitment, "AxisCultRecruitment" },
            { ModItemKeys.Skill_S_Aqua_Rare_GodsBlow, "GodsBlow" }, // 2 sounds
            { ModItemKeys.Skill_S_Aqua_SplashofJudgment, "SplashofJudgment" }, // 2 sounds
            { ModItemKeys.Skill_S_Aqua_TorrentialTears, "TorrentialTears" },
        };

        public static void PlaySound(string skillId)
        {
            if (!AquaSkillsSounds.TryGetValue(skillId, out string baseSound)) return;

            string soundToPlay = baseSound;

            if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty ||
                skillId == ModItemKeys.Skill_S_Aqua_SplashofJudgment ||
                skillId == ModItemKeys.Skill_S_Aqua_Rare_GodsBlow)
            {
                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, 2);

                if (skillId == ModItemKeys.Skill_S_Aqua_PartyTrick_NaturesBeauty)
                    soundToPlay = $"NaturesBeauty_{index}";
                if (skillId == ModItemKeys.Skill_S_Aqua_Rare_GodsBlow)
                    soundToPlay = $"GodsBlow_{index}";
                if (skillId == ModItemKeys.Skill_S_Aqua_SplashofJudgment)
                    soundToPlay = $"SplashofJudgment_{index}";
            }

            MasterAudio.StopBus("SE");
            MasterAudio.PlaySound(soundToPlay, 100f);
        }
        public static void getSprite(string path, Image img)
        {
            string path2 = ModManager.getModInfo("Aqua").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
        }

        public static Sprite getSprite(string path)
        {
            string path2 = ModManager.getModInfo("Aqua").assetInfo.ImageFromFile(path);
            return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
        }

        public static void getSpriteAsync(string path, Action<AsyncOperationHandle> collback)
        {
            string path2 = ModManager.getModInfo("Aqua").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
        }

        public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("Aqua");
            if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
            var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
            return AddressableLoadManager.LoadAddressableAsset<T>(address);
        }

        public static T GetAssetsAsync<T>(string path, string assetBundlePatch = null,
            AddressableLoadManager.ManageType type = AddressableLoadManager.ManageType.Stage) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("Aqua");
            if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
            var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
            return AddressableLoadManager.LoadAsyncCompletion<T>(address, type);
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
        public static void DivineLottery()
        {
            if (CachedSkills != null) return; // Уже инициализировано

            CachedSkills = new List<GDESkillData>();

            foreach (var gdeskillData in PlayData.ALLSKILLLIST.Concat(PlayData.ALLRARESKILLLIST))
            {
                if (string.IsNullOrEmpty(gdeskillData.User)) continue;
                if (gdeskillData.Category.Key == GDEItemKeys.SkillCategory_DefultSkill) continue;
                if (gdeskillData.NoDrop || gdeskillData.Lock) continue;
                if (gdeskillData.KeyID == GDEItemKeys.Skill_S_Phoenix_6) continue;

                var gdecharacterData = new GDECharacterData(gdeskillData.User);
                if (gdecharacterData != null && Misc.IsUseableCharacter(gdecharacterData.Key))
                {
                    CachedSkills.Add(gdeskillData);
                }
            }
            Debug.Log($"[SkillCache] Cached {CachedSkills.Count} usable skills");
        }
    }
}
