using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsScripts;
using static MiyukiSone.EventData;
using static MiyukiSone.Affection;
using GameDataEditor;
using I2.Loc;

namespace MiyukiSone
{
	public partial class EventTurn
	{
		private static readonly List<Action> YandereTurnActions = new List<Action>()
		{
			ChangeGold,
			ChangeSoulstones,
			ChangeRelicBarNum,
			ChangeEnemiesActions,
			ChangeSkillUpgrade,
			RemoveItems,
			DiscardSkill,
			DamageAllies
		};

		public static void YandereAction()
		{
			List<Action> actions = YandereTurnActions.ToList();
			if (MiyukiData.LastYandereTurnAction != -1 && actions.Count > 0) actions.RemoveAt(MiyukiData.LastYandereTurnAction);
			int randomIndex = RandomManager.RandomInt("RandomYandereAction", 0, actions.Count);
			actions[randomIndex].Invoke();
			MiyukiData.LastYandereTurnAction = randomIndex;
		}

		private static void RemoveItems()
		{
			var items = PlayData.TSavedata.Inventory?.Where(i => i != null).ToList();

			if (items != null && items.Count > 0)
			{
				var target = items.Random("MiyukiItemRemove");
				PartyInventory.InvenM?.DelItem(target);
				PartyInventory.Ins?.UpdateInvenUI();
			}
		}

		private static void DiscardSkill()
		{
			var skill = AllyTeam.Skills.Where(s => s.Master.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("RandomSkill");
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(b => b.Waste()), ScriptLocalization.System_SkillSelect.WasteSkill, false, true, true, false, true));
		}

		private static void DamageAllies()
		{
			int damage = PlayData.TSavedata.StageNum * 4;
			bool isPainDamage = AllyTeam.AliveChars.Any(a => a.BuffReturn(GDEItemKeys.Buff_B_Queen_10_T, false) == null);
			AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().ForEach(a => { TakeNonLethalDamage(a, damage, isPainDamage); });
		}

		private static void DebuffAlly()
		{
			AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("RandomAlly").AddBuff(ModItemKeys.Buff_B_Miyuki_Debuff_Ally);
		}

		private static void ChangeAllyHp()
		{
			//AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("MiyukiRandom").Dead();
			AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("MiyukiRandom").HP = -99;
		}
	}
}
