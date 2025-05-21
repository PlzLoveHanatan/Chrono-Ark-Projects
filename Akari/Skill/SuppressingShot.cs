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
    /// Suppressing Shot
    /// </summary>
    public class SuppressingShot : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            MasterAudio.PlaySound("Gun_Normal1", 100f, null, 0f, null, null, false, false);

            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills.FindAll(skill => Utils.Ammunition.Contains(skill.MySkill.KeyID));

            if (ammunitionInHand.Count <= 0) return;

            int numDiscard = 1;

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

            var target = Targets[0];

            for (int i = 0; i < FlameCheck; i++)
            {
                target.BuffAdd(ModItemKeys.Buff_B_FlameAmmunition, this.BChar, false, 0, false, -1, false);
            }
            for (int j = 0; j < FrostCheck; j++)
            {
                target.BuffAdd(ModItemKeys.Buff_B_FrostAmmunition, this.BChar, false, 0, false, -1, false);
            }
            for (int k = 0; k < PiercingCheck; k++)
            {
                target.BuffAdd(ModItemKeys.Buff_B_Armor_piercingAmmunition, this.BChar, false, 0, false, -1, false);
            }
        }        
    }
}
