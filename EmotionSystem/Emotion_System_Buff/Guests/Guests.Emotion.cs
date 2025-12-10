using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Guests
	{
		public class Emotion
		{
			public class Level : Buff, IP_Awake, IP_DamageTake, IP_DealDamage, IP_Heal_User, IP_EmotionLvUpBefore, IP_PlayerTurn, IP_SomeOneDead, IP_SkillUse_User, IP_Dodge
			{
				public CharEmotion Emotion;

				private int EmotionsGainThisTurn;
				public bool EmotionBlock;
				public bool UnlimitedEmotions;

				private bool IsSummon;

				public static List<int> CoinsToLevelUp = new List<int> { 3, 3, 5, 7, 9 };

				public static GameObject EmotionPrefab
				{
					get
					{
						return Investigators.Emotion.Level.EmotionPrefab;
					}
					set
					{
						Investigators.Emotion.Level.EmotionPrefab = value;
					}
				}

				public void Awake()
				{
					IsSummon = true;
					EmotionBlock = false;
					UnlimitedEmotions = false;
					EmotionsGainThisTurn = 0;

					if (BChar is BattleEnemy e && e.Boss && Utils.BossInvitations)
					{
						TeamLevel.Instance.LastEmotionalLevel = 0;
						TeamLevel.Instance.AbnormalityList.Clear();
						TeamLevel.Instance.AbnormalityList.AddRange(DataStore.Instance.Guest.Abnormality);
					}

					Emotion = BChar.UI.transform.GetChild(0)?.GetComponentInChildren<CharEmotion>();
					if (Emotion != null) return;

					if (EmotionPrefab == null)
					{
						EmotionPrefab = Utils_UI.GetAssets<GameObject>("Assets/ModAssets/EmotionUI.prefab", "emotionsystemunityassetbundle");
						if (EmotionPrefab == null)
						{
							Debug.Log("Assets cannot be loaded");
							return;
						}
					}

					var emotionUI = UnityEngine.Object.Instantiate(EmotionPrefab, BChar.UI.transform.GetChild(0));
					emotionUI.transform.localPosition = new Vector3(-40f, -30f, 0f);
					Emotion = emotionUI.GetComponent<CharEmotion>();
					int startLevel = TeamLevel.Instance.HighestEmotionLevel;
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
						else if (UnlimitedEmotions)
						{
							charEmotion.CanGetCoin = true;
						}
						else if (EmotionsGainThisTurn >= Utils.LevelsPerTurn)
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
						percentage = 0.05f;
					}

					int heal = (int)(bchar.GetStat.maxhp * percentage);

					//BattleSystem.DelayInput(Utils.HealingParticle(bchar, Utils.DummyChar, heal, true, true));
				}

				public void GainEmotionalBuffs(BattleChar bchar, int level = 0)
				{
					Utils.AddBuff(bchar, ModItemKeys.Buff_B_Guest_Light);

					if (level == 4)
					{
						Utils.AddBuff(BChar, ModItemKeys.Buff_B_Guest_Dice);
					}

					if (level == 5)
					{
						var buff = Utils.GetOrAddBuff(bchar, Utils.DummyChar, ModItemKeys.Buff_B_Guest_Light);
						(buff as Light).eternal = true;
					}
				}
			}
		}
	}
}
