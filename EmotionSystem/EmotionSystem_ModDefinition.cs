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
namespace EmotionSystem
{
	public class EmotionSystem_ModDefinition : ModDefinition
	{
		public override Type ModItemKeysType => typeof(ModItemKeys);
		public override List<object> BattleSystem_ModIReturn()
		{
			var list = base.BattleSystem_ModIReturn();

			list.Add(new ModIReturn());

			if (Utils.GuestEmotions)
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
					//StartTutorial();
				}

				if (Utils.InvestigatorEmotions)
				{
					Utils.AddBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level);
					Utils.AllyTeam.AliveChars.ForEach(a => Utils.AddBuff(a, ModItemKeys.Buff_B_Investigator_Emotional_Level));
				}
			}

			public void BattleStartUIOnBefore(BattleSystem Ins)
			{
				if (Utils.InvestigatorEmotions)
				{
					CreateEGOButton();
				}
			}

			public void EnemyAwake(BattleChar Enemy)
			{
				if (Utils.GuestEmotions)
				{
					Utils.AddBuff(Enemy, ModItemKeys.Buff_B_Guest_Emotional_Level);
				}
			}

			public void Turn()
			{
				if (Utils.CursedBosses)
				{
					foreach (var e in BattleSystem.instance.EnemyTeam.AliveChars)
					{
						if (e != null && e is BattleEnemy enemy && enemy.Boss)
						{
							if (DataStore.Instance.Guest.GuestCurses.Any(curse => enemy.BuffReturn(curse) != null))
							{
								return;
							}
							BattleSystem.DelayInputAfter(CurseSelection(enemy));
						}
					}
				}
			}



			public IEnumerator CurseSelection(BattleEnemy boss)
			{
				if (boss == null) yield break;

				List<Skill> list = new List<Skill>();
				var curseList = new List<string>(DataStore.Instance.Guest.CurseSelectionList);

				int countToAdd = Mathf.Min(2, curseList.Count);

				for (int i = 0; i < countToAdd; i++)
				{
					int randomIndex = RandomManager.RandomInt(RandomClassKey.Curse, 0, curseList.Count);
					string key = curseList[randomIndex];
					curseList.RemoveAt(randomIndex);

					try
					{
						var skill = Skill.TempSkill(key, boss, boss.MyTeam);
						if (skill == null || skill.MySkill == null)
						{
							Debug.LogWarning($"[CurseSelection] Skill {key} returned null, skipping.");
							continue;
						}

						list.Add(skill);
					}
					catch (Exception ex)
					{
						Debug.LogError($"[CurseSelection] Error while loading skill {key}: {ex.Message}");
						continue;
					}
				}

				if (list.Count > 0)
				{
					BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect
						(list, ApplyCurse, "", false, false, false, true, true));
				}
			}

			private void ApplyCurse(SkillButton button)
			{
				button.Myskill.isExcept = true;
				BattleSystem.instance.StartCoroutine(BattleSystem.instance.ForceAction(button.Myskill, button.Myskill.Master, false, false, true, null));
			}


			private void CreateEGOButton()
			{
				Transform parent = BattleSystem.instance.ActWindow.transform;

				GameObject egoButton = Utils_Ui.CreatGameObject("EGO_Button", parent);
				if (egoButton == null)
				{
					return;
				}

				egoButton.transform.SetParent(parent);
				egoButton.transform.localPosition = new Vector2(-324.6328f, 300.5991f);

				Image image = egoButton.AddComponent<Image>();
				Sprite sprite = Utils_Ui.GetSprite("EGO_Active.png");
				if (sprite == null)
				{
					return;
				}

				image.sprite = sprite;
				Utils_Ui.ImageResize(image, new Vector2(160f, 160f), new Vector2(-324.6328f, 300.5991f));

				EmotionSystem_EGO_Button egoSystem = egoButton.AddComponent<EmotionSystem_EGO_Button>();
				egoButton.AddComponent<EmotionSystem_EGO_Button_Script>();

				EmotionSystem_EGO_Button.instance = egoSystem;

				egoButton.SetActive(true);
			}

			public static void StartTutorial()
			{
				var tutorial = Utils_Ui.GetAssets<Tutorial>("Assets/ModAssets/EmotionSystemTutorial.asset", "tutorial");

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