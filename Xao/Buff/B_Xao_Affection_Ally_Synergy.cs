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
    public class B_Xao_Affection_Ally_Synergy : Buff, IP_BuffAddAfter, IP_SkillUse_User
    {
        private List<Skill> DynamicList = new List<Skill>();

        private readonly List<string> ChoiceSkill = new List<string>
        {
            ModItemKeys.Skill_S_Xao_B_Affection_0,
        };

        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            BuffIcon.AddComponent<Button>().onClick.AddListener(AllyCallSynergy);
        }

        public void AllyCallSynergy()
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
                Xao_Hearts.HeartsCheckAllySynergy(BChar, -1);
                Xao_Hearts.DestroyHeartAllySynergy();
                Utils.AllyHentaiText(BChar);
                Utils.PlayXaoSound("Xao_Affection_0");
            }
            else
            {

            }
        }

        public override void BuffStat()
        {
            PlusStat.cri = 3 * StackNum;
            PlusStat.dod = 3 * StackNum;
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker == BChar.Info.Ally && addedbuff == this)
            {
                Xao_Hearts.HeartsCheckAllySynergy(BChar, 1);
            }
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                Utils.AllyHentaiText(BChar);
            }
        }
    }
}