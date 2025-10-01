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
namespace ImaSuguRinne
{
	/// <summary>
	/// Resonance of Pain
	/// Attack all enemies with the same Taunt status as the target.
	/// </summary>
    public class S_Rinne_ResonanceofPain : Skill_Extended
    {
        public override void Init()
        {
            OnePassive = true;
            SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_WitchBoss_Ex_0).Particle_Path;
        }

        public override IEnumerator DrawAction()
        {
            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_ResonanceofPain_0)
            {
                Utils.CastSkill(BChar, MySkill);
            }
            return base.DrawAction();
        }

        public override void FixedUpdate()
        {
            if (BChar.BattleInfo.EnemyList.Count == 1)
            {
                MySkill.APChange = -1;
                SkillParticleOn();
            }
            else
            {
                SkillParticleOff();
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Targets[0] is BattleEnemy mainTarget)
            {
                List<BattleEnemy> additionalTargets = new List<BattleEnemy>();

                if (mainTarget.istaunt)
                {
                    foreach (var enemy in BattleSystem.instance.EnemyList)
                    {
                        if (enemy != mainTarget && enemy.istaunt)
                        {
                            additionalTargets.Add(enemy);
                        }
                    }
                }
                else
                {
                    foreach (var enemy in BattleSystem.instance.EnemyList)
                    {
                        if (enemy != mainTarget && !enemy.istaunt)
                        {
                            additionalTargets.Add(enemy);
                        } 
                    }
                }
                Targets.AddRange(additionalTargets);
            }

            foreach (var target in Targets)
            {
                Utils.CopyAndExtendDebuffs(target);
            }

            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_ResonanceofPain_0)
            {
                Utils.AllyTeam.Draw();
            }
            else
            {
                Skill skill = Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_ResonanceofPain_0, true, true, 0, 0, true, false);
                Utils.InsertSkillInDeck(BChar, skill);
            }
        }
    }
}