using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using I2.Loc;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public partial class EventTurn
	{
		private static readonly List<Action> DereTurnActions = new List<Action>()
		{
			() => ChangeGold(250),
			() => ChangeSoulstones(1),
			() => ChangeRelicBarNum(1),
			() => ChangeInventoryNum(2),
			ChangeEnemiesActions,
			ChangeSkillUpgrade,
			ChangeAlliesHp,
			//KillRandomEnemy,
			ReviveAllies
		};

		public static void DereAction()
		{
			List<Action> actions = DereTurnActions.ToList();
			if (MiyukiData.LastDereTurnAction != -1 && actions.Count > 0) actions.RemoveAt(MiyukiData.LastDereTurnAction);
			int randomIndex = RandomManager.RandomInt("RandomDereAction", 0, actions.Count);
			actions[randomIndex].Invoke();
			MiyukiData.LastDereTurnAction = randomIndex;
		}

		private static void KillRandomEnemy()
		{
			Utils.EnemyTeam.AliveChars.Where(e => e != null && e is BattleEnemy enemy && !enemy.Boss).ToList().Random("RandomEnemy").Dead();
		}

		private static void ReviveAllies()
		{
			AllyTeam.AliveChars.FindAll(a => a.IsDead).Select(a => { a.IsDead = false; return a; }).ToList();
		}
	}
}
