using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class DialogueBoxEvent
	{
		public static void KissRejection()
		{
			ChangeAffectionPoints(-5);
		}

		public static IEnumerator RestartStagePreserveProgress()
		{
			BattleSystem.instance.BattleEnd(true, false);
			FieldSystem.instance.ClearMap();
			string stageKey = GetCurrentStageKey(PlayData.TSavedata.StageNum);
			FieldSystem.instance.StageStart(stageKey);
			yield return new WaitForSeconds(0.5f);
			SaveManager.savemanager?.Save();
		}

		private static string GetCurrentStageKey(int stageNum)
		{
			switch (stageNum)
			{
				case 0: return GDEItemKeys.Stage_Stage1_1;
				case 1: return GDEItemKeys.Stage_Stage1_2;
				case 2: return GDEItemKeys.Stage_Stage2_1;
				case 3: return GDEItemKeys.Stage_Stage2_2;
				case 4: return GDEItemKeys.Stage_Stage3;
				case 5: return GDEItemKeys.Stage_Stage4;
				default: return GDEItemKeys.Stage_Stage1_1;
			}
		}

		private static void KillRandomAlly()
		{

		}

		private static void BuffEnemies()
		{

		}

		private static void DebuffAllies()
		{

		}

		private static void RemoveRelic()
		{

		}

		private static void RemoveEquip()
		{

		}

		private static void CurseEquip()
		{

		}
	}
}
