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
using System.Numerics;
namespace SuperHero
{
    /// <summary>
    /// Hero Complex
    /// </summary>
    public class B_SuperHero_HeroComplex : Buff, IP_SkillUse_User, IP_BuffAddAfter
    {
        public int HeroComplex;
        public int JusticeDamage;

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(HeroComplex * 3)).ToString());
        }
        public override void Init()
        {
            OnePassive = true;
        }

        public override void BuffStat()
        {
            int increaseStats = StackNum * 3;
            PlusStat.maxhp = increaseStats;
            PlusPerStat.Damage = increaseStats;
            PlusStat.cri = increaseStats;
            PlusStat.dod = increaseStats;
            PlusStat.def = increaseStats;
            PlusStat.reg = increaseStats;
            PlusStat.hit = increaseStats;
            PlusStat.RES_CC = increaseStats;
            PlusStat.RES_DEBUFF = increaseStats;
            PlusStat.RES_DOT = increaseStats;
            PlusStat.HIT_CC = increaseStats;
            PlusStat.HIT_DEBUFF = increaseStats;
            PlusStat.HIT_DOT = increaseStats;
            PlusStat.HEALTaken = increaseStats;
            PlusStat.DeadImmune = increaseStats;
            PlusStat.PlusCriDmg = -increaseStats;
            PlusStat.DMGTaken = - increaseStats;
            PlusStat.HEALTaken = increaseStats;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            if (addedbuff.BuffData.Key == buff)
            {
                if (BuffTaker.Info.KeyData == ModItemKeys.Character_SuperHero)
                {
                    HeroComplex++;
                    BuffStat();

                    if (BChar.Info.Passive is P_SuperHero superHero)
                        superHero.Complex = HeroComplex;
                }
                else
                    BuffTaker.BuffRemove(buff);
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            var superHero = ModItemKeys.Character_SuperHero;
            bool whoopsie = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, HeroComplex * 3);
            var newTargets = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != superHero).ToList();
            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, newTargets.Count);
            var randomTarget = newTargets[index];
            if (newTargets.Count == 0 || SkillD.MySkill.KeyID == ModItemKeys.Skill_S_SuperHero_UnwantedSuccessStory) return;
            if (whoopsie)
            {
                if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.PlusHit)
                {
                    if (SkillD.MySkill.Target.Key != GDEItemKeys.s_targettype_all_enemy)
                    {
                        Targets.Clear();
                        Targets.Add(randomTarget);
                    }
                    else
                    {
                        foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
                        {
                            if (ally.Info.KeyData != superHero)
                                ally.Damage(BChar, JusticeDamage, false, true, false, 0, false, false, false);
                        }
                    }
                }
            }
        }
    }
}