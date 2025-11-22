using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Investigators
	{
		public class Emotion
		{
			public class Level : Buff, IP_SkillUse_User, IP_PlayerTurn, IP_Dodge, IP_DamageTake, IP_Healed, IP_Awake, IP_DealDamage, IP_Kill, IP_SomeOneDead, IP_EmotionLvUpBefore
			{
				public CharEmotion Emotion;

				public int EmotionsGainThisTurn;
				public bool EmotionsCap;
				public bool InvertPoints;

				public static GameObject EmotionPrefab;
				public int EmotionLevel => Emotion.Level;

				public static List<int> CoinsToLevelUp = new List<int> { 3, 3, 5, 7, 9 };

				private void GainPosOrNegPoints(Vector3? pos = null, int amount = 1)
				{
					if (InvertPoints)
					{
						BChar.GetNegEmotion(pos, amount);
					}
					else
					{
						BChar.GetPosEmotion(pos, amount);
					}
				}

				public void Awake()
				{
					EmotionsGainThisTurn = 0;
					EmotionsCap = true;

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

					var emotionUI = UnityEngine.Object.Instantiate(EmotionPrefab, BChar.UI.transform.GetChild(0));
					emotionUI.transform.localPosition = new Vector3(40f, -25f, 0f);
					Emotion = emotionUI.GetComponent<CharEmotion>();
					Emotion.Init(BChar, 0, CoinsToLevelUp);
					emotionUI.SetActive(true);
				}

				public void Turn()
				{
					EmotionsGainThisTurn = 0;
					Emotion.CanGetCoin = true;

					if (EmotionLevel >= 4)
					{
						BChar.BuffAdd(ModItemKeys.Buff_B_Investigator_ManaReduction, BChar, false, 0, false, -1, false);
					}

					if (EmotionLevel >= 5)
					{
						BChar.BuffAdd(ModItemKeys.Buff_B_Investigator_Draw, BChar, false, 0, false, -1, false);
					}
				}

				public override void Init()
				{
					OnePassive = true;
				}

				public void SomeOneDead(BattleChar DeadChar)
				{
					if (DeadChar != BChar && DeadChar.Info.Ally)
					{
						var pos = DeadChar.GetPosUI();
						BChar.GetNegEmotion(pos, 3);
					}
				}

				public void SkillUse(Skill SkillD, List<BattleChar> Targets)
				{
					if (SkillD.Master == BChar)
					{
						if (SkillD.IsHeal || SkillD.IsDamage)
						{
							GainPosOrNegPoints(SkillD.GetPosUI());
						}
						else
						{
							BChar.GetNegEmotion(SkillD.GetPosUI());
						}
					}
				}

				public void KillEffect(SkillParticle SP)
				{
					if (SP.UseStatus == BChar)
					{
						var target = SP.TargetChar.First(bc => bc.IsDead);
						var pos = target?.GetPosUI();
						GainPosOrNegPoints(pos, 3);
					}
				}

				public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
				{
					if (Damage >= 1)
					{
						GainPosOrNegPoints(BChar.GetPosUI());
					}

					if (IsCri)
					{
						GainPosOrNegPoints(BChar.GetPosUI());
					}
				}

				public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
				{
					if (Dmg >= 1)
					{
						BChar.GetNegEmotion(User.GetPosUI());
						BattleSystem.DelayInput(DeathDoorCheck());

						if (Cri)
						{
							BChar.GetNegEmotion(User.GetPosUI());
						}
					}
				}

				private IEnumerator DeathDoorCheck()
				{
					if (Utils.ReturnBuff(BChar, GDEItemKeys.Buff_B_Neardeath) != null)
					{
						foreach (var ally in BChar.MyTeam.AliveChars_Vanish)
						{
							ally.GetNegEmotion();
						}
					}
					yield break;
				}

				public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
				{
					if (HealedChar == BChar)
					{
						GainPosOrNegPoints(Healer.GetPosUI());		
					}
				}

				public void Dodge(BattleChar Char, SkillParticle SP)
				{
					if (Char == BChar)
					{
						GainPosOrNegPoints(Char.GetPosUI());
					}
				}

				public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
				{
					string addedBuffKey = addedbuff.BuffData.Key;

					//if (ModBuffs.Contains(addedBuffKey)) return;

					if (BuffTaker == BChar && addedbuff.BuffData.Debuff)
					{
						BChar.GetNegEmotion(BuffUser.GetPos());
					}
					if (BuffTaker == BChar && !addedbuff.BuffData.Debuff)
					{
						BChar.GetPosEmotion(BuffUser.GetPos());
					}
				}

				public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
				{
					if (charEmotion.BChar == BChar)
					{
						EmotionsGainThisTurn++;

						if (!EmotionsCap)
						{
							charEmotion.CanGetCoin = true;
						}

						else if (EmotionsGainThisTurn >= 2)
						{
							charEmotion.CanGetCoin = false;
						}
					}
				}
			}
		}
	}
}
