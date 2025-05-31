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
namespace Mia
{
    public class S_Mia_FluffyStrike : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            Utils.TryPlayMiaSound(MySkill, BChar);

            BattleSystem.DelayInput(CastSkillOnEnemy());
        }

        private IEnumerator CastSkillOnEnemy()
        {
            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(s => s != MySkill).ToList();

            if (skillsInHand.Count <= 0) yield break;

            int index = skillsInHand.Count - 1;
            Skill bottomSkillInHand = skillsInHand[index];

            if (!bottomSkillInHand.IsDamage)
            {
                bottomSkillInHand.Delete(false);
                yield break;
            }

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
            Utils.TryPlayMiaSound(MySkill, BChar);

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