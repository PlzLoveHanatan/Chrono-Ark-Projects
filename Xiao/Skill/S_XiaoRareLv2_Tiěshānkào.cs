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
using ChronoArkMod.ModEditor;
using EmotionalSystem;
namespace Xiao
{
    /// <summary>
    /// Tiěshānkào
    /// Inflict 3 + &a <color=#f8181c>Burn</color>, equal (Emotional Level * 2).
    /// </summary>
    public class S_XiaoRareLv2_Tiěshānkào : Skill_Extended, IP_DamageChange_sumoperation
    {
        public override string DescExtended(string desc)
        {
            if (BattleSystem.instance != null)
            {
                if (BChar.EmotionLevel() >= 1)
                {
                    return base.DescExtended(desc).Replace("&a", (BChar.EmotionLevel() * 3).ToString());
                }
            }
            return base.DescExtended(desc).Replace("&a", 3.ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Stab", 100f, null, 0f, null, null, false, false);

            BattleSystem.DelayInput(this.ApplyBurn(Targets[0]));
        }
        public IEnumerator ApplyBurn(BattleChar Target)
        {
            yield return new WaitForSecondsRealtime(0.2f);

            if (Target.IsDead) yield break;

            Utils.ApplyBurn(Target, this.BChar, 3);

            if (BChar.EmotionLevel() >= 1)
            {
                Utils.ApplyBurn(Target, this.BChar, BChar.EmotionLevel() * 3);
            }

            yield break;
        }

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            var burnStacks = Target.BuffReturn(EmotionalSystem.ModItemKeys.Buff_B_Xiao_Burn, false) as B_Xiao_Burn;
            int burn = 0;

            if (burnStacks != null)
            {
                burn = burnStacks.Burn;
            }

            if (BChar.EmotionLevel() >= 3 && SkillD.Master == BChar && SkillD.MySkill.KeyID == ModItemKeys.Skill_S_XiaoRareLv2_Tiěshānkào)
            {
                PlusDamage += burn * 2;
            }
        }
    }
}