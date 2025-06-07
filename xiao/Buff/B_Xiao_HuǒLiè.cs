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
namespace Xiao
{
    /// <summary>
    /// Huǒ Liè
    /// </summary>
    public class B_Xiao_HuǒLiè : Buff, IP_SkillUse_Target
    {
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.PlusHit
                && hit != null && !hit.Info.Ally && !hit.Dummy)
            {
                Utils.ApplyBurn(hit, this.BChar, 2);
            }
        }
    }
}