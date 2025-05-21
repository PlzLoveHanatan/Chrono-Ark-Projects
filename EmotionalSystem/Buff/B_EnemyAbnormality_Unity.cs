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
    /// Unity
    /// </summary>
    public class B_EnemyAbnormality_Unity : Buff, IP_PlayerTurn, IP_Awake
    {
        public void Awake()
        {
            Unity();
        }
        public void Turn()
        {
            Unity();
        }
        private void Unity()
        {
            int Heal = (int)(BChar.GetStat.maxhp * 0.25f);
            int healAmount = Mathf.Min(25, Heal);

            if (BattleSystem.instance.EnemyList.Count > 1)
            {
                foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars)
                {
                    if (enemy != BChar)
                    {
                        enemy.Heal(BattleSystem.instance.DummyChar, healAmount, false, true, null);

                        Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Buff_Unity, this.BChar, this.BChar.MyTeam);
                        healingParticle.PlusHit = true;
                        healingParticle.FreeUse = true;

                        this.BChar.ParticleOut(healingParticle, enemy);
                    }
                }
            }
            else
            {
                BChar.Heal(BattleSystem.instance.DummyChar, healAmount, false, true, null);

                Skill healingParticle = Skill.TempSkill(ModItemKeys.Skill_S_Buff_Unity, this.BChar, this.BChar.MyTeam);
                healingParticle.PlusHit = true;
                healingParticle.FreeUse = true;

                this.BChar.ParticleOut(healingParticle, BChar);
            }
        }
    }
}