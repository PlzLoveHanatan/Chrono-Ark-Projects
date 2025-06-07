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
    /// Sakura
    /// </summary>
    public class Sakura : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.2f)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            List<Skill> mySkills = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill.Master.Info.KeyData == ModItemKeys.Character_Unica
            && skill != this.MySkill).ToList();

            List<Skill> otherSkills = BattleSystem.instance.AllyTeam.Skills.Where(skill => skill.Master.Info.KeyData != ModItemKeys.Character_Unica
            && skill != this.MySkill).ToList();

            if (mySkills.Count + otherSkills.Count < 4) return;

            List<Skill> skillsToDiscard = new List<Skill>();
            int discardedSkillsCount = 0;

            while (mySkills.Count > 0 && discardedSkillsCount < 3)
            {
                skillsToDiscard.Add(mySkills[0]);
                mySkills.RemoveAt(0);
                discardedSkillsCount++;
            }

            if (discardedSkillsCount < 3)
            {
                List<Skill> remainingSkills = otherSkills.Concat(mySkills).ToList();
                var numberList = Enumerable.Range(0, remainingSkills.Count).ToList();

                List<int> selectedNums = RandomManager.Random(numberList, this.MySkill.Master.GetRandomClass().SkillSelect, 3 - discardedSkillsCount);

                foreach (int num in selectedNums)
                {
                    skillsToDiscard.Add(remainingSkills[num]);
                    discardedSkillsCount++;
                }
            }

            foreach (var skill in skillsToDiscard)
            {
                skill.Delete(false);
            }

            this.SkillBasePlus.Target_BaseDMG = (int)(this.BChar.GetStat.atk * 0.2f) * discardedSkillsCount;

            if (this.BChar.Info.KeyData == ModItemKeys.Character_Unica)
            {
                for (int i = 0; i < discardedSkillsCount; i++)
                {
                    new P_Unica().ApplyEffects(this.BChar, discardedSkillsCount);
                }
            }
        }
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            {
                if (BattleSystem.instance.AllyTeam.Skills.FindAll((Skill s) => s != this.MySkill).Count > 4)
                {
                    base.SkillParticleOn();
                    return;
                }
                base.SkillParticleOff();
            }
        }
    }
}