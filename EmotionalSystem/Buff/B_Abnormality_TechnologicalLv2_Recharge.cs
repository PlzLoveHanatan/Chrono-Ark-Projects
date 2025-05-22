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
    public class B_Abnormality_TechnologicalLv2_Recharge : Buff, IP_Healed, IP_PlayerTurn, IP_SkillUse_Target, IP_Awake
    {
        private int HealedThisTurn;
        private bool FirstKill;
        public override void BuffStat()
        {
            PlusStat.atk = 0;
            PlusStat.HEALTaken = 20;
        }
        public void Awake()
        {
            FirstKill = true;
        }
        public void Turn()
        {
            HealedThisTurn = 0;
            FirstKill = true;
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar
                && hit != null && !hit.Info.Ally && !hit.Dummy /*&& SP.SkillData.MySkill.Target.Key == GDEItemKeys.s_targettype_enemy*/ && FirstKill)
            {
                if (hit.IsDead)
                {
                    BattleSystem.instance.AllyTeam.AP += 1;
                    FirstKill = false;
                }
            }
        }
        public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            if (HealedChar == this.BChar && HealedThisTurn < 3)
            {
                PlusStat.atk += 1;
                HealedThisTurn++;
            }
        }
    }
}