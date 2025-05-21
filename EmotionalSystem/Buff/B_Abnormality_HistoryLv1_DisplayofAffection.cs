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
	/// Gain Ignore Taunt on all skills.
	/// Deal 30% increased damage to enemies with only 1 or 2 Actions Counts.
	/// Otherwise, deal 15% less.
	/// </summary>
    public class B_Abnormality_HistoryLv1_DisplayofAffection : Buff, IP_DamageChange_sumoperation, IP_SkillUse_User
    {
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }
        public override void BuffStat()
        {
            PlusStat.IgnoreTaunt = true;
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && !SkillD.PlusHit && SkillD.Master == this.BChar)
            {
                MasterAudio.PlaySound("DisplayAffection", 100f, null, 0f, null, null, false, false);
            }
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && SkillD.IsDamage && !SkillD.PlusHit && SkillD.Master == this.BChar && Target != null && !Target.Info.Ally && !Target.Dummy)
            {
                if (Target is BattleEnemy enemy && enemy.SkillQueue.Count > 0)
                {
                    int castSpeed = enemy.SkillQueue[0].CastSpeed;

                    if (castSpeed <= 2 || castSpeed >= 9)
                    {
                        PlusDamage = (int)(0.15f * Damage);
                    }                    
                }
                else
                {
                    PlusDamage = -(int)(0.15f * Damage);
                }
            }
        }
    }
}