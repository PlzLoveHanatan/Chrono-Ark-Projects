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
    /// Gluttony
    /// Applies "Glutton's Mark" on attack.
    /// Against targets with "Glutton's Mark", all user attacks deal 15% more damage and heal the user for 15% of the damage dealt.
    /// </summary>
    public class B_Abnormality_HistoryLv2_Gluttony : Buff, IP_DamageChange_sumoperation, IP_SkillUse_Target
    {
        public override void BuffStat()
        {
            PlusStat.Penetration = 15f;
        }
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }

        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.PlusHit
            && hit != null && !hit.Info.Ally && !hit.Dummy)

            {
                MasterAudio.PlaySound("Gluttony", 100f, null, 0f, null, null, false, false);

                if (hit.BuffFind(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Gluttony_0, false))
                {
                    this.BChar.Heal(this.BChar, (int)(DMG * 0.15f), false, false, null);
                }
                else
                {
                    hit.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Gluttony_0, this.BChar, false, 0, false, -1, false);
                }
            }
        }

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Target.BuffFind(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Gluttony_0, false))
            {
                PlusDamage += (int)(Damage * 0.15f);
            }
        }
    }
}