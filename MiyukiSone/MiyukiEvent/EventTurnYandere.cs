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
using System.IdentityModel.Tokens;
using System.Collections;
using JetBrains.Annotations;

namespace MiyukiSone
{
	public partial class EventTurn
	{
		private static readonly List<Action> YandereTurnActions = new List<Action>()
		{
			() => ChangeGold(-250),
			() => ChangeSoulstones(-1),
			() => ChangeRelicBarNum(-1),
			() => ChangeInventoryNum(-2),
			ChangeEnemiesActions,
			ChangeSkillUpgrade,
			RemoveItems,
			DiscardSkill,
			ChangeAlliesHp,
			CurseRandomEquip,
		};

		public static void YandereAction()
		{
			List<Action> actions = YandereTurnActions.ToList();
			if (MiyukiData.LastYandereTurnAction != -1 && actions.Count > 0) actions.RemoveAt(MiyukiData.LastYandereTurnAction);
			int randomIndex = RandomManager.RandomInt("RandomYandereAction", 0, actions.Count);
			actions[randomIndex].Invoke();
			MiyukiData.LastYandereTurnAction = randomIndex;
		}

		public static void YandereActionCut()
		{
			List<Action> actions = new List<Action> { () => ChangeGold(-250), () => ChangeSoulstones(-1), () => ChangeRelicBarNum(-1), () => ChangeInventoryNum(-2), RemoveItems, CurseRandomEquip }; 
			int randomIndex = RandomManager.RandomInt("RandomYandereAction", 0, actions.Count);
			actions[randomIndex].Invoke();
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

		public static void DiscardSkill()
		{
			var skill = AllyTeam.Skills.Where(s => s.Master.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("RandomSkill");
			if (skill == null) return;
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(b => b.Waste()), ScriptLocalization.System_SkillSelect.WasteSkill, false, true, true, false, true));
			BattleSystem.DelayInputAfter(UpdateUI());
		}

		private static void CurseRandomEquip()
		{
			PartyInventory.InvenM.InventoryItems.Where(e => e != null).OfType<Item_Equip>().ToList().Random("MiyukiRandomEquip")?.Let(e => EquipCurse.RandomCurse(e));
			//PartyInventory.InvenM.InventoryItems.Where(e => e != null).OfType<Item_Equip>().ToList().ForEach(e => e.Curse = EquipCurse.RandomCurse(e));
			//PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki && a.Equip != null)?.SelectMany(c => c.Equip).OfType<Item_Equip>().ToList().ForEach(e => e.Curse = EquipCurse.RandomCurse(e));
			//PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki).SelectMany(c => c.Equip).OfType<Item_Equip>().Select(e => { e.Curse = EquipCurse.RandomCurse(e); return e; }).ToList();
		}

		private static void DebuffAlly()
		{
			AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("RandomAlly").AddBuff(ModItemKeys.Buff_B_Miyuki_Debuff_Ally);
		}
	}
}
