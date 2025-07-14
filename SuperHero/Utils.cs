using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModData;
using ChronoArkMod;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Experimental.U2D;
using static ChronoArkMod.ModEditor.Console.ConsoleManager;
using UnityEngine.UI;
using TMPro;
using ChronoArkMod.ModData.Settings;
using GameDataEditor;
using DarkTonic.MasterAudio;
using System.Collections;

namespace SuperHero
{
    public static class Utils
    {
        public static bool Timer => ModManager.getModInfo("SuperHero").GetSetting<ToggleSetting>("Timer").Value;
        public static bool Music => ModManager.getModInfo("SuperHero").GetSetting<ToggleSetting>("Music").Value;

        public static bool FirstTimer;

        public static void TimerCheck()
        {
            if (Timer)
            {
                var mod = ModManager.getModInfo("SuperHero");
                mod.GetSetting<ToggleSetting>("Timer").Value = false;
                mod.SaveSetting();
            }
        }

        public static int Attack;
        public static int JusticeSword;
        public static bool TimerOn;

        public static bool ItemTake;

        public static readonly List<string> HeroAttacks = new List<string>
        {
            ModItemKeys.Skill_S_SuperHero_ErasetheMobs,
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice,
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0,
            ModItemKeys.Skill_S_SuperHero_UnwantedSuccessStory,
            ModItemKeys.Skill_S_SuperHero_BloodstainedDress,
        };

        public static readonly List<string> HeroAttacksWithMark = new List<string>
        {
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice,
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0,
            ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1,
            ModItemKeys.Skill_S_SuperHero_JusticePatience,
            ModItemKeys.Skill_S_SuperHero_JusticeFinale,
        };

        public static readonly List<string> PainSharingAlly = new List<string>
        {
            GDEItemKeys.Buff_B_BloodyMist_ShareDamage_Ally,
            GDEItemKeys.Buff_B_ProgramMaster_LucyMain_Ally,
        };

        public static readonly List<string> PainSharingLucy = new List<string>
        {
            GDEItemKeys.Buff_B_BloodyMist_ShareDamage,
            GDEItemKeys.Buff_B_ProgramMaster_LucyMain,
        };

        private static readonly Dictionary<string, string> HeroMusic = new Dictionary<string, string>
        {
            { ModItemKeys.Skill_S_SuperHero_Rare_ApotheosisofJustice, "HappyEnd" },
            { ModItemKeys.Skill_S_SuperHero_Rare_JusticeHero, "Halcyon" },
            { ModItemKeys.Skill_S_SuperHero_Rare_JusticeDarkestHour, "Parousia" },

        };

        public static void PlaySong(string skill)
        {
            if (!Utils.Music) return;

            if (!HeroMusic.TryGetValue(skill, out string baseSound)) return;

            string soundToPlay = baseSound;

            MasterAudio.StopBus("BGM");
            //MasterAudio.StopBus("BattleBGM");
            MasterAudio.FadeBusToVolume("BGM", 0f, 0.5f, null, false, false);
            MasterAudio.FadeBusToVolume("BattleBGM", 0f, 0.5f, null, false, false);
            MasterAudio.PlaySound(soundToPlay, 100f, null, 0f, null, null, false, false);
        }
        public static IEnumerator StopSong()
        {
            yield return new WaitForFixedUpdate();
            MasterAudio.StopBus("BGM");
            MasterAudio.FadeBusToVolume("BGM", 1f, 1f, null, false, false);

            yield return null;

            //Action completionCallback = delegate ()
            //{
            //    MasterAudio.StopBus("BattleBGM");
            //};
            //MasterAudio.FadeBusToVolume("BattleBGM", 0f, 0.5f, completionCallback, true, true);
        }

        public static void RemovePainSharingBuffsFromAllAllies()
        {
            foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
            {
                if (ally == null) continue;

                foreach (var buffKey in PainSharingAlly)
                {
                    var buff = ally.BuffReturn(buffKey, false) as Buff;
                    if (buff != null)
                    {
                        buff.SelfDestroy();
                    }
                }
            }
        }

        public static void RemovePainSharingBuffsFromLucy()
        {
            var lucy = BattleSystem.instance.AllyTeam.LucyAlly;
            if (lucy == null) return;

            foreach (var buffKey in PainSharingLucy)
            {
                var buff = lucy.BuffReturn(buffKey, false) as Buff;
                if (buff != null)
                {
                    buff.SelfDestroy();
                }
            }
        }

        public static void ForceKill(BattleAlly ally)
        {
            if (ally == null || ally.IsDead) return;

            if (ally.HP <= 1)
            {
                ally.Dead(false, false);
                if (!ally.IsDead && ally.HP >= 0)
                {
                    ally.HPToZero();
                    ally.Dead(false, false);
                }
            }
            else
            {
                ally.HPToZero();
            }
        }

        public static void ForceKill(BattleChar ally)
        {
            if (ally == null || ally.IsDead) return;

            ally.HPToZero();
            ally.Dead(false, false);

            if (!ally.IsDead)
            {
                ally.HPToZero();
                ally.Dead(false, false);
            }
        }

        public static void getSprite(string path, Image img)
        {
            string path2 = ModManager.getModInfo("SuperHero").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
        }

        public static Sprite getSprite(string path)
        {
            string path2 = ModManager.getModInfo("SuperHero").assetInfo.ImageFromFile(path);
            return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
        }

        public static void getSpriteAsync(string path, Action<AsyncOperationHandle> collback)
        {
            string path2 = ModManager.getModInfo("SuperHero").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
        }

        public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("SuperHero");
            if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
            var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
            return AddressableLoadManager.LoadAddressableAsset<T>(address);
        }

        public static T GetAssetsAsync<T>(string path, string assetBundlePatch = null,
            AddressableLoadManager.ManageType type = AddressableLoadManager.ManageType.Stage) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("SuperHero");
            if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
            var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
            return AddressableLoadManager.LoadAsyncCompletion<T>(address, type);
        }
        public static GameObject creatGameObject(string name, Transform parent)
        {
            Transform existing = parent.Find(name);
            if (existing != null)
            {
                UnityEngine.Object.Destroy(existing.gameObject); // удаляем старый, если есть
            }

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
        public static GameObject CreateHeroIcon(BattleChar bchar, string name, string sprite, Vector3 offset, Vector3 size)
        {
            Vector3 basePos = bchar.GetTopPos();
            return CreateIconButton(name, bchar.transform, sprite, size, basePos + offset);
        }

        public static GameObject CreateIconButton(string name, Transform parent, string spriteNormal, Vector3 size, Vector3 worldPos)
        {
            GameObject iconObject = Utils.creatGameObject(name, parent);
            if (iconObject == null) return null;

            iconObject.transform.position = worldPos;

            Image oldImage = iconObject.GetComponent<Image>();
            if (oldImage != null)
                UnityEngine.Object.Destroy(oldImage);

            Image image = iconObject.AddComponent<Image>();
            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite == null) return null;
            image.sprite = sprite;

            Utils.ImageResize(image, size);

            // Прозрачность по желанию:
            // var color = image.color;
            // color.a = 0.5f;
            // image.color = color;

            // Отключение блокировки кликов:
            image.raycastTarget = false;

            iconObject.SetActive(true);

            // Отправляем объект на задний план
            iconObject.transform.SetAsFirstSibling();

            return iconObject;
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
