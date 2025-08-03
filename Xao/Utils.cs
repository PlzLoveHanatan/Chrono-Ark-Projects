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
using static TMPro.SpriteAssetUtilities.TexturePacker;
namespace Xao
{
    public static class Utils
    {
        public enum SpriteType
        {
            Chibi_Idle,
            Chibi_Attack,
            Chibi_AttackExtra_0,
            Chibi_AttackExtra_1,
            Chibi_Normal,
            Chibi_NormalBlush,
            Chibi_TakingDamage_0,
            Chibi_TakingDamage_1,
            Heart_Grey_0,
            Heart_Grey_1,
            Heart_Grey_2,
            Heart_Normal_0,
            Heart_Normal_1,
            Heart_Normal_2,
            HentaiText_0,
            HentaiText_1,
            HentaiText_2,
            HentaiText_3,
            HentaiText_4,
            HentaiText_5,
            HentaiText_6,
            HentaiText_7,
            HentaiText_8,
        };

        public static readonly Dictionary<SpriteType, string> SpritePaths = new Dictionary<SpriteType, string>()
        {
            { SpriteType.Chibi_Idle, "Visual/Chibi/Idle.png" },
            { SpriteType.Chibi_Attack, "Visual/Chibi/Attack.png" },
            { SpriteType.Chibi_AttackExtra_0, "Visual/Chibi/AttackExtra_0.png" },
            { SpriteType.Chibi_AttackExtra_1, "Visual/Chibi/AttackExtra_1.png" },
            { SpriteType.Chibi_Normal, "Visual/Chibi/Normal.png" },
            { SpriteType.Chibi_NormalBlush, "Visual/Chibi/NormalBlush.png" },
            { SpriteType.Chibi_TakingDamage_0, "Visual/Chibi/TakingDamage_0.png" },
            { SpriteType.Chibi_TakingDamage_1, "Visual/Chibi/TakingDamage_1.png" },
            { SpriteType.Heart_Grey_0, "Visual/Heart/HeartGrey_0.png" },
            { SpriteType.Heart_Normal_0, "Visual/Heart/Heart_Normal_0.png" },
            { SpriteType.HentaiText_0, "Visual/Text/H_text_1_L.png" },
            { SpriteType.HentaiText_1, "Visual/Text/H_text_1_M.png" },
            { SpriteType.HentaiText_2, "Visual/Text/H_text_1_S.png" },
            { SpriteType.HentaiText_3, "Visual/Text/H_text_2_L.png" },
            { SpriteType.HentaiText_4, "Visual/Text/H_text_2_M.png" },
            { SpriteType.HentaiText_5, "Visual/Text/H_text_2_S.png" },
            { SpriteType.HentaiText_6, "Visual/Text/H_text_3_L.png" },
            { SpriteType.HentaiText_7, "Visual/Text/H_text_3_M.png" },
            { SpriteType.HentaiText_8, "Visual/Text/H_text_3_S.png" },
        };

        public static readonly List<Vector3> TextPositions = new List<Vector3>
        {
            new Vector3(1f, 1f, 0f),
            new Vector3(0.5f, 0.5f, 0f),
            new Vector3(1.5f, 1.5f, 0f),
            new Vector3(0f, 0.2f, 0f),
        };

        public static readonly List<string> TextPromt = new List<string>
        {
            "Visual/H_text_1_L.png",
            "Visual/H_text_1_M.png",
            "Visual/H_text_1_S.png",
            "Visual/H_text_2_L.png",
            "Visual/H_text_2_M.png",
            "Visual/H_text_2_S.png",
            "Visual/H_text_3_L.png",
            "Visual/H_text_3_M.png",
            "Visual/H_text_3_S.png",
        };

        public static readonly Dictionary<SpriteType, Vector3> ChibiPosition = new Dictionary<SpriteType, Vector3>
        {
            { SpriteType.Chibi_Idle, new Vector3(0f, 0.95f) },
            { SpriteType.Chibi_Attack, new Vector3(-0.4f, 0.95f) },
            { SpriteType.Chibi_AttackExtra_0, new Vector3(-0.2f, 0.95f) },
            { SpriteType.Chibi_AttackExtra_1, new Vector3(-0.3f, 0.95f) },
            { SpriteType.Chibi_Normal, new Vector3(0f, 0.95f) },
            { SpriteType.Chibi_NormalBlush, new Vector3(0f, 0.95f) },
            { SpriteType.Chibi_TakingDamage_0, new Vector3(-0.2f, 0.95f) },
            { SpriteType.Chibi_TakingDamage_1, new Vector3(-0.2f, 0.95f) },
        };

        public static readonly Dictionary<string, string> HeartsPath = new Dictionary<string, string>
        {
            { "HeartGrey_0", "Visual/Heart/HeartGrey_0.png" },
            { "HeartGrey_1", "Visual/Heart/HeartGrey_0.png" },
            { "HeartGrey_2", "Visual/Heart/HeartGrey_0.png" },
            { "HeartNormal_0", "Visual/Heart/HeartNormal_0.png" },
            { "HeartNormal_1", "Visual/Heart/HeartNormal_0.png" },
            { "HeartNormal_2", "Visual/Heart/HeartNormal_0.png" },
        };

