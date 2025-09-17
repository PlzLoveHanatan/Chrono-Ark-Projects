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
        public override void Init()
        {
            PlusSkillPerFinal.Damage = 30;
            PlusSkillPerFinal.Heal = 30;
        }

        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 2 && MainSkill.IsDamage || MainSkill.IsHeal;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            PlusSkillPerFinal.Damage = 30;
            PlusSkillPerFinal.Heal = 30;

            BattleSystem.instance.AllyTeam.Draw();

            MasterAudio.PlaySound("Gun_Reload1", 100f, null, 0f, null, null, false, false);

            Utils.CreateRandomAmmunition(BChar, 2);
        }
    }
}