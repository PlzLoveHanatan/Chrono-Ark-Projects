using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public static class EventHelp
	{
		public static void MiyukiHelpAction()
		{
			List<int> availableIndexes = GetAvailableActions();
			var lastAction = MiyukiData.LastHelpAction;
			if (lastAction != -1 && availableIndexes.Count > 1) availableIndexes.Remove(lastAction);
			int randomIndex = availableIndexes[RandomManager.RandomInt("MiyukiHelpAction", 0, availableIndexes.Count)];
			MiyukiData.LastHelpAction = randomIndex;

			switch (randomIndex)
			{
				case 0: IncreaseBlackFog(); break;
				case 1: RestartBattle(); break;
				case 2: RestartStage(); break;
				case 3: HealAllies(); break;
				case 4: BuffAllies(); break;
				case 5: ReviveAllies(); break;
				case 6: KillRandomEnemy(); break;			
				case 7: DamageEnemies(); break;
				case 8: DebuffEnemies(); break;
				default: break;
			}
		}

		private static List<int> GetAvailableActions()
		{
			List<int> actions = new List<int> { 0, 1, 2 };
			bool deadAllies = AllyTeam.AliveChars?.Where(a => a.IsDead).Count() > 0;
			bool aliveEnemies = Bs.EnemyTeam.AliveChars?.Count() > 0;
			if (!deadAllies) actions.AddRange(new int[] { 3, 4 });
			if (deadAllies) actions.Add(5);
			if (aliveEnemies) actions.AddRange(new int[] { 6, 7, 8 });
			return actions;
		}

		private static void IncreaseBlackFog()
		{

		}

		private static void RestartBattle()
		{

		}

		private static void RestartStage()
		{

		}

		private static void HealAllies()
		{

		}

		private static void BuffAllies()
		{

		}

		private static void ReviveAllies()
		{

		}

		private static void KillRandomEnemy()
		{

		}

		private static void DamageEnemies()
		{

		}

		private static void DebuffEnemies()
		{

		}
	}
}
