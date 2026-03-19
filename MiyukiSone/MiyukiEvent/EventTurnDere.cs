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
			ChangeGold,
			ChangeSoulstones,
			ChangeRelicBarNum,
			ChangeEnemiesActions,
			ChangeInventoryNum,
			ChangeSkillUpgrade
		};

		public static void DereAction()
		{
			List<Action> actions = DereTurnActions.ToList();
			if (MiyukiData.LastDereTurnAction != -1 && actions.Count > 0) actions.RemoveAt(MiyukiData.LastDereTurnAction);
			int randomIndex = RandomManager.RandomInt("RandomDereAction", 0, actions.Count);
			actions[randomIndex].Invoke();
			MiyukiData.LastDereTurnAction = randomIndex;
		}
	}
}
