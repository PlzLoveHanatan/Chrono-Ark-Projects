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
    /// Bloodstained Dress
    /// Apply 'Scarlet Remnant' to all allies.
    /// </summary>
    public class S_SuperHero_BloodstainedDress : Skill_Extended
    {
        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var superHero = ModItemKeys.Character_SuperHero;
            var allies = BattleSystem.instance.AllyTeam.AliveChars.Where(x => x != null & x.Info.KeyData != superHero).ToList();
            int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, allies.Count);
            var randomTarget = allies[index];
            randomTarget.BuffAdd(ModItemKeys.Buff_B_SuperHero_ScarletRemnant_0, BChar, false, 0, false, -1, false);
        }
    }
}