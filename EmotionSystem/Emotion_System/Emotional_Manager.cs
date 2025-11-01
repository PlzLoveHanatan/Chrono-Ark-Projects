using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using UnityEngine;

namespace EmotionSystem
{
	public static class EmotionalManager
	{
		public static GameObject EmotionTrajectoryPos;
		public static GameObject EmotionTrajectoryNeg;

		public static void GiveEmotionsToChar(BattleChar character, int amount, Vector3? source = null)
		{
			for (int i = 0; i < amount; i++)
			{
				character.GetRandomEmotion(source);
			}
		}

		public static void GiveEmotionsToAllies(int amount, Vector3? source = null)
		{
			foreach (var ally in BattleSystem.instance.AllyTeam.AliveChars)
			{
				GiveEmotionsToChar(ally, amount, source);
			}
		}

		public static void GetEmotion(this BattleChar bc, Vector3? source, bool isPos)
		{
			var emotion = bc.MyEmotion();
			if (emotion == null) return;
			if (Utils.EmotionPointsParticles && source != null)
			{
				GameObject prefab;
				if (isPos)
				{
					if (EmotionTrajectoryPos == null)
					{
						EmotionTrajectoryPos = Utils_Ui.GetAssets<GameObject>("Assets/ModAssets/EmotionTrajectoryPos.prefab", "emotionsystemunityassetbundle");
					}
					prefab = EmotionTrajectoryPos;
				}
				else
				{
					if (EmotionTrajectoryNeg == null)
					{
						EmotionTrajectoryNeg = Utils_Ui.GetAssets<GameObject>("Assets/ModAssets/EmotionTrajectoryNeg.prefab", "emotionsystemunityassetbundle");
					}
					prefab = EmotionTrajectoryNeg;
				}
				if (prefab == null)
				{
					emotion.ObtainCoin(isPos);
					return;
				}
				var trajectory = UnityEngine.Object.Instantiate(prefab, bc.UI.transform.GetChild(0));
				trajectory.GetComponent<EmotionTrajectory>().Init(source.Value, emotion);
				trajectory.SetActive(true);
				//Debug.Log("Emotion coin from " + source.Value + " to " + emotion.transform.position);
				return;
			}
			else
			{
				emotion.ObtainCoin(isPos);
			}
		}

		public static void GetPosEmotion(this BattleChar bc, Vector3? source = null, int emotion = 1)
		{
			for (int i = 0; i < emotion; i++)
			{
				GetEmotion(bc, source, true);
			}
		}

		public static void GetNegEmotion(this BattleChar bc, Vector3? source = null, int emotion = 1)
		{
			for (int i = 0; i < emotion; i++)
			{
				GetEmotion(bc, source, false);
			}
		}

		public static void GetRandomEmotion(this BattleChar bc, Vector3? source = null, int emotion = 1)
		{
			for (int i = 0; i < emotion; i++)
			{
				GetEmotion(bc, source, RandomManager.RandomPer("EmotionCoin", 2, 1));
			}
		}

		public static CharEmotion MyEmotion(this BattleChar bc)
		{
			if (bc is BattleAlly)
			{
				var buff = bc.BuffReturn(ModItemKeys.Buff_B_Investigator_Emotional_Level) as Investigators.Emotion.Level;
				return buff?.Emotion;
			}
			else if (bc is BattleEnemy)
			{
				var buff = bc.BuffReturn(ModItemKeys.Buff_B_Guest_Emotional_Level) as Guests.Emotion.Level;
				return buff?.Emotion;
			}
			return null;
		}

		public static void SetEmotionCapInvestigator(this BattleChar bc, bool isEmotionCap = false)
		{
			if (Utils.ReturnBuff(bc, ModItemKeys.Buff_B_Investigator_Emotional_Level) is Investigators.Emotion.Level buff)
			{
				buff.EmotionsCap = isEmotionCap;
			}
		}

		public static void SetEmotionCapGuest(this BattleChar bc, bool isEmotionCap = true)
		{
			if (Utils.ReturnBuff(bc, ModItemKeys.Buff_B_Guest_Emotional_Level) is Guests.Emotion.Level buff)
			{
				buff.EmotionBlock = isEmotionCap;
			}
		}

		public static void AddEmotionLevel(this BattleChar bc, bool isForceLevelUp = false)
		{
			var emotion = bc.MyEmotion();
			var emotionalLevel = Utils.ReturnBuff(bc, ModItemKeys.Buff_B_Investigator_Emotional_Level) as Investigators.Emotion.Level;

			if (emotionalLevel != null)
			{
				if (isForceLevelUp)
				{
					emotionalLevel.EmotionsCap = false;
					emotion.CanGetCoin = true;
				}
				else if (emotionalLevel.EmotionsGainThisTurn >= 2)
				{
					return;
				}
			}

			int currentLevel = emotion.Level;
			int coinsNeeded = Investigators.Emotion.Level.CoinsToLevelUp[currentLevel];

			for (int i = 0; i < coinsNeeded; i++)
			{
				bc.GetPosEmotion();
			}

			if (emotionalLevel != null)
			{
				emotionalLevel.EmotionsCap = true;
			}	
		}

		public static void ResetEmotionTurn(this BattleChar bc, int amount = 1)
		{
			var emotion = bc.MyEmotion();

			if (emotion == null)
			{
				return;
			}

			// Reset turn-based emotion counters if the ally emotion buff is active
			if (Utils.ReturnBuff(bc, ModItemKeys.Buff_B_Investigator_Emotional_Level) is Investigators.Emotion.Level buff)
			{
				buff.EmotionsGainThisTurn -= amount;
			}

			// Allow the character to gain emotion coins again
			emotion.CanGetCoin = true;
		}

		public static int EmotionLevel(this BattleChar bc)
		{
			var emotion = bc.MyEmotion();
			if (emotion == null) return 0;
			return emotion.Level;
		}

		public static Vector3? GetPosUI(this Skill skill)
		{
			if (skill.MyButton != null)
			{
				return skill.MyButton.transform.position;
			}
			if (skill.BasicSkillButton != null)
			{
				return skill.BasicSkillButton.transform.position;
			}
			return null;
		}
		public static Vector3? GetPosUI(this BattleChar bc)
		{
			if (bc.Dummy || bc.IsLucyNoC) return null;
			if (bc is BattleAlly ally)
			{
				return ally.GetPos();
			}
			if (bc is BattleEnemy enemy)
			{
				try
				{
					var UICamera = UIManager.inst.UIcamera;
					var FieldCamera = BattleSystem.instance.battlecamera.ObjectCam;
					var enemyPos = enemy.GetPos();
					var posScreen = FieldCamera.WorldToScreenPoint(enemyPos);
					return UICamera.ScreenToWorldPoint(posScreen);
				}
				catch
				{
					return null;
				}
			}
			return null;
		}
	}
}
