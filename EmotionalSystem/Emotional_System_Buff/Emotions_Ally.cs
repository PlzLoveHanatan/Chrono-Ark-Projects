using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using EmotionSystem;
using UnityEngine;
using static EmotionalSystemSkillExtended.ExEmotions;

namespace EmotionalSystemBuff
{
	public class EmotionsAlly
	{
		public class Draw : Buff, IP_SkillUseHand_Team
		{
			public int skillUse = 0;

			public override void Init()
			{
				base.Init();
				skillUse = 0;
				LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Ally_Draw);
			}

			public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
			{
				return AddedSkill.ExtendedFind<Ex_Draw>() == null && AddedSkill.Master == BChar;
			}

			public void SKillUseHand_Team(Skill skill)
			{
				if (skill.Master == this.BChar)
				{
					skillUse++;

					if (skillUse >= 2)
					{
						BattleSystem.instance.AllyTeam.Draw(1);
						SelfDestroy();
					}
				}
			}
		}

		public class ManaReduction : Buff, IP_SkillUseHand_Team
		{
			public override void Init()
			{
				base.Init();
				LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Ally_ManaReduction);
			}

			public void SKillUseHand_Team(Skill skill)
			{
				if (skill.Master == this.BChar)
				{
					SelfDestroy();
				}
			}

			public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
			{
				return AddedSkill.ExtendedFind<Ex_ManaReduction>() == null && AddedSkill.Master == BChar;
			}
		}

		public class EmotionalLevelAlly : Buff, IP_SkillUse_User, IP_PlayerTurn, IP_Dodge, IP_DamageTake, IP_Healed, IP_Awake, IP_DealDamage, IP_Kill, IP_SomeOneDead, IP_EmotionLvUpBefore
		{
			public CharEmotion Emotion;

			public int EmotionsGainThisTurn;
			public bool EmotionsCap;

			public static GameObject EmotionPrefab;
			public int EmotionalLevel => Emotion.Level;

			public static List<int> CoinsToLevelUp = new List<int> { 3, 3, 5, 7, 9 };

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

				if (EmotionalLevel >= 4)
				{
					BChar.BuffAdd(ModItemKeys.Buff_B_Ally_ManaReduction, BChar, false, 0, false, -1, false);
				}

				if (EmotionalLevel >= 5)
				{
					BChar.BuffAdd(ModItemKeys.Buff_B_Ally_Draw, BChar, false, 0, false, -1, false);
				}
			}

			public override void Init()
			{
				base.Init();
				OnePassive = true;
			}

			public void SomeOneDead(BattleChar DeadChar)
			{
				if (BChar.Info.Ally == DeadChar.Info.Ally && DeadChar != BChar)
				{
					var pos = DeadChar.GetPosUI();
					for (int i = 0; i < 3; i++)
					{
						BChar.GetNegEmotion(pos);
					}
				}
			}

			public void SkillUse(Skill SkillD, List<BattleChar> Targets)
			{
				if (SkillD.IsHeal || SkillD.IsDamage && SkillD.Master == BChar)
				{
					BChar.GetPosEmotion(SkillD.GetPosUI());
				}

				else if (SkillD.Master == BChar)
				{
					BChar.GetNegEmotion(SkillD.GetPosUI());
				}
			}

			public void KillEffect(SkillParticle SP)
			{
				if (SP.UseStatus == BChar)
				{
					var target = SP.TargetChar.First(bc => bc.IsDead);
					var pos = target?.GetPosUI();
					for (int i = 0; i < 3; i++)
					{
						BChar.GetPosEmotion(pos);
					}
				}
			}

			public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
			{
				if (Damage >= 1)
				{
					BChar.GetPosEmotion(BChar.GetPosUI());
				}

				if (IsCri)
				{
					BChar.GetPosEmotion(BChar.GetPosUI());
				}
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (Dmg >= 1)
				{
					BChar.GetNegEmotion(User.GetPosUI());

					if (Cri)
					{
						BChar.GetNegEmotion(User.GetPosUI());
					}
				}
			}

			public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
			{
				if (HealedChar == BChar)
				{
					BChar.GetPosEmotion(Healer.GetPosUI());
				}
			}

			public void Dodge(BattleChar Char, SkillParticle SP)
			{
				if (Char == BChar)
				{
					BChar.GetPosEmotion(SP.UseStatus.GetPosUI());
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
