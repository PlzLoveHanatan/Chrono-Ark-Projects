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
	/// Aqua-Grade Purification
	/// </summary>
    public class S_Aqua_AquaGradePurification : Skill_Extended, IP_ChangeDamageState
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            var target = Targets[0];

            //if (target != null && target is BattleEnemy enemy)
            //{
            //    int healingDamage = (int)(BChar.GetStat.reg * 1.2f);
            //    enemy.Damage(this.BChar, healingDamage, false, true, false, 0, false, false, false);
            //}

            if (target != null)
            {
                var buffs = target.GetBuffs(BattleChar.GETBUFFTYPE.BUFF, true, false);
                var debuffs = target.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false);

                foreach (var buff in buffs)
                {
                    target.BuffRemove(buff.BuffData.Key, false);
                }

                foreach (var debuff in debuffs)
                {
                    target.BuffRemove(debuff.BuffData.Key, false);
                }
            }
        }
        public void ChangeDamageState(SkillParticle SP, BattleChar Target, int DMG, bool Cri, ref bool ToHeal, ref bool ToPain)
        {
            if (Target.Info.Ally && SP.SkillData == this.MySkill)
            {
                ToHeal = true;
            }
        }
    }
}