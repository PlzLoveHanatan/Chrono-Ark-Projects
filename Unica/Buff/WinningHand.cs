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
namespace Unica
{
    /// <summary>
    /// Winning Hand
    /// </summary>
    public class WinningHand : Buff, IP_SkillUse_User, IP_DamageChange_sumoperation
    {
        public override void Init()
        {
            base.Init();
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            this.Damage++;
        }        
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (this.Damage >= 1)
            {
                PlusDamage = (int)(Damage * 0.3f);
                SelfStackDestroy();
            }
        }
        private int Damage = 0;
    }
}
    
