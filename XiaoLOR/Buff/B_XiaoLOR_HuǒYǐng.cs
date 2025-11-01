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
using EmotionSystem;
using NLog.Targets;
namespace XiaoLOR
{
	/// <summary>
	/// Huǒ Yǐng
	/// </summary>
    public class B_XiaoLOR_HuǒYǐng : Buff, IP_SkillUse_Team_Target
    {
        public void SkillUseTeam_Target(Skill skill, List<BattleChar> Targets)
        {
            var target = Targets[0];

            if (!skill.FreeUse && !target.Info.Ally && skill.IsDamage && skill.Master == BChar && !target.Dummy && !target.IsDead)
            {
                foreach (BattleChar t in Targets)
                {                  
                    BattleSystem.DelayInput(AdditionalAttack(t));
                }
            }
        }

        public IEnumerator AdditionalAttack(BattleChar Target)
        {
            yield return new WaitForSeconds(0.06f);

            if (Target.IsDead || StackNum == 0) yield break;

            Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_XiaoLORUnique_YǐngYàn, this.BChar, this.BChar.MyTeam);
            skill.PlusHit = true;

            BChar.ParticleOut(skill, Target);

            Utils.ApplyBurn(Target, BChar);

            if (BChar.EmotionLevel() >= 4)
            {
                Utils.ApplyBurn(Target, BChar);
            }

            SelfStackDestroy();
            yield break;
        }
    }
}