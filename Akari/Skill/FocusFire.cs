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
namespace Akari
{
    /// <summary>
    /// Focus Fire
    /// </summary>
    public class FocusFire : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(BChar.GetStat.atk * 0.4f)).ToString());
        }

        public override void FixedUpdate()
        {
            if (BChar.BattleInfo.EnemyList.Count == 1)
            {
                base.SkillParticleOn();
                On();
                return;
            }
            base.SkillParticleOff();
            Off();
        }
        public void On()
        {
            SkillBasePlus.Target_BaseDMG = (int)(BChar.GetStat.atk * 0.4f);
        }
        public void Off()
        {
            SkillBasePlus.Target_BaseDMG = 0;
        }
        public override void Init()
        {
            base.Init();
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            MasterAudio.PlaySound("Gun_Normal", 100f, null, 0f, null, null, false, false);

            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills.FindAll(skill => Utils.Ammunition.Contains(skill.MySkill.KeyID));

            if (ammunitionInHand.Count <= 0) return;

            int numDiscard = 3;

            int bonusDiscard = 0;

            var TheBossOrders = BChar.BuffReturn(ModItemKeys.Buff_B_TheBossOrders, false);

            if (TheBossOrders?.StackNum >= 1)
            {
                bonusDiscard = 1;
            }

            numDiscard += bonusDiscard;

            if (numDiscard > ammunitionInHand.Count)
            {
                numDiscard = ammunitionInHand.Count;
            }

            List<Skill> randomAmmunition = RandomManager.Random(ammunitionInHand, MySkill.Master.GetRandomClass().SkillSelect, numDiscard);

            foreach (var ammunition in randomAmmunition)
            {
                ammunition.Delete(false);
            }

            int FlameCheck = randomAmmunition.Count(skill => skill.MySkill.KeyID == ModItemKeys.Skill_FlameAmmunition);
            int FrostCheck = randomAmmunition.Count(skill => skill.MySkill.KeyID == ModItemKeys.Skill_FrostAmmunition);
            int PiercingCheck = randomAmmunition.Count(skill => skill.MySkill.KeyID == ModItemKeys.Skill_Armor_piercingAmmunition);
            int AmmunitionCheck = FlameCheck + FrostCheck + PiercingCheck;

            PlusSkillPerFinal.Damage = 15 * AmmunitionCheck;


            var enemies = BattleSystem.instance.EnemyTeam.AliveChars;

            foreach (var enemy in enemies)
            {
                for (int i = 0; i < FlameCheck; i++)
                {
                    enemy.BuffAdd(ModItemKeys.Buff_B_FlameAmmunition, BChar, false, 0, false, -1, false);
                }
                for (int j = 0; j < FrostCheck; j++)
                {
                    enemy.BuffAdd(ModItemKeys.Buff_B_FrostAmmunition, BChar, false, 0, false, -1, false);
                }
                for (int k = 0; k < PiercingCheck; k++)
                {
                    enemy.BuffAdd(ModItemKeys.Buff_B_Armor_piercingAmmunition, BChar, false, 0, false, -1, false);
                }
            }
        }
    }
}

