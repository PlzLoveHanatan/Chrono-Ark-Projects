using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using static MiyukiSone.Affection;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public static class EventRandom
	{
		public static void PawWithGame()
		{
			Bs.StartCoroutine(PawWithGameCo());
		}

		private static IEnumerator PawWithGameCo()
		{
			yield return new WaitForSeconds(1.5f);
			UIManager.InstantiateActiveAddressable(UIManager.inst.AR_PauseUI, AddressableLoadManager.ManageType.None);
			if (UnityEngine.Random.Range(0, 2) == 1 && Pd.StageNum > 0)
			{
				UIManager.inst.StartCoroutine(PauseWindow.RestartCo());
			}
			else
			{
				PauseWindow.QuitGameDel();
			}
		}

		public static void RestartCurrentStage()
		{
			BattleSystem.instance.StartCoroutine(RestartCurrentStageCo());
		}

		private static IEnumerator RestartCurrentStageCo()
		{
			BattleSystem.instance.BattleEnd(true, false);
			FieldSystem.instance.ClearMap();
			string stageKey;
			switch (PlayData.TSavedata.StageNum)
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
			ChangeAffectionPoints(5);
		}

		private static void UsingConsumbales()
		{
			ChangeAffectionPoints(1);
		}

		private static void Using101()
		{
			ChangeAffectionPoints(2);
		}

		private static void AddUpgradeOnSkill()
		{

		}

		private static void AddDowngradeOnSkill()
		{

		}
	}
}
