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
namespace Xao
{
    /// <summary>
    /// Affection
    /// </summary>
    public class B_Xao_Affection : Buff, IP_BuffAddAfter
    {
        public bool FirstTransform;

        private List<Skill> DynamicList = new List<Skill>();

        private readonly List<string> ChoiceSkill = new List<string>
        {
            ModItemKeys.Skill_S_Xao_B_Affection_0,
        };

        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            BuffIcon.AddComponent<Button>().onClick.AddListener(XaoCall);
        }

        public void XaoCall()
        {
            if (BChar.GetStat.Stun || !BattleSystem.instance.ActWindow.CanAnyMove) return;

            BattleSystem.DelayInputAfter(Selection());
        }

        private IEnumerator Selection()
        {
            yield return null;

            DynamicList.Clear();

            foreach (var key in ChoiceSkill)
            {
                var skill = Skill.TempSkill(key, BChar, BChar.MyTeam);
                if (skill == null || skill.MySkill == null) continue;
                DynamicList.Add(skill);
            }
            if (DynamicList.Count == 0) yield break;

            BattleSystem.DelayInput(
                BattleSystem.I_OtherSkillSelect(
                    DynamicList,
                    SkillButton,
                    ScriptLocalization.System_SkillSelect.EffectSelect,
                    true, false
                )
            );
        }

        public void SkillButton(SkillButton myButton)
        {
            if (myButton == null || myButton.Myskill == null || myButton.Myskill.MySkill == null) return;

            string key = myButton.Myskill.MySkill.KeyID;

            //overload = key == ModItemKeys.Skill_S_Xao_B_Affection_0 ? 0 : overload;

            if (key == ModItemKeys.Skill_S_Xao_B_Affection_0)
            {
                BChar.Overload = 0;
                SelfStackDestroy();
                Xao_Hearts.HeartsCheck(BChar, -1);
            }
            else
            {

            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var buff = ModItemKeys.Buff_B_Xao_Affection;
            var xao = ModItemKeys.Character_Xao;

            if (BuffTaker.Info.KeyData == xao && addedbuff.BuffData.Key == buff && addedbuff.StackNum >= 3 && !FirstTransform)
            {
                ChangeFix();
                FirstTransform = true;

                if (BChar.Info.Passive is P_Xao passive)
                {
                    passive.SkillChange = true;
                }
            }
        }

        public IEnumerator ChangeFix()
        {
            yield return null;
            var team = BattleSystem.instance.AllyTeam;
            var fixedSkill = (BChar as BattleAlly)?.MyBasicSkill?.buttonData;

            if (fixedSkill?.MySkill != null && Utils.XaoSkillList.TryGetValue(fixedSkill.MySkill.KeyID, out var newSkillID2))
            {
                var newSkill = Skill.TempSkill(newSkillID2, fixedSkill.Master, fixedSkill.Master.MyTeam);
                Skill refillSkill = newSkill.CloneSkill();
                (BChar as BattleAlly).MyBasicSkill.SkillInput(refillSkill);
                (BChar as BattleAlly).BattleBasicskillRefill = refillSkill;
                (BChar as BattleAlly).BasicSkill = refillSkill;
                int ind = BChar.MyTeam.Chars.IndexOf(BChar);
                if (ind >= 0)
                {
                    BChar.MyTeam.Skills_Basic[ind] = refillSkill;
                }
            }

            var allSkillsToChange = team.Skills
                .Concat(team.Skills_Deck)
                .Concat(team.Skills_UsedDeck)
                .Where(skill => skill?.MySkill != null && Utils.XaoSkillList.ContainsKey(skill.MySkill.KeyID))
                .ToList();

            foreach (Skill skill in allSkillsToChange)
            {
                if (Utils.XaoSkillList.TryGetValue(skill.MySkill.KeyID, out var newSkillID))
                {
                    var newSkill = Skill.TempSkill(newSkillID, skill.Master, skill.Master.MyTeam);
                    skill.SkillChange(newSkill);
                }
            }
            yield break;
        }
    }
}