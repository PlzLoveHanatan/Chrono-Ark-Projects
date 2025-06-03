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
    /// Vortex Chores
    /// </summary>
    public class S_Mia_VortexChores : SkillExtedned_IlyaP
    {
        private int useCount = 0;
        private int DiscardCount = 0;
        private BattleChar TargetTemp;
        private BattleChar SkillOwner;

        public override void IlyaWaste()
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            BattleSystem.DelayInput(Waste());
        }

        public IEnumerator Waste()
        {
            Skill targetSkill = BChar.MyTeam.Skills_UsedDeck.FirstOrDefault(skill => skill.MySkill.KeyID == ModItemKeys.Skill_S_Mia_VortexChores);

            if (targetSkill != null)
            {
                BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(targetSkill);
                BattleSystem.instance.AllyTeam.Skills_Deck.Add(targetSkill);
                
            }

            BattleSystem.instance.AllyTeam.Draw();

            yield return null;
            yield break;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse || MySkill.BasicSkill) return;

            Utils.TryPlayMiaSound(MySkill, BChar);

            SkillOwner = BChar;

            if (Targets.Count > 0)
            {
                TargetTemp = Targets[0];
            }

            var vortexChores = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill.MySkill.KeyID == ModItemKeys.Skill_S_Mia_VortexChores
            && skill != MySkill).ToList();

            if (vortexChores.Count > 0)
            {
                BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(vortexChores, new SkillButton.SkillClickDel(Del), ScriptLocalization.System_SkillSelect.WasteSkill));
            }
        }

        public void Del(SkillButton skillbutton)
        {
            if (useCount >= 3) return;
            Skill selectedSkill = skillbutton.Myskill;
            BattleSystem.DelayInput(Except(selectedSkill));
        }

        public IEnumerator Except(Skill selectedSkill)
        {
            yield return BattleSystem.instance.StartCoroutine(
                BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));

            if (selectedSkill == null || selectedSkill.MySkill.KeyID != ModItemKeys.Skill_S_Mia_VortexChores) yield break;

            if (useCount >= 3) yield break;

            selectedSkill.Delete(false);
            //BattleSystem.instance.AllyTeam.Draw();

            useCount++;
            DiscardCount++;

            Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_S_Mia_VortexChores, BChar, BChar.MyTeam);
            newSkill.PlusHit = true;
            newSkill.FreeUse = true;

            //if (DiscardCount == 1)
            //{
            //    BChar.BuffAdd(ModItemKeys.Skill_S_Mia_VortexChores, BChar, false, 0, false, -1, false);
            //}
            if (DiscardCount == 2)
            {
                if (BChar.Info.KeyData == ModItemKeys.Character_Mia)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_SavageImpulse, BChar, false, 0, false, -1, false);
                }

                else
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Mia_InstinctSurge, BChar, false, 0, false, -1, false);
                }
            }

            BattleChar target;

            if (TargetTemp.IsDead)
            {
                target = BChar.BattleInfo.EnemyList.Random(BChar.GetRandomClass().Main);
            }
            else
            {
                target = TargetTemp;
            }

            BChar.ParticleOut(MySkill, newSkill, target);

            if (useCount < 3)
            {
                var vortexChores = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != MySkill
                && skill.MySkill.KeyID == ModItemKeys.Skill_S_Mia_VortexChores).ToList();

                if (vortexChores.Count > 0)
                {
                    BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(vortexChores, new SkillButton.SkillClickDel(Del), ScriptLocalization.System_SkillSelect.WasteSkill));
                }
            }
        }
    }
}