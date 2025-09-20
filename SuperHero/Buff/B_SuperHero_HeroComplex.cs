using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using UnityEngine.ResourceManagement.AsyncOperations;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using Unity.Collections.LowLevel.Unsafe;
namespace SuperHero
{
    /// <summary>
    /// Hero Complex
    /// </summary>
    public class B_SuperHero_HeroComplex : Buff, IP_BuffAddAfter, IP_SomeOneDead, IP_Awake
    {
        private readonly List<string> Objects = new List<string>()
        {
            "HeroMascot",
            "Star1",
            "Star2",
            "Star3",
            "VillainMascot",
            "StarVillian1",
            "StarVillian2",
            "StarVillian3",
        };

        private readonly List<string> VillainObjects = new List<string>()
        {
            "VillainMascot",
            "StarVillian1",
            "StarVillian2",
            "StarVillian3",
        };

        private readonly List<string> HeroObjects = new List<string>()
        {
            "HeroMascot",
            "Star1",
            "Star2",
            "Star3",
        };

        public int JusticeDamage;

        private GameObject heroMascot;
        private GameObject villainMascot;

        private GameObject StarYellow1;
        private GameObject StarYellow2;
        private GameObject StarYellow3;

        private GameObject StarPurple1;
        private GameObject StarPurple2;
        private GameObject StarPurple3;

        //public override string DescExtended()
        //{
        //    string text = Utils.SuperHeroMod(BChar) ? ModLocalization.HeroComplex_1 : ModLocalization.HeroComplex_0;
        //    return text.Replace("&a", (StackNum * 2).ToString());
        //}

        public void Awake()
        {
            var superHero = ModItemKeys.Character_SuperHero;

            if (BChar.Info.KeyData == superHero && heroMascot == null && villainMascot == null)
            {
                heroMascot = CreateHeroMascot(BChar);
            }
        }

        public override void Init()
        {
            OnePassive = true;
        }

        public override void BuffStat()
        {
            int increaseStats = StackNum * 2;
            PlusPerStat.MaxHP = increaseStats;
            PlusStat.maxhp = increaseStats;
            //PlusStat.atk = increaseStats;
            PlusPerStat.Damage = increaseStats;
            PlusStat.cri = increaseStats;
            PlusStat.dod = increaseStats;
            PlusStat.def = increaseStats;
            //PlusStat.reg = increaseStats;
            PlusStat.hit = increaseStats;
            PlusStat.RES_CC = increaseStats;
            PlusStat.RES_DEBUFF = increaseStats;
            PlusStat.RES_DOT = increaseStats;
            PlusStat.HIT_CC = increaseStats;
            PlusStat.HIT_DEBUFF = increaseStats;
            PlusStat.HIT_DOT = increaseStats;
            PlusStat.HEALTaken = increaseStats;
            PlusStat.DeadImmune = increaseStats;
            //PlusStat.PlusCriDmg = increaseStats;
            PlusStat.CRIGetDMG = -increaseStats;
            PlusStat.DMGTaken = -increaseStats;
            PlusStat.HEALTaken = increaseStats;
            PlusStat.Penetration = increaseStats;
            PlusStat.AggroPer = increaseStats;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff == this && BuffTaker == Utils.SuperHero)
            {
                BuffStat();
                EnsureYellowStars(ref StarYellow1, ref StarYellow2, ref StarYellow3, BChar, StackNum);

                if (BChar.Info.Passive is P_SuperHero superHero)
                {
                    superHero.Complex = StackNum;
                }

                if (StackNum >= 20 && !Utils.SuperHeroMod(BChar) && villainMascot == null)
                {
                    DestroyObjects(heroMascot);
                    heroMascot = null;
                    villainMascot = CreateVillainMascot(BChar);
                }

                if (StackNum >= 25 && !Utils.SuperHeroMod(BChar))
                {
                    DestroyObjects(StarYellow1, StarYellow2, StarYellow3);
                    StarYellow1 = StarYellow2 = StarYellow3 = null;

                    CreatePurpleStars(BChar, out StarPurple1, out StarPurple2, out StarPurple3);

                    SuperHero_FaceChange.ChooseFace(BChar, true);
                    Utils.AddBuff(BChar, BattleSystem.instance.DummyChar, ModItemKeys.Buff_B_SuperHero_JusticeAscension);
                }
            }
            else
            {
                BuffTaker.BuffRemove(ModItemKeys.Buff_B_SuperHero_HeroComplex);
            }
        }

