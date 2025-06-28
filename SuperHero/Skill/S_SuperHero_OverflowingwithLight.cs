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
    /// Overflowing with Light 
    /// </summary>
    public class S_SuperHero_OverflowingwithLight : Skill_Extended
    {
        public override bool Terms()
        {
            if (BChar.Info.KeyData == ModItemKeys.Character_SuperHero)
                return true;

            return false;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var team = BattleSystem.instance.AllyTeam;
            team.Draw(2);
            team.AP += 2;
            
            team.LucyChar.Overload = 0;
        }
    }
}