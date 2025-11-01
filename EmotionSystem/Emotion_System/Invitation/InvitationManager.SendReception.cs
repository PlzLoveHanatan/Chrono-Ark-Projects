using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using GameDataEditor;
using UnityEngine;

namespace EmotionSystem
{
	public partial class InvitationManager
	{
		public void SendReception(BattleSystem system)
		{
			string currentBoss = system.MainQueueData.Key;

			if (currentBoss == GDEItemKeys.EnemyQueue_Queue_FanaticBoss)
			{
				ClearWaves(system);
				return;
			}

			if (system == null || system.MainQueueData == null || !ReceptionChains.ContainsKey(currentBoss))
			{
				return;
			}

			GuestSequence sequence = ReceptionChains[currentBoss];

			if (!string.IsNullOrEmpty(sequence.FirstGuest))
			{
				FirstGuestInvitation = true;

				if (!string.IsNullOrEmpty(sequence.SecondGuest))
				{
					SecondGuestInvitation = true;

					if (RandomManager.RandomInt(RandomClassKey.Boss, 0, 2) == 0)
					{
						(sequence.SecondGuest, sequence.FirstGuest) = (sequence.FirstGuest, sequence.SecondGuest);
					}
				}
			}

			FirstGuestTheme = sequence.FirstGuest;

			if (!string.IsNullOrEmpty(sequence.SecondGuest))
			{
				SecondGuestTheme = sequence.SecondGuest;
			}
			else
			{
				SecondGuestTheme = null;
			}

			if (system.EnemyWaveData == null)
			{
				system.EnemyWaveData = new WaveData();
			}

			if (Utils.DistortedBosses)
			{
				sequence.FogTurn += (int)(sequence.FogTurn * 0.3f);
			}

			SetWave(ref system.MainQueueData.Wave2, sequence.FirstGuest, 99, sequence.FogTurn, 2);
			SetWave(ref system.MainQueueData.Wave3, sequence.SecondGuest, 99, sequence.FogTurn, 3);

			if (sequence.FogTurn > 0)
			{
				system.MainQueueData.CustomeFogTurn = sequence.FogTurn;
				system.FogTurn = sequence.FogTurn;
			}

			InvitationActive = !string.IsNullOrEmpty(sequence.FirstGuest);
			RewardMultiplier = string.IsNullOrEmpty(sequence.SecondGuest) ? 1 : 2;
		}

		private List<GDEEnemyData> LoadWave(string bossKey)
		{
			List<GDEEnemyData> result = new List<GDEEnemyData>();

			if (string.IsNullOrEmpty(bossKey) ||
				GDEDataManager.masterData == null ||
				!GDEDataManager.masterData.ContainsKey(bossKey) ||
				!(GDEDataManager.masterData[bossKey] is Dictionary<string, object> data) ||
				!data.ContainsKey("Enemys") ||
				!(data["Enemys"] is List<object> rawList))
			{
				return result;
			}

			foreach (object obj in rawList)
			{
				if (obj is string enemyKey)
				{
					result.Add(new GDEEnemyData(enemyKey));
				}
			}

			return result;
		}

		private void SetWave(ref List<GDEEnemyData> wave, string key, int turn, int fogTurn, int waveIndex)
		{
			if (string.IsNullOrEmpty(key)) return;

			wave = LoadWave(key);
			BattleSystem.instance.MainQueueData.CustomeFogTurn = fogTurn;

			if (waveIndex == 2)
			{
				BattleSystem.instance.EnemyWaveData.wave2turn = turn;
				BattleSystem.instance.EnemyWaveData.wave2out = false;
			}
			else if (waveIndex == 3)
			{
				BattleSystem.instance.EnemyWaveData.wave3turn = turn;
				BattleSystem.instance.EnemyWaveData.wave3out = false;
			}

			Debug.Log($"[Invitation] Wave loaded: {key}");
		}


		private void ClearWaves(BattleSystem system)
		{
			SpecialCase = true;

			system.EnemyWaveData = new WaveData();
			system.MainQueueData.Wave2 = null;
			system.MainQueueData.Wave3 = null;
			RewardMultiplier = 1;

			Debug.Log("[Invitation] Waves cleared, SpecialCase = true");
		}
	}
}
