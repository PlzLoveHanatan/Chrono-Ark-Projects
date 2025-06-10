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
using System.Security.Cryptography.X509Certificates;
namespace XiaoLOR
{
    /// <summary>
    /// Furious Fire Rending the Skies
    /// </summary>
    public class B_XiaoLOR_FuriousFireRendingtheSkies : Buff, IP_PlayerTurn_1
    {
        private int EnhancedAttacks;
        public void Turn1()
        {
            EnhancedAttacks = 0;

            if (EnhancedAttacks >= 2) return;

            var attackSkills = BattleSystem.instance.AllyTeam.Skills.Where(s => s.IsDamage);

            if (attackSkills != null)
            {
                foreach (var skill in attackSkills)
                {
                    //if (skill.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_XiaoLOR_FuriousFireRendingtheSkies) == null)
                    {
                        skill.ExtendedAdd(ModItemKeys.SkillExtended_Ex_XiaoLOR_FuriousFireRendingtheSkies);
                        EnhancedAttacks++;

                        if (EnhancedAttacks >= 2) break;
                    }
                }
            }
        }
    }
}