        public void EnsureYellowStars(ref GameObject star1, ref GameObject star2, ref GameObject star3, BattleChar bChar, int stackNum)
        {
            if (stackNum >= 4 && star1 == null)
            {
                star1 = Utils.CreateHeroIcon(bChar, "Star1", "Ui/Star1.png", new Vector3(1.25f, 0.95f, 0f), new Vector3(75f, 75f));
            }

            if (stackNum >= 8 && star2 == null)
            {
                star2 = Utils.CreateHeroIcon(bChar, "Star2", "Ui/Star2.png", new Vector3(-1.45f, 1f, 0f), new Vector3(75f, 75f));
            }

            if (stackNum >= 12 && star3 == null)
            {
                star3 = Utils.CreateHeroIcon(bChar, "Star3", "Ui/Star3.png", new Vector3(-0.15f, 1.65f, 0f), new Vector3(75f, 75f));
            }

            if (stackNum >= 16)
            {
                StartRotation(star1);
                StartRotation(star2);
                StartRotation(star3);
            }
        }

        public void CreatePurpleStars(BattleChar bChar, out GameObject star1, out GameObject star2, out GameObject star3)
        {
            star1 = Utils.CreateHeroIcon(bChar, "StarVillian1", "Ui/StarVillian1.png", new Vector3(1.25f, 0.95f, 0f), new Vector3(75f, 75f));
            star2 = Utils.CreateHeroIcon(bChar, "StarVillian2", "Ui/StarVillian2.png", new Vector3(-1.45f, 1f, 0f), new Vector3(75f, 75f));
            star3 = Utils.CreateHeroIcon(bChar, "StarVillian3", "Ui/StarVillian3.png", new Vector3(-0.15f, 1.65f, 0f), new Vector3(75f, 75f));

            StartRotation(star1);
            StartRotation(star2);
            StartRotation(star3);
        }

        public static GameObject CreateVillainMascot(BattleChar bChar)
        {
            return Utils.CreateHeroIcon(bChar, "VillainMascot", "Ui/VillainMascot.png", new Vector3(-0.2f, 0.4f, 0f), new Vector3(150f, 150f));
            
        }
        public static GameObject CreateHeroMascot(BattleChar bChar)
        {
            return Utils.CreateHeroIcon(bChar, "HeroMascot", "Ui/HeroMascot.png", new Vector3(-0.2f, 0.4f, 0f), new Vector3(150f, 150f));
        }

        public void UpdateHeroMod(bool isHeroMod, bool isVillainMod)
        {
            if (isHeroMod)
            {
                DestroyObjectIfNotNull(ref villainMascot);
                DestroyObjectIfNotNull(ref StarPurple1);
                DestroyObjectIfNotNull(ref StarPurple2);
                DestroyObjectIfNotNull(ref StarPurple3);

                if (StackNum >= 1 && heroMascot == null)
                {
                    heroMascot = CreateHeroMascot(BChar);
                }

                EnsureYellowStars(ref StarYellow1, ref StarYellow2, ref StarYellow3, BChar, StackNum);

                SuperHero_FaceChange.ChooseFace(BChar, false);
            }

            if (isVillainMod)
            {
                DestroyObjectIfNotNull(ref heroMascot);
                DestroyObjectIfNotNull(ref StarYellow1);
                DestroyObjectIfNotNull(ref StarYellow2);
                DestroyObjectIfNotNull(ref StarYellow3);

                if (StackNum >= 20 && villainMascot == null)
                {
                    villainMascot = CreateVillainMascot(BChar);
                }

                if (StackNum >= 25)
                {
                    CreatePurpleStars(BChar, out StarPurple1, out StarPurple2, out StarPurple3);
                }
                SuperHero_FaceChange.ChooseFace(BChar, true);
            }
        }

        private void DestroyObjectIfNotNull(ref GameObject obj)
        {
            if (obj != null)
            {
                UnityEngine.Object.Destroy(obj);
                obj = null;
            }
        }

        public void DestroyObjects(params GameObject[] objects)
        {
            foreach (var obj in objects)
            {
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }
        }

        private void StartRotation(GameObject star)
        {
            if (star != null)
            {
                SuperHero_Script rotator = star.GetComponent<SuperHero_Script>();
                if (rotator == null)
                {
                    rotator = star.AddComponent<SuperHero_Script>();
                }
                rotator.StartRotation();
            }
        }

        public void SomeOneDead(BattleChar DeadChar)
        {
            if (DeadChar.Info.KeyData == ModItemKeys.Character_SuperHero)
            {
                foreach (var obj in Objects)
                {
                    if (obj != null)
                    {
                        DestroyObjectByName(obj);
                    }
                }
            }
        }

        public void DestroyObjectByName(string objName)
        {
            GameObject obj = GameObject.Find(objName);
            if (obj != null)
            {
                UnityEngine.Object.Destroy(obj);
            }
        }
    }
}