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
    public class B_EnemyAbnormality_MirrorAdjustment : Buff, IP_Hit, IP_PlayerTurn, IP_Awake
    {
        private bool ReflectedDamage;

		public void Awake()
		{
			Utils.AddBuff(BChar, ModItemKeys.Buff_B_EnemyAbnormality_MirrorAdjustment_0);
		}

		public override string DescExtended()
        {
            string text = ReflectedDamage ? "Inactive" : "Active";
            return base.DescExtended().Replace("&a", text.ToString());
        }

        public void Turn()
        {
            ReflectedDamage = false;
			Utils.AddBuff(BChar, ModItemKeys.Buff_B_EnemyAbnormality_MirrorAdjustment_0);
		}

        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if (!SP.UseStatus.IsLucy && !SP.UseStatus.Dummy && Dmg > 1 && !SP.SkillData.PlusHit && !ReflectedDamage)
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Buff_MirrorAdjustment, BChar, BChar.MyTeam);

                int damage = (int)(Dmg * 0.5);

                SP.UseStatus.Damage(SP.UseStatus, damage, false, true, false, 0, false, false, false);
                BChar.ParticleOut(skill, SP.UseStatus);
                Utils.RemoveBuff(BChar, ModItemKeys.Buff_B_EnemyAbnormality_MirrorAdjustment_0, true);
                ReflectedDamage = true;
            }
        }

		
	}
}