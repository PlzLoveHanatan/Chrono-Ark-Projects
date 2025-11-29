using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using HarmonyLib;
using UnityEngine;
using static EmotionSystem.DataStore.LibraryFloor;

namespace EmotionSystem
{
	public partial class EmotionlSystem_Plugin
	{
		[HarmonyPatch(typeof(ResultUI))]
		[HarmonyPatch(nameof(ResultUI.Init))]
		class ResultScreenPatch
		{
			[HarmonyPostfix]
			static void Postfix(ResultUI __instance)
			{
				if (PlayData.TSavedata.bMist == null)
				{
					PlayData.TSavedata.bMist = new BloodyMist();
					PlayData.TSavedata.bMist.Level = 4;
					PlayData.TSavedata.bMist.Level3_Option = 0;

					__instance.DifficultyObj.SetActive(true);
					__instance.BloodyMistObj.SetActive(true);

					Sprite sprite = AddressableLoadManager.LoadAsyncCompletion<Sprite>(
						new GDEImageDatasData(GDEItemKeys.ImageDatas_Image_BloodyMist).Sprites_Path[3],
						AddressableLoadManager.ManageType.Stage
					);
					__instance.BloodyMistImage.sprite = sprite;

					__instance.BloodyMistText.text = "";
				}

				RectTransform textRect = __instance.BloodyMistText.GetComponent<RectTransform>();
				if (textRect != null)
				{
					textRect.anchoredPosition += new Vector2(0, 80);
				}

				StringBuilder sb = new StringBuilder();

				if (Utils.AllyEmotions)
				{
					sb.AppendLine("+" + ModLocalization.EmotionSystem_Emotions_Ally);
				}

				if (Utils.EnemyEmotions)
				{
					sb.AppendLine("+" + ModLocalization.EmotionSystem_Emotions_Enemy);
				}

				string floorName = GetLibraryFloor();
				if (CurrentFloor != null && !string.IsNullOrEmpty(floorName) && Utils.AllyEmotions)
				{
					sb.AppendLine("+" + floorName);
				}

				if (Utils.BossInvitations)
				{
					sb.AppendLine("+" + ModLocalization.EmotionSystem_Boss_Invitations);
				}

				if (Utils.DistortedBosses)
				{
					sb.AppendLine("+" + ModLocalization.EmotionSystem_Distorted_Bosses);
				}

				string originalText = __instance.BloodyMistText.text;
				__instance.BloodyMistText.text = originalText + "\n" + ModLocalization.EmotionSystem + "\n" + sb.ToString();
			}

			public static string GetLibraryFloor()
			{
				switch (CurrentFloorType)
				{
					case DataStore.LibraryFloorType.History:
						return ModLocalization.EmotionSystem_Floor_History;
					case DataStore.LibraryFloorType.Technological:
						return ModLocalization.EmotionSystem_Floor_Technological;
					case DataStore.LibraryFloorType.Literature:
						return ModLocalization.EmotionSystem_Floor_Literature;
					case DataStore.LibraryFloorType.Art:
						return ModLocalization.EmotionSystem_Floor_Art;
					case DataStore.LibraryFloorType.Natural:
						return ModLocalization.EmotionSystem_Floor_Natural;
					default:
						return "";
				}
			}
		}
	}
}
