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
using Spine;
using NLog.Targets;
namespace Akari
{
    public class Ex_AmmunitionDiscard : Skill_Extended, IP_SkillUse_User, IP_DamageChange_sumoperation, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP >= 1 && MainSkill.IsDamage || MainSkill.IsHeal;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills
                    .FindAll(skill => Utils.Ammunition.Contains(skill.MySkill.KeyID));

            if (ammunitionInHand.Count <= 0) return;

            if (SkillD.IsHeal
                && SkillD.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_AmmunitionDiscard) != null
                && SkillD.Master == this.BChar
                && !SkillD.BasicSkill && !SkillD.FreeUse
                && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {

                SkillD.ExtendedAdd(ModItemKeys.SkillExtended_Ex_IncreaseHeal);

                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, ammunitionInHand.Count);
                Skill selectedAmmo = ammunitionInHand[index];
                selectedAmmo.Delete(false);
            }
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills
                    .FindAll(skill => Utils.Ammunition.Contains(skill.MySkill.KeyID));

            if (ammunitionInHand.Count <= 0) return;

            if (SkillD.IsDamage
                && SkillD.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_AmmunitionDiscard) != null 
                && SkillD.Master == BChar 
                && !SkillD.BasicSkill 
                && !SkillD.FreeUse 
                && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                PlusDamage = (int)(Damage * 0.3f);
            }
        }
        public void SkillUseAfter(Skill SkillD)
        {
            if (SkillD.IsDamage 
                && SkillD.Master == this.BChar
                && SkillD.ExtendedFind_DataName(ModItemKeys.SkillExtended_Ex_AmmunitionDiscard) != null
                && !SkillD.BasicSkill 
                && !SkillD.FreeUse 
                && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills
                    .FindAll(skill => Utils.Ammunition.Contains(skill.MySkill.KeyID));

                if (ammunitionInHand.Count <= 0) return;

                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, ammunitionInHand.Count);
                Skill selectedAmmo = ammunitionInHand[index];
                selectedAmmo.Delete(false);
            }
        }
    }
}
    


