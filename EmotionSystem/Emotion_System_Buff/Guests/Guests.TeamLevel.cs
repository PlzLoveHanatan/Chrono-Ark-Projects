using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Guests
	{
		public class TeamLevel : Buff, IP_EnemyNewTurn, IP_EmotionLvUpBefore
		{
			private static TeamLevel _Instance;

			public static TeamLevel Instance
			{
				get
				{
					if (BattleSystem.instance == null) return null;
					if (_Instance == null || _Instance.BS == null)
					{
						_Instance = new TeamLevel(BattleSystem.instance);
					}
					return _Instance;
				}
			}


			public int LastEmotionalLevel;
			public int HighestEmotionLevel;
			public BattleSystem BS;

			public TeamLevel(BattleSystem bs)
			{
				LastEmotionalLevel = 0;
				HighestEmotionLevel = 0;
				BS = bs;
				AbnormalityList.Clear();
				AbnormalityList.AddRange(DataStore.Instance.Guest.Abnormality);
			}

			public readonly List<DataStore.Abnormality> AbnormalityList = new List<DataStore.Abnormality>();

			public int EmotionalLevel
			{
				get
				{
					int sumLevel = 0;
					int sumCharacter = 0;
					foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
					{
						var emotion = enemy.MyEmotion();
						if (emotion != null)
						{
							sumCharacter++;
							sumLevel += emotion.Level;
						}
					}
					if (sumLevel == 0) return 0;
					return sumLevel / sumCharacter;
				}
			}

			public int AllPosCoinNum
			{
				get
				{
					int num = 0;
					foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
					{
						var emotion = enemy.MyEmotion();
						if (emotion != null)
						{
							num += emotion.AccumPosCoin;
						}
					}
					return num;
				}
			}

			public int AllNegCoinNum
			{
				get
				{
					int num = 0;
					foreach (var enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
					{
						var emotion = enemy.MyEmotion();
						if (emotion != null)
						{
							num += emotion.AccumNegCoin;
						}
					}
					return num;
				}
			}

			public void EnemyNewTurn()
			{
				if (EmotionalLevel > LastEmotionalLevel)
				{
					for (int i = LastEmotionalLevel + 1; i <= EmotionalLevel; i++)
					{
						var enemies = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.ToList();
						var currentBoss = enemies.FirstOrDefault(e => e is BattleEnemy enemy && enemy.Boss) as BattleEnemy;

						if (currentBoss != null)
						{
							enemies.RemoveAll(e => e != currentBoss);
						}

						var availableList = AvailableAbnormalities(enemies);
						availableList = FilterAbnormalitiesByLevel(availableList, i);

						if (availableList.Count == 0) break;

						// select: negative or positive
						int pos = AllPosCoinNum;
						int neg = AllNegCoinNum;
						//var isPos = RandomManager.RandomPer("EnemyAbnormality", pos + neg, pos);
						var isPos = RandomManager.RandomPer(BChar.GetRandomClass().Target, pos + neg, pos);
						var selectFrom = availableList.FindAll(abno => abno.Type == (isPos ? DataStore.AbnoType.Pos : DataStore.AbnoType.Neg));
						if (selectFrom.Count == 0)
						{
							selectFrom = availableList.ToList();
						}
						var abnoReceive = selectFrom.Random(BChar.GetRandomClass().SkillSelect);

						// select: enemy target
						var enemyCanGet = enemies.FindAll(enemy => !BannedAbnormality(enemy, abnoReceive.Name));
						if (enemyCanGet.Count == 0) continue; // shouldn't read here
						var enemyTarget = enemyCanGet.Random(BChar.GetRandomClass().SkillSelect);

						enemyTarget.BuffAdd(abnoReceive.Name, enemyTarget);
						AbnormalityList.Remove(abnoReceive);
					}

					LastEmotionalLevel = EmotionalLevel;
				}
			}

			private List<DataStore.Abnormality> FilterAbnormalitiesByLevel(List<DataStore.Abnormality> list, int level)
			{
				switch (level)
				{
					case 1:
						return list.FindAll(a => a.Level == 1);
					case 2:
						return list.FindAll(a => a.Level == 1);
					case 3:
						return list.FindAll(a => a.Level == 2);
					case 4:
						return list.FindAll(a => a.Level == 2);
					case 5:
						return list.FindAll(a => a.Level == 3);
					default:
						return list;
				}
			}

			public List<DataStore.Abnormality> AvailableAbnormalities(List<BattleChar> characters)
			{
				if (characters == null || characters.Count == 0)
				{
					return new List<DataStore.Abnormality>();
				}

				var bosses = characters.OfType<BattleEnemy>().Where(e => e.Boss).ToList();
				var normals = characters.OfType<BattleEnemy>().Where(e => !e.Boss).ToList();
				var result = new HashSet<DataStore.Abnormality>();

				if (bosses.Count > 0)
				{
					foreach (var boss in bosses)
					{
						foreach (var abno in AbnormalityList)
						{
							if (!BannedAbnormality(boss, abno.Name))
							{
								result.Add(abno);
							}
						}
					}
				}
				else
				{
					foreach (var enemy in normals)
					{
						foreach (var abno in AbnormalityList)
						{
							if (!BannedAbnormality(enemy, abno.Name))
							{
								result.Add(abno);
							}
						}
					}
				}

				return result.ToList();
			}


			public static bool BannedAbnormality(BattleChar bc, string abnoName)
			{
				if (bc == null || bc.Info == null) return false;

				if (bc is BattleEnemy enemy)
				{
					if (enemy.Boss)
					{
						if (DataStore.Instance.Guest.BannedBossAbnormality.TryGetValue(bc.Info.KeyData, out var bannedList))
						{
							return bannedList.Contains(abnoName);
						}

					}
					else
					{
						if (DataStore.Instance.Guest.BannedAbnormality.TryGetValue(bc.Info.KeyData, out var bannedList))
						{
							return bannedList.Contains(abnoName);
						}
					}
				}
				return false;
			}


			public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
			{
				if (charEmotion.BChar is BattleEnemy)
				{
					IEnumerator UpdateMaxLevel()
					{
						yield return new WaitForEndOfFrame();
						int levelNow = EmotionalLevel;
						if (levelNow > HighestEmotionLevel) HighestEmotionLevel = levelNow;
						yield break;
					}
					BattleSystem.instance.StartCoroutine(UpdateMaxLevel());
				}
			}
		}
	}
}
