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
namespace EmotionalSystem
{
    /// <summary>
    /// The Seventh Bullet
    /// </summary>
    public class B_Abnormality_TechnologicalLv2_TheSeventhBullet : Buff, IP_SkillUse_User
    {
        private int PewPew;

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", PewPew.ToString());
        }

        public override void BuffStat()
        {
            PlusPerStat.Damage = 20;
        }

        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
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
                PewPew++;

                if (PewPew == 2)
                {
                    BChar.BuffAdd(ModItemKeys.Buff_B_Abnormality_TechnologicalLv2_TheSeventhBullet_0, this.BChar, false, 0, false, -1, false);
                }

                if (PewPew >= 3)
                {
                    var nonLethalDamage = GDEItemKeys.Buff_B_Momori_P_NoDead;

                    if (randomTarget.Info.Ally)
                    {
                        randomTarget.BuffAdd(nonLethalDamage, this.BChar, false, 0, false, -1, false);
                        SkillD.ExtendedAdd(ModItemKeys.SkillExtended_Ex_EmotionalSystem_PainDamage);
                    }
                    
                    Targets.Clear();
                    Targets.Add(randomTarget);

                    if (randomTarget.Info.Ally)
                    {
                        BattleSystem.DelayInputAfter(RemoveNoDead());
                    }

                    PewPew = 0;
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