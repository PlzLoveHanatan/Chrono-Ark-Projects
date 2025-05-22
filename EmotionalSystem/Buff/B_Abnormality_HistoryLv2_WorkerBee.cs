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
	/// Worker Bee
	/// </summary>
    public class B_Abnormality_HistoryLv2_WorkerBee : Buff, IP_DamageChange_Hit_sumoperation, IP_Dodge
    {
        public override void BuffStat()
        {
            base.BuffStat();
            PlusStat.def = -10;
            //this.PlusStat.DMGTaken = 10f;
        }
        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && !SkillD.Master.Info.Ally)
            {
                MasterAudio.PlaySound("WorkerBee", 100f, null, 0f, null, null, false, false);

                SkillD.Master.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_WorkerBee_0, this.BChar, false, 0, false, -1, false);
            }
        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == this.BChar && !SP.UseStatus.Info.Ally)
            {
                MasterAudio.PlaySound("WorkerBee", 100f, null, 0f, null, null, false, false);

                SP.UseStatus.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_WorkerBee_0, this.BChar, false, 0, false, -1, false);
            }
        }
    }
}