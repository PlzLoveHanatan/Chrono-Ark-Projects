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
using TileTypes;
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;
using System.Web.Compilation;
namespace EmotionalSystem
{
    /// <summary>
    /// Additional Dice
    /// Gain one more action per turn.
    /// </summary>
    public class B_EnemyEmotionalLevel_Dice : Buff, IP_EnemyNewTurn
    {
        private readonly Dictionary<string, List<string>> BossActions = new Dictionary<string, List<string>>()
        {
            { GDEItemKeys.Enemy_MBoss_0, new List<string> { GDEItemKeys.Skill_S_MBoss_0, GDEItemKeys.Skill_S_MBoss_1, GDEItemKeys.Skill_S_MBoss_2 } },
            { GDEItemKeys.Enemy_S1_ArmorBoss, new List<string> { GDEItemKeys.Skill_S_Armor_0, GDEItemKeys.Skill_S_Armor_2, GDEItemKeys.Skill_S_Armor_3 } },
            { GDEItemKeys.Enemy_S1_WitchBoss, new List<string> { ModItemKeys.Skill_S_Boss_Witch_Curse, ModItemKeys.Skill_S_Boss_Witch_Curse_0 } },
            { GDEItemKeys.Enemy_Boss_Golem, new List<string> { GDEItemKeys.Skill_S_Golem_1, GDEItemKeys.Skill_S_Golem_2 } },
            { GDEItemKeys.Enemy_S1_BossDorchiX, new List<string> { GDEItemKeys.Skill_S_DorchiX_1, GDEItemKeys.Skill_S_DorchiX_2 } },
            { GDEItemKeys.Enemy_MBoss2_0, new List<string> { GDEItemKeys.Skill_S_MBoss2_0_2, GDEItemKeys.Skill_S_MBoss2_0_3 } },
            { GDEItemKeys.Enemy_S2_Joker, new List<string> { GDEItemKeys.Skill_S_Joker_1, GDEItemKeys.Skill_S_Joker_2 } },
            { GDEItemKeys.Enemy_S2_Shiranui, new List<string> { GDEItemKeys.Skill_S_Shiranui_1, GDEItemKeys.Skill_S_Shiranui_3 } },
            { GDEItemKeys.Enemy_TheDealer, new List<string> { GDEItemKeys.Skill_S_TheDealer_1, GDEItemKeys.Skill_S_TheDealer_2, GDEItemKeys.Skill_S_TheDealer_3} },
            { GDEItemKeys.Enemy_S2_MainBoss_1_0, new List<string> { GDEItemKeys.Skill_S_S2_Mainboss_1_Left_0, GDEItemKeys.Skill_S_S2_Mainboss_1_Left_1, GDEItemKeys.Skill_S_S2_Mainboss_1_Left_2 } },
            { GDEItemKeys.Enemy_S2_MainBoss_1_1, new List<string> { GDEItemKeys.Skill_S_S2_Mainboss_1_Right_0, GDEItemKeys.Skill_S_S2_Mainboss_1_Right_1, GDEItemKeys.Skill_S_S2_Mainboss_1_Right_2 } },
            { GDEItemKeys.Enemy_S2_BombClownBoss, new List<string> { GDEItemKeys.Skill_S_BombClown_P, GDEItemKeys.Skill_S_BombClown_0, GDEItemKeys.Skill_S_BombClown_1 } },
            { GDEItemKeys.Enemy_MBoss2_1, new List<string> { GDEItemKeys.Skill_S_MBoss2_1_1, GDEItemKeys.Skill_S_MBoss2_1_2, GDEItemKeys.Skill_S_MBoss2_1_3 } },
            { GDEItemKeys.Enemy_SR_GunManBoss, new List<string> { GDEItemKeys.Skill_S_Gunman_0, GDEItemKeys.Skill_S_Gunman_1 } },
            { GDEItemKeys.Enemy_S3_Boss_Pope, new List<string> { GDEItemKeys.Skill_S_S3_Pope_1, GDEItemKeys.Skill_S_S3_Pope_2, GDEItemKeys.Skill_S_S3_Pope_3} },
            { GDEItemKeys.Enemy_S3_Boss_TheLight, new List<string> { GDEItemKeys.Skill_S_TheLight_1, GDEItemKeys.Skill_S_TheLight_2, GDEItemKeys.Skill_S_TheLight_3 } },
            { GDEItemKeys.Enemy_S3_Boss_Reaper, new List<string> { GDEItemKeys.Skill_S_Boss_Reaper_1, GDEItemKeys.Skill_S_Boss_Reaper_2 } },
            { GDEItemKeys.Enemy_S3_FanaticBoss, new List<string> { GDEItemKeys.Skill_S_FanaticBoss_2, GDEItemKeys.Skill_S_FanaticBoss_3 } },
            { GDEItemKeys.Enemy_LBossFirst, new List<string> { GDEItemKeys.Skill_S_LBossFirst_0, GDEItemKeys.Skill_S_LBossFirst_2, GDEItemKeys.Skill_S_LBossFirst_3 } },
            { GDEItemKeys.Enemy_S4_King_0, new List<string> { GDEItemKeys.Skill_S_S4_King_P1_1, GDEItemKeys.Skill_S_S4_King_P1_2, GDEItemKeys.Skill_S_S4_King_P2_0, GDEItemKeys.Skill_S_S4_King_P2_1 } },
            { GDEItemKeys.Enemy_ProgramMaster, new List<string> { GDEItemKeys.Skill_S_ProgramMaster_0, GDEItemKeys.Skill_S_ProgramMaster_1 } },
        };

        public void EnemyNewTurn()
        {
            if (BChar is BattleEnemy enemy)
            {
                Skill skill;

                if (BossActions.ContainsKey(BChar.Info.KeyData))
                { // predefined action for original game's enemies
                    var actions = BossActions[BChar.Info.KeyData];
                    var action = actions.Random(BChar.GetRandomClass().SkillSelect);
                    skill = Skill.TempSkill(action, BChar, BChar.MyTeam);
                }
                else if (enemy.Boss)
                { // predefined action for undefined bosses (likely mod bosses)
                    skill = Skill.TempSkill(ModItemKeys.Skill_S_EmotionalSystem_RevengeStrike, BChar, BChar.MyTeam);
                }
                else
                { // random action for undefined regular enemies
                    try
                    {
                        skill = enemy.Ai.SkillSelect(enemy.ActionCount);
                    }
                    catch // just in case something goes wrong
                    {
                        skill = enemy.Ai.SkillSelect(0);
                    }
                }
                var target = enemy.Ai.TargetSelect(skill);
                int countdown = GetNewActionTime(enemy.SkillQueue.Select(cs => cs.CastSpeed).ToList());

                BattleSystem.instance.EnemyCastEnqueue(enemy, skill, target, countdown, false);
                BattleSystem.instance.EnemyCastSkills = BattleSystem.instance.EnemyCastSkills.OrderBy(cs => cs.CastSpeed).ToList();
            }
        }
        public int GetNewActionTime(List<int> existingActionTimes)
        {
            existingActionTimes.Add(0);
            existingActionTimes.Add(int.MaxValue);
            existingActionTimes.Sort();

            for (int i = 0; i < existingActionTimes.Count - 1; i++)
            {
                int current = existingActionTimes[i];
                int next = existingActionTimes[i + 1];

                if (next - current > 10)
                {
                    return current + 2;
                }
            }

            return 2;
        }
    }
}