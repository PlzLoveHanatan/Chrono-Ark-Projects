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
using EmotionalSystem;
namespace XiaoLOR
{
    /// <summary>
    /// A Fighter that Never Retreats
    /// All attacks inflict 1 <color=#f8181c>Burn</color>.
    /// When attacked, inflict 1 <color=#f8181c>Burn</color> to the attacker.
    /// At Emotional Level 3 or higher inflict 1 additional <color=#f8181c>Burn</color>. 
    /// </summary>
    public class B_XiaoLOR_AFighterthatNeverRetreats : Buff, IP_SkillUse_Target, IP_DamageChange_Hit_sumoperation, IP_Dodge
    {
        private int MaxBurn;
        public override void Init()
        {
            MaxBurn = 0;
        }
        public override void BuffStat()
        {
            PlusPerStat.Damage = 15;
            PlusStat.def = 15;
            //PlusStat.DMGTaken = -30f;            
            //PlusStat.RES_DOT = 30f;
            //PlusStat.RES_CC = 30f;
            //PlusStat.RES_DEBUFF = 30f;
            //PlusStat.DeadImmune = 30;
            PlusStat.Strength = true;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.Usestate_L.IsDead)
            {
                base.SelfDestroy(false);
            }
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (!hit.Info.Ally && hit.HP >= 1 && SP.SkillData.IsDamage && !SP.SkillData.PlusHit && MaxBurn < 5)
            {
                Utils.ApplyBurn(hit, this.BChar);

                if (BChar.EmotionLevel() >= 3)
                {
                    Utils.ApplyBurn(hit, this.BChar);
                }

                MaxBurn++;
            }
        }
        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && !SkillD.Master.Info.Ally)
            {
                Utils.ApplyBurn(SkillD.Master, this.BChar);

                if (BChar.EmotionLevel() >= 3)
                {
                    Utils.ApplyBurn(SkillD.Master, this.BChar);
                }
            }
        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == this.BChar && !SP.UseStatus.Info.Ally)
            {
                Utils.ApplyBurn(SP.UseStatus, this.BChar);

                if (BChar.EmotionLevel() >= 3)
                {
                    Utils.ApplyBurn(SP.UseStatus, this.BChar);
                }
            }
        }
    }
}