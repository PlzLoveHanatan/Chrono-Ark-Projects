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
			{"QoH_BattleStart", "请把它交给我吧！" },
			{"QoH_BattleStart_0", "以爱与正义之名～ 魔法少女到来！" },

			{"QoH_Chest", "你可知道胜利的荣光与其中世界的美丽？不知道吗？没关系！因为我们此时此刻所见的... 正是那一切。" },
			{"QoH_Cri", "看吧，邪恶就这样被净化了！" },
			{"QoH_Curse", "我... 发过誓... 我要... 保护你们所有人... 直到最后..." },
			{"QoH_Healed", "原谅我... 这是我的... 过失... 全部都是..." },

			{"QoH_BattleIdle", "啊！需要我的帮助吗？" },
			{"QoH_BattleIdle_0", "看来这个敌人会成为挑战... 交给我吧！" },

			{"QoH_FieldIdle_0", "即便我献出生命... 我也会保护这家机构的员工和这座城市的人们！" },
			{"QoH_FieldIdle_1", "今天没有警告吗，我有些疑惑... 别乱想... 我绝对没有因为缺乏重大事件而感到无聊！" },

			{"QoH_Kill", "嘿嘿，小事一桩！" },
			{"QoH_Kill_0", "嘿嘿，正义已得伸张！现在... 我得立刻赶回她身边，向她讲述这次镇压的精彩瞬间！" },

			{"QoH_Master", "该付出代价的时候到了吗...？" },

			{"QoH_Pharos", "从我脑海里滚出去..." },
			{"QoH_Pharos_0", "奥秘的渣滓..." },
			{"QoH_Pharos_1", "哈哈... 现在和平将会统治了，然后...？" },

			{"QoH_Potion", "嗯！轻松搞定！下次也请指望我的帮助吧！" },

			{"QoH_DeathDoor", "呃，不可能..." },
			{"QoH_DeathDoorAlly", "...我失败了。你已不再需要我了吗？" },
		};

		private static readonly Dictionary<string, string> QoHVoiceZH_TW = new Dictionary<string, string>
		{
			{"QoH_BattleStart", "請把它交給我吧！" },
			{"QoH_BattleStart_0", "以愛與正義之名～ 魔法少女到來！" },

			{"QoH_Chest", "你可知道勝利的榮光與其中世界的美麗？不知道嗎？沒關係！因為我們此時此刻所見的... 正是那一切。" },
			{"QoH_Cri", "看吧，邪惡就這樣被淨化了！" },
			{"QoH_Curse", "我... 發過誓... 我要... 保護你們所有人... 直到最後..." },
			{"QoH_Healed", "原諒我... 這是我的... 過失... 全部都是..." },

			{"QoH_BattleIdle", "啊！需要我的幫助嗎？" },
			{"QoH_BattleIdle_0", "看來這個敵人會成為挑戰... 交給我吧！" },

			{"QoH_FieldIdle_0", "即便我獻出生命... 我也會保護這家機構的員工與這座城市的人們！" },
			{"QoH_FieldIdle_1", "今天沒有警告嗎，我有些疑惑... 別亂想... 我絕對沒有因為缺乏重大事件而感到無聊！" },

			{"QoH_Kill", "嘿嘿，小事一樁！" },
			{"QoH_Kill_0", "嘿嘿，正義已得伸張！現在... 我得立刻趕回她身邊，向她講述這次鎮壓的精彩瞬間！" },

			{"QoH_Master", "該付出代價的時候到了嗎...？" },

			{"QoH_Pharos", "從我腦海裡滾出去..." },
			{"QoH_Pharos_0", "奧秘的渣滓..." },
			{"QoH_Pharos_1", "哈哈... 現在和平將會統治了，然後...？" },

			{"QoH_Potion", "嗯！輕鬆搞定！下次也請指望我的幫助吧！" },

			{"QoH_DeathDoor", "呃，不可能..." },
			{"QoH_DeathDoorAlly", "...我失敗了。你已不再需要我了嗎？" },
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
	}
}