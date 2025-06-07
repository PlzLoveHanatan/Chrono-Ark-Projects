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
using NLog.Targets;
using Spine;

namespace Unica
{
    /// <summary>
    /// Water Splash
    /// </summary>
    public class WaterSplash : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (SkillD.FreeUse) return;

            if (Targets.Count > 0)
            {
                this.TargetTemp = Targets[0];
            }

            var waterSplashSkills = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill.MySkill.KeyID == ModItemKeys.Skill_WaterSplash 
            && skill != this.MySkill).ToList();

            if (waterSplashSkills.Count > 0)
            {
                BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(waterSplashSkills, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.WasteSkill));
            }
            else
            {
                DiscardSkill();
            }
        }
        private void DiscardSkill()
        {
            var discardList = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill 
            && skill.Master != BattleSystem.instance.AllyTeam.LucyAlly).ToList();

            if (discardList.Count == 0) return;

            var skillToDiscard = discardList.FirstOrDefault(skill => skill.ExtendedFind_DataName(ModItemKeys.SkillExtended_ExSheathe) != null)
                ?? discardList.Where(skill => skill.Master.Info.KeyData == ModItemKeys.Character_Unica).OrderBy(skill => skill.AP).FirstOrDefault()
                ?? discardList.OrderBy(skill => skill.AP).FirstOrDefault();

            skillToDiscard?.Delete(false);

            if (this.BChar.Info.KeyData == ModItemKeys.Character_Unica)
            {
                new P_Unica().ApplyEffects(this.BChar, 1);
            }
        }

        public void Del(SkillButton skillbutton)
        {
            if (useCount >= 3) return;
            Skill selectedSkill = skillbutton.Myskill;
            BattleSystem.DelayInput(this.Except(selectedSkill));
        }

        public IEnumerator Except(Skill selectedSkill)
        {
            yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.ActWindow.Window.SkillInstantiate(BattleSystem.instance.AllyTeam, true));

            if (selectedSkill == null || selectedSkill.MySkill.KeyID != ModItemKeys.Skill_WaterSplash) yield break;

            if (useCount >= 3) yield break;

            selectedSkill.Delete(false);
            BattleSystem.instance.AllyTeam.Draw();

            if (this.BChar.Info.KeyData == ModItemKeys.Character_Unica)
            {
                new P_Unica().ApplyEffects(this.BChar, 1);
            }

            useCount++;

            Skill newSkill = Skill.TempSkill(ModItemKeys.Skill_WaterSplash_0, this.BChar, this.BChar.MyTeam);
            newSkill.PlusHit = true;
            newSkill.FreeUse = true;

            if (TargetTemp.IsDead)
            {
                this.BChar.ParticleOut(this.MySkill, newSkill, this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main));
            }
            else
            {
                this.BChar.ParticleOut(this.MySkill, newSkill, TargetTemp);
            }            

            if (BattleSystem.instance.AllyTeam.Skills.Count(skill => skill.MySkill.KeyID == ModItemKeys.Skill_WaterSplash) == 0)
            {
                var discardList = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill 
                && skill.Master != BattleSystem.instance.AllyTeam.LucyAlly).ToList();

                if (discardList.Count > 0)
                {
                    var myCharacterSkills = discardList.Where(skill => skill.Master.Info.KeyData == ModItemKeys.Character_Unica).OrderBy(skill => skill.AP).ToList();

                    Skill skillToDiscard = myCharacterSkills.FirstOrDefault()
                        ?? discardList.OrderBy(skill => skill.AP).FirstOrDefault();

                    if (skillToDiscard != null)
                    {
                        BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skillToDiscard }, new SkillButton.SkillClickDel(this.Del1), ScriptLocalization.System_SkillSelect.WasteSkill));
                    }
                }
            }

            if (useCount < 3)
            {
                var newWaterSplashSkills = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill != this.MySkill 
                && skill.MySkill.KeyID == ModItemKeys.Skill_WaterSplash).ToList();

                if (newWaterSplashSkills.Count > 0)
                {
                    BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(newWaterSplashSkills, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.WasteSkill));
                }
            }
        }
        public void Del1(SkillButton skillbutton)
        {
            Skill selectedSkill = skillbutton.Myskill;
            DiscardLowestManaSkills(new List<Skill> { selectedSkill });
        }
        public void DiscardLowestManaSkills(List<Skill> skills)
        {
            var sortedSkills = skills.OrderBy(skill => skill.AP).ToList();
            Skill skillToDiscard = sortedSkills.FirstOrDefault();

            skillToDiscard?.Delete(false);
        }

        private int useCount = 0;
        private BattleChar TargetTemp;
    }
}
