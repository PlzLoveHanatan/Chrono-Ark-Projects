using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using ChronoArkMod.ModData.Settings;
using static BattleChar;
using System.Reflection;
using Steamworks;
using static EmotionalSystemBuff.EmotionsAlly;
using EmotionalSystem;

namespace EmotionalSystemBuff
{
	public class EmotionsEnemy
	{
		public class EmotionalLevelEnemy : Buff, IP_Awake, IP_DamageTake, IP_DealDamage, IP_Heal_User, IP_EmotionLvUpBefore, IP_PlayerTurn, IP_SomeOneDead, IP_SkillUse_User, IP_Dodge
		{
			public CharEmotion Emotion;

			private int EmotionsGainThisTurn;
			public bool EmotionBlock;

			private bool IsSummon;

			public static List<int> CoinsToLevelUp = new List<int> { 3, 3, 5, 7, 9 };

			public static GameObject EmotionPrefab
			{
				get
				{
					return EmotionalLevelAlly.EmotionPrefab;
				}
				set
				{
					EmotionalLevelAlly.EmotionPrefab = value;
				}
			}

			public void Awake()
			{
				IsSummon = true;
				EmotionBlock = false;
				EmotionsGainThisTurn = 0;

				if (BChar is BattleEnemy e && e.Boss && Utils.BossInvitations)
				{
					EnemyTeamLevel.Instance.LastEmotionalLevel = 0;
					EnemyTeamLevel.Instance.AbnormalityList.Clear();
					EnemyTeamLevel.Instance.AbnormalityList.AddRange(DataStore.Instance.Enemies.AbnormalityEnemy);
				}

				Emotion = BChar.UI.transform.GetChild(0)?.GetComponentInChildren<CharEmotion>();
				if (Emotion != null) return;

				if (EmotionPrefab == null)
				{
					EmotionPrefab = Utils_Ui.GetAssets<GameObject>("Assets/ModAssets/EmotionUI.prefab", "emotionsystemunityassetbundle");
					if (EmotionPrefab == null)
					{
						Debug.Log("Assets cannot be loaded");
						return;
					}
				}

				var emotionUI = Object.Instantiate(EmotionPrefab, BChar.UI.transform.GetChild(0));
				emotionUI.transform.localPosition = new Vector3(-40f, -30f, 0f);
				Emotion = emotionUI.GetComponent<CharEmotion>();
				int startLevel = EnemyTeamLevel.Instance.HighestEmotionLevel;
				Emotion.Init(BChar, startLevel, CoinsToLevelUp);
				for (int i = 1; i <= startLevel; i++)
				{
					EmotionLvUp(Emotion, i);
				}
				emotionUI.SetActive(true);
				if (BChar is BattleEnemy enemy)
				{
					var turnObj = enemy.MyUIObject.transform.Find("EnemyTurnList");
					if (turnObj != null) turnObj.localPosition += new Vector3(0f, -30f, 0f);
				}

				IsSummon = false;
				Utils.UnlockSkillPreview(false, true);
			}

			public void Turn()
			{
				Emotion.CanGetCoin = true;
				EmotionsGainThisTurn = 0;
			}

			public void SkillUse(Skill SkillD, List<BattleChar> Targets)
			{
				if (EmotionBlock) return;

				if (SkillD.Master == BChar && SkillD.IsDamage)
				{
					BChar.GetPosEmotion();
				}
				else
				{
					BChar.GetNegEmotion();
				}
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (EmotionBlock) return;

				if (Dmg > 0)
				{
					BChar.GetNegEmotion();
				}
			}

			public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
			{
				if (EmotionBlock) return;

				if (Damage >= 1)
				{
					BChar.GetPosEmotion();
				}

				if (IsCri)
				{
					BChar.GetPosEmotion();
				}
			}

			public int Heal_User(BattleChar Target, int HealNum)
			{
				if (HealNum > 0 && !EmotionBlock)
				{
					BChar.GetPosEmotion();
				}
				return HealNum;
			}

			public void SomeOneDead(BattleChar DeadChar)
			{
				if (BChar.Info.Ally == DeadChar.Info.Ally && DeadChar != BChar && !EmotionBlock)
				{
					for (int i = 0; i < 3; i++)
					{
						BChar.GetNegEmotion();
					}
				}

				//if (DeadChar == BChar && DeadChar.EmotionLevel() >= 4 && DeadChar is BattleEnemy enemy && enemy.Boss)
				//{
				//	//int randomNum = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 3);
				//	BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, 1));
				//}
			}

			public void Dodge(BattleChar Char, SkillParticle SP)
			{
				if (EmotionBlock) return;

				if (Char == BChar)
				{
					BChar.GetPosEmotion();
				}
			}

