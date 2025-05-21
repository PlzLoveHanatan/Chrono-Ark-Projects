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
namespace Akari
{
    /// <summary>
    /// Judgment's Pistol
    /// Whenever the wearer's plays a skill from hand discard a random Ammunition, increase this skill's damage by 20%.
    /// </summary>
    public class JudgmentsPistol : EquipBase, IP_SkillUse_User, IP_DamageChange_sumoperation, IP_PlayerTurn, IP_SkillUse_User_After
    {
        private int ammunitionDiscarded;

        public override void Init()
        {
            this.PlusStat.atk = 3f;
            this.PlusStat.reg = 3f;
            this.PlusStat.cri = 5f;
            this.PlusStat.hit = 5f;
            base.Init();
            OnePassive = true;
        }

        public void Turn()
        {
            ammunitionDiscarded = 0;
            BChar.BuffAdd(ModItemKeys.Buff_AmmoSupply, BChar, false, 0, false, -1, false);
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (ammunitionDiscarded >= 2) return;

            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills
                    .FindAll(skill => Utils.Ammunition.Contains(skill.MySkill.KeyID));

            if (ammunitionInHand.Count <= 0) return;

            if (SkillD.IsHeal && SkillD.Master == BChar && !SkillD.BasicSkill && !SkillD.FreeUse && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                MasterAudio.PlaySound("Gun_Normal", 100f, null, 0f, null, null, false, false);

                SkillD.ExtendedAdd(ModItemKeys.SkillExtended_Ex_IncreaseHeal);

                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, ammunitionInHand.Count);
                Skill selectedAmmo = ammunitionInHand[index];
                selectedAmmo.Delete(false);

                ammunitionDiscarded++;
            }
        }

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (ammunitionDiscarded >= 2) return;

            List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills
                    .FindAll(skill => Utils.Ammunition.Contains(skill.MySkill.KeyID));

            if (ammunitionInHand.Count <= 0) return;

            if (SkillD.IsDamage && SkillD.Master == this.BChar && !SkillD.BasicSkill && !SkillD.FreeUse && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                PlusDamage += (int)(Damage * 0.3f);
            }
        }

        public void SkillUseAfter(Skill SkillD)
        {
            if (ammunitionDiscarded >= 2) return;

            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.BasicSkill && !SkillD.FreeUse && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills
                    .FindAll(skill => Utils.Ammunition.Contains(skill.MySkill.KeyID));

                if (ammunitionInHand.Count <= 0) return;

                MasterAudio.PlaySound("Gun_Normal", 100f, null, 0f, null, null, false, false);

                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, ammunitionInHand.Count);
                Skill selectedAmmo = ammunitionInHand[index];
                selectedAmmo.Delete(false);

                ammunitionDiscarded++;
            }
        }

    }
}