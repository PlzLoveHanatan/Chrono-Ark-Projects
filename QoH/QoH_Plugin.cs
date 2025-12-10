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
using System.Runtime.CompilerServices;
using static CharacterDocument;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using TMPro;
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

		private static readonly Dictionary<string, string> QoHVoiceKR = new Dictionary<string, string>
		{
			{"QoH_BattleStart", "제발, 나에게 맡겨라!" },
			{"QoH_BattleStart_0", "사랑과 정의의 이름으로~ 마법소녀 등장!" },

			{"QoH_Chest", "그대는 승리의 영광과 그 안에 있는 세계의 아름다움을 아는가? 모른다? 괜찮다! 우리가 바로 이 순간 보고 있는 것이 바로 그것이다..." },
			{"QoH_Cri", "보라, 악은 이렇게 정화되었다!" },
			{"QoH_Curse", "나는... 맹세했었다... 너희 모두를... 끝까지 지키겠다고..." },
			{"QoH_Healed", "용서해 줘... 나를... 이것은 나의... 과오였다... 전부..." },

			{"QoH_BattleIdle", "헉! 나의 도움이 필요한 일이 있는가?" },
			{"QoH_BattleIdle_0", "이 적은 분명히 난관이 되겠군... 나에게 맡겨라!" },

			{"QoH_FieldIdle_0", "내 목숨이 희생되더라도... 나는 이 시설의 직원들과 도시의 사람들을 지키겠다!" },
			{"QoH_FieldIdle_1", "오늘은 경고도 없구나... 이상하네... 설마... 사건이 없어 지루함을 느끼고 있었다는 생각은 절대 아니다!" },

			{"QoH_Kill", "헤헤, 쉬운 일이지!" },
			{"QoH_Kill_0", "헤헤, 정의가 집행됐다! 이제... 나는 즉시 서둘러 그녀에게 돌아가 이 제압의 하이라이트를 전해야 해!" },

			{"QoH_Master", "대가를 치를 시간이 온 건가...?" },

			{"QoH_Pharos", "내 머릿속에서 나가..." },
			{"QoH_Pharos_0", "아르카나 쓰레기..." },
			{"QoH_Pharos_1", "하하... 이제 평화가 깃들겠지, 그리고...?" },

			{"QoH_Potion", "음! 깔끔하게 해결! 다음에도 나의 도움을 기대해도 좋다!" },

			{"QoH_DeathDoor", "으윽, 이럴 수가..." },
			{"QoH_DeathDoorAlly", "...나는 실패했다. 이제 더는 나의 도움이 필요 없는가?" },
		};

		private static readonly Dictionary<string, string> QoHVoiceJP = new Dictionary<string, string>
		{
			{"QoH_BattleStart", "どうか、私に任せてくれ！" },
			{"QoH_BattleStart_0", "愛と正義の名のもとに～ 魔法の乙女、参上！" },

			{"QoH_Chest", "汝は勝利の栄光と、その中にある世界の美しさを知っているか？ いや？ よいさ！ まさに今、我らが見ているもの… それこそがそれだ。" },
			{"QoH_Cri", "見よ、悪はかくして浄化された！" },
			{"QoH_Curse", "私は… 誓った… 皆を… 最後まで守ると…" },
			{"QoH_Healed", "許してくれ… これは… 我の… 過ちだった… すべて…" },

			{"QoH_BattleIdle", "はっ！ 我が助けを要するのか？" },
			{"QoH_BattleIdle_0", "この敵は厄介だろう… 任せよ！" },

			{"QoH_FieldIdle_0", "たとえ我が命が失われようとも… この施設の職員と都市の人々を守る！" },
			{"QoH_FieldIdle_1", "今日は警告もないのか… 不思議だ… まさか… 出来事がなくて退屈だと思っていたなどということは決してない！" },

			{"QoH_Kill", "へへ、楽勝だ！" },
			{"QoH_Kill_0", "へへ、正義執行！ さて… 急いで彼女のもとへ戻り、この鎮圧のハイライトを語らねば！" },

			{"QoH_Master", "代償を払う時か…？" },

			{"QoH_Pharos", "私の頭から出て行け…" },
			{"QoH_Pharos_0", "アルカナのクズ…" },
			{"QoH_Pharos_1", "はは… これで平和が訪れる、そして…？" },

			{"QoH_Potion", "よし！ 見事に片付いた！ 次も我が助力を当てにするがよい！" },

			{"QoH_DeathDoor", "くっ、そんなはずが…" },
			{"QoH_DeathDoorAlly", "…失敗した。もはや我を必要としないのか？" },
		};

		private static readonly Dictionary<string, string> QoHVoiceZH = new Dictionary<string, string>
		{
			{"QoH_BattleStart", "不要离我太远喔！" },
			{"QoH_BattleStart_0", "以爱与正义之名～魔法少女参上！" },

			{"QoH_Chest", "我有预感……这是能带来胜利的宝物！" },
			{"QoH_Cri", "看哪，邪恶已被扫除！" },
			{"QoH_Curse", "我发誓……会保护……大家……直到……" },
			{"QoH_Healed", "原谅……我……都是……我的错……" },

			{"QoH_BattleIdle", "啊！有什么能帮到你的？" },
			{"QoH_BattleIdle_0", "这可真不妙……让我来吧！" },

			{"QoH_FieldIdle_0", "即使献上生命……我也会保护这机构与都市的人们！" },
			{"QoH_FieldIdle_1", "今天没有警报吗……绝对不是因为缺乏表现机会而感到无聊喔！" },

			{"QoH_Kill", "嘿嘿，轻松！" },
			{"QoH_Kill_0", "正义得到伸张！真想……现在就赶回去，跟她讲讲这次「镇压」的精彩！" },

			{"QoH_Master", "到了……付出代价……的时候吗？" },

			{"QoH_Pharos", "滚出……我的脑袋……" },
			{"QoH_Pharos_0", "阿卡纳……可悲……" },
			{"QoH_Pharos_1", "哈哈……和平到来……然后呢？" },

			{"QoH_Potion", "嗯，非常顺利！下次也别忘了找我哟！" },

			{"QoH_DeathDoor", "呐，这不可能……" },
			{"QoH_DeathDoorAlly", "...我失败了……你会就这样离开吗？" },
		};

		private static readonly Dictionary<string, string> QoHVoiceZH_TW = new Dictionary<string, string>
		{
			{"QoH_BattleStart", "不要離我太遠喔！" },
			{"QoH_BattleStart_0", "以愛與正義之名～魔法少女參上！" },

			{"QoH_Chest", "我有預感……這是能帶來勝利的寶物！" },
			{"QoH_Cri", "看哪，邪惡已被掃除！" },
			{"QoH_Curse", "我發誓……會保護……大家……直到……" },
			{"QoH_Healed", "原諒……我……都是……我的錯……" },

			{"QoH_BattleIdle", "啊！有什麼能幫到你的？" },
			{"QoH_BattleIdle_0", "這可真不妙……讓我來吧！" },

			{"QoH_FieldIdle_0", "即使獻上生命……我也會保護這機構與都市的人們！" },
			{"QoH_FieldIdle_1", "今天沒有警報嗎……絕對不是因為缺乏表現機會而感到無聊喔！" },

			{"QoH_Kill", "嘿嘿，輕鬆！" },
			{"QoH_Kill_0", "正義得到伸張！真想……現在就趕回去，跟她講講這次「鎮壓」的精彩！" },

			{"QoH_Master", "到了……付出代價……的時候嗎？" },

			{"QoH_Pharos", "滾出……我的腦袋……" },
			{"QoH_Pharos_0", "阿卡納……可悲……" },
			{"QoH_Pharos_1", "哈哈……和平到來……然後呢？" },

			{"QoH_Potion", "嗯，非常順利！下次也別忘了找我喲！" },

			{"QoH_DeathDoor", "吶，這不可能……" },
			{"QoH_DeathDoorAlly", "...我失敗了……你會就這樣離開嗎？" },
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
					case "Korean": selectedDict = QoHVoiceKR; break;
					case "English": selectedDict = QoHVoiceEN; break;
					case "Japanese": selectedDict = QoHVoiceJP; break;
					case "Chinese": selectedDict = QoHVoiceZH; break;
					case "Chinese-TW": selectedDict = QoHVoiceZH_TW; break;
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

		//// Adding custom keywords to skills with base Description (damage/heal/accuracy/critical chance or if skill already have Keyword)
		//[HarmonyPatch(typeof(SkillToolTip), "Input")]
		//public static class SkillToolTip_Input_Plugin
		//{
		//	[HarmonyPostfix]
		//	public static void Postfix(SkillToolTip __instance, Skill Skill)
		//	{
		//		if (__instance?.Desc == null || Skill?.MySkill == null) return;

		//		var kw = Skill.MySkill.PlusKeyWords.FirstOrDefault(a => a.Key == ModItemKeys.SkillKeyword_KeyWord_Boobs);
		//		if (kw == null) return;

		//		string myWord = SkillToolTip.ColorChange("FF7C34", kw.Name);
		//		var lines = (__instance.Desc.text ?? string.Empty).Split('\n').ToList();
		//		int idx = lines.FindIndex(l => l.Contains("<b>") && l.Contains("</b>"));

		//		if (idx >= 0)
		//		{
		//			lines[idx] += ". " + myWord;
		//		}
		//		else
		//		{
		//			lines.Insert(0, $"<b>{myWord}</b>");
		//		}

		//		__instance.Desc.text = string.Join("\n", lines);
		//	}
		//}
	}
}