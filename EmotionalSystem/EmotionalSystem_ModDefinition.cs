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
using EmotionalSystem;
using static EmotionalSystemBuff.EmotionsEnemy;
namespace EmotionalSystem
{
	public class EmotionalSystem_ModDefinition : ModDefinition
	{
		public override Type ModItemKeysType => typeof(ModItemKeys);
		public override List<object> BattleSystem_ModIReturn()
		{
			var list = base.BattleSystem_ModIReturn();

			list.Add(new ModIReturn());

			if (Utils.EnemyEmotions)
			{
				list.Add(EnemyTeamLevel.Instance);
			}
			return list;
		}

		public class ModIReturn : IP_BattleStart_UIOnBefore, IP_BattleStart_Ones, IP_EnemyAwake
		{
			public void BattleStart(BattleSystem Ins)
			{
				if (Utils.EmotionalSystemTutorial)
				{
					//StartTutorial();
				}

				if (Utils.AllyEmotions)
				{
					Utils.AddBuff(Utils.AllyTeam.LucyAlly, ModItemKeys.Buff_B_Lucy_Emotional_Level);
					Utils.AllyTeam.AliveChars.ForEach(a => Utils.AddBuff(a, ModItemKeys.Buff_B_Ally_Emotional_Level));
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
					Utils.AddBuff(Enemy, ModItemKeys.Buff_B_Enemy_Emotional_Level);
				}
			}

			//public void BattleEnd()
			//{
			//	if (PlayData.TSavedata.StageNum == 4 && Utils.BossInvitations && InvitationManager.Instance.SetWaves)
			//	{
			//		FieldSystem instance = FieldSystem.instance;
			//		instance.BattleAfterDelegate = (FieldSystem.BattleAfterDel)Delegate.Combine(instance.BattleAfterDelegate, new FieldSystem.BattleAfterDel(() =>
			//		InvitationManager.Instance.StartNewReception(GDEItemKeys.EnemyQueue_Queue_S3_Reaper)));
			//	}
			//}

			private void CreateEGOButton()
			{
				// Родительский объект (окно действий)
				Transform parent = BattleSystem.instance.ActWindow.transform;

				// Создаём кнопку
				GameObject egoButton = Utils_Ui.CreatGameObject("EGO_Button", parent);
				if (egoButton == null)
				{
					return;
				}

				egoButton.transform.SetParent(parent);
				egoButton.transform.localPosition = new Vector2(-324.6328f, 300.5991f);

				// Добавляем и настраиваем изображение
				Image image = egoButton.AddComponent<Image>();
				Sprite sprite = Utils_Ui.GetSprite("EGO_Active.png");
				if (sprite == null)
				{
					return;
				}

				image.sprite = sprite;
				Utils_Ui.ImageResize(image, new Vector2(160f, 160f), new Vector2(-324.6328f, 300.5991f));

				// Добавляем логику EGO кнопки
				EmotionalSystem_EGO_Button egoSystem = egoButton.AddComponent<EmotionalSystem_EGO_Button>();
				egoButton.AddComponent<EmotionalSystem_EGO_Button_Script>();

				// Сохраняем ссылку на экземпляр
				EmotionalSystem_EGO_Button.instance = egoSystem;

				egoButton.SetActive(true);
			}

			public static void StartTutorial()
			{
				var tutorial = Utils_Ui.GetAssets<Tutorial>("Assets/ModAssets/EmotionalSystemTutorial.asset", "tutorial");

				if (tutorial != null)
				{
					var obj = UIManager.InstantiateActiveAddressable(UIManager.inst.AR_TutorialUI, AddressableLoadManager.ManageType.Stage).GetComponent<TutorialObject>();
					obj.transform.Find("Window").localPosition += new Vector3(0, 100, 0);
					obj.transform.Find("Window/VideoImage").localPosition += new Vector3(0, 60, 0);
					obj.transform.Find("Window/TextPos").localPosition += new Vector3(0, 120, 0);
					obj.gameObject.AddComponent<TutorialLocalizer>().Init(obj);
					obj.Init(tutorial);
					Utils.EmotionalSystemTutorial = false;
				}
			}
		}
	}
}