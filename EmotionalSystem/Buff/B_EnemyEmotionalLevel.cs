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
using EmotionSystem;
using Object = UnityEngine.Object;
using ChronoArkMod.ModData.Settings;
using EmotionalSystem;
using static BattleChar;
using System.Reflection;
namespace EmotionalSystem
{
	public class B_EnemyEmotionalLevel : Buff, IP_Awake, IP_DamageTake, IP_DealDamage, IP_Heal_User, IP_EmotionLvUpBefore, IP_PlayerTurn, IP_SomeOneDead, IP_SkillUse_User, IP_Dodge
	{
		public CharEmotion Emotion;

		private int EmotionsGainThisTurn;

		public static List<int> CoinsToLevelUp = new List<int> { 3, 3, 5, 7, 9 };

		public static GameObject EmotionPrefab
		{
			get
			{
				return B_EmotionalLevel.EmotionPrefab;
			}
			set
			{
				B_EmotionalLevel.EmotionPrefab = value;
			}
		}

		public void Awake()
		{
			EmotionsGainThisTurn = 0;

			Emotion = BChar.UI.transform.GetChild(0)?.GetComponentInChildren<CharEmotion>();
			if (Emotion != null) return;

			if (EmotionPrefab == null)
			{
				EmotionPrefab = Utils.GetAssets<GameObject>("Assets/ModAssets/EmotionUI.prefab", "emotionsystemunityassetbundle");
				if (EmotionPrefab == null)
				{
					Debug.Log("Assets cannot be loaded");
					return;
				}
			}

			var emotionUI = Object.Instantiate(EmotionPrefab, BChar.UI.transform.GetChild(0));
			emotionUI.transform.localPosition = new Vector3(-40f, -30f, 0f);
			Emotion = emotionUI.GetComponent<CharEmotion>();
			int startLevel = B_EnemyTeamEmotionalLevel.Instance.HighestEmotionLevel;
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
		}

		public void Turn()
		{
			Emotion.CanGetCoin = true;
			EmotionsGainThisTurn = 0;
		}

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
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
			if (Dmg > 0)
			{
				BChar.GetNegEmotion();
			}
		}

		public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
		{
			if (IsCri)
			{
				BChar.GetPosEmotion();
			}
		}

		public int Heal_User(BattleChar Target, int HealNum)
		{
			if (HealNum > 0)
			{
				BChar.GetPosEmotion();
			}
			return HealNum;
		}

		public void SomeOneDead(BattleChar DeadChar)
		{
			if (BChar.Info.Ally == DeadChar.Info.Ally && DeadChar != this.BChar)
			{
				var pos = DeadChar.GetPosUI();
				for (int i = 0; i < 3; i++)
				{
					BChar.GetNegEmotion();
				}
			}

			if (DeadChar == this.BChar && DeadChar.EmotionLevel() >= 4 && DeadChar is BattleEnemy enemy && enemy.Boss)
			{
				int randomNum = RandomManager.RandomInt(BattleRandom.PassiveItem, 1, 3);
				BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, randomNum));
			}
		}

		public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
		{
			if (charEmotion.BChar == BChar)
			{
				EmotionsGainThisTurn++;

				switch (nextLevel)
				{
					case 3:
						GainEmotionalBuffs(BChar, 3);
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

				if (EmotionsGainThisTurn >= 2)
				{
					charEmotion.CanGetCoin = false;
				}
			}
		}

		public void GainEmotionalHeal(BattleChar bchar)
		{
			float percentage = 0.2f;

			if (bchar is BattleEnemy enemy && enemy.Boss)
			{
				percentage = 0.1f;
			}

			int heal = (int)(bchar.GetStat.maxhp * percentage);

			BattleSystem.DelayInput(Utils.HealingParticle(bchar, Utils.DummyChar, heal, true, true));
		}

		public void GainEmotionalBuffs(BattleChar bchar, int level = 0)
		{
			Utils.AddBuff(bchar, ModItemKeys.Buff_B_EnemyEmotionalLevel_Light);

			if (level == 3)
			{
				Utils.AddBuff(BChar, ModItemKeys.Buff_B_EnemyEmotionalLevel_Dice);
			}

			if (level == 5)
			{
				var buff = Utils.GetOrAddBuff(bchar, Utils.DummyChar, ModItemKeys.Buff_B_EnemyEmotionalLevel_Light);
				(buff as B_EnemyEmotionalLevel_Light).eternal = true;
			}
		}


		public void Dodge(BattleChar Char, SkillParticle SP)
		{
			if (Char == BChar)
			{
				BChar.GetPosEmotion();
			}
		}
	}
}