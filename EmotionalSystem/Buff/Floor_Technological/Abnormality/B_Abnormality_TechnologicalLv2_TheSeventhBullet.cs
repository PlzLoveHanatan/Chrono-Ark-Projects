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
using NLog.Targets;
namespace EmotionalSystem
{
    /// <summary>
    /// The Seventh Bullet
    /// </summary>
    public class B_Abnormality_TechnologicalLv2_TheSeventhBullet : Buff, IP_SkillUse_User
    {
        private int AttackUses;

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", AttackUses.ToString());
        }

        public override void BuffStat()
        {
            PlusPerStat.Damage = 40;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse)
            {
                Utils.PlaySound("Floor_Technological_TheSeventhBullet");

                AttackUses++;

                if (AttackUses == 2)
                {
                    Utils.AddBuff(BChar, BChar, ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet_0);
                }

                if (AttackUses >= 3)
                {
                    EmotionalSystem_Scripts.AttackRedirect(BChar, SkillD, Targets, SkillD.TargetDamage);
                    Utils.RemoveBuff(BChar, ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet_0, true);
					AttackUses = 0;
				}
            }
        }
    }
}