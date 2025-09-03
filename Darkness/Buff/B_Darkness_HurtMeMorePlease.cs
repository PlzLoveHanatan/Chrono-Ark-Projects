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
    public class B_Darkness_HurtMeMorePlease : Buff, IP_DamageChange_Hit_sumoperation, IP_Dodge, IP_BuffAddAfter
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(BChar.GetStat.atk * 0.4f)).ToString());
        }

        public override void Init()
        {
            base.Init();
            PlusStat.Strength = true;
            PlusStat.AggroPer = 70;
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

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker.Info.KeyData != ModItemKeys.Character_Darkness && addedbuff == this)
            {
                SelfDestroy();
            }
        }
    }
}
