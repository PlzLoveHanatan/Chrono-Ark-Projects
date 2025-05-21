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
    /// Clean
    /// </summary>
    public class B_Abnormality_TechnologicalLv2_Clean : Buff, IP_DamageChange_sumoperation, IP_DealDamage, IP_PlayerTurn, IP_Awake
    {
        public int Critical;
        private bool ManaRestore;
        private bool FirstManaRestore;

        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", Critical.ToString());
        }
        public void Awake()
        {
            FirstManaRestore = true;
            ManaRestore = true;
        }
        public void Turn()
        {
            Critical = 0;
            FirstManaRestore = true;
            ManaRestore = true;
        }
        public override void BuffStat()
        {
            PlusStat.cri = 20f;
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && SkillD.IsDamage && !SkillD.PlusHit && SkillD.Master == this.BChar && Target != null && !Target.Info.Ally && !Target.Dummy)
            {
                if (Target is BattleEnemy enemy && (enemy.SkillQueue.Count >= 9 || enemy.SkillQueue.Count <= 0))
                {
                    PlusDamage = (int)(0.25f * Damage);
                }
            }
        }
        public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
        {
            if (IsCri)
            {
                Critical++;

                if (!ManaRestore) return;

                //if (Critical >= 2 && FirstManaRestore)
                //{
                //    BattleSystem.instance.AllyTeam.AP += 1;
                //    FirstManaRestore = false;
                //}
                if (Critical >= 1)
                {
                    BattleSystem.instance.AllyTeam.AP += 1;
                    ManaRestore = false;
                }
            }
        }
    }
}