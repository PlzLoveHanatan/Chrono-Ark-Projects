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
    /// Liè Yàn Xīn Lián
    /// </summary>
    public class B_XiaoLOR_LièYànXīnLián : Buff, IP_SkillUse_Target
    {
        public override void BuffStat()
        {
            PlusStat.cri = 15;
            PlusStat.hit = 15;
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (!hit.Info.Ally && hit.HP >= 1 && SP.SkillData.IsDamage == Cri)
            {
                Utils.ApplyBurn(hit, this.BChar, 2);
            }
        }
    }
}