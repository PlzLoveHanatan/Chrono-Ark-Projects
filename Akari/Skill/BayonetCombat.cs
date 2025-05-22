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
namespace Akari
{
    /// <summary>
    /// Bayonet Combat
    /// </summary>
    public class BayonetCombat : Skill_Extended
    {        
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Melee_Normal", 100f, null, 0f, null, null, false, false);

            Utils.CreateRandomAmmunition(BChar, 2);
        }
    }
}