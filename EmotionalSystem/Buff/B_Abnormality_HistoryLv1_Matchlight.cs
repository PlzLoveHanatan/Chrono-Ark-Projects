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
    /// Matchlight
    /// </summary>
    public class B_Abnormality_HistoryLv1_Matchlight : Buff, IP_DamageChange_sumoperation, IP_SkillUse_User, IP_SkillUse_User_After, IP_SkillUse_Target
    {
        private bool SelfDamage;
        private int Matchlight;
        public override string DescExtended()
        {
            return base.DescExtended()
                .Replace("&a", ((int)(float)Matchlight * 20).ToString())
                .Replace("&b", ((int)(float)Matchlight * 10).ToString())
                .Replace("&c", ((int)(this.BChar.GetStat.maxhp * 0.2f * Matchlight)).ToString())
                .Replace("&d", Matchlight.ToString());
        }

        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.PlusHit
                && hit != null && !hit.Info.Ally && !hit.Dummy)
            {
                MasterAudio.PlaySound("Matchlight", 100f, null, 0f, null, null, false, false);

                Utils.ApplyBurn(hit, this.BChar);
            }
        }
        public override void Init()
        {
            base.Init();
            Matchlight = 0;
            SelfDamage = true;
        }
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }

        public virtual void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && SkillD.Master == this.BChar && !SkillD.PlusHit && Matchlight >= 1)
            {
                int chancePercent = Matchlight * 10;

                bool neverLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, chancePercent);

                if (neverLucky)
                {
                    SelfDamage = false;
                }
            }
        }

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD.IsDamage && SkillD.Master == this.BChar && !SkillD.PlusHit && Matchlight >= 1)
            {
                PlusDamage += (int)(Damage * 0.2f * Matchlight);
            }
            else
            {
                PlusDamage = 0;
            }
        }
        public void SkillUseAfter(Skill SkillD)
        {           
            if (SkillD.IsDamage && SkillD.Master == this.BChar && !SkillD.PlusHit && !SelfDamage)
            {
                var nonLethalDamage = GDEItemKeys.Buff_B_Momori_P_NoDead;
                int selfDamage = (int)(BChar.GetStat.maxhp * 0.2f * Matchlight);

                this.BChar.BuffAdd(nonLethalDamage, this.BChar, false, 0, false, -1, false);
                this.BChar.Damage(this.BChar, selfDamage, false, true, false, 0, false, false, false);
                this.BChar.BuffRemove(nonLethalDamage, false);
                SelfDamage = true;
                Matchlight++;
                MasterAudio.PlaySound("Explode", 100f, null, 0f, null, null, false, false);

                //SelfDestroy();
            }
            else if (SkillD.IsDamage && SkillD.Master == this.BChar && !SkillD.PlusHit)
            {
                Matchlight++;
                MasterAudio.PlaySound("NoExplode", 100f, null, 0f, null, null, false, false);
            }

            if (Matchlight > 5)
            {
                Matchlight = 5;
            }
        }
    }
}