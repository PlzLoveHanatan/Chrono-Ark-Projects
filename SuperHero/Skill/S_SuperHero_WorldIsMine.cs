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
    public class S_SuperHero_WorldIsMine : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var superHero = ModItemKeys.Character_SuperHero;
            var allies = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null && x.Info.KeyData != superHero);
            var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish;
            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, enemies.Count);
            var newTarget = enemies[index];
            Targets.Clear();

            if (BChar.Info.Passive is P_SuperHero Hero && Hero.SuperHeroPassive)
            {
                if (enemies.Any())
                {
                    Targets.AddRange(enemies);
                }
                else
                {
                    Targets.Add(newTarget);
                }
            }
            else
            {
                var combinedTargets = allies.Concat(enemies).ToList();
                if (combinedTargets.Any())
                {
                    Targets.AddRange(combinedTargets);
                }
                else
                {
                    
                    Targets.Add(newTarget);
                }
            }
        }
    }
}