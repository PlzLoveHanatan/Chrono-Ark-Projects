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
namespace Aqua
{
	/// <summary>
	/// Aqua's Hagoromo
	/// Apply "Aqua Veil" to the attacker.
	/// At the start of each turn cast a random Party Trick.
	/// <color=#919191>Makes you dodge better... or just confuse enemies with your fabulousness.</color>
	/// </summary>
    public class E_Aqua_Hagoromo : EquipBase, IP_PlayerTurn, IP_DamageChange_Hit_sumoperation, IP_Dodge
    {
        public override void Init()
        {
            PlusStat.dod = 25;
        }

        public void Turn()
		{
            Skill partyTrick = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_PartyTrick, this.BChar, this.BChar.MyTeam);
            partyTrick.FreeUse = true;

            //BChar.ParticleOut(partyTrick, BChar);
            BattleSystem.DelayInputAfter(BattleSystem.instance.SkillRandomUseIenum(BChar, partyTrick, false, false, false));
        }

        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (Damage > 0 && !SkillD.Master.Info.Ally)
            {
                SkillD.Master.BuffAdd(ModItemKeys.Buff_B_Aqua_AquaVeil, this.BChar, false, 0, false, -1, false);
            }
        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == this.BChar && !SP.UseStatus.Info.Ally)
            {
                SP.UseStatus.BuffAdd(ModItemKeys.Buff_B_Aqua_AquaVeil, this.BChar, false, 0, false, -1, false);
            }
        }
    }
}