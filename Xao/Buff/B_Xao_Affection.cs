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
    public class B_Xao_Affection : Buff, IP_BuffAddAfter, IP_Awake
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
                Utils.PopHentaiText(BChar);
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
            string buff = ModItemKeys.Buff_B_Xao_Affection;
            string xao = ModItemKeys.Character_Xao;
            string normalMod = ModItemKeys.Buff_B_Xao_Mod_0;
            string hornyMod = ModItemKeys.Buff_B_Xao_Mod_1;
            if (BuffTaker.Info.KeyData == xao && addedbuff == this)
            {
                Xao_Hearts.HeartsCheck(BChar, 1);
                Utils.PopHentaiText(BChar);

                if (StackNum >= 3 && !FirstTransform)
                {
                    string characterPrefix = ModItemKeys.Character_Xao;
                    SkinData firstSkin = SaveManager.NowData.EnableSkins.FirstOrDefault(v => v.skinKey.StartsWith(characterPrefix));

                    if (!string.IsNullOrEmpty(firstSkin.skinKey))
                    {
                        Xao_Face_Change.ChooseFace(BChar, firstSkin.skinKey);
                    }
                    else
                    {
                        Xao_Face_Change.ChooseFace(BChar, ModItemKeys.Character_Xao);
                    }

                    if (BChar.BuffReturn(ModItemKeys.Buff_B_Xao_Mod_0, false) != null)
                    {
                        BChar.BuffRemove(normalMod);
                        Utils.AddBuff(BChar, hornyMod);
                    }

                    BChar.Overload = 0;
                    FirstTransform = true;
                    if (BChar.Info.Passive is P_Xao passive)
                    {
                        passive.HornyMod = true;
                        BattleSystem.DelayInput(passive.ChangeFix());
                    }
                }
            }
        }

        public void Awake()
        {

        }
    }
}