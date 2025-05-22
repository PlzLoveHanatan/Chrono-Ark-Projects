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
    public class B_Abnormality_TechnologicalLv1_MetallicRinging : Buff, IP_SkillUse_Target
    {
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }
        public override void BuffStat()
        {
            this.PlusStat.HIT_CC = 10f;
            this.PlusStat.HIT_DEBUFF = 10f;
            this.PlusStat.HIT_DOT = 10f;
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.PlusHit
                && hit != null && !hit.Info.Ally && !hit.Dummy)
            {
                SelfStackDestroy();
                hit.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv1_MetallicRinging_0, this.BChar, false, 0, false, -1, false);
            }
        }
    }
}