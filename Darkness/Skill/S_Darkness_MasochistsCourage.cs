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
using System.Threading;
namespace Darkness
{
    /// <summary>
    /// Masochist's Courage
    /// Take <color=purple>&a Pain Damage</color> and heal all alies by amount.
    /// </summary>
    public class S_Darkness_MasochistsCourage : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
            base.Init();
        }
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.6f)).ToString()).Replace("&b", ((int)(BChar.GetStat.maxhp * 0.2f)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Utils.TryPlayDarknessSound(SkillD, BChar);

            var nonLethalDamage = GDEItemKeys.Buff_B_Momori_P_NoDead;
            int masochistsCourage = (int)(BChar.GetStat.maxhp * 0.6f);
            int barrierValue = (int)(BChar.GetStat.maxhp * 0.6f);
            bool applyBuff = BChar.HP <= barrierValue;

            BChar.BuffAdd(nonLethalDamage, BChar, false, 0, false, -1, false);
            BChar.Damage(BChar, masochistsCourage, false, true, false, 0, false, false, false);
            BChar.BuffAdd(ModItemKeys.Buff_S_Darkness_StubbornKnight, BChar, false, 0, false, -1, false).BarrierHP += barrierValue;
            BChar.BuffRemove(nonLethalDamage, false);

            if (applyBuff)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_Darkness_HurtMeMorePlease, BChar, false, 0, false, -1, false);
                BChar.BuffAdd(ModItemKeys.Buff_S_Darkness_StubbornKnight, BChar, false, 0, false, -1, false).BarrierHP += (int)(BChar.GetStat.maxhp * 0.2f);
            }
        }
    }
}