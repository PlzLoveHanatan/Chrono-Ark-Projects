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
using EmotionalSystem;
namespace Xiao
{
    /// <summary>
    /// Fervid Emotions
    /// </summary>
    public class S_XiaoLv2_FervidEmotions : Skill_Extended
    {
        //public override string DescExtended(string desc)
        //{
        //    if (BattleSystem.instance != null)
        //    {
        //        if (BChar.EmotionLevel() >= 2)
        //        {
        //            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.2f) * BChar.EmotionLevel()).ToString());
        //        }
        //        else if (BChar.EmotionLevel() >= 4)
        //        {
        //            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.4f)).ToString());
        //        }
        //    }

        //    return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.2f)).ToString());
        //}
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Fervid", 100f, null, 0f, null, null, false, false);

            foreach (var target in Targets)
            {
                if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                {
                    Utils.ApplyBurn(target, this.BChar, 4);
                    BChar.GetNegEmotion(target.GetPosUI());

                    var burnStacks = target.BuffReturn(EmotionalSystem.ModItemKeys.Buff_B_Xiao_Burn, false) as B_Xiao_Burn;

                    if (burnStacks?.Burn < 8 || burnStacks?.Burn == 0)
                    {
                        Utils.ApplyBurn(target, this.BChar, 4);
                    }

                    if (BChar.EmotionLevel() >= 2)
                    {
                        Utils.ApplyBurn(target, this.BChar, 2);
                    }
                }
            }          
        }

        //public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        //{
        //    if (BChar.EmotionLevel() >= 2 && SkillD.MySkill.KeyID == ModItemKeys.Skill_S_XiaoLv2_FervidEmotions)
        //    {
        //        PlusDamage += (int)(this.BChar.GetStat.atk * 0.2f);
        //    }

        //    else if (BChar.EmotionLevel() >= 4 && SkillD.MySkill.KeyID == ModItemKeys.Skill_S_XiaoLv2_FervidEmotions)
        //    {
        //        PlusDamage += (int)(this.BChar.GetStat.atk * 0.4f);
        //    }
        //}
    }
}