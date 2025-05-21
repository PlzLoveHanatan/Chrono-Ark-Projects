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
using NLog.Targets;
namespace EmotionalSystem
{
	/// <summary>
	/// Mirror Adjustment
	/// </summary>
    public class B_EnemyAbnormality_MirrorAdjustment : Buff, IP_Hit, IP_PlayerTurn
    {
        private int ReflectedDamage;

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ReflectedDamage.ToString());
        }
        public void Turn()
        {
            ReflectedDamage = 0;
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if (!SP.UseStatus.IsLucy && !SP.UseStatus.Dummy && Dmg > 1 && !SP.SkillData.PlusHit && ReflectedDamage < 1)
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Buff_MirrorAdjustment, BChar, BChar.MyTeam);

                var nonLethalDamage = GDEItemKeys.Buff_B_Momori_P_NoDead;
                int Damage = (int)(Dmg * 0.4f);

                SP.UseStatus.BuffAdd(nonLethalDamage, SP.UseStatus, false, 0, false, -1, false);
                SP.UseStatus.Damage(SP.UseStatus, Damage, false, true, false, 0, false, false, false);
                SP.UseStatus.BuffRemove(nonLethalDamage, false);

                this.BChar.ParticleOut(skill, SP.UseStatus);
                ReflectedDamage++;
            }
        }
    }
}