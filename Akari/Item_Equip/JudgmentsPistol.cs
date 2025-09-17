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
    public class JudgmentsPistol : EquipBase, IP_SkillUse_User, IP_PlayerTurn
    {
        private bool AmmoDiscard;

        public override void Init()
        {
            PlusStat.atk = 3f;
            PlusStat.reg = 3f;
            PlusStat.cri = 10f;
            OnePassive = true;
        }

        public void Turn()
        {
            AmmoDiscard = false;
            Utils.CreateRandomAmmunition(BChar, 1);
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (!AmmoDiscard && SkillD.Master == BChar && (SkillD.IsDamage || SkillD.IsHeal) && !Utils.Ammunition.Contains(SkillD.MySkill.KeyID))
            {
                List<Skill> ammunitionInHand = BattleSystem.instance.AllyTeam.Skills.FindAll(s => Utils.Ammunition.Contains(s.MySkill.KeyID));

                if (ammunitionInHand.Count > 0)
                {
                    MasterAudio.PlaySound("Gun_Normal", 100f, null, 0f, null, null, false, false);

                    SkillD.ExtendedAdd(ModItemKeys.SkillExtended_Ex_JudgmentsPistol);

                    int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, ammunitionInHand.Count);
                    Skill selectedAmmo = ammunitionInHand[index];
                    selectedAmmo.Delete(false);

                    AmmoDiscard = true;
                }
            }
        }
    }
}