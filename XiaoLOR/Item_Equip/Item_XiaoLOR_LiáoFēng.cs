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
using EmotionSystem;
namespace XiaoLOR
{
	/// <summary>
	/// Liáo Fēng
	/// </summary>
    public class Item_XiaoLOR_LiáoFēng : EquipBase, IP_SkillUse_Target
    {
        public override void Init()
        {
            this.PlusStat.atk = 2;
            this.PlusStat.hit = 5f;
            this.PlusStat.cri = 25f;
            this.PlusStat.HIT_DOT = 20f;
            base.Init();
        }

        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.FreeUse && hit != null && !hit.Info.Ally && !hit.Dummy)
            {
                Utils.ApplyBurn(hit, BChar);

                if (BChar.EmotionLevel() >= 3)
                {
                    Utils.ApplyBurn(hit, BChar, 2);
                }
            }
        }
    }
}