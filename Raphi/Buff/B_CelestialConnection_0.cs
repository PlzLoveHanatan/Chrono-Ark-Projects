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
	/// Celestial Connection
	/// </summary>
    public class B_CelestialConnection_0 : Buff
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            BuffIcon.AddComponent<Button>().onClick.AddListener(RaphiAllyCall);
        }
        public void RaphiAllyCall()
        {
            if (BChar.GetStat.Stun || !BattleSystem.instance.ActWindow.CanAnyMove) return;

            if (BattleSystem.instance.AllyTeam.Skills.Count == 0 && StackNum >= 1)
            {
                BattleSystem.instance.AllyTeam.Draw();
                SelfStackDestroy();
            }
            else
            {
                BattleSystem.DelayInputAfter(Del());
            }
        }

        private IEnumerator Del()
        {
            yield return new WaitForFixedUpdate();

            List<Skill> skills = BChar.MyTeam.Skills;
            if (skills.Count <= 0) yield break;

            List<Skill> choiceList = new List<Skill> { skills[skills.Count - 1], skills[0] };

            BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(choiceList, SkillButton, ModLocalization.Discard));
        }

        public void SkillButton(SkillButton myButton)
        {
            BattleSystem.DelayInput(Waste(myButton));
            base.SelfStackDestroy();
        }

        private IEnumerator Waste(SkillButton myButton)
        {
            List<Skill> skillInHand = BattleSystem.instance.AllyTeam.Skills;
            int index = skillInHand.IndexOf(myButton.Myskill);

            if (index < 0) yield break;

            int ap = Mathf.Min(myButton.Myskill.AP, 2);
            BattleSystem.DelayInput(DiscardSkill(myButton.Myskill));

            if (BattleSystem.instance.AllyTeam.Skills.Count == 0 || index == skillInHand.Count - 1)
            {
                BattleSystem.instance.AllyTeam.Draw(ap);
            }
            else if (index == 0)
            {
                BattleSystem.instance.AllyTeam.AP += ap;
            }

            yield break;
        }
        public IEnumerator DiscardSkill(Skill selectSkill)
        {
            yield return BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true);
            selectSkill.Delete(false);
            yield return BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true);
            yield break;
        }
    }
}