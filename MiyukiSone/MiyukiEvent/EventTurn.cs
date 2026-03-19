using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Security.X509.Extensions;
using static MiyukiSone.EventData;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using GameDataEditor;
using UnityEngine;
using I2.Loc;
using System.IO.Ports;
using System.Windows.Forms;

namespace MiyukiSone
{
	public partial class EventTurn
	{
		#region Data & Constructors

		private static readonly List<string> DerePositiveBuffKeys = new List<string>()
		{

		};
		#endregion

		public static void MiyukiTurnAction()
		{
			(IsDere ? (Action)DereAction : YandereAction)();
		}


		#region Basic Miyuki Actions
		private static void ChangeGold()
		{
			Pd._Gold += MiyukiResult(250);
		}

		private static void ChangeSoulstones()
		{
			Pd._Soul += MiyukiResult(1);
		}

		private static void ChangeRelicBarNum()
		{
			//for (int i = PlayData.TSavedata.ArkPassivePlus; i < PlayData.TSavedata.ArkPassivePlus + num; i++)
			//{
			//	PlayData.TSavedata.Passive_Itembase.Add(null);
			//}
			PlayData.TSavedata.ArkPassivePlus += MiyukiResult(1);
			if (UIManager.NowActiveUI is ArkPartsUI) UIManager.NowActiveUI.Delete();
		}

		private static void ChangeEnemiesActions()
		{
			if (IsDere) UtilsScripts.RemoveActions(Utils.EnemyTeam.AliveChars_Vanish);
			else Utils.EnemyTeam.AliveChars_Vanish.ForEach(e => e.BuffAdd("", DummyChar));
		}

		private static void ChangeInventoryNum()
		{
			if (IsDere)
			{
				PartyInventory.InvenM.ChangeMaxInventoryNum(2);
				PartyInventory.InvenM.CreateInven(PartyInventory.InvenM.InventoryItems.Count + 2);
				PartyInventory.InvenM.ItemUpdateFromInven();
			}
			else
			{
				PartyInventory.InvenM.ChangeMaxInventoryNum(2);
			}
		}

		private static void ChangeSkillUpgrade()
		{
			var skill = AllyTeam.Skills.Where(s => s.CharinfoSkilldata.SKillExtended == null).ToList().Random();
			string text = IsDere ? "Select skill to obtain special upgrade" : "Select skill to obtain downgrade";
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, ChangeUpgrade, text, false, true, true, false, true));
		}

		private static void ChangeUpgrade(SkillButton button)
		{
			if (IsDere)
			{
				button.Myskill.CelestialUpgrade();
			}
			else
			{
				var upgradeList = PlayData.GetEnforce(true, button.Myskill);
				if (upgradeList.Count > 0)
				{
					var selected = upgradeList.Random("MiyukiRandomDowngrade");
					button.Myskill.CharinfoSkilldata.SKillExtended = selected;
					button.Myskill.ExtendedAdd_Battle(selected);
				}
			}
		}
		#endregion
	}
}
