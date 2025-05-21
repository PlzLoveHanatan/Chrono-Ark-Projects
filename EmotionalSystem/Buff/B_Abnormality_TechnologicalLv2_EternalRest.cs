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
	/// Eternal Rest
	/// </summary>
    public class B_Abnormality_TechnologicalLv2_EternalRest : Buff, IP_DamageChange_sumoperation, IP_SkillUse_Target
    {
        public override void BuffStat()
        {
            PlusStat.PlusCriDmg = 20f;
        }
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.PlusHit && Target != null && !Target.Info.Ally && !Target.IsDead)
            {
                var debuffs = Target.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false);

                if (debuffs.Count > 0)
                {
                    PlusDamage += (int)(Damage * Math.Min(5, debuffs.Count) * 0.05f);
                }
            }
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.PlusHit
                && hit != null && !hit.Info.Ally && !hit.Dummy && DMG >= hit.GetStat.maxhp / 5)
            {
                hit.BuffAdd(GDEItemKeys.Buff_B_Common_Rest, this.BChar, false, 100, false, -1, false);
            }
        }
    }
}