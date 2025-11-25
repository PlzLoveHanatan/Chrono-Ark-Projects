using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Guests
	{
		public class Dice : Buff, IP_EnemyNewTurn // Additional action
		{
			public void EnemyNewTurn()
			{
				if (BChar is BattleEnemy enemy)
				{
					Skill skill;

					if (DataStore.Instance.Guest.BossActions.ContainsKey(BChar.Info.KeyData))
					{
						// predefined action for original game's enemies
						var actions = DataStore.Instance.Guest.BossActions[BChar.Info.KeyData];
						var action = actions.Random(BChar.GetRandomClass().SkillSelect);

						// reroll bosses nasty actions
						if (RerollActions.TryGetValue(BChar.Info.KeyData, out var rerollList))
						{
							// rerollList[0] = nasty action (25%)
							// rerollList[1] = normal action (75%)
							action = RollActionOverride(25, rerollList[0], rerollList[1]);
						}
						skill = Skill.TempSkill(action, BChar, BChar.MyTeam);
					}
					else if (enemy.Boss)
					{
						// predefined action for undefined bosses (likely mod bosses)
						skill = Skill.TempSkill(ModItemKeys.Skill_S_Abnormality_Guest_EmotionBurst, BChar, BChar.MyTeam);
					}
					else  // random action for undefined regular enemies
					{
						try // just in case something goes wrong
						{
							skill = enemy.Ai.SkillSelect(enemy.ActionCount);
						}
						catch
						{
							skill = enemy.Ai.SkillSelect(0);
						}
					}

					var target = enemy.Ai.TargetSelect(skill);

					if (target == null)
					{
						Debug.LogWarning($"[{enemy.Info.KeyData}] Dice: target is null, selecting random ally instead.");

						var aliveAllies = Utils.AllyTeam.AliveChars.ToList();

						if (aliveAllies == null || aliveAllies.Count == 0)
						{
							Debug.LogWarning($"[{enemy.Info.KeyData}] Dice: no alive allies to target, skipping action.");
							return;
						}

						target = new List<BattleChar>
						{
							aliveAllies.Random(enemy.GetRandomClass().Target)
						};
					}

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

			private string RollActionOverride(int chancePercent, string actionOnSuccess, string actionOnFail)
			{
				bool roll = RandomManager.RandomPer(RandomClassKey.Boss, 100, chancePercent);
				return roll ? actionOnSuccess : actionOnFail;
			}

			private readonly Dictionary<string, List<string>> RerollActions = new Dictionary<string, List<string>>
			{
				{ GDEItemKeys.Enemy_S1_WitchBoss, new List<string> { ModItemKeys.Skill_S_Guest_CurseWeak, ModItemKeys.Skill_S_Guest_CursePain } },
				{ GDEItemKeys.Enemy_Boss_Golem, new List<string> { GDEItemKeys.Skill_S_Golem_1, GDEItemKeys.Skill_S_Golem_2 } },
				{ GDEItemKeys.Enemy_S2_Shiranui, new List<string> { GDEItemKeys.Skill_S_Shiranui_3, GDEItemKeys.Skill_S_Shiranui_1 } },
				{ GDEItemKeys.Enemy_ProgramMaster, new List<string> { GDEItemKeys.Skill_S_ProgramMaster_0, GDEItemKeys.Skill_S_ProgramMaster_1 } },
			};
		}
	}
}
