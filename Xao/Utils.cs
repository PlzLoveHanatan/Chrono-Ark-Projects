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
using System.Runtime.InteropServices.WindowsRuntime;
using static CharacterDocument;
using System.Web;
using Spine;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
namespace Xao
{
    public static class Utils
    {
        public static int RareNum;

        public static bool RareBuffAwake;

        public static bool ItemTake;
        public static BattleTeam AllyTeam => BattleSystem.instance.AllyTeam;
        public static BattleChar Xao => AllyTeam.AliveChars.FirstOrDefault(x => x?.Info.KeyData == ModItemKeys.Character_Xao);
        public static bool XaoHornyMod => Xao?.Info?.Passive is P_Xao p && p.HornyMod;

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
            Combo_0,
            Combo_1,
            Combo_2,
            Combo_3,
            XaoFace_BandagePanties,
            XaoFace_BandagePantiesW,
            XaoFace_BandAids,
            XaoFace_Bikini,
            XaoFace_BlackMaidPantyhose,
            XaoFace_CuteLaceBow,
            XaoFace_Kaiju,
            XaoFace_MagicalTeen,
            XaoFace_Maid,
            XaoFace_Miko,
            XaoFace_Swimsuit,
            XaoFace_WhiteMaidPantyhose,
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
            { SpriteType.Combo_0, "Visual/Combo/Combo_0.png" },
            { SpriteType.Combo_1, "Visual/Combo/Combo_1.png" },
            { SpriteType.Combo_2, "Visual/Combo/Combo_2.png" },
            { SpriteType.Combo_3, "Visual/Combo/Combo_3.png" },
            { SpriteType.XaoFace_BandagePanties, "Visual/Faces/BandagePanties.png" },
            { SpriteType.XaoFace_BandagePantiesW, "Visual/Faces/BandagePantiesW.png" },
            { SpriteType.XaoFace_BandAids , "Visual/Faces/BandAids.png" },
            { SpriteType.XaoFace_Bikini , "Visual/Faces/Bikini.png" },
            { SpriteType.XaoFace_BlackMaidPantyhose , "Visual/Faces/BlackMaidPantyhose.png" },
            { SpriteType.XaoFace_CuteLaceBow , "Visual/Faces/CuteLaceBow.png" },
            { SpriteType.XaoFace_Kaiju , "Visual/Faces/Kaiju.png" },
            { SpriteType.XaoFace_MagicalTeen , "Visual/Faces/MagicalTeen.png" },
            { SpriteType.XaoFace_Maid , "Visual/Faces/Maid.png" },
            { SpriteType.XaoFace_Miko , "Visual/Faces/Miko.png" },
            { SpriteType.XaoFace_Swimsuit , "Visual/Faces/Swimsuit.png" },
            { SpriteType.XaoFace_WhiteMaidPantyhose , "Visual/Faces/WhiteMaidPantyhose.png" },
        };

        public static readonly List<Vector3> TextPositions = new List<Vector3>
        {
            new Vector3(0.4f, -0.4f, 0f),
            new Vector3(-0.6f, -0.6f, 0f),
            new Vector3(-1.7f, -0.7f, 0f),
            new Vector3(0.2f, -0.8f, 0f),
            new Vector3(1.1f, -0.1f, 0f),
        };

        public static readonly List<string> TextPromt = new List<string>
        {
            "Visual/Text/H_text_1_L.png",
            "Visual/Text/H_text_1_M.png",
            "Visual/Text/H_text_1_S.png",
            "Visual/Text/H_text_2_L.png",
            "Visual/Text/H_text_2_M.png",
            "Visual/Text/H_text_2_S.png",
            "Visual/Text/H_text_3_L.png",
            "Visual/Text/H_text_3_M.png",
            "Visual/Text/H_text_3_S.png",
        };

