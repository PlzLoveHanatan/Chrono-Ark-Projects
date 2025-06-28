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
            foreach (var target in Targets)
            {
                if (target is BattleEnemy enemy)
                    enemy.BuffAdd(ModItemKeys.Buff_B_SuperHero_BlindingGlory, BChar, false, 0, false, -1, false);

                if (target is BattleAlly ally && ally.Info.KeyData != ModItemKeys.Character_SuperHero)
                    ally.BuffAdd(ModItemKeys.Buff_B_SuperHero_HeroPresence, BChar, false, 0, false, -1, false);
            }
        }
    }
}