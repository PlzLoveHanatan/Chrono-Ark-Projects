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

namespace Urunhilda
{
    public static class Utils
    {
        public static bool RewardTake;

        public static void UrunhildaFirstReward()
        {
            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 500));
            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Consume_ArtifactPouch, 1));
        }

        public static void UrunhildaReward(int num)
        {
            for (int i = 0; i < num; i++)
            {
                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 500));
                InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_ArtifactPouch, 1));
            }
        }

        public static void IncreaseArkPassiveNum(int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                PlayData.TSavedata.Passive_Itembase.Add(null);
            }
            PlayData.TSavedata.ArkPassivePlus += num;

            if (UIManager.NowActiveUI is ArkPartsUI)
            {
                UIManager.NowActiveUI.Delete();
            }
        }
        public static void CreateSkill(string skill, BattleChar bchar)
        {
            if (bchar == null)
            {
                Debug.Log($"[CreateSkill] BattleChar is null for skill {skill}");
                return;
            }
            Skill newSkill = Skill.TempSkill(skill, bchar, bchar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(newSkill, true);
        }

        public static Skill CreateSkill(string skill, BattleChar bchar, bool isExcept = false, bool isDiscarded = false, int num = 0, int mana = 0)
        {
            if (bchar == null)
            {
                Debug.Log($"[CreateSkill] BattleChar is null for skill {skill}");
                return null;
            }
            Skill newSkill = Skill.TempSkill(skill, bchar, bchar.MyTeam);
            newSkill.isExcept = isExcept;
            if (isDiscarded) newSkill.AutoDelete = num;
            newSkill.AP = mana;
            BattleSystem.instance.AllyTeam.Add(newSkill, true);
            return newSkill;
        }

        public static void AddBuff(BattleChar bchar, string buffKey, int num = 0)
        {
            for (int i = 0; i < num; i++)
            {
                if (bchar == null || buffKey.IsNullOrEmpty()) return;
                bchar.BuffAdd(buffKey, bchar, false, 0, false, -1, false);
            }
        }

        public static void ReverseDebuffs(BattleChar BChar, BattleChar BuffTaker, string debuff, string buff)
        {
            if (BuffTaker is BattleAlly ally)
            {
                if (ally.Info == null || string.IsNullOrEmpty(ally.Info.KeyData))
                {
                    Debug.LogWarning("[ReverseDebuffs] BuffTaker Info or KeyData is null, skipping.");
                    return;
                }

                var data = new GDECharacterData(ally.Info.KeyData);

                if (data.Gender == 0) // Male
                {
                    if (ally.BuffReturn(debuff, false) != null)
                    {
                        ally.BuffRemove(debuff, true);
                        ally.BuffAdd(buff, BChar, false, 0, false, -1, false);
                    }
                }
                else // Female
                {
                    if (ally.BuffReturn(debuff, false) != null)
                    {
                        ally.BuffRemove(debuff, true);
                    }
                    if (ally.BuffReturn(buff, false) != null)
                    {
                        ally.BuffRemove(buff, true);
                    }
                }
            }
        }

        public static void ReverseBuffs(BattleChar BChar, BattleChar BuffTaker, string buff, string debuff)
        {
            if (BuffTaker is BattleEnemy enemy)
            {
                if (enemy.BuffReturn(buff, false) != null)
                {
                    enemy.BuffRemove(buff, true);
                    enemy.BuffAdd(debuff, BChar, false, 0, false, -1, false);
                }
            }
            else if (BuffTaker is BattleAlly ally)
            {
                if (ally.Info == null || string.IsNullOrEmpty(ally.Info.KeyData))
                {
                    Debug.LogWarning("[ReverseBuffs] BuffTaker Info or KeyData is null, skipping.");
                    return;
                }

                var data = new GDECharacterData(ally.Info.KeyData);

                if (data.Gender != 0)
                {
                    if (ally.BuffReturn(debuff, false) != null)
                    {
                        ally.BuffRemove(debuff, true);
                    }
                    if (ally.BuffReturn(buff, false) != null)
                    {
                        ally.BuffRemove(buff, true);
                    }
                }
            }
        }

        public static void ForceMPUpgrade()
        {
            PlayData.TSavedata.SoulUpgrade.AP++; // Also increase next Mana upgrade

            //PlayData.TSavedata._AP++;

            MasterAudio.PlaySound("SE_ClickButton", 1f);
            MasterAudio.PlaySound("SE_MpRe", 1f);

            var charStatUI = UIManager.inst.CharstatUI.GetComponent<CharStatV4>();
            if (charStatUI != null)
            {
                for (int i = 0; i < charStatUI.ManaAlign.transform.childCount; i++)
                {
                    charStatUI.ManaAlign.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite = charStatUI.ManaSprite;
                }
                for (int j = 0; j < PlayData.AP; j++)
                {
                    charStatUI.ManaAlign.transform.GetChild(j).gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning("[Mod] CharStatV4 UI not found while forcing MPUpgrade.");
            }
        }


        public static void getSprite(string path, Image img)
        {
            string path2 = ModManager.getModInfo("Urunhilda").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
        }

        public static Sprite getSprite(string path)
        {
            string path2 = ModManager.getModInfo("Urunhilda").assetInfo.ImageFromFile(path);
            return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
        }

        public static void getSpriteAsync(string path, Action<AsyncOperationHandle> collback)
        {
            string path2 = ModManager.getModInfo("Urunhilda").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
        }

        public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("Urunhilda");
            if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
            var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
            return AddressableLoadManager.LoadAddressableAsset<T>(address);
        }

        public static T GetAssetsAsync<T>(string path, string assetBundlePatch = null,
            AddressableLoadManager.ManageType type = AddressableLoadManager.ManageType.Stage) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("Urunhilda");
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

        public static GameObject CreateIcon(BattleChar bchar, string name, string sprite, Vector3 offset, Vector3 size)
        {
            Vector3 basePos = bchar.GetTopPos();
            return CreateIconUi(name, bchar.transform, sprite, size, basePos + offset);
        }

        public static GameObject CreateIconUi(string name, Transform parent, string spriteNormal, Vector3 size, Vector3 worldPos)
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

        public static void DestroyAndNullify(ref GameObject obj)
        {
            if (obj != null)
            {
                UnityEngine.Object.Destroy(obj);
                obj = null;
            }
        }

        public static void DestroyAndNullify(params GameObject[] objects)
        {
            foreach (var obj in objects)
            {
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }
        }
    }
}
