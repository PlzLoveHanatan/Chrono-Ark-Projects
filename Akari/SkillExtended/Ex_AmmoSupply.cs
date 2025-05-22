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
	/// Create 2 random Ammunition in hand.
	/// </summary>
    public class Ex_AmmoSupply : Skill_Extended
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            PlusSkillPerFinal.Damage = 15;

            MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);

            Utils.CreateRandomAmmunition(BChar, 2);
        }
    }
}