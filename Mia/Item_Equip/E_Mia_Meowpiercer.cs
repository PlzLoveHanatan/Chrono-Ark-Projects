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
    public class E_Mia_Meowpiercer : EquipBase, IP_Discard, IP_PlayerTurn, IP_SkillUse_Target
    {
        private int Meowpiercer;

        public override void Init()
        {
            PlusStat.atk = 2;
            PlusStat.PlusCriDmg = 25;
            PlusStat.hit = 5;
            PlusStat.HIT_CC = 10f;
            PlusStat.HIT_DEBUFF = 10f;
            PlusStat.HIT_DOT = 10f;
        }

        public void Turn()
        {
            Meowpiercer = 0;
        }

        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar && !SP.SkillData.FreeUse
                && hit != null && !hit.Info.Ally && !hit.Dummy)
            {
                hit.BuffAdd(ModItemKeys.Buff_B_Mia_Pawcut, this.BChar, false, 0, false, -1, false);
            }
        }

        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if (!Click && !HandFullWaste && Meowpiercer < 4)
            {
                BChar.BuffAdd(ModItemKeys.Buff_B_Mia_E_Meowpiercer, BChar, false, 0, false, -1, false);
                Meowpiercer++;
            }
        }
    }
}