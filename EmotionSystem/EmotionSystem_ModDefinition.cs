using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using ChronoArkMod.ModData;
using EmotionSystem;
using TileTypes;
namespace EmotionSystem
{
	public class EmotionSystem_ModDefinition : ModDefinition
	{
		public override Type ModItemKeysType => typeof(ModItemKeys);
		public override List<object> BattleSystem_ModIReturn()
		{
			var list = base.BattleSystem_ModIReturn();

			list.Add(new ModIReturn());

			if (Utils.EnemyEmotions)
			{
				list.Add(Guests.TeamLevel.Instance);
			}
			return list;
		}

		public class ModIReturn : IP_BattleStart_UIOnBefore, IP_BattleStart_Ones, IP_EnemyAwake, IP_PlayerTurn
		{
			public void BattleStart(BattleSystem Ins)
			{
				if (Utils.EmotionSystemTutorial)
				{
					StartTutorial();
				}

				if (Utils.AllyEmotions)
				{
					Utils.AddBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level);
					Utils.AllyTeam.AliveChars.ForEach(a => Utils.AddBuff(a, ModItemKeys.Buff_B_Investigator_Emotional_Level));
					Utils.UnlockSkillPreview(false, false, false, true);
				}

				var whetstone = PlayData.TSavedata.Inventory.Find(i => i != null && i.itemkey == ModItemKeys.Item_Consume_C_EmotionSystem_Whetstone);

				if (whetstone != null && Utils.Data.WhetstoneCharge < 2)
				{
					Utils.Data.WhetstoneCharge++;
				}
			}

			public void BattleStartUIOnBefore(BattleSystem Ins)
			{
				if (Utils.AllyEmotions)
				{
					CreateEGOButton();
				}
			}

			public void EnemyAwake(BattleChar Enemy)
			{
				if (Utils.EnemyEmotions)
				{
					Utils.AddBuff(Enemy, ModItemKeys.Buff_B_Guest_Emotional_Level);
				}
			}

			public void Turn()
			{
				if (!Utils.DistortedBosses) return;

				if (BattleSystem.instance.BossBattle)
				{
					BattleSystem.instance.AllyTeam.WaitCount += 2;
				}

				foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars)
				{
					if (e is BattleEnemy enemy && enemy.Boss)
					{
						if (DataStore.Instance.Guest.GuestCurses.Any(curse => enemy.BuffReturn(curse) != null))
						{
							return;
						}
						BattleSystem.DelayInputAfter(CurseSelection(enemy));
					}
				}
			}


			public IEnumerator CurseSelection(BattleEnemy boss)
			{
				if (boss == null) yield break;

				List<Skill> list = new List<Skill>();
				var curseList = new List<string>(DataStore.Instance.Guest.CurseSelectionList);

				int countToAdd = Mathf.Min(2, curseList.Count); // how much add curses to selection

				for (int i = 0; i < countToAdd; i++)
				{
					int randomIndex = RandomManager.RandomInt(RandomClassKey.Curse, 0, curseList.Count);
					string key = curseList[randomIndex];
					curseList.RemoveAt(randomIndex);
					var skill = Skill.TempSkill(key, Utils.DummyChar, Utils.DummyChar.MyTeam);
					if (skill == null || skill.MySkill == null) continue;

					list.Add(skill);
				}

				if (list.Count > 0)
				{
					BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, button => ApplyCurse(button, boss), ModLocalization.EmotionSystem_Distortion_Selection, false, false, true, false, true));
				}
			}

			private void ApplyCurse(SkillButton button, BattleEnemy boss)
			{
				if (button == null || button.Myskill?.MySkill == null || boss == null) return;

				string key = button.Myskill.MySkill.KeyID;

				if (DataStore.Instance.Guest.CurseMap.TryGetValue(key, out string curse))
				{
					Utils.AddBuff(boss, curse); // Apply the selected curse buff to the boss

					if (curse == ModItemKeys.Buff_B_Guest_Distortion_0)
					{
						Utils.AddBuff(boss, ModItemKeys.Buff_B_Guest_Distortion_0_0);
					}

					if (curse == ModItemKeys.Buff_B_Guest_Distortion_2)
					{
						Utils.AddBuff(boss, GDEItemKeys.Buff_B_Armor_P_1);
					}
					else if (curse == ModItemKeys.Buff_B_Guest_Distortion_3)
					{
						BattleSystem.instance.AllyTeam.AP -= 1;
					}
					else if (curse == ModItemKeys.Buff_B_Guest_Distortion_5)
					{
						Utils.AddBuff(boss, GDEItemKeys.Buff_B_Blockdebuff);
					}
				}

				button.Myskill.isExcept = true;
			}

			private void CreateEGOButton()
			{
				Transform parent = BattleSystem.instance.ActWindow.transform;

				var floorType = DataStore.LibraryFloor.CurrentFloorType;

				// Проверяем — включена ли "Chibi Angela"
				var setType = Utils.ChibiAngela
					? DataStore.VisualUi.EGOUi.SpriteSetType.Angela
					: DataStore.Instance.Visual.EGOButton.GetSetForFloor(floorType);

				var defaultType = DataStore.Instance.Visual.EGOButton.GetDefault(setType);
				var visualData = DataStore.Instance.Visual.EGOButton.GetData(setType, defaultType.Value);

				Sprite sprite = Utils_UI.GetSprite(visualData.Path);
				GameObject egoButton = Utils_UI.CreateUIImage("EGO_Button", parent, sprite, visualData.Size, visualData.Position, true);

				var egoSystem = egoButton.AddComponent<EmotionSystem_EGO_Button>();
				egoButton.AddComponent<EmotionSystem_EGO_Button_Script>();
				EmotionSystem_EGO_Button.instance = egoSystem;

				egoButton.SetActive(true);
			}

			public static void StartTutorial()
			{
				var tutorial = Utils_UI.GetAssets<Tutorial>("Assets/ModAssets/EmotionSystemTutorial.asset", "tutorial");

				if (tutorial != null)
				{
					var obj = UIManager.InstantiateActiveAddressable(UIManager.inst.AR_TutorialUI, AddressableLoadManager.ManageType.Stage).GetComponent<TutorialObject>();
					obj.transform.Find("Window").localPosition += new Vector3(0, 100, 0);
					obj.transform.Find("Window/VideoImage").localPosition += new Vector3(0, 60, 0);
					obj.transform.Find("Window/TextPos").localPosition += new Vector3(0, 120, 0);
					obj.gameObject.AddComponent<TutorialLocalizer>().Init(obj);
					obj.Init(tutorial);
					Utils.EmotionSystemTutorial = false;
				}
			}
		}
	}
}