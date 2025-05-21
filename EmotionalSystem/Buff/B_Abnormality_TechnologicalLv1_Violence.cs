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
	/// Violence
	/// </summary>
    public class B_Abnormality_TechnologicalLv1_Violence : Buff, IP_DamageChange
    {
        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD.IsDamage && SkillD.Master == this.BChar && !SkillD.PlusHit
                && Target != null && !Target.Info.Ally && !Target.Dummy)
            {
                int randomNum = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 4);
                if (randomNum == 1)
                {
                    Damage = (int)(Damage * 1.15f);

                }
                if (randomNum == 2)
                {
                    Damage = (int)(Damage * 1.30f);
                }
                else
                {
                    Damage = (int)(Damage * 0.85f);
                }
            }
            return Damage;
        }
    }
}