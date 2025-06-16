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
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
namespace Darkness
{
    /// <summary>
    /// Hurt Me More, Please ♡
    /// </summary>
    public class B_Darkness_HurtMeMorePlease : Buff/*, IP_TargetedAlly*/, IP_DamageChange_Hit_sumoperation, IP_Dodge
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(BChar.GetStat.atk * 0.6f)).ToString());
        }

        public override void Init()
        {
            base.Init();
            this.PlusStat.Strength = true;
        }
        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && !SkillD.Master.Info.Ally)
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_HerosParry_0, BChar, BChar.MyTeam);
                skill.FreeUse = true;
                skill.PlusHit = true;
                BattleSystem.DelayInput(Wait());
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, SkillD.Master, false, false, true, null));

                //var text = ModLocalization.HurtMe;
                //BattleSystem.DelayInput(BattleText.InstBattleTextAlly_Co(BChar, text));
                EffectView.TextOutSimple(this.BChar, this.BuffData.Name);
            }
        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == BChar && !SP.UseStatus.Info.Ally)
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_HerosParry_0, BChar, BChar.MyTeam);
                skill.FreeUse = true;
                skill.PlusHit = true;
                BattleSystem.DelayInput(this.Wait());
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, SP.UseStatus, false, false, true, null));
                EffectView.TextOutSimple(this.BChar, this.BuffData.Name);
            }
        }
        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.2f);
            yield break;
        }

        //public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        //{
        //    using (List<BattleChar>.Enumerator enumerator2 = SaveTargets.GetEnumerator())
        //    {
        //        while (enumerator2.MoveNext())
        //        {
        //            if (enumerator2.Current.Info.Ally)
        //            {
        //                Skill skill2 = Skill.TempSkill(ModItemKeys.Skill_S_Darkness_HerosParry_0, base.Usestate_L, base.Usestate_L.MyTeam);
        //                skill2.PlusHit = true;
        //                skill2.FreeUse = true;
        //                this.BChar.ParticleOut(skill2, Attacker);
        //                var text = ModLocalization.HurtMe;
        //                BattleSystem.DelayInput(BattleText.InstBattleTextAlly_Co(BChar, text));
        //                EffectView.TextOutSimple(this.BChar, this.BuffData.Name);
        //                yield return new WaitForSeconds(0.1f);
        //                break;
        //            }
        //        }
        //    }
        //    List<BattleChar>.Enumerator enumerator = default(List<BattleChar>.Enumerator);
        //    yield break;
        //}
    }
}
