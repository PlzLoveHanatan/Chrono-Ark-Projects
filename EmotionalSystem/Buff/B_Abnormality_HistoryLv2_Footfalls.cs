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
    /// Footfalls
    /// </summary>
    public class B_Abnormality_HistoryLv2_Footfalls : Buff, IP_SkillUse_User
    {
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && BChar.HP <= BChar.GetStat.maxhp / 2)
            {
                MasterAudio.PlaySound("Cry", 100f, null, 0f, null, null, false, false);

                foreach (var target in Targets)
                {
                    if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                    {
                        int enemyHP = (int)(target.GetStat.maxhp * 0.7f);
                        int maxDamage = Mathf.Min(70, enemyHP);
                        var nonLethalDamage = GDEItemKeys.Buff_B_Momori_P_NoDead;

                        int dealt = target.Damage(this.BChar, maxDamage, false, true, false, 0, false, false, false);

                        Utils.ApplyBurn(target, this.BChar, 5);

                        int selfDamage = dealt / 3;

                        BChar.BuffAdd(nonLethalDamage, this.BChar, false, 0, false, -1, false);
                        this.BChar.Damage(this.BChar, selfDamage, false, true, false, 0, false, false, false);
                        this.BChar.BuffRemove(nonLethalDamage, false);
                    }
                }

                SelfDestroy();
            }
        }
    }
}