			public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
			{
				if (charEmotion.BChar == BChar)
				{
					EmotionsGainThisTurn++;

					switch (nextLevel)
					{
						case 4:
							GainEmotionalBuffs(BChar, 4);
							GainEmotionalHeal(BChar);
							break;

						case 5:
							GainEmotionalBuffs(BChar, 5);
							GainEmotionalHeal(BChar);
							break;

						default:
							GainEmotionalHeal(BChar);
							GainEmotionalBuffs(BChar);
							break;
					}

					if (EmotionBlock)
					{
						charEmotion.CanGetCoin = false;
					}

					else if (EmotionsGainThisTurn >= 2)
					{
						charEmotion.CanGetCoin = false;
					}
				}
			}

			public void GainEmotionalHeal(BattleChar bchar)
			{
				if (IsSummon) return;

				float percentage = 0.1f;

				if (bchar is BattleEnemy enemy && enemy.Boss)
				{
					percentage = 0.025f;
				}

				int heal = (int)(bchar.GetStat.maxhp * percentage);

				BattleSystem.DelayInput(Utils.HealingParticle(bchar, Utils.DummyChar, heal, true, true));
			}

			public void GainEmotionalBuffs(BattleChar bchar, int level = 0)
			{
				Utils.AddBuff(bchar, ModItemKeys.Buff_B_Enemy_Light);

				if (level == 4)
				{
					Utils.AddBuff(BChar, ModItemKeys.Buff_B_Enemy_Dice);
				}

				if (level == 5)
				{
					var buff = Utils.GetOrAddBuff(bchar, Utils.DummyChar, ModItemKeys.Buff_B_Enemy_Light);
					(buff as Light).eternal = true;
				}
			}
		}

		public class Dice : Buff, IP_EnemyNewTurn // Additional action
		{
			public void EnemyNewTurn()
			{
				if (BChar is BattleEnemy enemy)
				{
					Skill skill;

					if (DataStore.Instance.Enemies.BossActions.ContainsKey(BChar.Info.KeyData))
					{
						// predefined action for original game's enemies
						var actions = DataStore.Instance.Enemies.BossActions[BChar.Info.KeyData];
						var action = actions.Random(BChar.GetRandomClass().SkillSelect);
						skill = Skill.TempSkill(action, BChar, BChar.MyTeam);
					}
					else if (enemy.Boss)
					{
						// predefined action for undefined bosses (likely mod bosses)
						skill = Skill.TempSkill(ModItemKeys.Skill_S_EmotionalSystem_DummyHeal, BChar, BChar.MyTeam);
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
		}

		public class Light : Buff, IP_DamageChange
		{
			public bool eternal = false;
			public override string DescExtended()
			{
				int damage = BChar.EmotionLevel() * 5;
				var str = base.DescExtended().Replace("&a", damage.ToString());
				if (eternal)
				{
					int index = str.IndexOf("\n");
					return str.Substring(0, index);
				}
				return str;
			}

			public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
			{
				if (!eternal && !View && SkillD.IsDamage)
				{
					BattleSystem.DelayInputAfter(DelayDestory());
				}
				int damage = BChar.EmotionLevel() * 5;
				return (int)Misc.PerToNum(Damage, 100 + damage);
			}

			private IEnumerator DelayDestory()
			{
				SelfDestroy();
				yield break;
			}
		}

		public class EnemyTeamLevel : Buff, IP_EnemyNewTurn, IP_EmotionLvUpBefore
		{
			private static EnemyTeamLevel _Instance;

			public static EnemyTeamLevel Instance
			{
				get
				{
					if (BattleSystem.instance == null) return null;
					if (_Instance == null || _Instance.BS == null)
					{
						_Instance = new EnemyTeamLevel(BattleSystem.instance);
					}
					return _Instance;
				}
			}


			public int LastEmotionalLevel;
			public int HighestEmotionLevel;
			public BattleSystem BS;

			public EnemyTeamLevel(BattleSystem bs)
			{
				LastEmotionalLevel = 0;
				HighestEmotionLevel = 0;
				BS = bs;
				AbnormalityList.Clear();
				AbnormalityList.AddRange(DataStore.Instance.Enemies.AbnormalityEnemy);
			}

			public List<Abnormality> AbnormalityList = new List<Abnormality>();

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
						var selectFrom = availableList.FindAll(abno => abno.Type == (isPos ? AbnoType.Pos : AbnoType.Neg));
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

			private List<Abnormality> FilterAbnormalitiesByLevel(List<Abnormality> list, int level)
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

			public List<Abnormality> AvailableAbnormalities(List<BattleChar> characters)
			{
				if (characters == null || characters.Count == 0)
				{
					return new List<Abnormality>();
				}

				var bosses = characters.OfType<BattleEnemy>().Where(e => e.Boss).ToList();
				var normals = characters.OfType<BattleEnemy>().Where(e => !e.Boss).ToList();
				var result = new HashSet<Abnormality>();

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
						if (DataStore.Instance.Enemies.BannedBossAbnormality.TryGetValue(bc.Info.KeyData, out var bannedList))
						{
							return bannedList.Contains(abnoName);
						}

					}
					else
					{
						if (DataStore.Instance.Enemies.BannedEnemyAbnormality.TryGetValue(bc.Info.KeyData, out var bannedList))
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
