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
using System.Net;
using NLog.Targets;
using System.Runtime.InteropServices.WindowsRuntime;
namespace Satanichia
{
    /// <summary>
    /// Devil's Jape
    /// </summary>
    public class S_Satanichia_DevilsJape : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            BattleSystem.DelayInput(CastSkillOnEnemy());
        }

        private IEnumerator CastSkillOnEnemy()
        {
            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill).ToList();

            if (skillsInHand.Count <= 0) yield break;

            int index = skillsInHand.Count - 1;
            Skill bottomSkillInHand = skillsInHand[index];

            if (!bottomSkillInHand.IsDamage) yield break;
            
            Skill cloneSkill = bottomSkillInHand.CloneSkill(true, null, null, true);

            var enemies = BattleSystem.instance.EnemyTeam.AliveChars;

            if (enemies.Count > 0)
            {
                var enemyWithLowestHP = enemies.OrderBy(e => e.HP).FirstOrDefault();

                BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(cloneSkill, enemyWithLowestHP, false, false, true, null));
            }

            bottomSkillInHand.Delete(false);

            yield break;
        }

        public override void IlyaWaste()
        {
            BattleSystem.instance.AllyTeam.AP += 1;
            BattleSystem.DelayInput(Draw());
        }

        public IEnumerator Draw()
        {
            Skill skill = BattleSystem.instance.AllyTeam.Skills_Deck.Find(s => s.IsDamage);
            if (skill == null)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }
            else
            {
                yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(skill, null));
            }

            yield return null;
            yield break;
        }
    }
}