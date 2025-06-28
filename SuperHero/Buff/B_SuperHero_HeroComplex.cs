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

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(HeroComplex)).ToString());
        }

        public override void BuffStat()
        {
            HeroComplex = StackNum * 5;
            PlusPerStat.Damage = StackNum * 3;
            PlusStat.dod = StackNum * 3;
            PlusStat.reg = StackNum * 3;
            PlusStat.HEALTaken = StackNum * 3;
            PlusStat.DeadImmune = StackNum * 3;
            PlusStat.hit = StackNum * 3;
            PlusStat.PlusCriDmg = StackNum * 3;
            PlusStat.def = StackNum * 3;
            PlusStat.RES_CC = StackNum * 3;
            PlusStat.RES_DEBUFF = StackNum * 3;
            PlusStat.RES_DOT = StackNum * 3;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker.Info.KeyData != ModItemKeys.Character_SuperHero && addedbuff.BuffData.Key == ModItemKeys.Buff_B_SuperHero_HeroComplex)
                SelfDestroy();
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            bool whoopsie = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, HeroComplex);
            var newTargets = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null && a != BChar).ToList();
            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, newTargets.Count);
            var randomTarget = newTargets[index];
            if (newTargets.Count == 0) return;
            if (whoopsie)
            {
                if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.PlusHit
                    && SkillD.MySkill.Target.Key != GDEItemKeys.s_targettype_all_enemy)
                {
                    Targets.Clear();
                    Targets.Add(randomTarget);
                }
            }
        }
    }
}