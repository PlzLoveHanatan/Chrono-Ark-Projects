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
namespace EmotionalSystem
{
    public class B_Abnormality_HistoryLv3_BarrierofThorns : Buff, IP_Dodge, IP_DamageChange_Hit_sumoperation
    {
        public override void BuffStat()
        {
            PlusStat.def = 15;
            PlusStat.DeadImmune = 15;
        }
        //public override string DescExtended()
        //{
        //    return base.DescExtended()
        //        .Replace("&a", ((int)(this.BChar.GetStat.maxhp)).ToString());
        //}
        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && !SkillD.Master.Info.Ally)
            {
                MasterAudio.PlaySound("BarrierThorns", 100f, null, 0f, null, null, false, false);

                SkillD.Master.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv3_BarrierofThorns_0, this.BChar, false, 0, false, -1, false);
                //Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Abnormality_HistoryLv3_BarrierofThorns_Pos_0, this.BChar, this.BChar.MyTeam);
                //skill.FreeUse = true;
                //skill.PlusHit = true;
                //BattleSystem.DelayInput(this.Wait());
                //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, SkillD.Master, false, false, true, null));
            }
        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == this.BChar && !SP.UseStatus.Info.Ally)
            {
                MasterAudio.PlaySound("BarrierThorns", 100f, null, 0f, null, null, false, false);

                SP.UseStatus.BuffAdd(ModItemKeys.Buff_B_Abnormality_HistoryLv3_BarrierofThorns_0, this.BChar, false, 0, false, -1, false);
                //Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Abnormality_HistoryLv3_BarrierofThorns_Pos_0, this.BChar, this.BChar.MyTeam);
                //skill.FreeUse = true;
                //skill.PlusHit = true;
                //BattleSystem.DelayInput(this.Wait());
                //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, SP.UseStatus, false, false, true, null));
            }
        }
        //public IEnumerator Wait()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    yield break;
        //}
    }
}