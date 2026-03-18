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
				PauseWindow.ReturnToMainDel();
			}
			else
			{
				PauseWindow.QuitGameDel();
			}
		}

		public static IEnumerator ExitGame()
		{
			SaveManager.savemanager.Save();
			yield return new WaitForSeconds(2f);
			UIManager.InstantiateActiveAddressable(UIManager.inst.AR_PauseUI, AddressableLoadManager.ManageType.None);
			PauseWindow.QuitGameDel();
		}

		public static void RestartCurrentRun()
		{
			BattleSystem.instance.StartCoroutine(RestartCurrentStageCo(0));
		}

		public static void RestartCurrentStage(int stageNum = 0)
		{
			BattleSystem.instance.StartCoroutine(RestartCurrentStageCo(stageNum));
		}

		private static IEnumerator RestartCurrentStageCo(int stageNum = 0)
		{
			BattleSystem.instance.BattleEnd(true, false);
			FieldSystem.instance.ClearMap();
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
	}
}
