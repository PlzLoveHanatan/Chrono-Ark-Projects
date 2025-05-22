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
namespace EmotionalSystem
{
    /// <summary>
    /// Coffin
    /// </summary>
    public class B_Abnormality_TechnologicalLv3_Coffin : Buff, IP_SkillUse_User, IP_Awake, IP_PlayerTurn
    {
        private bool OncePerTurn;

        public override string DescExtended()
        {
            if (OncePerTurn)
            {
                return base.DescExtended().Replace("&a", "The wings tremble on the verge of collapse".ToString());
            }
            return base.DescExtended().Replace("&a", "The wings have crumbled to ash".ToString());
        }

        public void Awake()
        {
            OncePerTurn = true;
        }
        public void Turn()
        {
            OncePerTurn = true;
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage && SkillD.Master == BChar && !SkillD.FreeUse && !SkillD.PlusHit && OncePerTurn)
            {
                foreach (var target in Targets)
                {
                    if (target != null && !target.Info.Ally && !target.Dummy && !target.IsDead)
                    {
                        if (target is BattleEnemy enemy && enemy.Boss)
                        {
                            if (target.HP <= target.GetStat.maxhp * 0.45f)
                            {
                                var bossSkill = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(skill => skill.Usestate == target);
                                BattleSystem.instance.EnemyCastSkills.Remove(bossSkill);
                                BattleSystem.instance.ActWindow.CastingWasteFixed(bossSkill);
                                OncePerTurn = false;
                            }
                        }
                        else
                        {
                            if (target.HP <= target.GetStat.maxhp * 0.9f)
                            {
                                var enemySkill = BattleSystem.instance.EnemyCastSkills.FirstOrDefault(skill => skill.Usestate == target);
                                BattleSystem.instance.EnemyCastSkills.Remove(enemySkill);
                                BattleSystem.instance.ActWindow.CastingWasteFixed(enemySkill);
                                OncePerTurn = false;
                            }
                        }
                    }
                }
            }
        }
    }
}