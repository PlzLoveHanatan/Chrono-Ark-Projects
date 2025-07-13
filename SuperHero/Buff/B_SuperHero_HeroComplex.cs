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
    public class B_SuperHero_HeroComplex : Buff, IP_SkillUse_User, IP_BuffAddAfter, IP_SomeOneDead, IP_Awake
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

        public int JusticeDamage;
        public bool SuperHero;
        private bool SuperVillain;

        private bool FirstStar;
        private bool SecondStar;
        private bool ThirdStar;

        private bool VillainMascot;
        private bool HeroMascot;

        private GameObject heroMascot;
        private GameObject villainMascot;

        private GameObject StarYellow1;
        private GameObject StarYellow2;
        private GameObject StarYellow3;

        private GameObject StarPurple1;
        private GameObject StarPurple2;
        private GameObject StarPurple3;

        public override string DescExtended()
        {
            if (BattleSystem.instance != null && SuperHero)
            {
                return base.DescExtended().Replace("&a", 0.ToString());
            }
            return base.DescExtended().Replace("&a", (StackNum * 4).ToString());
        }
        public void Awake()
        {
            var superHero = ModItemKeys.Character_SuperHero;

            if (BChar.Info.KeyData == superHero && !HeroMascot)
            {
                heroMascot = Utils.CreateHeroIcon(BChar, "HeroMascot", "Ui/HeroMascot.png", new Vector3(-0.2f, 0.4f, 0f), new Vector3(150f, 150f));
                HeroMascot = true;
            }

            Utils.RemovePainSharingBuffsFromAllAllies();
            Utils.RemovePainSharingBuffsFromLucy();
        }

        public override void Init()
        {
            OnePassive = true;
        }

        public override void BuffStat()
        {
            int increaseStats = StackNum * 4;
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
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker.Info.KeyData == ModItemKeys.Character_SuperHero)
                {
                    BuffStat();

                    if (BChar.Info.Passive is P_SuperHero superHero)
                    {
                        superHero.Complex = StackNum;
                    }
                    if (StackNum >= 4 && !FirstStar)
                    {
                        StarYellow1 = Utils.CreateHeroIcon(BChar, "Star1", "Ui/Star1.png", new Vector3(1.25f, 0.95f, 0f), new Vector3(75f, 75f));
                        FirstStar = true;
                    }
                    if (StackNum >= 8 && !SecondStar)
                    {
                        StarYellow2 = Utils.CreateHeroIcon(BChar, "Star2", "Ui/Star2.png", new Vector3(-1.45f, 1f, 0f), new Vector3(75f, 75f));
                        SecondStar = true;
                    }
                    if (StackNum >= 12 && !ThirdStar)
                    {
                        StarYellow3 = Utils.CreateHeroIcon(BChar, "Star3", "Ui/Star3.png", new Vector3(-0.15f, 1.65f, 0f), new Vector3(75f, 75f));
                        ThirdStar = true;
                    }
                    if (StackNum >= 16)
                    {
                        StartRotation(StarYellow1);
                        StartRotation(StarYellow2);
                        StartRotation(StarYellow3);
                    }
                    if (StackNum >= 20 && !VillainMascot && !SuperHero)
                    {
                        DestroyObjects(heroMascot);
                        heroMascot = null;
                        //GameObject obj = GameObject.Find("HeroMascot");
                        //if (obj != null)
                        //{
                        //    UnityEngine.Object.Destroy(obj);
                        //}

                        villainMascot = Utils.CreateHeroIcon(BChar, "VillainMascot", "Ui/VillainMascot.png", new Vector3(-0.2f, 0.4f, 0f), new Vector3(150f, 150f));
                        VillainMascot = true;
                    }

                    if (StackNum >= 25 && !SuperVillain && !SuperHero)
                    {
                        DestroyObjects(StarYellow1, StarYellow2, StarYellow3);
                        StarYellow1 = null;
                        StarYellow2 = null;
                        StarYellow3 = null;

                        StarPurple1 = Utils.CreateHeroIcon(BChar, "StarVillian1", "Ui/StarVillian1.png", new Vector3(1.25f, 0.95f, 0f), new Vector3(75f, 75f));
                        StarPurple2 = Utils.CreateHeroIcon(BChar, "StarVillian2", "Ui/StarVillian2.png", new Vector3(-1.45f, 1f, 0f), new Vector3(75f, 75f));
                        StarPurple3 = Utils.CreateHeroIcon(BChar, "StarVillian3", "Ui/StarVillian3.png", new Vector3(-0.15f, 1.65f, 0f), new Vector3(75f, 75f));

                        StartRotation(StarPurple1);
                        StartRotation(StarPurple2);
                        StartRotation(StarPurple3);

                        SuperHero_FaceChange.ChooseFace(BChar, true);
                        SuperVillain = true;
                    }

                    if (SuperHero && StackNum >= 20 && (villainMascot != null || StarPurple1 != null))
                    {
                        try
                        {
                            VillainObjectsCheck();
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Hero Complex: Patch Catch: " + e.ToString());
                        }
                    }
                }
                else
                {
                    BuffTaker.BuffRemove(buff, true);
                }
            }
        }

        public void VillainObjectsCheck()
        {
            DestroyVillainObject();
            heroMascot = Utils.CreateHeroIcon(BChar, "HeroMascot", "Ui/HeroMascot.png", new Vector3(-0.2f, 0.4f, 0f), new Vector3(150f, 150f));
            StarYellow1 = Utils.CreateHeroIcon(BChar, "Star1", "Ui/Star1.png", new Vector3(1.25f, 0.95f, 0f), new Vector3(75f, 75f));
            StarYellow2 = Utils.CreateHeroIcon(BChar, "Star2", "Ui/Star2.png", new Vector3(-1.45f, 1f, 0f), new Vector3(75f, 75f));
            StarYellow3 = Utils.CreateHeroIcon(BChar, "Star3", "Ui/Star3.png", new Vector3(-0.15f, 1.65f, 0f), new Vector3(75f, 75f));

            StartRotation(StarYellow1);
            StartRotation(StarYellow2);
            StartRotation(StarYellow3);

            SuperHero_FaceChange.ChooseFace(BChar, false);
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

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            string key = SkillD.MySkill.KeyID;
            var target = Targets[0];
            var worldIsMine = ModItemKeys.Skill_S_SuperHero_WorldIsMine;
            var unwantedSuccessStory = ModItemKeys.Skill_S_SuperHero_UnwantedSuccessStory;
            bool whoopsie;
            int chance = 0;

            if (!SuperHero)
            {
                chance = StackNum * 4;
            }
            else
            {
                chance = 0;
            }

            whoopsie = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, chance);
            var superHero = ModItemKeys.Character_SuperHero;
            var newTarget = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != superHero).ToList();

            var bloodMistPainSharing = GDEItemKeys.Buff_B_BloodyMist_ShareDamage_Ally;
            var pmPainSharing = GDEItemKeys.Buff_B_ProgramMaster_LucyMain_Ally;

            var phoenix = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == GDEItemKeys.Character_Phoenix);

            var aliveAllies = BattleSystem.instance.AllyTeam.AliveChars.Any(a => a != null && a.Info.KeyData != superHero);
            var enemyTeam = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Where(a => a != null).ToList();

            if (newTarget.Count == 0 || key == unwantedSuccessStory || key == worldIsMine) return;

            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, newTarget.Count);
            var randomTarget = newTarget[index];

            if (whoopsie)
            {
                BattleAlly ally = randomTarget as BattleAlly;
                if (ally == null)
                {
                    Debug.Log("Selected randomTarget is not a BattleAlly.");
                    return;
                }
                var bloodMistPainSharingReturn = ally?.BuffReturn(bloodMistPainSharing, false) as B_BloodyMist_ShareDamage_Ally;
                var pmPainSharingReturn = ally?.BuffReturn(pmPainSharing, false) as B_ProgramMaster_LucyMain_Ally;

                if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.PlusHit)
                {
                    if (SkillD.MySkill.Target.Key != GDEItemKeys.s_targettype_all_enemy)
                    {
                        Targets.Clear();
                        Targets.Add(randomTarget);

                        if (ally != null && StackNum >= 25)
                        {
                            Utils.ForceKill(ally);
                        }
                        else if (StackNum < 25)
                        {
                            if (bloodMistPainSharingReturn != null)
                            {
                                bloodMistPainSharingReturn.SelfDestroy();
                            }
                            else if (pmPainSharingReturn != null)
                            {
                                pmPainSharingReturn.SelfDestroy();
                            }
                        }
                    }
                }
                else if (SkillD.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy)
                {
                    if (StackNum < 25 && aliveAllies)
                    {
                        foreach (var enemy in enemyTeam)
                        {
                            enemy.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex_0, BChar, false, 0, false, -1, false);
                        }
                        foreach (var singleAlly in newTarget)
                        {
                            if (singleAlly == null) continue;

                            var bmBuff = singleAlly.BuffReturn(GDEItemKeys.Buff_B_BloodyMist_ShareDamage_Ally, false) as B_BloodyMist_ShareDamage_Ally;
                            if (bmBuff != null)
                            {
                                bmBuff.SelfDestroy();
                            }

                            var pmBuff = singleAlly.BuffReturn(GDEItemKeys.Buff_B_ProgramMaster_LucyMain_Ally, false) as B_ProgramMaster_LucyMain_Ally;
                            if (pmBuff != null)
                            {
                                pmBuff.SelfDestroy();
                            }
                        }
                    }
                    foreach (var singleAlly in newTarget)
                    {
                        if (singleAlly == null) continue;

                        if (StackNum >= 25)
                        {
                            if (singleAlly == phoenix && phoenix.HP <= -150)
                            {
                                phoenix.Dead(false, false);
                                continue;
                            }

                            if (singleAlly is BattleAlly allyCheck)
                            {
                                Utils.ForceKill(allyCheck);
                                continue;
                            }
                        }
                        if (singleAlly.Info.KeyData != superHero)
                        {
                            singleAlly.Damage(BChar, JusticeDamage, false, true, false, 0, false, false, false);
                        }
                    }
                }
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
        public void DestroyVillainObject()
        {
            foreach (var obj in VillainObjects)
            {
                if (obj != null)
                {
                    DestroyObjectByName(obj);
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