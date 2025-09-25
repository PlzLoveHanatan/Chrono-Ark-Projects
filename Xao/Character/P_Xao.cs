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
using Spine.Unity.Examples;
using System.Web.Compilation;
using static CharacterDocument;
namespace Xao
{
    /// <summary>
    /// Xao
    /// Passive:
    /// </summary>
    public class P_Xao : Passive_Char, IP_SkillUse_User, IP_DamageTake, IP_Healed, IP_SomeOneDead, IP_SkillUse_User_After, IP_Draw, IP_PlayerTurn, IP_SkillUse_BasicSkill, IP_SkillUseHand_Team, IP_SkillUseHand_Basic_Team
    {
        public bool HornyMod = false;

        public override void Init()
        {
            OnePassive = true;
        }

        public void Turn()
        {
            if (HornyMod && BChar.BuffReturn(ModItemKeys.Buff_B_Xao_Affection, false) is B_Xao_Affection affection && affection?.StackNum >= 3)
            {
                int randomIndex = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, Utils.XaoRandomSkill.Count);
                var randomSkill = Utils.XaoRandomSkill[randomIndex];
                Utils.CreateSkill(randomSkill, BChar, true, true, 1, 0, true);
                Utils.PopHentaiText(BChar);
            }
            else
            {
                Utils.AddBuff(BChar, ModItemKeys.Buff_B_Xao_Affection);
            }
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (User != BChar && Dmg >= 1)
            {
                Utils.ChooseBattleChibi(BChar, 3, true);
            }
        }

        public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            if (HealedChar == BChar && Healer != BChar)
            {
                Utils.ChooseBattleChibi(BChar, 3, false, true);
            }
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == BChar)
            {
                if (SkillD.NotCount)
                {
                    if (BChar.Overload >= 1)
                    {
                        BChar.Overload = 0;
                    }
                    else
                    {
                        BChar.Overload = 1;
                    }
                }

                Xao_Combo.ComboChange();

                if (Utils.HentaiSkills.Contains(SkillD.MySkill.KeyID) && HornyMod)
                {
                    Utils.PopHentaiText(BChar);
                }

                if (SkillD.IsDamage)
                {
                    Utils.ChooseBattleChibi(BChar, 5, false, false, true);
                }
                else
                {
                    Utils.ChooseBattleChibi(BChar, 3, false, true, false);
                }
            }
        }

        public void SkillUseAfter(Skill SkillD)
        {
            if (SkillD.Master == BChar)
            {
                if (BChar.Overload > 1)
                {
                    BChar.Overload = 0;
                }
            }
        }

        public void SkillUseBasicSkill(Skill skill)
        {
            if (skill.Master == BChar)
            {
                IEnumerator FixedSkillFix()
                {
                    yield return new WaitForEndOfFrame();
                    (BChar as BattleAlly)?.MyBasicSkill.SkillInput(BChar.BattleBasicskillRefill);
                    yield break;
                }
                BattleSystem.instance.StartCoroutine(FixedSkillFix());
            }
        }

        public void SomeOneDead(BattleChar DeadChar)
        {
            if (DeadChar == BChar)
            {
                Utils.DestroyObjects(Utils.ChibiNames);
                Xao_Hearts.DestroyAndNullifyAll();
            }
        }

        public IEnumerator Draw(Skill Drawskill, bool NotDraw)
        {
            if (!HornyMod) yield break;

            if (Utils.XaoSkillList.TryGetValue(Drawskill.MySkill.KeyID, out var newSkillID))
            {
                var newSkill = Skill.TempSkill(newSkillID, Drawskill.Master, Drawskill.Master.MyTeam);
                Drawskill.SkillChange(newSkill);
            }
            yield return null;
        }

        public void SKillUseHand_Team(Skill skill)
        {
            if (skill.Master == BChar) return;

            BChar.StartCoroutine(OverloadCheck());
        }

        public void SKillUseHand_Basic_Team(Skill skill)
        {
            if (skill.Master == BChar) return;

            BChar.StartCoroutine(OverloadCheck());
        }

        public IEnumerator OverloadCheck()
        {
            yield return null;

            if (Utils.Xao.Overload > 1)
            {
                Utils.Xao.Overload = 0;
            }
        }
    }
}