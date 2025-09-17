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
    public class BayonetSword : EquipBase, IP_SkillUse_User, IP_PlayerTurn
    {
        private int AttackUsesThisTurn;

        public override void Init()
        {
            PlusPerStat.Damage = 25;
            PlusStat.dod = 10f;
            PlusStat.HIT_CC = 10f;
            PlusStat.HIT_DEBUFF = 10f;
            PlusStat.HIT_DOT = 10f;
            OnePassive = true;
        }

        public void Turn()
        {
            AttackUsesThisTurn = 0;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (AttackUsesThisTurn >= 2) return;

            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.BasicSkill && !SkillD.FreeUse && !Utils.Ammunition.Concat(Utils.RangeAttacks).Contains(SkillD.MySkill.KeyID))
            {
                MasterAudio.PlaySound("Melee_Normal", 100f, null, 0f, null, null, false, false);

                AttackUsesThisTurn++;

                Utils.CreateRandomAmmunition(BChar);
            }
        }
    }
}