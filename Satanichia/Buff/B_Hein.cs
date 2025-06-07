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
namespace Satanichia
{
    public class B_Hein : Buff, IP_SkillUse_Team_Target
    {
        private List<string> DynamicList = new List<string>();
        private bool AdditionalHit;

        public void SkillUseTeam_Target(Skill skill, List<BattleChar> Targets)
        {
            var target = Targets[0];
            DynamicList.Clear();
            AdditionalHit = true;

            if (!skill.FreeUse && !target.Info.Ally && skill.IsDamage && !target.Dummy && !target.IsDead)
            {
                DynamicList.Add(skill.MySkill.KeyID);

                foreach (var t in Targets)
                {
                    BattleSystem.DelayInput(AdditionalAttack(t));
                }
            }
        }

        public IEnumerator AdditionalAttack(BattleChar Target)
        {
            yield return new WaitForSeconds(0.06f);

            if (Target.IsDead || !AdditionalHit) yield break;

            var newSkill = DynamicList[0];

            Skill skill = Skill.TempSkill(newSkill, this.BChar, this.BChar.MyTeam);
            skill.PlusHit = true;

            Skill_Extended skill_Extended = new Skill_Extended();
            skill_Extended.PlusSkillPerFinal.Damage = -60;
            skill.ExtendedAdd(skill_Extended);

            BChar.ParticleOut(skill, Target);

            AdditionalHit = false;

            yield break;
        }
    }
}