        public static readonly Dictionary<string, Vector3> HeartsPosition = new Dictionary<string, Vector3>
        {
            { "HeartGrey_0", new Vector3(1.65f, 0.4f) },
            { "HeartGrey_1", new Vector3(1.25f, 1f) },
            { "HeartGrey_2", new Vector3(1.65f, 1.6f) },
            { "HeartNormal_0", new Vector3(1.65f, 0.4f) },
            { "HeartNormal_1", new Vector3(1.25f, 1f) },
            { "HeartNormal_2", new Vector3(1.65f, 1.6f) },
        };

        public static readonly List<string> HentaiSkills = new List<string>
        {
            ModItemKeys.Skill_S_Xao_Test,
        };

        public static readonly List<string> ChibiNames = new List<string>
        {
            "Chibi_Idle",
            "Chibi_Attack",
            "Chibi_AttackExtra_0",
            "Chibi_AttackExtra_1",
            "Chibi_Normal",
            "Chibi_NormalBlush",
            "Chibi_TakingDamage_0",
            "Chibi_TakingDamage_1",
        };

        public static readonly List<string> Hearts = new List<string>
        {
            "HeartGrey_0",
            "HeartGrey_1",
            "HeartGrey_2",
            "HeartNormal_0",
            "HeartNormal_1",
            "HeartNormal_2",
        };

        public static readonly Dictionary<string, string> XaoSkillList = new Dictionary<string, string>
        {   
            { ModItemKeys.Skill_S_Xao_BikiniTime_0, ModItemKeys.Skill_S_Xao_BikiniTime_Love_0},
            { ModItemKeys.Skill_S_Xao_BikiniTime_1, ModItemKeys.Skill_S_Xao_BikiniTime_Love_1},
            { ModItemKeys.Skill_S_Xao_BikiniTime_2, ModItemKeys.Skill_S_Xao_BikiniTime_Love_2},
            { ModItemKeys.Skill_S_Xao_BikiniTime_3, ModItemKeys.Skill_S_Xao_BikiniTime_Love_3},
            { ModItemKeys.Skill_S_Xao_CowGirl_0, ModItemKeys.Skill_S_Xao_CowGirl_Love_0},
            { ModItemKeys.Skill_S_Xao_CowGirl_1, ModItemKeys.Skill_S_Xao_CowGirl_Love_1},
            { ModItemKeys.Skill_S_Xao_CowGirl_2, ModItemKeys.Skill_S_Xao_CowGirl_Love_2},
        };



        private static int LastTextPositionIndex = -1;
        private static int LastTextPromptIndex = -1;

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

        public static Skill CreateSkill(string skill, BattleChar bchar, bool isExcept = false, bool isDiscarded = false, int discardedAfter = 0, int mana = 0)
        {
            if (bchar == null)
            {
                Debug.Log($"[CreateSkill] BattleChar is null for skill {skill}");
                return null;
            }
            Skill newSkill = Skill.TempSkill(skill, bchar, bchar.MyTeam);
            newSkill.isExcept = isExcept;
            if (isDiscarded) newSkill.AutoDelete = discardedAfter;
            newSkill.AP = mana;
            BattleSystem.instance.AllyTeam.Add(newSkill, true);
            return newSkill;
        }

        public static void getSprite(string path, Image img)
        {
            string path2 = ModManager.getModInfo("Xao").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, img);
        }

        public static Sprite getSprite(string path)
        {
            string path2 = ModManager.getModInfo("Xao").assetInfo.ImageFromFile(path);
            return AddressableLoadManager.LoadAsyncCompletion<Sprite>(path2, AddressableLoadManager.ManageType.None);
        }

        public static void getSpriteAsync(string path, Action<AsyncOperationHandle> collback)
        {
            string path2 = ModManager.getModInfo("Xao").assetInfo.ImageFromFile(path);
            AddressableLoadManager.LoadAsyncAction(path2, AddressableLoadManager.ManageType.None, collback);
        }

        public static void LoadSpriteAsync(string path, Action<Sprite> onLoaded)
        {
            getSpriteAsync(path, handle =>
            {
                Sprite sprite = (Sprite)handle.Result;
                onLoaded?.Invoke(sprite);
            });
        }

