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
    /// Spores
    /// </summary>
    public class B_Abnormality_HistoryLv2_Spores : Buff, IP_DamageChange_Hit_sumoperation, IP_Dodge
    {
        public override void BuffStat()
        {
            this.PlusStat.Strength = true;
            this.PlusStat.AggroPer = 50;
            this.PlusStat.def = 15;
            base.BuffStat();
        }
        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && !SkillD.Master.Info.Ally)
            {
                MasterAudio.PlaySound("Spores", 100f, null, 0f, null, null, false, false);

                Utils.ApplyBurn(SkillD.Master, this.BChar, 4);
            }
        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == this.BChar && !SP.UseStatus.Info.Ally)
            {
                MasterAudio.PlaySound("Spores", 100f, null, 0f, null, null, false, false);

                Utils.ApplyBurn(SP.UseStatus, this.BChar, 4);
            }
        }
    }
}