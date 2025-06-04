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
namespace Raphi
{
    /// <summary>
    /// Heavenly Wrath
    /// </summary>
    public class B_HeavenlyWrath : Buff
    {
        public override void FixedUpdate()
        {
            var allySkills = BattleSystem.instance.AllyTeam.Skills;

            foreach (var skill in allySkills)
            {
                if (skill != null && skill.IsDamage && skill.ExtendedFind<Ex_Raphi_2>() == null)
                {
                    Utils.AddExHeavenlyWrath(skill);
                }
            }
        }
    }
}