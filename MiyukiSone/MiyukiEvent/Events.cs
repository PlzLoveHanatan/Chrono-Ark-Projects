using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Security.X509.Extensions;
using static MiyukiSone.EventsData;
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
	public static class Events
	{
		#region Data & Constructors

		private static readonly List<string> DerePositiveBuffKeys = new List<string>()
		{

		};

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
			KillRandomAlly,
		};

		private static readonly List<Action> DereTurnActions = new List<Action>()
		{
			() => ChangeGold(250),
			() => ChangeSoulstones(1),
			() => ChangeRelicBarNum(1),
			() => ChangeInventoryNum(2),
			ChangeEnemiesActions,
			ChangeSkillUpgrade,
			ChangeAlliesHp,
			KillRandomEnemy,
			//ReviveAllies
		};
		#endregion

		public static void MiyukiTurnAction()
		{
			if (!IsKuudere) return;
			(IsDere ? (Action)DereAction : YandereAction)();
		}

		#region Event Dere
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
			var finalEnemy = Utils.EnemyTeam.AliveChars.Where(e => e != null && e is BattleEnemy enemy && !enemy.Boss).ToList().Random();
			if (finalEnemy == null) return;
			Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_KillEnemy, MiyukiBchar, MiyukiBchar.MyTeam);
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(b => finalEnemy.Dead()), "I love YOU that much!", false, false, true, false, true));
		}

		//public static void ReviveAllies()
		//{
		//	BattleSystem.instance.AllyList.FindAll(a => a.Info.Incapacitated).Select(a => { a.Info.Incapacitated = false; a.Info.Hp = a.Info.get_stat.maxhp; return a; }).ToList();
		//}
		#endregion

		#region Basic Miyuki Actions
		private static void ChangeGold(int amount)
		{
			Pd._Gold += amount;
		}

		private static void ChangeSoulstones(int amount)
		{
			Pd._Soul += amount;
		}

		private static void ChangeRelicBarNum(int amount)
		{
			if (IsDere)
			{
				for (int i = PlayData.TSavedata.ArkPassivePlus; i < PlayData.TSavedata.ArkPassivePlus + amount; i++)
				{
					PlayData.TSavedata.Passive_Itembase.Add(null);
				}
			}
			else
			{
				for (int i = 0; i < amount; i++)
				{
					if (PlayData.TSavedata.Passive_Itembase.Count > 0)
					{
						PlayData.TSavedata.Passive_Itembase.RemoveAt(PlayData.TSavedata.Passive_Itembase.Count - 1);
					}
				}
			}

			PlayData.TSavedata.ArkPassivePlus += amount;
			if (UIManager.NowActiveUI is ArkPartsUI) UIManager.NowActiveUI.Delete();
		}

		private static void ChangeInventoryNum(int amount)
		{
			if (PartyInventory.InvenM == null)
			{
				Debug.LogWarning("[Miyuki] PartyInventory.InvenM is null, skipping inventory change");
				return;
			}

			if (IsDere)
			{
				int currentSize = PlayData.TSavedata.Inventory.Count;
				int newSize = currentSize + amount;
				PlayData.MaxInventory = newSize;
				PlayData.TSavedata.MaxinventoryNumPlus += amount;

				for (int i = 0; i < amount; i++)
				{
					PlayData.TSavedata.Inventory.Add(null);
				}

				PartyInventory.Ins?.UpdateInvenUI();
			}
			else
			{
				PartyInventory.InvenM.ChangeMaxInventoryNum(amount, new List<string>());
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
			if (skill == null) return;
			string text = IsDere ? "Select skill to obtain special upgrade" : "Select skill to obtain downgrade";
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, ChangeUpgrade, text, false, true, true, false, true));
		}

		private static void ChangeUpgrade(SkillButton button)
		{
			if (button.Myskill == null) return;
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

		private static void AddSkillIntoData()
		{

		}
		#endregion

		#region Event Yandere
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

		private static void DiscardSkill()
		{
			var skill = AllyTeam.Skills.Where(s => s.Master.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("RandomSkill");
			if (skill == null) return;
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(b => b.Waste()), ScriptLocalization.System_SkillSelect.WasteSkill, false, true, true, false, true));
			BattleSystem.DelayInputAfter(UpdateUI());
		}

		public static void CurseRandomEquip()
		{
			PartyInventory.InvenM.InventoryItems.Where(e => e != null).OfType<Item_Equip>().ToList().Random("MiyukiRandomEquipCurse")?.Let(e => EquipCurse.RandomCurse(e));
			//PartyInventory.InvenM.InventoryItems.Where(e => e != null).OfType<Item_Equip>().ToList().ForEach(e => e.Curse = EquipCurse.RandomCurse(e));
			//PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki && a.Equip != null)?.SelectMany(c => c.Equip).OfType<Item_Equip>().ToList().ForEach(e => e.Curse = EquipCurse.RandomCurse(e));
			PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki).SelectMany(c => c.Equip).OfType<Item_Equip>().ToList().Random("MiyukiRandomEquipCurse")?.Let(e => e.Curse = EquipCurse.RandomCurse(e));
		}

		public static void KillRandomAlly()
		{
			var ally = AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random();
			if (ally == null) return;
			Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_KillAlly, MiyukiBchar, MiyukiBchar.MyTeam);
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(b => ally.Dead()), "Why YOU hate Me so much?", false, false, true, false, true));
		}

		private static void DebuffAlly()
		{
			AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("RandomAlly").AddBuff(ModItemKeys.Buff_B_Miyuki_Debuff_Ally);
		}
		#endregion

		#region Event Special
		public static void RestartRun()
		{
			if (BattleSystem.instance != null) BattleSystem.instance.StartCoroutine(RestartRunCo());
			else FieldSystem.instance?.StartCoroutine(RestartRunCo());
		}

		private static IEnumerator RestartRunCo()
		{
			yield return new WaitForSeconds(1.5f);
			UIManager.InstantiateActiveAddressable(UIManager.inst.AR_PauseUI, AddressableLoadManager.ManageType.None);
			yield return UIManager.inst.StartCoroutine(PauseWindow.RestartCo());
			//PauseWindow.ReturnToMainDel();
		}

		public static IEnumerator ExitGame()
		{
			SaveManager.savemanager.Save();
			yield return new WaitForSeconds(2f);
			UIManager.InstantiateActiveAddressable(UIManager.inst.AR_PauseUI, AddressableLoadManager.ManageType.None);
			PauseWindow.QuitGameDel();
			yield return new WaitForSeconds(1.5f);
			if (UIManager.inst.ArkPartUI == null) UnityEngine.Application.Quit();
		}

		public static void RestartStage(int stageNum = 0)
		{
			BattleSystem.instance.StartCoroutine(RestartStageCo(stageNum));
		}

		private static IEnumerator RestartStageCo(int stageNum = 0)
		{
			BattleSystem.instance?.BattleEnd(false, false);
			FieldSystem.instance?.ClearMap();

			string stageKey;
			stageNum = stageNum == 0 ? Pd.StageNum : stageNum;
			switch (stageNum)
			{
				case 0: stageKey = GDEItemKeys.Stage_Stage1_1; break;
				case 1: stageKey = GDEItemKeys.Stage_Stage1_2; break;
				case 2: stageKey = GDEItemKeys.Stage_Stage2_1; break;
				case 3: stageKey = GDEItemKeys.Stage_Stage2_2; break;
				case 4: stageKey = GDEItemKeys.Stage_Stage3; break;
				case 5: stageKey = GDEItemKeys.Stage_Stage4; break;
				default: stageKey = GDEItemKeys.Stage_Stage1_1; break;
			}

			FieldSystem.instance.StageStart(stageKey);
			yield return new WaitForSeconds(0.5f);
			SaveManager.savemanager?.Save();

		}

		private static void RecievingGift()
		{

		}

		private static void UsingConsumbales()
		{

		}

		private static void Using101()
		{

		}
		#endregion
	}
}
