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
namespace Aqua
{
    /// <summary>
    /// Liquid Courage
    /// </summary>
    public class B_Aqua_LiquidCourage : Buff, IP_SkillUse_User
    {

        public override void Init()
        {
            PlusPerStat.Damage = 30;
        }

        public override bool UseSkillBuff(Skill Myskill)
        {
            return Myskill.IsDamage;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            bool neverLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 15);

            if (neverLucky)
            {
                var newTargets = BattleSystem.instance.AllyTeam.AliveChars
                    .Where(a => a != null && a != BChar)
                    .Concat(BattleSystem.instance.EnemyList.Where(e => e != null))
                    .ToList();

                int index = RandomManager.RandomInt(BattleRandom.PassiveItem, 0, newTargets.Count);
                var randomTarget = newTargets[index];

                if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.PlusHit
                    && SkillD.MySkill.Target.Key != GDEItemKeys.s_targettype_all_enemy)
                {

                    var nonLethalDamage = GDEItemKeys.Buff_B_Momori_P_NoDead;

                    if (randomTarget.Info.Ally)
                    {
                        randomTarget.BuffAdd(nonLethalDamage, this.BChar, false, 0, false, -1, false);
                        SkillD.ExtendedAdd(ModItemKeys.SkillExtended_Ex_Aqua_PainDamage);
                    }

                    Targets.Clear();
                    Targets.Add(randomTarget);

                    if (randomTarget.Info.Ally)
                    {
                        BattleSystem.DelayInputAfter(RemoveNoDead());
                    }
                }
            }
        }

        public IEnumerator RemoveNoDead()
        {
            foreach (BattleChar ally in BChar.MyTeam.AliveChars)
            {
                var nonLethalDamage = GDEItemKeys.Buff_B_Momori_P_NoDead;

                if (ally.BuffFind(nonLethalDamage))
                {
                    ally.BuffRemove(nonLethalDamage, false);
                }
            }
            yield break;
        }
    }
}