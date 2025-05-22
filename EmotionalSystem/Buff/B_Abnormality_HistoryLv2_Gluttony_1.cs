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
    /// Fairy's Favor
    /// Applies "Glutton's Mark" on attack.
    /// </summary>
    public class B_Abnormality_HistoryLv2_Gluttony_1 : Buff, IP_SkillUse_Target
    {
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.PlusHit
            && hit != null && !hit.Info.Ally && !hit.Dummy)

            {
                MasterAudio.PlaySound("Gluttony_1", 100f, null, 0f, null, null, false, false);

                hit.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv2_Gluttony_0, this.BChar, false, 0, false, -1, false);
            }
        }
    }
}