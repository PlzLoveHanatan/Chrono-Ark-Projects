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
namespace Darkness
{
    /// <summary>
    /// Pleasure Piercer ♡
    /// </summary>
    public class Equip_Darkness_PleasurePiercer : EquipBase, IP_SkillUse_User
    {
        public override void Init()
        {
            OnePassive = true;
            PlusPerStat.Damage = 30;
            PlusStat.hit = -30;
            PlusStat.def = 5;
            PlusStat.PlusCriDmg = 5;
            PlusStat.HIT_DEBUFF = 20f;
            PlusStat.HIT_DOT = 20f;
            PlusStat.HIT_CC = 20f;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            var buff = ModItemKeys.Buff_B_Darkness_HitMeHarder;
            foreach (var target in Targets)
            {
                if (!target.Info.Ally)
                {
                    target.BuffAdd(buff, BChar, false, 0, false, -1, false);
                }
            }

            if (SkillD.Master == BChar && SkillD.IsDamage && BChar.BarrierHP >= 15)
                SkillD.MySkill.NODOD = true;
        }
    }
}