        public static readonly Dictionary<string, SpriteType> SkinKeyToSpriteType = new Dictionary<string, SpriteType>
        {
            { ModItemKeys.Character_Skin_Xao_Bandage_Panties, SpriteType.XaoFace_BandagePanties },
            { ModItemKeys.Character_Skin_Xao_Bandage_Panties_W, SpriteType.XaoFace_BandagePantiesW },
            { ModItemKeys.Character_Skin_Xao_Band_Aids, SpriteType.XaoFace_BandAids },
            { ModItemKeys.Character_Skin_Xao_Bikini, SpriteType.XaoFace_Bikini },
            { ModItemKeys.Character_Skin_Xao_Black_Maid_Pantyhose, SpriteType.XaoFace_BlackMaidPantyhose },
            { ModItemKeys.Character_Skin_Xao_Cute_Lace_Bow, SpriteType.XaoFace_CuteLaceBow },
            { ModItemKeys.Character_Xao, SpriteType.XaoFace_Kaiju },
            { ModItemKeys.Character_Skin_Xao_Magical_Teen, SpriteType.XaoFace_MagicalTeen },
            { ModItemKeys.Character_Skin_Xao_Maid, SpriteType.XaoFace_Maid },
            { ModItemKeys.Character_Skin_Xao_Miko, SpriteType.XaoFace_Miko },
            { ModItemKeys.Character_Skin_Xao_Swimsuit, SpriteType.XaoFace_Swimsuit },
            { ModItemKeys.Character_Skin_Xao_White_Maid_Pantyhose, SpriteType.XaoFace_WhiteMaidPantyhose },
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

        public static readonly Dictionary<SpriteType, Vector3> ComboPosition = new Dictionary<SpriteType, Vector3>
        {
            { SpriteType.Combo_0, new Vector3(2f, 2f) },
            { SpriteType.Combo_1, new Vector3(0f, 0.95f) },
            { SpriteType.Combo_2, new Vector3(0f, 0.95f) },
            { SpriteType.Combo_3, new Vector3(0f, 0.95f) },
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
            ModItemKeys.Skill_S_Xao_BikiniTime_0,
            ModItemKeys.Skill_S_Xao_BikiniTime_1,
            ModItemKeys.Skill_S_Xao_BikiniTime_2,
            ModItemKeys.Skill_S_Xao_BikiniTime_3,
            ModItemKeys.Skill_S_Xao_BikiniTime_Love_0,
            ModItemKeys.Skill_S_Xao_BikiniTime_Love_1,
            ModItemKeys.Skill_S_Xao_BikiniTime_Love_2,
            ModItemKeys.Skill_S_Xao_BikiniTime_Love_3,

            ModItemKeys.Skill_S_Xao_CowGirl_0,
            ModItemKeys.Skill_S_Xao_CowGirl_1,
            ModItemKeys.Skill_S_Xao_CowGirl_2,
            ModItemKeys.Skill_S_Xao_CowGirl_Love_0,
            ModItemKeys.Skill_S_Xao_CowGirl_Love_1,
            ModItemKeys.Skill_S_Xao_CowGirl_Love_2,

            ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_0,
            ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_1,
            ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_2,
            ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_Love_0,
            ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_Love_1,
            ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_Love_2,

            ModItemKeys.Skill_S_Xao_MagicalGirlPussy_0,
            ModItemKeys.Skill_S_Xao_MagicalGirlPussy_1,
            ModItemKeys.Skill_S_Xao_MagicalGirlPussy_2,
            ModItemKeys.Skill_S_Xao_MagicalGirlPussy_3,
            ModItemKeys.Skill_S_Xao_MagicalGirlPussy_Love_0,
            ModItemKeys.Skill_S_Xao_MagicalGirlPussy_Love_1,
            ModItemKeys.Skill_S_Xao_MagicalGirlPussy_Love_2,
            ModItemKeys.Skill_S_Xao_MagicalGirlPussy_Love_3,

            ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_0,
            ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_1,
            ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_2,
            ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_Love_0,
            ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_Love_1,
            ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_Love_2,

            ModItemKeys.Skill_S_Xao_MikoExperienceAnal_0,
            ModItemKeys.Skill_S_Xao_MikoExperienceAnal_1,
            ModItemKeys.Skill_S_Xao_MikoExperienceAnal_2,
            ModItemKeys.Skill_S_Xao_MikoExperienceAnal_Love_0,
            ModItemKeys.Skill_S_Xao_MikoExperienceAnal_Love_1,
            ModItemKeys.Skill_S_Xao_MikoExperienceAnal_Love_2,

            ModItemKeys.Skill_S_Xao_MikoExperiencePussy_0,
            ModItemKeys.Skill_S_Xao_MikoExperiencePussy_1,
            ModItemKeys.Skill_S_Xao_MikoExperiencePussy_2,
            ModItemKeys.Skill_S_Xao_MikoExperiencePussy_3,
            ModItemKeys.Skill_S_Xao_MikoExperiencePussy_Love_0,
            ModItemKeys.Skill_S_Xao_MikoExperiencePussy_Love_1,
            ModItemKeys.Skill_S_Xao_MikoExperiencePussy_Love_2,
            ModItemKeys.Skill_S_Xao_MikoExperiencePussy_Love_3,

            ModItemKeys.Skill_S_Xao_SwimsuitDay_0,
            ModItemKeys.Skill_S_Xao_SwimsuitDay_1,
            ModItemKeys.Skill_S_Xao_SwimsuitDay_2,
            ModItemKeys.Skill_S_Xao_SwimsuitDay_Love_0,
            ModItemKeys.Skill_S_Xao_SwimsuitDay_Love_1,
            ModItemKeys.Skill_S_Xao_SwimsuitDay_Love_2,

            ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_0,
            ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_01,
            ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_1,
            ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_2,
            ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_3,
            ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_4,

            ModItemKeys.Skill_S_Xao_Rare_SleepSex_0,
            ModItemKeys.Skill_S_Xao_Rare_SleepSex_01,
            ModItemKeys.Skill_S_Xao_Rare_SleepSex_1,
            ModItemKeys.Skill_S_Xao_Rare_SleepSex_2,
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

        public static readonly List<string> ComboNames = new List<string>
        {
            "Combo_0",
            "Combo_1",
            "Combo_2",
            "Combo_3",
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
            { ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_0, ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_Love_0},
            { ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_1, ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_Love_1},
            { ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_2, ModItemKeys.Skill_S_Xao_ExperienceMaidFootjob_Love_2},
            { ModItemKeys.Skill_S_Xao_MagicalGirlPussy_0, ModItemKeys.Skill_S_Xao_MagicalGirlPussy_Love_0},
            { ModItemKeys.Skill_S_Xao_MagicalGirlPussy_1, ModItemKeys.Skill_S_Xao_MagicalGirlPussy_Love_1},
            { ModItemKeys.Skill_S_Xao_MagicalGirlPussy_2, ModItemKeys.Skill_S_Xao_MagicalGirlPussy_Love_2},
            { ModItemKeys.Skill_S_Xao_MagicalGirlPussy_3, ModItemKeys.Skill_S_Xao_MagicalGirlPussy_Love_3},
            { ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_0, ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_Love_0},
            { ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_1, ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_Love_1},
            { ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_2, ModItemKeys.Skill_S_Xao_MagicalGirlThighjob_Love_2},
            { ModItemKeys.Skill_S_Xao_MikoExperienceAnal_0, ModItemKeys.Skill_S_Xao_MikoExperienceAnal_Love_0},
            { ModItemKeys.Skill_S_Xao_MikoExperienceAnal_1, ModItemKeys.Skill_S_Xao_MikoExperienceAnal_Love_1},
            { ModItemKeys.Skill_S_Xao_MikoExperienceAnal_2, ModItemKeys.Skill_S_Xao_MikoExperienceAnal_Love_2},
            { ModItemKeys.Skill_S_Xao_MikoExperiencePussy_0, ModItemKeys.Skill_S_Xao_MikoExperiencePussy_Love_0},
            { ModItemKeys.Skill_S_Xao_MikoExperiencePussy_1, ModItemKeys.Skill_S_Xao_MikoExperiencePussy_Love_1},
            { ModItemKeys.Skill_S_Xao_MikoExperiencePussy_2, ModItemKeys.Skill_S_Xao_MikoExperiencePussy_Love_2},
            { ModItemKeys.Skill_S_Xao_MikoExperiencePussy_3, ModItemKeys.Skill_S_Xao_MikoExperiencePussy_Love_3},
            { ModItemKeys.Skill_S_Xao_SwimsuitDay_0, ModItemKeys.Skill_S_Xao_SwimsuitDay_Love_0},
            { ModItemKeys.Skill_S_Xao_SwimsuitDay_1, ModItemKeys.Skill_S_Xao_SwimsuitDay_Love_1},
            { ModItemKeys.Skill_S_Xao_SwimsuitDay_2, ModItemKeys.Skill_S_Xao_SwimsuitDay_Love_2},
            { ModItemKeys.Skill_S_Xao_Rare_SleepSex_0, ModItemKeys.Skill_S_Xao_Rare_SleepSex_01},
            { ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_0, ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_01},
            { ModItemKeys.Skill_S_Xao_MaidPanties_0, ModItemKeys.Skill_S_Xao_MaidPanties_Love_0},
            { ModItemKeys.Skill_S_Xao_MaidPanties_1, ModItemKeys.Skill_S_Xao_MaidPanties_Love_1},
            { ModItemKeys.Skill_S_Xao_MaidPanties_2, ModItemKeys.Skill_S_Xao_MaidPanties_Love_2},
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
        public static Skill CreateSkill(string skill, BattleChar bchar, bool isExcept = false, bool isDiscarded = false, int discardedAfter = 0, int mana = 0, bool isNotCount = false)
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
            newSkill.NotCount = isNotCount;
            BattleSystem.instance.AllyTeam.Add(newSkill, true);
            return newSkill;
        }
        public static void AddBuff(BattleChar bchar, string buffKey, int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                if (bchar == null || buffKey.IsNullOrEmpty()) return;
                bchar.BuffAdd(buffKey, bchar, false, 0, false, -1, false);
            }
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
            //Transform existing = parent.Find(name);
            //if (existing != null)
            //{
            //    UnityEngine.Object.Destroy(existing.gameObject); // удаляем старый, если есть
            //}

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

        public static GameObject CreateIcon(BattleChar bchar, string name, string sprite, Vector3 offset, Vector3 size, bool isSibling = true, bool isRecast = false)
        {
            if (name == null || sprite == null)
            {
                return null;
            }

            Debug.Log($"[CreateIcon] Creating icon: {name} at {offset} with sprite: {sprite}");
            Vector3 basePos = bchar.GetTopPos();
            return CreateIconUi(name, bchar.transform, sprite, size, basePos + offset, isSibling, isRecast);
        }

        public static GameObject CreateIcon(string name, string sprite, Vector3 offset, Vector3 size, Transform parent, bool isSibling = true, bool isRecast = false)
        {
            if (name == null || sprite == null || parent == null)
            {
                return null;
            }
            return CreateIconUi(name, parent, sprite, size, offset, isSibling, isRecast);
        }

        public static GameObject CreateIconUi(string name, Transform parent, string spriteNormal, Vector3 size, Vector3 worldPos, bool isSibling = true, bool isRecast = false)
        {
            GameObject iconObject = Utils.CreatGameObject(name, parent);
            if (iconObject == null) return null;

            iconObject.transform.position = worldPos;

            //Image oldImage = iconObject.GetComponent<Image>();
            //if (oldImage != null)
            //{
            //    UnityEngine.Object.Destroy(oldImage);
            //}

            Image image = iconObject.AddComponent<Image>();
            if (image == null) return null;

            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite == null)
            {
                Debug.LogError($"[CreateIconUi] Sprite not found: {spriteNormal}");
                return null;
            }

            image.sprite = sprite;
            image.raycastTarget = isRecast;

            Utils.ImageResize(image, size);

            iconObject.SetActive(true);

            if (isSibling)
            {
                iconObject.transform.SetAsFirstSibling();
            }

            return iconObject;
        }


        public static GameObject CreateComboButton(string name, Transform trans, string spriteNormal, Vector2 size, Vector2 pos)
        {
            GameObject newObject = Utils.CreatGameObject(name, trans);
            if (newObject == null)
            {
                return null;
            }

            newObject.transform.SetParent(trans);
            newObject.transform.localPosition = pos;

            Image image = newObject.AddComponent<Image>();
            Sprite sprite = Utils.getSprite(spriteNormal);
            if (sprite == null)
            {
                return null;
            }

            image.sprite = sprite;
            Utils.ImageResize(image, size, pos);
            newObject.SetActive(true);

            return newObject;
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

        public static T AddComponent<T>(GameObject go) where T : Component
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
                Debug.LogWarning("GetRandomTextPosition: TextPositions list is empty.");
                return Vector3.zero;
            }

            if (TextPositions.Count == 1)
            {
                Debug.Log("GetRandomTextPosition: Only one position available: " + TextPositions[0]);
                return TextPositions[0];
            }

            int index;
            do
            {
                index = UnityEngine.Random.Range(0, TextPositions.Count);
            } while (index == LastTextPositionIndex);

            LastTextPositionIndex = index;

            Debug.Log("GetRandomTextPosition: Selected index " + index + " => " + TextPositions[index]);
            return TextPositions[index];
        }

