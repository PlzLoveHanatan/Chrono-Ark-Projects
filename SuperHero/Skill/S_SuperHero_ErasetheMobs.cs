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
    public class S_SuperHero_ErasetheMobs : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var buff = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            var buff2 = BChar.BuffReturn(buff, false) as B_SuperHero_HeroComplex;
            if (buff2 != null)
            {
                buff2.JusticeDamage = (int)(BChar.GetStat.atk * 1.1f);
            }

            foreach (var target in Targets)
            {
                if (target is BattleEnemy enemy)
                {
                    if (enemy.Boss && enemy.HP <= enemy.GetStat.maxhp * 0.25f)
                        enemy.HPToZero();
                    else if (enemy.HP <= enemy.GetStat.maxhp * 0.5f)
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