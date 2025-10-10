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
    public class B_Abnormality_HistoryLv1_Ashes : Buff, IP_SkillUse_Target
    {
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }

        public override void BuffStat()
        {
            PlusStat.HIT_CC = 10f;
            PlusStat.HIT_DEBUFF = 10f;
            PlusStat.HIT_DOT = 10f;
        }

        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar && !SP.SkillData.PlusHit)
            {
                Utils.PlaySound("Floor_History_Ashes");
                Utils.ApplyBurn(hit, BChar);
            }
        }
    }
}