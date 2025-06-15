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
    /// Masochist's Courage
    /// Take <color=purple>&a Pain Damage</color> and heal all alies by amount.
    /// </summary>
    public class S_Darkness_MasochistsCourage : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.maxhp * 0.3)).ToString()).Replace("&b", ((int)(BChar.GetStat.maxhp * 0.4)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int masochistsCourage = (int)(BChar.GetStat.maxhp * 0.3f);
            int barrierValue = (int)(BChar.GetStat.maxhp * 0.4f);
            bool applyBuff = BChar.HP <= barrierValue;

            BChar.Damage(BChar, masochistsCourage, false, true, false, 0, false, false, false);
            foreach (var b in BChar.MyTeam.AliveChars)
            {
                if (b != null && b != BChar)
                {
                    b.Heal(BChar, masochistsCourage, false, false, null);

                    Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_DummyHeal, BChar, BChar.MyTeam);
                    healingParticle.PlusHit = true;
                    healingParticle.FreeUse = true;

                    BChar.ParticleOut(healingParticle, b);
                }
            }

            if (applyBuff)
            {
                BChar.BuffAdd(ModItemKeys.Buff_S_Darkness_StubbornKnight, BChar, false, 0, false, -1, false).BarrierHP += barrierValue;
                foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    e.BuffAdd(ModItemKeys.Buff_B_Darkness_HitMeHarder, BChar, false, 0, false, -1, false);
                }
            }
        }
    }
}