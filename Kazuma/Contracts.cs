using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace Kazuma
{
	public class Contracts
	{
		public class Contract : Buff
		{
			public readonly List<ItemBase> RewardItems = new List<ItemBase>();

			public override void Init()
			{
				AddRewards();
				RegisterRewards();
			}

			private void AddRewards()
			{
				bool isLucky = RandomManager.RandomPer(RandomClassKey.BattleReward, 100, 25);
				int soulStonesReward = isLucky ? 1 : 0;
				int goldReward = RandomManager.RandomInt(RandomClassKey.BattleReward, 0, 500);

				RewardItems.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, soulStonesReward));
				RewardItems.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, goldReward));
			}

			private void RegisterRewards()
			{
				if (RewardItems.Count > 0)
				{
					foreach (var item in RewardItems)
					{
						Itemviews.Add(item);
					}
				}
			}
		}

		public class Dangerous : Contract, IP_Awake
		{
			private readonly List<string> CurseList = new List<string>
			{
				GDEItemKeys.Buff_B_CursedMob_0,
				GDEItemKeys.Buff_B_CursedMob_1,
				GDEItemKeys.Buff_B_CursedMob_2,
				GDEItemKeys.Buff_B_CursedMob_3,
				GDEItemKeys.Buff_B_CursedMob_4,
				GDEItemKeys.Buff_B_CursedMob_5,
			};

			public void Awake()
			{
				BattleSystem.DelayInputAfter(ApplyCurse());
			}

			private IEnumerator ApplyCurse()
			{
				var enemies = BattleSystem.instance.EnemyTeam.AliveChars;

				if (enemies.Count > 0)
				{
					int randomIndex = RandomManager.RandomInt(RandomClassKey.Enemy, 0, enemies.Count);
					var randomEnemy = enemies[randomIndex];
					string randomCurse = CurseList[RandomManager.RandomInt(RandomClassKey.Curse, 0, CurseList.Count)];
					randomEnemy.BuffAdd(randomCurse, BattleSystem.instance.DummyChar);
				}
				else
				{
					SelfDestroy();
				}

				yield break;
			}
		}

		public class Work : Contract, IP_PlayerTurn
		{
			private int cannotAct = 2;

			public override void Init()
			{
				bool isStun = cannotAct > 0;
				PlusStat.Stun = isStun;
			}

			public void Turn()
			{
				cannotAct--;
			}
		}

		public class Emergency : Contract, IP_PlayerTurn
		{
			// determine random turn to kill enemy
			//private int deadLine = RandomManager.RandomInt(RandomClassKey.Curse, 1, 4);
			private int deadLine = 3;

			public override string DescExtended()
			{
				return base.DescExtended().Replace("&a", deadLine.ToString());
			}

			public void Turn()
			{
				deadLine--;

				if (deadLine <= 0)
				{
					SelfDestroy();
				}
			}
		}

		public class Inspection : Contract
		{
			public void KillEffect(SkillParticle SP)
			{
				if (SP.SkillData.Master.Info.KeyData != ModItemKeys.Character_Kazuma && SP.TargetChar.Contains(BChar) && BChar.IsDead)
				{
					SelfDestroy();
				}
			}
		}

		public class Inspection_0 : Buff, IP_Dead
		{
			public void Dead()
			{
				if (BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALL, false, false).Count > 0)
				{
					BattleSystem.DelayInput(RemoveContract());
				}
			}

			private IEnumerator RemoveContract()
			{
				var kazuma = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x.Info.KeyData == ModItemKeys.Character_Kazuma);

				if (kazuma?.BuffReturn(ModItemKeys.Buff_B_Contract_04_Inspection, false) != null)
				{
					kazuma?.BuffRemove(ModItemKeys.Buff_B_Contract_04_Inspection, true);
				}
				yield break;
			}
		}

		public class Delivery : Contract, IP_DamageTake
		{
			private int damageTaken = 3;

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (Dmg >= 1 && !resist)
				{
					damageTaken--;

					if (damageTaken <= 0)
					{
						SelfDestroy();
					}
				}
			}

			public override string DescExtended()
			{
				return base.DescExtended().Replace("&a", damageTaken.ToString());
			}
		}

		public class Axis : Contract, IP_EnemyAwake, IP_Awake
		{
			public void Awake()
			{
				BattleSystem.instance.EnemyTeam.AliveChars_Vanish.ForEach(e => e.BuffAdd(ModItemKeys.Buff_B_Contract_06_Axis_0, BChar));
			}

			public override void Init()
			{
				PlusStat.AggroPer = 100;
			}

			public void EnemyAwake(BattleChar Enemy)
			{
				Enemy.BuffAdd(ModItemKeys.Buff_B_Contract_06_Axis_0, BChar);
			}
		}

		public class Axis_0 : B_Taunt
		{
			public override void SkillUse(Skill SkillD, List<BattleChar> Targets)
			{
				Targets.Clear();
				Targets.Add(Usestate_F);
			}
		}

		public class Guild : Contract, IP_Awake
		{
			public void Awake()
			{
				BattleSystem.DelayInputAfter(ApplyContract());
			}

			private IEnumerator ApplyContract()
			{
				var enemies = BattleSystem.instance.EnemyTeam.AliveChars;

				if (enemies.Count > 0)
				{
					int randomIndex = RandomManager.RandomInt(RandomClassKey.Enemy, 0, enemies.Count);
					var randomEnemy = enemies[randomIndex];
					randomEnemy.BuffAdd(ModItemKeys.Buff_B_Contract_07_Guild_0, BattleSystem.instance.DummyChar);
				}
				else
				{
					SelfDestroy();
				}

				yield break;
			}
		}

		public class Guild_0 : Contract, IP_Kill
		{
			private int? _killSkillCost = null;

			private int KillSkillCost
			{
				get
				{
					if (_killSkillCost == null)
					{
						var allyTeam = BattleSystem.instance.AllyTeam;
						var damageSkills = allyTeam.Skills_Deck.Concat(allyTeam.Skills)
							.Where(s => s.IsDamage)
							.ToList();

						if (damageSkills.Count > 0)
						{
							var randomSkill = damageSkills[RandomManager.RandomInt(RandomClassKey.AllSkill, 0, damageSkills.Count)];
							_killSkillCost = randomSkill.UsedApNum;
						}
						else
						{
							_killSkillCost = 0;
						}
					}

					return _killSkillCost.Value;
				}
			}

			public override string DescExtended()
			{
				return base.DescExtended().Replace("&a", KillSkillCost.ToString());
			}

			public void KillEffect(SkillParticle SP)
			{
				if (SP.SkillData.UsedApNum != KillSkillCost && SP.TargetChar.Contains(BChar) && BChar.IsDead)
				{
					BattleSystem.DelayInput(RemoveContract());
				}
			}

			private IEnumerator RemoveContract()
			{
				var kazuma = BattleSystem.instance.AllyTeam.AliveChars.FirstOrDefault(x => x.Info.KeyData == ModItemKeys.Character_Kazuma);

				if (kazuma?.BuffReturn(ModItemKeys.Buff_B_Contract_07_Guild, false) != null)
				{
					kazuma?.BuffRemove(ModItemKeys.Buff_B_Contract_07_Guild, true);
				}
				yield break;
			}
		}

		public class LandLord : Contract, IP_SkillUse_User
		{
			public void KillEffect(SkillParticle SP)
			{
				bool isHaveDebuffs = BChar.GetBuffs(BattleChar.GETBUFFTYPE.ALL, false, false).Count > 0;

				if (!isHaveDebuffs && SP.TargetChar.Contains(BChar) && BChar.IsDead)
				{
					SelfDestroy();
				}
			}

			public void SkillUse(Skill SkillD, List<BattleChar> Targets)
			{
				if (SkillD.Master == BChar && SkillD.IsDamage)
				{
					SelfDestroy();
				}
			}
		}
	}
}
