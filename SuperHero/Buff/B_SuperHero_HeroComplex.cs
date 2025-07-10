using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
namespace SuperHero
{
    /// <summary>
    /// Hero Complex
    /// </summary>
    public class B_SuperHero_HeroComplex : Buff, IP_SkillUse_User, IP_BuffAddAfter
    {
        public int JusticeDamage;
        private bool SuperVillian;
        private bool FirstStar;
        private bool SecondStar;
        private bool ThirdStar;
        private bool VillianMascot;
        private GameObject star1;
        private GameObject star2;
        private GameObject star3;
        private GameObject villianMascot;
        private GameObject villianStar1;
        private GameObject villianStar2;
        private GameObject villianStar3;

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", (StackNum * 4).ToString());
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
            PlusStat.PlusCriDmg = increaseStats;
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
                        star1 = Utils.CreateHeroIcon(BChar, "Star1", "Ui/Star1.png", new Vector3(1.25f, 0.95f, 0f), new Vector3(75f, 75f));
                        FirstStar = true;
                    }
                    if (StackNum >= 8 && !SecondStar)
                    {
                        star2 = Utils.CreateHeroIcon(BChar, "Star2", "Ui/Star2.png", new Vector3(-1.45f, 1f, 0f), new Vector3(75f, 75f));
                        SecondStar = true;
                    }
                    if (StackNum >= 12 && !ThirdStar)
                    {
                        star3 = Utils.CreateHeroIcon(BChar, "Star3", "Ui/Star3.png", new Vector3(-0.15f, 1.65f, 0f), new Vector3(75f, 75f));
                        ThirdStar = true;
                    }
                    if (StackNum >= 16)
                    {
                        StartRotation(star1);
                        StartRotation(star2);
                        StartRotation(star3);
                    }
                    if (StackNum >= 20 && !VillianMascot)
                    {
                        GameObject obj = GameObject.Find("HeroMascot");
                        if (obj != null)
                        {
                            UnityEngine.Object.Destroy(obj);
                        }
                        villianMascot = Utils.CreateHeroIcon(BChar, "VillainMascot", "Ui/VillainMascot.png", new Vector3(-0.2f, 0.4f, 0f), new Vector3(150f, 150f));
                        VillianMascot = true;
                    }

                    if (StackNum >= 25 && !SuperVillian)
                    {
                        DestroyStars(star1, star2, star3);
                        star1 = null;
                        star2 = null;
                        star3 = null;

                        villianStar1 = Utils.CreateHeroIcon(BChar, "StarVillian1", "Ui/StarVillian1.png", new Vector3(1.25f, 0.95f, 0f), new Vector3(75f, 75f));
                        villianStar2 = Utils.CreateHeroIcon(BChar, "StarVillian2", "Ui/StarVillian2.png", new Vector3(-1.45f, 1f, 0f), new Vector3(75f, 75f));
                        villianStar3 = Utils.CreateHeroIcon(BChar, "StarVillian3", "Ui/StarVillian3.png", new Vector3(-0.15f, 1.65f, 0f), new Vector3(75f, 75f));

                        StartRotation(villianStar1);
                        StartRotation(villianStar2);
                        StartRotation(villianStar3);

                        SuperHero_Villian.BattleImageChange(BChar);
                        SuperVillian = true;
                    }
                }
                else
                {
                    BuffTaker.BuffRemove(buff, true);
                }
            }
        }
        public static void DestroyStars(params GameObject[] stars)
        {
            foreach (var star in stars)
            {
                if (star != null)
                {
                    UnityEngine.Object.Destroy(star);
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
            var superHero = ModItemKeys.Character_SuperHero;
            var target = Targets[0];
            var phoenix = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == GDEItemKeys.Character_Phoenix);
            bool whoopsie = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, StackNum * 4);
            var newTargets = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != superHero).ToList();
            var aliveAllies = BattleSystem.instance.AllyTeam.AliveChars.Any(a => a != null && a.Info.KeyData != superHero);
            var enemyTeam = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Where(a => a != null).ToList();
            var worldIsMine = ModItemKeys.Skill_S_SuperHero_WorldIsMine;

            if (newTargets.Count == 0 || SkillD.MySkill.KeyID == ModItemKeys.Skill_S_SuperHero_UnwantedSuccessStory) return;
            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, newTargets.Count);
            var randomTarget = newTargets[index];

            if (whoopsie)
            {
                if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.PlusHit && SkillD.MySkill.KeyID != worldIsMine)
                {
                    if (SkillD.MySkill.Target.Key != GDEItemKeys.s_targettype_all_enemy)
                    {
                        Targets.Clear();
                        Targets.Add(randomTarget);
                        if (phoenix != null && randomTarget == phoenix && phoenix.HP <= -150)
                        {
                            phoenix.Dead(false, false);
                        }
                        if (randomTarget == null) return;
                    }
                    else if (SkillD.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy)
                    {
                        if (StackNum < 25 && aliveAllies)
                        {
                            foreach (var enemy in enemyTeam)
                            {
                                enemy.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex_0, BChar, false, 0, false, -1, false);
                            }
                        }
                        foreach (var ally in newTargets)
                        {
                            if (phoenix != null && ally == phoenix && phoenix.HP <= -150)
                            {
                                phoenix.Dead(false, false);
                            }
                            if (ally.Info.KeyData != superHero)
                            {
                                ally.Damage(BChar, JusticeDamage, false, true, false, 0, false, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}