        public static string GetRandomText()
        {
            if (TextPromt.Count == 0)
            {
                Debug.LogWarning("GetRandomText: TextPromt list is empty.");
                return "";
            }

            if (TextPromt.Count == 1)
            {
                Debug.Log("GetRandomText: Only one prompt available: " + TextPromt[0]);
                return TextPromt[0];
            }

            int index;
            do
            {
                index = UnityEngine.Random.Range(0, TextPromt.Count);
            } while (index == LastTextPromptIndex);

            LastTextPromptIndex = index;

            Debug.Log("GetRandomText: Selected index " + index + " => " + TextPromt[index]);
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

        public static void ChibiStartAnimation(GameObject obj, bool isBounce)
        {
            if (obj != null)
            {
                Xao_Chibi_Animations script = obj.GetComponent<Xao_Chibi_Animations>() ?? obj.AddComponent<Xao_Chibi_Animations>();

                if (isBounce)
                {
                    script?.StartBounce();
                }
                else
                {
                    script?.StartSpin();
                }
            }
        }

        public static void StartComboPopOut(GameObject obj)
        {
            if (obj != null)
            {
                Xao_Combo_Animations script = obj.GetComponent<Xao_Combo_Animations>() ?? obj.AddComponent<Xao_Combo_Animations>();
                script?.PlayPopIn();
            }
        }

        public static void StartHeartsPopOut(GameObject obj)
        {
            if (obj != null)
            {
                Xao_Hearts_Animations script = obj.GetComponent<Xao_Hearts_Animations>() ?? obj.AddComponent<Xao_Hearts_Animations>();
                script.PlayPopIn = true;
            }
        }

        public static void StartHeartsGreyPopOut(GameObject obj)
        {
            if (obj != null)
            {
                Xao_Hearts_Animations script = obj.GetComponent<Xao_Hearts_Animations>() ?? obj.AddComponent<Xao_Hearts_Animations>();
                script.PlayGreyIn = true;
            }
        }
        public static void StartTextPopOut(GameObject obj)
        {
            if (obj != null)
            {
                Xao_Text_Animations script = obj.GetComponent<Xao_Text_Animations>() ?? obj.AddComponent<Xao_Text_Animations>();
                script?.StartScaleUp();
            }
        }

        public static void PopHentaiText(BattleChar bchar)
        {
            GameObject randomHentaitext = Utils.CreateIcon(bchar, "RandomHentaiText", Utils.GetRandomText(), Utils.GetRandomTextPosition(), new Vector3(100f, 100f), false, false);
            Utils.StartTextPopOut(randomHentaitext);
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
        public static void AllyHentaiText(BattleChar bchar)
        {
            if (bchar != Xao && new GDECharacterData(bchar.Info.KeyData).Gender == 1)
            {
                PopHentaiText(bchar);
            }
        }

        public static void RareSimpleExchange(BattleChar bchar)
        {
            if (!RareBuffAwake)
            {
                AddBuff(bchar, ModItemKeys.Buff_B_Xao_S_SimpleExchange);
                RareBuffAwake = true;
            }

            // Определяем мана/свойства для текущего RareNum
            bool isZeroMana = (RareNum % 2 == 0);
            int mana = isZeroMana ? 0 : 1;
            bool swift = isZeroMana;

            // По умолчанию glitch выключен
            bool glitch = !swift && RareNum < 7;

            // Определяем Key и heartNum
            string skillKey;
            int heartNum;

            switch (RareNum)
            {
                case 0:
                case 1:
                    skillKey = XaoHornyMod ? ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_01 : ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_0;
                    heartNum = 0;
                    break;
                case 2:
                case 3:
                    skillKey = ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_1;
                    heartNum = 2;
                    break;
                case 4:
                case 5:
                    skillKey = ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_2;
                    heartNum = 3;
                    break;
                case 6:
                case 7:
                    skillKey = ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_3;
                    heartNum = 4;
                    break;
                default:
                    skillKey = ModItemKeys.Skill_S_Xao_Rare_SimpleExchange_4;
                    heartNum = 5;
                    glitch = true;
                    break;
            }

            // Создаём скилл
            Skill skill = CreateSkill(skillKey, bchar, true, true, 1, mana, swift);

            if (skill != null)
            {
                BattleSystem.DelayInput(IncreaseNumForRare(skill, bchar, glitch, swift, heartNum));
            }

            // Увеличиваем счётчик для следующего вызова
            RareNum++;
        }

        private static IEnumerator IncreaseNumForRare(Skill skill, BattleChar bchar, bool isGlitch = false, bool isSwift = false, int heartNum = 0)
        {
            yield return null;

            if (isGlitch)
            {
                GlitchEffect(skill);
            }

            if (!isSwift)
            {
                AddBuff(bchar, ModItemKeys.Buff_B_Xao_Affection);
            }

            string baseName = skill.MySkill != null ? new GDESkillData(skill.MySkill.KeyID).Name : "Unknown Skill";
            string heartChar = XaoHornyMod ? "♥" : "♡";
            string heartString = heartNum > 0 ? new string(heartChar[0], heartNum) : "";

            skill.MySkill.Name = $"{baseName} {heartString} - {RareNum}";
            skill.MySkill.Description = RareSimpleExhangeDescription;
            skill.MyButton?.InputData(skill, null, false);
        }

        public static void GlitchEffect(this Skill changeFrom)
        {
            if (changeFrom.MyButton != null)
            {
                UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("StoryGlitch/GlitchSkillEffect"), changeFrom.MyButton.transform);
                UnityEngine.Object.Destroy(obj, 0.5f);
            }
        }

        public static string RareSimpleExhangeDescription
        {
            get
            {
                var text = ModLocalization.RareDescription ?? "";
                return text;
            }
        }

        public static void RareSleepSex(BattleChar bchar, string skillKey, int mana = 0, int heartNum = 0, bool isHornyMod = false)
        {
            if (bchar == null || string.IsNullOrEmpty(skillKey)) return;

            Skill skill = CreateSkill(skillKey, bchar, true, true, 1, mana);
            if (skill != null)
            {
                BattleSystem.DelayInput(RareSleepSexDescription(skill, heartNum, isHornyMod));
            }
        }

        public static IEnumerator RareSleepSexDescription(Skill skill, int heartNum = 0, bool isHornyMod = false)
        {
            if (skill == null) yield break;

            yield return null;

            GlitchEffect(skill);

            string baseName = skill.MySkill != null ? new GDESkillData(skill.MySkill.KeyID).Name : "Unknown Skill";
            string heartChar = isHornyMod ? "♥" : "♡";
            string heartCount = heartNum > 0 ? new string(heartChar[0], heartNum) : "";

            if (skill.MySkill != null)
            {
                skill.MySkill.Name = $"{baseName} {heartCount}";
            }

            skill.MyButton?.InputData(skill, null, false);
        }

        public static class SkillCache
        {
            public static List<GDESkillData> CachedSkills;

            public static void Init()
            {
                if (CachedSkills != null) return; // Уже инициализировано

                CachedSkills = new List<GDESkillData>();

                foreach (var gdeskillData in PlayData.ALLSKILLLIST)
                {
                    if (string.IsNullOrEmpty(gdeskillData.User)) continue;
                    if (gdeskillData.Category.Key == GDEItemKeys.SkillCategory_LucySkill) continue;
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
}
