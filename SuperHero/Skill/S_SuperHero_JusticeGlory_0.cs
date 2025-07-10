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
    /// <color=#FFC000>Justice ☆ Glory</color>
    /// </summary>
    public class S_SuperHero_JusticeGlory_0 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];
            var phoenix = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x != null && x.Info.KeyData == GDEItemKeys.Character_Phoenix);
            if (target == phoenix && phoenix != null && phoenix.HP <= -150)
            {
                phoenix.Dead(false, false);
            }
            if (target == null) return;
        }
    }
}