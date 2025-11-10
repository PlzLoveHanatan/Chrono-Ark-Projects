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

				StringBuilder sb = new StringBuilder();

				if (Utils.InvestigatorEmotions)
				{
					sb.AppendLine("+" + ModLocalization.EmotionSystem_EmotionsInvestigator);
				}

				if (Utils.GuestEmotions)
				{
					sb.AppendLine("+" + ModLocalization.EmotionSystem_EmotionsGuest);
				}

				string floorName = GetLibraryFloor();
				if (CurrentFloor != null && !string.IsNullOrEmpty(floorName) && Utils.InvestigatorEmotions)
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
					default:
						return "";
				}
			}
		}
	}
}
