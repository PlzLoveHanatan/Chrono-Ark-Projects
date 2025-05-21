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
namespace Raphi
{
	/// <summary>
	/// When played from hand, discard the bottom skill and draw skills equal to its cost (Max 2).
	/// Create "Judgement Ascension" in hand.  
	/// Sheathe: Restore 1 mana and draw 1 skill, prioritizing Attack skills.
	/// </summary>
    public class JudgementDescent : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

            if (SkillD.FreeUse || SkillD.BasicSkill) return;

            if (SaveManager.NowData.EnableSkins.Any((SkinData v) => v.skinKey == "SummerRaphiel"))
            {
                Skill skill = Skill.TempSkill(ModItemKeys.Skill_JudgementAscension, this.BChar, this.BChar.MyTeam);
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }
            else
            {
                Skill skill1 = Skill.TempSkill(ModItemKeys.Skill_JudgementAscension, this.BChar, this.BChar.MyTeam);
                BattleSystem.instance.AllyTeam.Add(skill1, true);
            }

            List<Skill> list = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill).ToList();

            if (list.Count == 0) return;

            int num = Math.Min(list[list.Count - 1].AP, 2);

            list[list.Count - 1].Delete(false);
            BattleSystem.instance.AllyTeam.Draw(num);
        }
        public override void IlyaWaste()
        {
            BattleSystem.instance.AllyTeam.AP += 1;
            BattleSystem.DelayInput(this.Draw());
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