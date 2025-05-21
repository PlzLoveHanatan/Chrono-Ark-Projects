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
namespace Raphi
{
	/// <summary>
	/// Wings of Glory
	/// Whenever critical occur gain buff
	/// </summary>
    public class WingsofGlory : EquipBase, IP_SkillUse_Target, IP_PlayerTurn
    {
        private int CriticalHit;

        public override void Init()
        {
            PlusStat.atk = 3f;
            PlusStat.hit = 5f;
            PlusStat.cri = 5f;
            base.Init();
        }
        
        public void Turn()
        {
            CriticalHit = 0;
        }

        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (DMG >= 1 && Cri && CriticalHit < 1 && SP.LastHit && SP.SkillData.IsDamage)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_HeavensTouch, BChar, false, 0, false, -1, false);
                CriticalHit++;
            }
        }
    }
}