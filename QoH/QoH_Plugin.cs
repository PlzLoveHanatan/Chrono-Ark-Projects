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
using HarmonyLib;
using EmotionSystem;
using TileTypes;
namespace QoH
{
	public class QoH_Plugin : ChronoArkPlugin
	{
		public const string modname = "QoH";

		public const string version = "1.0";

		public const string author = "Midana";

		public static ModInfo ThisMod => ModManager.getModInfo(modname);

		Harmony harmony = new Harmony("QoH");

		public override void Dispose()
		{
			if (harmony != null)
			{
				harmony.UnpatchSelf();
			}
		}

		public override void Initialize()
		{
			try
			{
				harmony.PatchAll();
			}
			catch (Exception e)
			{
				Debug.Log("QoH: Patch Catch: " + e.ToString());
			}
		}

		public static bool QoHInParty()
		{
			return PlayData.TSavedata.Party.Any(x => x.KeyData == ModItemKeys.Character_QoH);
		}


		[HarmonyPatch(typeof(FieldSystem), "StageStart")]
		public static class StagePatch
		{
			[HarmonyPostfix]
			public static void StageStartPostfix()
			{
				if (!QoHInParty()) return;

				if (PlayData.TSavedata.StageNum >= 0)
				{
					if (!QoH_Utils.Data.JusticeEquip && QoH_Utils.MagicalEquip)
					{
						QoH_Utils.Data.JusticeEquip = true;
						GainjusticeReward();
					}

					if (!QoH_Utils.Data.SunMoonQuest && QoH_Utils.SunMoonQuest)
					{
						QoH_Utils.Data.SunMoonQuest = true;
						GainSunMoonQuest();
					}
				}
			}

			private static void GainjusticeReward()
			{
				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_QoH_FormingHate));
				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Equip_E_QoH_LovelyGift));
				//PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Passive_R_QoH_Star));
			}

			private static void GainSunMoonQuest()
			{
				if (PlayData.TSavedata.IdentifyItems.Find((string x) => x == GDEItemKeys.Item_Scroll_Scroll_Transfer) == null)
				{
					PlayData.TSavedata.IdentifyItems.Add(GDEItemKeys.Item_Scroll_Scroll_Transfer);
				}

				//PlayData.TSavedata.Passive_Itembase.Add(ItemBase.GetItem(ModItemKeys.Item_Passive_R_QoH_SunMoon));

				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(ModItemKeys.Item_Passive_R_QoH_SunMoon));
				PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Transfer));
			}
		}

		private static readonly Dictionary<string, string> QoHVoiceEN = new Dictionary<string, string>
		{
			{"QoH_BattleStart", "Prithee, leave it in my care!" },
			{"QoH_BattleStart_0", "In the name of Love and Justice~ Here cometh Magical Maiden!" },

			{"QoH_Chest", "Dost thou know of the glory of victory and the beauty of the world therein? Nay? All good! For what we behold now, at this very moment... is just that." },
			{"QoH_Cri", "Behold, for evil hath been cleansed thus!" },
			{"QoH_Curse", "I... swore I would... protect thee all... to the bitter..." },
			{"QoH_Healed", "Forgive... me... 'Twas my... failing... All of it..." },

			{"QoH_BattleIdle", "Gasp! Doth aught requireth mine assistance?" },
			{"QoH_BattleIdle_0", "Tis evident this foe shall pose a challenge... allow me!" },

			//{"QoH_FieldIdle", "So... this is what it hath all come to. I'm just like them. I failed the suppression, I'm a useless— Get out of my head..." },
			{"QoH_FieldIdle_0", "Even if my life be made forfeit... I shall protect the employees of this establishment and the people of the City!" },
			{"QoH_FieldIdle_1", "No warnings today, I must wonder... Perish the thought... I have definitely not harbored thoughts of boredom induced by the lack of eventful action!" },
			

			{"QoH_Kill", "Heh heh, 'tis light work!" },
			{"QoH_Kill_0", "Heh heh, justice, served! Now... I must forthwith make haste and return to her to regale her with the haaaaai-lights of this suppression!" },

			{"QoH_Master", "Is it time to pay the price...?" },

			{"QoH_Pharos", "Get out of my head..." },
			{"QoH_Pharos_0", "Arcana scum..." },
			{"QoH_Pharos_1", "Ha ha... now peace shall reign, and...?" },

			{"QoH_Potion", "Hm! Handily managed! Prithee, count on mine assistance next time as well!" },

			{"QoH_DeathDoor", "Ngh, this cannot be..." },
			{"QoH_DeathDoorAlly", "...I failed. Hast thou no need of me any longer?" },

		};

		[HarmonyPatch(typeof(PrintText))]
		[HarmonyPatch(nameof(PrintText.TextInput))]
		public class VoiceOn
		{
			[HarmonyPrefix]
			public static bool Prefix(PrintText __instance, string inText)
			{
				if (!QoH_Utils.MagicalVoice)
				{
					return true;
				}

				string language = LocalizationManager.CurrentLanguage;
				Dictionary<string, string> selectedDict;

				switch (language)
				{
					//case "Korean": selectedDict = QoHVoiceKR; break;
					case "English": selectedDict = QoHVoiceEN; break;
					//case "Japanese": selectedDict = QoHVoiceJP; break;
					//case "Chinese": selectedDict = QoHVoiceJP; break;
					default: selectedDict = QoHVoiceEN; break;
				}

				foreach (var kvp in selectedDict)
				{
					if (inText.Contains(kvp.Value))
					{
						Utils.PlaySound(kvp.Key, true);
					}
				}

				return true;
			}
		}
	}
}