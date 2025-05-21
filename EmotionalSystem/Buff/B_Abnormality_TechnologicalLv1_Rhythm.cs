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
	/// Rhythm 
	/// </summary>
    public class B_Abnormality_TechnologicalLv1_Rhythm : Buff, IP_SkillUse_User, IP_SkillUse_Target
    {
        private int Rhythm;
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", Rhythm.ToString());
        }
        public override void Init()
        {
            base.Init();
            Rhythm = 0;
        }
        public override void BuffStat()
        {
            PlusPerStat.Damage = Rhythm * 4;
            PlusStat.DMGTaken = Rhythm * 4;
        }
       
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.PlusHit && Rhythm < 5)
            {
                PlusPerStat.Damage += 4;
                PlusStat.DMGTaken += 4;
                Rhythm++;
            }
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.PlusHit
                && hit != null && !hit.Info.Ally && !hit.Dummy)
            {
                hit.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_Rhythm_0, this.BChar, false, 0, false, -1, false);
            }
        }
        
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }
    }
}