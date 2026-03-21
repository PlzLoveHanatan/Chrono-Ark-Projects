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
using System.Collections;
using Spine;

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
			if (!MiyukiDecides) return;
			(IsDere ? (Action)DereAction : YandereAction)();
		}


		#region Basic Miyuki Actions
		private static void ChangeGold(int amount)
		{
			Pd._Gold += MiyukiResult(amount);
		}

		private static void ChangeSoulstones(int amount)
		{
			Pd._Soul += MiyukiResult(amount);
		}

		private static void ChangeRelicBarNum(int amount)
		{
			//for (int i = PlayData.TSavedata.ArkPassivePlus; i < PlayData.TSavedata.ArkPassivePlus + num; i++)
			//{
			//	PlayData.TSavedata.Passive_Itembase.Add(null);
			//}
			PlayData.TSavedata.ArkPassivePlus += MiyukiResult(amount);
			if (UIManager.NowActiveUI is ArkPartsUI) UIManager.NowActiveUI.Delete();
		}

		private static void ChangeInventoryNum(int amount)
		{
			if (IsDere)
			{
				PartyInventory.InvenM.ChangeMaxInventoryNum(amount);
				PartyInventory.InvenM.CreateInven(PartyInventory.InvenM.InventoryItems.Count + 2);
				PartyInventory.InvenM.ItemUpdateFromInven();
			}
			else
			{
				PartyInventory.InvenM.ChangeMaxInventoryNum(amount);
			}
		}

		private static void ChangeEnemiesActions()
		{
			if (IsDere) UtilsScripts.RemoveActions(Utils.EnemyTeam.AliveChars_Vanish);
			else Utils.EnemyTeam.AliveChars_Vanish.ForEach(e => e.BuffAdd(ModItemKeys.Buff_B_Miyuki_Enemy_ExtraAction, DummyChar));
		}

		public static void ChangeSkillUpgrade()
		{
			var skill = AllyTeam.Skills.Where(s => s != null && s.CharinfoSkilldata.SKillExtended == null && s.MySkill.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && !s.isExcept).ToList().Random();
			string text = IsDere ? "Select skill to obtain special upgrade" : "Select skill to obtain downgrade";
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, ChangeUpgrade, text, false, true, true, false, true));
		}

		private static void ChangeUpgrade(SkillButton button)
		{
			if (IsDere) button.Myskill.CelestialUpgrade();
			else button.Myskill.NormalUpgrade(true);
			BattleSystem.DelayInputAfter(UpdateUI());
		}

		private static IEnumerator UpdateUI()
		{
			yield return null;
			Bs.ActWindow.Draw(AllyTeam, false);
		}

		private static void ChangeAlliesHp()
		{
			int amount = PlayData.TSavedata.StageNum * 4;
			if (IsDere)
			{
				AllyTeam.AliveChars.ForEach(a => a.Heal(DummyChar, amount, false));
			}
			else
			{
				bool isPainDamage = AllyTeam.AliveChars.Any(a => a.BuffReturn(GDEItemKeys.Buff_B_Queen_10_T, false) == null);
				AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().ForEach(a => UtilsScripts.TakeNonLethalDamage(a, amount, isPainDamage));
			}
			
		}
	}

	#endregion
}
