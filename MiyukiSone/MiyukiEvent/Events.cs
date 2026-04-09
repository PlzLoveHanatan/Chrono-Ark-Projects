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
using ChronoArkMod;

namespace MiyukiSone
{
	public static class Events
	{
		#region Data & Constructors
		private static readonly List<Action> DereTurnActions = new List<Action>()
		{
			() => ChangeGold(250),
			() => ChangeSoulstones(1),
			() => ChangeRelicBarNum(1),
			() => ChangeInventoryNum(2),
			ChangeEquipSlots,
			ChangeEnemiesActions,
			ChangeSkillUpgrade,
			ChangeAlliesHp,
			KillRandomEnemy,
			//ReviveAllies
		};

		private static readonly List<Action> YandereTurnActions = new List<Action>()
		{
			() => ChangeGold(-250),
			() => ChangeSoulstones(-1),
			() => ChangeRelicBarNum(-1),
			() => ChangeInventoryNum(-2),
			ChangeEquipSlots,
			ChangeEnemiesActions,
			ChangeSkillUpgrade,
			RemoveItems,
			DiscardSkill,
			ChangeAlliesHp,
			CurseRandomEquip,
			KillRandomAlly,
		};
		#endregion

		public static void MiyukiTurnAction()
		{
			if (!IsKuudere) return;
			(IsDere ? (Action)DereAction : YandereAction)();
			SaveManager.savemanager.Save();
		}

		#region Event Dere
		public static void DereAction()
		{
			DereTurnActions.RandomElement()?.Invoke();
		}

