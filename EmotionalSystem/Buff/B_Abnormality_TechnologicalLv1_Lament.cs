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
namespace EmotionalSystem
{
    /// <summary>
    /// Lament
    /// </summary>
    public class B_Abnormality_TechnologicalLv1_Lament : Buff, IP_PlayerTurn_1, IP_DamageChange_sumoperation, IP_Awake
    {
        public void Awake()
        {
            AddAttackPowerToAllies();
        }

        public void Turn1()
        {
            AddAttackPowerToAllies();
        }

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.PlusHit && Target != null && !Target.Info.Ally && !Target.IsDead)
            {
                if (Target.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Count <= 1)
                {
                    PlusDamage -= (int)(Damage * 0.10f);
                }
            }
        }
        public void AddAttackPowerToAllies()
        {
            if (BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false).Count >= 1)
            {
                foreach (var ally in BChar.MyTeam.AliveChars)
                {
                    ally.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Lament_0, this.BChar, false, 0, false, -1, false);
                }
            }
        }
    }
}