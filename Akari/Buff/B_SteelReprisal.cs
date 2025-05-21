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
namespace Akari
{
    /// <summary>
    /// Steel Reprisal
    /// </summary>
    public class B_SteelReprisal : Buff, IP_DamageChange_Hit_sumoperation, IP_Dodge
    {
        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && !SkillD.Master.Info.Ally)
            {
                Skill SteelReprisal = Skill.TempSkill(ModItemKeys.Skill_SteelKnuckles_0, BChar, BChar.MyTeam);
                SteelReprisal.FreeUse = true;
                SteelReprisal.PlusHit = true;
                BattleSystem.DelayInput(Wait());
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(SteelReprisal, SkillD.Master, false, false, true, null));
                SelfDestroy(false);
            }
        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == BChar && !SP.UseStatus.Info.Ally)
            {
                Skill SteelReprisal = Skill.TempSkill(ModItemKeys.Skill_SteelKnuckles_0, BChar, BChar.MyTeam);
                SteelReprisal.FreeUse = true;
                SteelReprisal.PlusHit = true;
                BattleSystem.DelayInput(this.Wait());
                BattleSystem.DelayInput(BattleSystem.instance.ForceAction(SteelReprisal, SP.UseStatus, false, false, true, null));
            }
        }
        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.3f);
            yield break;
        }
    }
}