		private static void KillRandomEnemy()
		{
			var finalEnemy = BattleSystem.instance.EnemyTeam.AliveChars.Where(e => e != null && e is BattleEnemy enemy && !enemy.Boss).RandomElement();
			if (finalEnemy == null) return;
			Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_KillEnemy, MiyukiBchar, MiyukiBchar.MyTeam);
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(b => finalEnemy.Dead()), "I love YOU that much!", false, false, true, false, true));
		}
		#endregion

		#region Basic Miyuki Actions
		private static void ChangeGold(int amount)
		{
			PlayData.TSavedata._Gold += amount;
		}

		private static void ChangeSoulstones(int amount)
		{
			PlayData.TSavedata._Soul += amount;
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
			if (PartyInventory.InvenM == null || PlayData.TSavedata.Inventory.Count <= 0) return;

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

		private static void ChangeEquipSlots()
		{
			if (IsDere) PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki).RandomElement()?.Equip?.Add(null);
			else PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki).RandomElement()?.Equip?.Remove(null);		
		}

		private static void ChangeEnemiesActions()
		{
			if (IsDere) UtilsScripts.RemoveActions(BattleSystem.instance.EnemyTeam.AliveChars_Vanish);
			else BattleSystem.instance.EnemyTeam.AliveChars_Vanish.ForEach(e => e.BuffAdd(ModItemKeys.Buff_B_Miyuki_Enemy_ExtraAction, DummyChar));
		}

		public static void ChangeSkillUpgrade()
		{
			var skill = BattleSystem.instance.AllyTeam.Skills.Where(s => s != null && s.CharinfoSkilldata.SKillExtended == null && s.MySkill.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && !s.isExcept && !s.Master.IsLucy && s.Master.Info.KeyData != ModItemKeys.Character_Miyuki).RandomElement();
			if (skill == null) return;
			string text = IsDere ? ModLocalization.MiyukiUpgrade : ModLocalization.MiyukiDowngrade;
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, ChangeUpgrade, text, false, true, true, false, true));
		}

		private static void ChangeUpgrade(SkillButton button)
		{
			if (button.Myskill == null) return;
			if (MiyukiDecides) button.Myskill.CelestialUpgrade();
			else button.Myskill.NormalUpgrade();
			BattleSystem.DelayInputAfter(UpdateUI());
		}

		private static IEnumerator UpdateUI()
		{
			yield return null;
			BattleSystem.instance?.ActWindow.Draw(BattleSystem.instance.AllyTeam, false);
		}

		private static void ChangeAlliesHp()
		{
			int amount = PlayData.TSavedata.StageNum * 4;
			if (IsDere)
			{
				BattleSystem.instance.AllyTeam.AliveChars.ForEach(a => a.Heal(DummyChar, amount, false));
			}
			else
			{
				bool isPainDamage = BattleSystem.instance.AllyTeam.AliveChars.Any(a => a.BuffReturn(GDEItemKeys.Buff_B_Queen_10_T, false) == null);
				BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().ForEach(a => UtilsScripts.TakeNonLethalDamage(a, amount, isPainDamage));
			}

		}
		#endregion

		#region Event Yandere
		public static void YandereAction()
		{
			YandereTurnActions.RandomElement()?.Invoke();
		}

		public static void YandereActionCut()
		{
			List<Action> actions = new List<Action>
			{
				() => ChangeGold(-250),
				() => ChangeSoulstones(-1),
				() => ChangeRelicBarNum(-1),
				() => ChangeInventoryNum(-2),
				ChangeEquipSlots,
				RemoveItems,
				CurseRandomEquip
			};
			actions.RandomElement()?.Invoke();
		}

		private static void RemoveItems()
		{
			var item = PlayData.TSavedata.Inventory?.RandomElement();
			if (item != null)
			{
				PartyInventory.InvenM?.DelItem(item);
				PartyInventory.Ins?.UpdateInvenUI();
			}
		}

		private static void DiscardSkill()
		{
			var skill = BattleSystem.instance.AllyTeam.Skills.Where(s => s != null && s.Master.Info.KeyData != ModItemKeys.Character_Miyuki).RandomElement();
			if (skill == null) return;
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(b => b.Waste()), ScriptLocalization.System_SkillSelect.WasteSkill, false, true, true, false, true));
			BattleSystem.DelayInputAfter(UpdateUI());
		}

		public static void CurseRandomEquip()
		{
			PartyInventory.InvenM.InventoryItems.Where(e => e != null).OfType<Item_Equip>().RandomElement()?.Let(e => EquipCurse.RandomCurse(e));
			//PartyInventory.InvenM.InventoryItems.Where(e => e != null).OfType<Item_Equip>().ToList().ForEach(e => e.Curse = EquipCurse.RandomCurse(e));
			//PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki && a.Equip != null)?.SelectMany(c => c.Equip).OfType<Item_Equip>().ToList().ForEach(e => e.Curse = EquipCurse.RandomCurse(e));
			PlayData.TSavedata.Party.Where(a => a.KeyData != ModItemKeys.Character_Miyuki).SelectMany(c => c.Equip).OfType<Item_Equip>().RandomElement()?.Let(e => e.Curse = EquipCurse.RandomCurse(e));
		}

		public static void KillRandomAlly()
		{
			var ally = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != null && a.Info.KeyData != ModItemKeys.Character_Miyuki).RandomElement();
			if (ally == null) return;
			Skill skill = Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Special_KillAlly, MiyukiBchar, MiyukiBchar.MyTeam);
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(new List<Skill> { skill }, new SkillButton.SkillClickDel(b => ally.Dead()), "Why YOU hate Me so much?", false, false, true, false, true));
		}

		private static void DebuffAlly()
		{
			BattleSystem.instance.AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("RandomAlly").AddBuff(ModItemKeys.Buff_B_Miyuki_Debuff_Ally);
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
			if (BattleSystem.instance != null) BattleSystem.instance.StartCoroutine(RestartStageCo(stageNum));
			else FieldSystem.instance?.StartCoroutine(RestartStageCo(stageNum));
		}

		private static IEnumerator RestartStageCo(int stageNum = 0)
		{
			MiyukiTextEvent();
			BattleSystem.instance?.BattleEnd(false, false);
			FieldSystem.instance?.ClearMap();

			string stageKey;

			if (stageNum != 0)
			{
				switch (stageNum)
				{
					case 1: stageKey = GDEItemKeys.Stage_Stage1_1; break;
					case 2: stageKey = GDEItemKeys.Stage_Stage1_2; break;
					case 3: stageKey = GDEItemKeys.Stage_Stage2_1; break;
					case 4: stageKey = GDEItemKeys.Stage_Stage2_2; break;
					case 5: stageKey = GDEItemKeys.Stage_Stage3; break;
					case 6: stageKey = GDEItemKeys.Stage_Stage4; break;
					default: stageKey = GDEItemKeys.Stage_Stage1_1; break;
				}
				PlayData.TSavedata.NowStageMapKey = stageKey;			
				SaveManager.savemanager?.Save();
				yield return new WaitForSeconds(0.5f);
				FieldSystem.instance.StageStart(stageKey);
			}
			else
			{
				SaveManager.savemanager?.Save();
				yield return new WaitForSeconds(0.5f);
				string currentKey = PlayData.TSavedata.NowStageMapKey;

				if (string.IsNullOrEmpty(currentKey))
				{
					FieldSystem.instance.StageStart("");
				}
				if (currentKey == "MasterMap")
				{
					FieldSystem.instance.GoMasterSDMap();
				}
				else if (currentKey == "MasterMap2")
				{
					FieldSystem.instance.GoMasterSDMap2();
				}
				else if (currentKey == "MasterMap3")
				{
					FieldSystem.instance.GoMasterSDMap3();
				}
				else if (currentKey == "MasterMapEnding")
				{
					FieldSystem.instance.GoMasterFinal3DMap_3();
				}
				else if (currentKey == "MasterMap_LastBattleAfter1")
				{
					FieldSystem.instance.GoMasterFinal3DMap_LastBattleAfter(1);
				}
				else if (currentKey == "MasterMap_PMFirstSee")
				{
					FieldSystem.instance.StartCoroutine(FieldSystem.instance.GoMasterFinal3DMap_1());
				}
				else if (currentKey == "ClockTower")
				{
					FieldSystem.instance.StartCoroutine(FieldSystem.instance.GoClockTower());
				}
				else if (currentKey == "MonoMap1")
				{
					FieldSystem.instance.GoMonoMap1();
				}
				else if (PlayData.GetIsCampKey(currentKey))
				{
					FieldSystem.instance.CampfireMap();
				}
				else
				{
					FieldSystem.instance.StageStart(currentKey);
				}
			}
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
