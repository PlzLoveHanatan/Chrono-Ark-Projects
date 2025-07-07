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
using UnityEngine.Video;
namespace SuperHero
{
    /// <summary>
    /// Erase the Mobs
    /// </summary>
    public class S_SuperHero_ErasetheMobs : Skill_Extended, IP_SkillUse_Target
    {
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            var buff = BChar.BuffReturn(ModItemKeys.Buff_B_SuperHero_HeroComplex, false) as B_SuperHero_HeroComplex;
            if (buff != null)
                buff.JusticeDamage = DMG;
        }

        public override void Init()
        {
            OnePassive = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            foreach (var target in Targets)
            {
                if (target is BattleEnemy enemy)
                {
                    if (enemy.Boss && enemy.HP <= enemy.GetStat.maxhp * 0.2f)
                        enemy.HPToZero();
                    else if (enemy.HP <= enemy.GetStat.maxhp * 0.4f)
                        enemy.HPToZero();
                }
                else
                {
                    for (var i = 0; i < 3; i++)
                    {
                        BChar.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroComplex, BChar, false, 0, false, -1, false);
                    }
                }
            }
        }
    }
}