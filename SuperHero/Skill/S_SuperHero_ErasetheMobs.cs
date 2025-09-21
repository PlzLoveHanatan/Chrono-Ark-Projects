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

        public override string DescExtended(string desc)
        {
            string text = Utils.SuperHeroMod(BChar) ? ModLocalization.EraseMobs_1 : ModLocalization.EraseMobs_0;
            return base.DescExtended(desc).Replace("Description", text);
        }

        public override bool Terms()
        {
            return BChar.Info.KeyData == ModItemKeys.Character_SuperHero;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (!Utils.SuperHeroMod(BChar))
            {
                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
                    if (ally == Utils.SuperHero) continue;

                    int damage = ally.GetStat.maxhp / 2;
                    ally?.Damage(BChar, damage, false, false, true);
                }
            }

            foreach (var target in Targets)
            {
                if (target is BattleEnemy enemy)
                {
                    if (enemy.Boss && enemy.HP <= enemy.GetStat.maxhp * 0.3f)
                    {
                        enemy.HPToZero();
                    }

                    else if (enemy.HP <= enemy.GetStat.maxhp * 0.6f)
                    {
                        enemy.HPToZero();
                    }
                }
            }
        }
    }
}