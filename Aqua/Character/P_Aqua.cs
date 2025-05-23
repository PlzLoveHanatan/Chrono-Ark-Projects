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
namespace Aqua
{
	/// <summary>
	/// Aqua
	/// Passive:
	/// </summary>
    public class P_Aqua : Passive_Char, IP_DamageTake, IP_BuffAddAfter
    {
        public override void Init()
        {
            OnePassive = true;
        }

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (Dmg >= 1)
            {
                foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    if (ally != BChar && ally != null)
                    {
                        ally.Heal(BattleSystem.instance.DummyChar, 3f, false, true, null);

                        Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Aqua_DummyHeal, this.BChar, this.BChar.MyTeam);
                        healingParticle.PlusHit = true;
                        healingParticle.FreeUse = true;

                        this.BChar.ParticleOut(healingParticle, ally);
                    }
                }

                foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    if (enemy != null)
                    {
                        enemy.BuffAdd(ModItemKeys.Buff_B_Aqua_CryingShame, this.BChar, false, 0, false, -1, false);
                    }
                }
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            var debuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, false, false);
            var painSharing = GDEItemKeys.Buff_B_BloodyMist_ShareDamage;
            var pmPainSharing = GDEItemKeys.Buff_B_ProgramMaster_LucyMain;

            if (BuffTaker == BChar && debuffs != null)
            {
                foreach (var debuff in debuffs)
                {
                    if (debuff.BuffData.Key == painSharing || debuff.BuffData.Key == pmPainSharing) continue;

                    debuff.SelfDestroy();
                }
            }
        }
    }
}