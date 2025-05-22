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
namespace Aqua
{
	/// <summary>
	///  Sacred Laundry Staff
	/// </summary>
    public class E_Aqua_SacredLaundryStaff : EquipBase, IP_SkillUse_Target, IP_PlayerTurn
    {
        public override void Init()
        {
            PlusPerStat.Heal = 20;
            PlusStat.hit = 10;
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == this.BChar && !SP.SkillData.FreeUse
                && hit != null && !hit.Info.Ally && !hit.Dummy)
            {
                hit.BuffAdd(ModItemKeys.Buff_B_Aqua_Drenched, this.BChar, false, 0, false, -1, false);
            }
        }
        public void Turn()
        {
            var allies = BattleSystem.instance.AllyTeam.AliveChars;
            var drenched = ModItemKeys.Buff_B_Aqua_Drenched;

            {
                foreach (var ally in allies)
                {
                    if (ally?.BuffReturn(drenched, false) != null)
                    {
                        ally.BuffRemove(drenched, false);
                    }
                }
            }
        }
    }
}