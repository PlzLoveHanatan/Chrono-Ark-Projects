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
            //var buff = ModItemKeys.Buff_B_SuperHero_HeroComplex;
            //var buff2 = BChar.BuffReturn(buff, false) as B_SuperHero_HeroComplex;
            //if (buff2 != null)
            //{
            //    buff2.JusticeDamage = (int)(BChar.GetStat.atk * 0.9f);
            //}

            //foreach (var target in BattleSystem.instance.AllyTeam.AliveChars)
            //{
            //    if (target.Info.KeyData != ModItemKeys.Character_SuperHero)
            //    {
            //        target.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroPresence, BChar, false, 0, false, -1, false);
            //    }
            //}'
            var superHero = ModItemKeys.Character_SuperHero;
            var allies = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null && x.Info.KeyData != superHero);
            var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Concat(allies);
            Targets.Clear();
            Targets.AddRange(enemies);
        }
    }
}