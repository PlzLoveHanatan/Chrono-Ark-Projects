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
using JetBrains.Annotations;
using Steamworks;

namespace SuperHero
{
    public static class Utils
    {
        public static bool Timer => ModManager.getModInfo("SuperHero").GetSetting<ToggleSetting>("Timer").Value;
        public static bool Music => ModManager.getModInfo("SuperHero").GetSetting<ToggleSetting>("Music").Value;
        public static bool OldStats => ModManager.getModInfo("SuperHero").GetSetting<ToggleSetting>("Old Stats").Value;

        public static bool FirstTimer;
        public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
        public static BattleTeam EnemyTeam => BattleSystem.instance.EnemyTeam;
        public static BattleChar SuperHero => AllyTeam.AliveChars.FirstOrDefault(x => x?.Info.KeyData == ModItemKeys.Character_SuperHero);
        public static bool SuperHeroMod(BattleChar bchar)
        {
            return bchar.Info.Passive is P_SuperHero hero && hero.SuperHero;
        }
        public static bool SuperVillainMod(BattleChar bchar)
        {
            return bchar.Info.Passive is P_SuperHero hero && hero.SuperVillain;
        }

        public static bool SuperStats;

        public static void TimerCheck()
        {
            if (Timer)
            {
                var mod = ModManager.getModInfo("SuperHero");
                mod.GetSetting<ToggleSetting>("Timer").Value = false;
                mod.SaveSetting();
            }
        }

        public static bool TimerOn;

        public static bool Equip;

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
            ModItemKeys.Skill_S_SuperHero_JusticePatience_0,
            ModItemKeys.Skill_S_SuperHero_JusticeFinale,
        };

