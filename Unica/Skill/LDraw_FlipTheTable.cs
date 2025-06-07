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
namespace Unica
{
	/// <summary>
	/// Flip The Table
	/// Draw 3 skills and select skill to discard.
	/// </summary>
    public class LDraw_FlipTheTable : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            BattleSystem.DelayInput(DrawAndDiscard());
        }
        public IEnumerator DrawAndDiscard()
        {
            yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._Draw(3));
            yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));

            List<Skill> list = new List<Skill>();
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills.ToList())
            {
                if (skill.MySkill.KeyID != ModItemKeys.Skill_LDraw_FlipTheTable)
                {
                    list.Add(skill);
                }
            }
            if (list.Count >= 1)
            {
                yield return BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.WasteSkill);
            }
        }
        public void Del(SkillButton skillbutton)
        {
            BattleSystem.DelayInput(this.Except(skillbutton));
        }
        public IEnumerator Except(SkillButton skillbutton)
        {
            yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills.ToList())
            {
                if (skill == skillbutton.Myskill)
                {
                    skill.Delete(false);
                }
            }
            yield break;
        }
    }
}