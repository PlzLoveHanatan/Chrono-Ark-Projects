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

			{"QoH_FieldIdle", "The Second Warning... hm! 'Tis evident! Now is the time for thee to deliver unto them the smiting of justice! Ho! With this hairpin and E.G.O Gear, thy most gracious gift...and do away with this chaos!" },
			{"QoH_FieldIdle_0", "Even if my life be made forfeit... I shall protect the employees of this establishment and the people of the City! Ho! Arcana... Beats!" },

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
				if (!QoH_Utils.QoHVoice)
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