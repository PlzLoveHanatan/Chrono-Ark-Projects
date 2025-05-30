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
            if (Utils.AquaVoiceSkills && MySkill?.MySkill != null && BChar.Info.KeyData == ModItemKeys.Character_Aqua)
            {
                Utils.PlaySound(MySkill.MySkill.KeyID);
            }

            var target = Targets[0];

            if (target != null)
            {
                var buffs = target.GetBuffs(BattleChar.GETBUFFTYPE.ALL, false, false);

                foreach (var buff in buffs)
                {
                    buff.SelfDestroy();
                }
            }
        }
        public void ChangeDamageState(SkillParticle SP, BattleChar Target, int DMG, bool Cri, ref bool ToHeal, ref bool ToPain)
        {
            if (Target.Info.Ally && SP.SkillData == MySkill)
            {
                ToHeal = true;
            }
        }
    }
}