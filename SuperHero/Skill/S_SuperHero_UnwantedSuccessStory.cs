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
using static System.Windows.Forms.LinkLabel;
namespace SuperHero
{
    /// <summary>
    /// Unwanted Success Story
    /// </summary>
    public class S_SuperHero_UnwantedSuccessStory : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var heroKey = ModItemKeys.Character_SuperHero;
            var hero = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(c => c.Info.KeyData == heroKey);
            var allies = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a.Info.KeyData != heroKey).ToList();
            int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, allies.Count);
            BattleChar randomTarget = allies[randomIndex];

            if (randomTarget != null)
            {
                randomTarget.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroPresence, BChar, false, 0, false, -1, false);
                randomTarget.Damage(hero, (int)(hero.GetStat.atk), false, true, false, 0, false, false, false);
            }
        }
    }
}