        public static T GetAssets<T>(string path, string assetBundlePatch = null) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("Xao");
            if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
            var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
            return AddressableLoadManager.LoadAddressableAsset<T>(address);
        }

        public static T GetAssetsAsync<T>(string path, string assetBundlePatch = null,
            AddressableLoadManager.ManageType type = AddressableLoadManager.ManageType.Stage) where T : UnityEngine.Object
        {
            var mod = ModManager.getModInfo("Xao");
            if (string.IsNullOrEmpty(assetBundlePatch)) assetBundlePatch = mod.DefaultAssetBundlePath;
            var address = mod.assetInfo.ObjectFromAsset<T>(assetBundlePatch, path);
            return AddressableLoadManager.LoadAsyncCompletion<T>(address, type);
        }
        public static GameObject CreatGameObject(string name, Transform parent)
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

        public static GameObject CreateIcon(BattleChar bchar, string name, string sprite, Vector3 offset, Vector3 size, bool isSibling = true)
        {
            if (name == null || sprite == null)
            {
                return null;
            }

            Debug.Log($"[CreateIcon] Creating icon: {name} at {offset} with sprite: {sprite}");
            Vector3 basePos = bchar.GetTopPos();
            return CreateIconUi(name, bchar.transform, sprite, size, basePos + offset, isSibling);
        }

        public static GameObject CreateIconUi(string name, Transform parent, string spriteNormal, Vector3 size, Vector3 worldPos, bool isSibling = true)
        {
            GameObject iconObject = Utils.CreatGameObject(name, parent);
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
            if (isSibling)
            {
                // Отправляем объект на задний план
                iconObject.transform.SetAsFirstSibling();
            }

            return iconObject;
        }

        public static GameObject ReplaceChibiIcon(string chibiName, BattleChar bchar, string name, string sprite, Vector3 offset, Vector3 size, bool isSibling = true)
        {
            var existing = GameObject.Find(chibiName);
            if (existing != null)
            {
                UnityEngine.Object.Destroy(existing);
            }

            return Utils.CreateIcon(bchar, name, sprite, offset, size, isSibling);
        }

        public static GameObject ReplaceChibiIcon(List<string> chibiNames, BattleChar bchar, string name, string sprite, Vector3 offset, Vector3 size, bool isSibling = true)
        {
            foreach (var chibiName in chibiNames)
            {
                var existing = GameObject.Find(chibiName);
                if (existing != null)
                {
                    UnityEngine.Object.Destroy(existing);
                    break;
                }
            }

            return Utils.CreateIcon(bchar, name, sprite, offset, size, isSibling);
        }

        public static void CreateIdleChibi()
        {
            string xao = ModItemKeys.Character_Xao;
            var aliveXao = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == xao);
            if (aliveXao != null)
            {
                Utils.CreateIcon(aliveXao, "Chibi_Idle", Utils.SpritePaths[SpriteType.Chibi_Idle], Utils.ChibiPosition[SpriteType.Chibi_Idle], new Vector3(250f, 250f));
            }
        }

        public static T AddScript<T>(GameObject go) where T : Component
        {
            return go.AddComponent<T>();
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
        public static void DestroyObjects(params GameObject[] objects)
        {
            foreach (var obj in objects)
            {
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }
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

        public static Vector3 GetRandomTextPosition()
        {
            if (TextPositions.Count == 0)
            {
                return Vector3.zero;
            }
            if (TextPositions.Count == 1)
            {
                return TextPositions[0];
            }

            int index;
            do
            {
                index = UnityEngine.Random.Range(0, TextPositions.Count);
            } while (index == LastTextPositionIndex);

            LastTextPositionIndex = index;
            return TextPositions[index];
        }

        public static string GetRandomText()
        {
            if (TextPromt.Count == 0)
                return "";

            if (TextPromt.Count == 1)
                return TextPromt[0];

            int index;
            do
            {
                index = UnityEngine.Random.Range(0, TextPromt.Count);
            } while (index == LastTextPromptIndex);

            LastTextPromptIndex = index;
            return TextPromt[index];
        }

        public static void DestroyAndCreateChibi(ref GameObject obj)
        {
            if (GameObject.Find("Chibi_Idle") == null)
            {
                Utils.DestroyAndNullify(ref obj);
                Utils.CreateIdleChibi();
            }
        }
        public static void DestroyAndCreateChibi(GameObject obj)
        {
            if (GameObject.Find("Chibi_Idle") == null)
            {
                Utils.DestroyAndNullify(ref obj);
                Utils.CreateIdleChibi();
            }
        }

        public static void StartBounce(GameObject obj)
        {
            if (obj != null)
            {
                Xao_Script_Chibi_BounceFadeOut script = obj.GetComponent<Xao_Script_Chibi_BounceFadeOut>();
                if (script == null)
                {
                    script = obj.AddComponent<Xao_Script_Chibi_BounceFadeOut>();
                }
                script.StartBounce();
            }
        }
        public static void StartSpin(GameObject obj)
        {
            if (obj != null)
            {
                Xao_Script_Chibi_SpinFadeOut script = obj.GetComponent<Xao_Script_Chibi_SpinFadeOut>();
                if (script == null)
                {
                    script = obj.AddComponent<Xao_Script_Chibi_SpinFadeOut>();
                }
                script.StartSpinAndFadeOut();
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
    }
}
