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
	/// <summary>
	/// Beast's Punchline
	/// </summary>
    public class S_Mia_BeastsPunchline : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            Utils.TryPlayMiaSound(MySkill, BChar);

            BattleSystem.DelayInput(CastSkillOnEnemy());
        }

        private IEnumerator CastSkillOnEnemy()
        {
            List<Skill> skillsInHand = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill).ToList();

            if (skillsInHand.Count <= 0) yield break;

            Skill topSkillInHand = skillsInHand[0];

            if (!topSkillInHand.IsHeal) 
            {
                topSkillInHand.Delete(false);
                yield break;
            }

            Skill cloneSkill = topSkillInHand.CloneSkill(true, null, null, true);

            var allies = BattleSystem.instance.AllyTeam.AliveChars;

            if (allies.Count > 0)
            {
                var allyWithLowestHP = allies.OrderBy(e => e.HP).FirstOrDefault();

                BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(cloneSkill, allyWithLowestHP, false, false, true, null));
            }

            topSkillInHand.Delete(false);

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
            Skill skill = BattleSystem.instance.AllyTeam.Skills_Deck.Find(s => s.IsHeal);

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