        public static readonly List<string> SuperHeroDebuff = new List<string>
        {
            ModItemKeys.Buff_B_SuperHero_MarkofJustice,
            ModItemKeys.Buff_B_SuperHero_ScarletRemnant,
            ModItemKeys.Buff_B_SuperHero_HerosSpotlight,
            ModItemKeys.Buff_B_SuperHero_HeroPresence,
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

        public static readonly Dictionary<string, string> SwitchToHero = new Dictionary<string, string>
        {
            { ModItemKeys.Skill_S_SuperHero_JusticePatience, ModItemKeys.Skill_S_SuperHero_JusticePatience_0 },
            { ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0, ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1 },
            { ModItemKeys.Skill_S_SuperHero_JusticeFinale, ModItemKeys.Skill_S_SuperHero_JusticePatience_0 },
        };

        public static readonly Dictionary<string, string> SwitchToVillain = new Dictionary<string, string>
        {
            { ModItemKeys.Skill_S_SuperHero_JusticePatience_0, ModItemKeys.Skill_S_SuperHero_JusticePatience },
            { ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_1, ModItemKeys.Skill_S_SuperHero_IntheNameofJustice_0 },
        };

        public static void CreateSkill(BattleChar bchar, string skill)
        {
            Skill newSkill = Skill.TempSkill(skill, bchar, bchar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(newSkill, true);
        }

        public static Skill CreateSkill(BattleChar bchar, string skillKey, bool isExcept = false, bool isDiscarded = false, int discardedAfter = 0, int mana = 0, bool isNotCount = false, bool isAddToHand = true)
        {
            Skill newSkill = Skill.TempSkill(skillKey, bchar, bchar.MyTeam);
            newSkill.isExcept = isExcept;

            if (isDiscarded)
            {
                newSkill.AutoDelete = discardedAfter;
            }

            newSkill.AP = mana;
            newSkill.NotCount = isNotCount;

            if (isAddToHand)
            {
                BattleSystem.instance.AllyTeam.Add(newSkill, true);
            }
            return newSkill;
        }

        public static void AddBuff(BattleChar target, BattleChar user, string buffKey, int buffNum = 1)
        {
            for (int i = 0; i < buffNum; i++)
            {
                if (target == null || buffKey.IsNullOrEmpty()) return;
                target.BuffAdd(buffKey, user, false, 0, false, -1, false);
            }
        }

        public static void AddDebuff(BattleChar enemy, BattleChar user, string buffKey, int debuffNum = 1, int percentage = 0)
        {
            if (enemy == null || buffKey.IsNullOrEmpty()) return;

            for (int i = 0; i < debuffNum; i++)
            {
                enemy.BuffAdd(buffKey, user, false, percentage, false, -1, false);
            }
        }

        public static void PlaySong(string skill)
        {
            if (!Utils.Music) return;

            if (!HeroMusic.TryGetValue(skill, out string baseSound)) return;

            string soundToPlay = baseSound;

            MasterAudio.FadeBusToVolume("FieldBGM", 0f, 1f, null, false, false);
            MasterAudio.FadeBusToVolume("BattleBGM", 0f, 1f, null, false, false);
            MasterAudio.StopBus("BGM");
            MasterAudio.StopBus("BGM");
            MasterAudio.StopBus("StoryBGM");
            MasterAudio.PlaySound(soundToPlay, 100f, null, 0f, null, null, false, false);
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

        public static void ForceKill(BattleChar bchar)
        {
            if (bchar == null || bchar.IsDead) return;

            bchar.HPToZero();
            bchar.Dead(false, false);
        }

        public static void JusticeKillAllies()
        {
            foreach (var ally in AllyTeam.AliveChars)
            {
                if (ally == SuperHero) continue;

                for (int i = 0; i < 5; i++)
                {
                    ally.HPToZero();
                    ally.Dead(false, false);
                }
            }
        }
        public static void JusticeKillAll()
        {
            foreach (var target in AllyTeam.AliveChars.Concat(EnemyTeam.AliveChars_Vanish))
            {
                if (target == SuperHero) continue;

                for (int i = 0; i < 5; i++)
                {
                    target.HPToZero();
                    target.Dead(false, false);
                }
            }
        }

        public static IEnumerator HealingParticle(BattleChar target, BattleChar user, int healingNum = 0, bool isHealing = false)
        {
            yield return null;

            if (isHealing)
            {
                target.Heal(user, healingNum, false, true, null);
            }

            Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_SuperHero_DummyHeal, user, user.MyTeam);
            healingParticle.PlusHit = true;
            healingParticle.FreeUse = true;

            target.ParticleOut(healingParticle, target);
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

        public static void AttackRedirect(BattleChar bchar, Skill skillD, List<BattleChar> targets, int chance = 0, int damage = 0)
        {
            bool neverLucky = RandomManager.RandomPer(bchar.GetRandomClass().Main, 100, chance);
            if (!neverLucky) return;

            var aliveAllies = AllyTeam.AliveChars.Where(x => x != SuperHero).ToList();
            if (aliveAllies.Count == 0) return;

            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, aliveAllies.Count);
            var randomTarget = aliveAllies[index];

            if (skillD.IsDamage && skillD.Master == bchar && !skillD.FreeUse && !skillD.PlusHit)
            {
                targets.Clear();

                if (skillD.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy)
                {
                    foreach (var enemy in EnemyTeam.AliveChars)
                    {
                        AddBuff(enemy, bchar, ModItemKeys.Buff_B_SuperHero_EnemyResist);
                    }

                    targets.AddRange(aliveAllies);

                    foreach (var ally in aliveAllies)
                    {
                        ally?.Damage(bchar, damage, false, false);

                        //if (SuperVillainMod(bchar))
                        //{
                        //    ForceKill(ally);
                        //}
                    }
                }

                else if (randomTarget != null)
                {
                    targets.Add(randomTarget);

                    if (SuperVillainMod(bchar))
                    {
                        //ForceKill(randomTarget);
                    }
                }
            }
        }

        public static IEnumerator SuperHeroModCheck(BattleChar bchar, bool isHeroMod = false, bool isVillainMod = false, bool isAddBuff = false)
        {
            yield return null;

            if (bchar.Info.Passive is P_SuperHero hero)
            {
                hero.SuperHero = isHeroMod;
                hero.SuperVillain = isVillainMod;
            }

            if (bchar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) is B_SuperHero_HeroComplex complex)
            {
                complex.UpdateHeroMod(isHeroMod, isVillainMod);
            }

            string buffKey = isHeroMod ? ModItemKeys.Buff_B_SuperHero_JusticeAscension : ModItemKeys.Buff_B_SuperHero_JusticeHero;

            if (bchar.BuffReturn(buffKey, false) != null)
            {
                bchar.BuffRemove(buffKey, true);
            }

            if (isAddBuff && bchar.BuffReturn(ModItemKeys.Buff_B_SuperHero_JusticeHero, false) == null)
            {
                AddBuff(bchar, BattleSystem.instance.DummyChar, ModItemKeys.Buff_B_SuperHero_JusticeHero);
            }
        }

        public static void ApplyJusticeMark(BattleChar bchar, bool isAlmostVillain = false)
        {
            if (!SuperHeroMod(bchar) || isAlmostVillain)
            {
                var allies = AllyTeam.AliveChars.Where(x => x != SuperHero && x.BuffReturn(ModItemKeys.Buff_B_E_SuperHero_LightArmor) == null).ToList();
                int index = RandomManager.RandomInt(bchar.GetRandomClass().Main, 0, allies.Count);
                var randomAlly = allies[index];

                AddDebuff(randomAlly, bchar, ModItemKeys.Buff_B_SuperHero_MarkofJustice);
            }
        }
        public static void SkillChange(this Skill changeFrom, Skill changeTo, bool keepID = true, bool keepExtended = true)
        {
            if (changeFrom.MyButton != null)
            {
                UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("StoryGlitch/GlitchSkillEffect"), changeFrom.MyButton.transform);
                UnityEngine.Object.Destroy(obj, 1f);
            }

            List<Skill_Extended> ExtendedToKeep = new List<Skill_Extended>();
            ExtendedToKeep.AddRange(changeTo.AllExtendeds.Select(ex => ex.Clone() as Skill_Extended));
            foreach (Skill_Extended skill_Extended in changeFrom.AllExtendeds)
            {
                foreach (string text in changeFrom.MySkill.SkillExtended)
                {
                    if (keepExtended && !text.Contains(skill_Extended.Name))
                    {
                        ExtendedToKeep.Add(skill_Extended.Clone() as Skill_Extended);
                    }
                    skill_Extended.SelfDestroy();
                }
            }

            bool createExcept = keepExtended && changeFrom.isExcept;
            changeFrom.Init(changeTo.MySkill, changeFrom.Master, changeFrom.Master.MyTeam);
            if (createExcept) changeFrom.isExcept = true;

            foreach (var skill_Extended in ExtendedToKeep)
            {
                if (skill_Extended.BattleExtended)
                {
                    changeFrom.ExtendedAdd_Battle(skill_Extended);
                }
                else
                {
                    changeFrom.ExtendedAdd(skill_Extended);
                }
            }

            changeFrom.Image_Skill = changeTo.Image_Skill;
            changeFrom.Image_Button = changeTo.Image_Button;
            changeFrom.Image_Basic = changeTo.Image_Basic;

            if (changeFrom.CharinfoSkilldata == null) changeFrom.CharinfoSkilldata = new CharInfoSkillData(changeFrom.MySkill);

            changeFrom.CharinfoSkilldata.SkillInfo = changeFrom.MySkill;
            Skill_Extended oldUpgrade = changeFrom.CharinfoSkilldata.SKillExtended;
            if (!keepID)
            {
                changeFrom.CharinfoSkilldata.CopyData(changeTo.CharinfoSkilldata);
            }
            if (keepExtended)
            {
                changeFrom.CharinfoSkilldata.SKillExtended = oldUpgrade;
            }
            else
            {
                changeFrom.CharinfoSkilldata.SKillExtended = changeTo.CharinfoSkilldata.SKillExtended;
            }
            BattleSystem.instance.StartCoroutine(BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));
        }

        public static IEnumerator CheckModSkills(bool isHeroMod = false)
        {
            yield return null;

            var dictionary = isHeroMod ? SwitchToHero : SwitchToVillain;

            foreach (Skill skill in AllyTeam.Skills)
            {
                if (dictionary.TryGetValue(skill.MySkill.KeyID, out var newSkillID))
                {
                    var newSkill = Skill.TempSkill(newSkillID, skill.Master, skill.Master.MyTeam);
                    skill.SkillChange(newSkill);
                }

                //if (isHeroMod && skill.MySkill.KeyID == ModItemKeys.Skill_S_SuperHero_JusticeFinale)
                //{
                //    skill.Remove();
                //}
            }
        }
    }
}
