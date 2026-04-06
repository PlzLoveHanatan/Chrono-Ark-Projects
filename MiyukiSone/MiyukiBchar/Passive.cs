using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using ChronoArkMod.ModEditor;
using UnityEngine;
using GameDataEditor;
using I2.Loc;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using static MiyukiSone.EventsData;
using static MiyukiSone.DialogueData;
using static MiyukiSone.UtilsScripts;
using static MiyukiSone.Dialogue;
using DarkTonic.MasterAudio;
using System.EnterpriseServices;
using UnityEngine.UI;
using System.ServiceModel.Channels;
using static MiyukiSone.Buffs;
using Spine;
using Newtonsoft.Json.Linq;

namespace MiyukiSone
{
	public class MiyukiPassive : Passive_Char, IP_PlayerTurn_1, IP_BattleStart_Ones, IP_DamageTake, IP_DrawNumChange, IP_Targeted, IP_MiyukiCharImgChange, IP_TurnEnd
	{
		#region Data & Constructors
		private MiyukiInputEvent chatInputField;

		public static bool CreateCharacterDraw = false;
		public bool WallBreakerEquipped = false;

		// Fixed Ability Skill List
		public List<string> MiyukiChoiceList = new List<string>();

		private readonly List<Action> MiyukiPaws;

		public MiyukiPassive()
		{
			MiyukiPaws = new List<Action>()
			{
				PawsWithMana,
				PawsWithExchange,
				PawsWithStandBy,
				PawsWithCost,
				PawsWithSwift,
				//PawsWithUpgrade,
				PawsWithBlackFog
			};
		}

		// Skills to redirect skills
		private readonly HashSet<string> edgeCaseSkills = new HashSet<string>()
		{
			GDEItemKeys.Skill_S_Witch_P_0,
			GDEItemKeys.Skill_S_Witch_2,
			GDEItemKeys.Skill_S_ShadowPriest_12,
			GDEItemKeys.Skill_S_ShadowPriest_3,
			GDEItemKeys.Skill_S_Queen_7,
			GDEItemKeys.Skill_S_Queen_13,
			GDEItemKeys.Skill_S_MissChain_6,
		};

		// Main List
		public static readonly List<string> AvaliableCharacterDraw = new List<string>();

		public static readonly Dictionary<string, string> CharacterDrawList = new Dictionary<string, string>()
		{
			{ ModItemKeys.Skill_S_Miyuki_EternalPromise, GDEItemKeys.Skill_S_Leryn_Draw}, // Inspiration
			{ ModItemKeys.Skill_S_Miyuki_EternalVow, GDEItemKeys.Skill_S_MissChain_12_LucyD }, // Burning Draw
			{ ModItemKeys.Skill_S_Miyuki_GlitchingPhone, "???" },
			{ ModItemKeys.Skill_S_Miyuki_GracefulSwing, GDEItemKeys.Skill_S_Priest_7_LucyD },// Divine Revelation
			{ ModItemKeys.Skill_S_Miyuki_HappyBirthday, GDEItemKeys.Skill_S_LucyD_7 }, // Renovate
			{ ModItemKeys.Skill_S_Miyuki_MeasuredLove, GDEItemKeys.Skill_S_Lian_Draw }, // Begone!
			{ ModItemKeys.Skill_S_Miyuki_Pandemonium, ModItemKeys.Skill_S_Miyuki_LucyDraw_FracturedIllusion}, // Fractured Illusion
			{ ModItemKeys.Skill_S_Miyuki_QueenBee, GDEItemKeys.Skill_S_Joey_9_LucyD}, // Strawberry-flawored Tablet
			//{ ModItemKeys.Skill_S_Miyuki_Rare_FinalView, "???"},
			//{ ModItemKeys.Skill_S_Miyuki_Rare_GameUpdate, "???"},
			//{ ModItemKeys.Skill_S_Miyuki_Rare_JustforYOU, "???"},
			{ ModItemKeys.Skill_S_Miyuki_Special_EternalKiss, ModItemKeys.Skill_S_Miyuki_LucyDraw_MomoriHelp}, // Miyuki, Help
			{ ModItemKeys.Skill_S_Miyuki_StepToward, GDEItemKeys.Skill_S_TW_Blue_Draw0}, // Observation
			{ ModItemKeys.Skill_S_Miyuki_SweetRestraint, GDEItemKeys.Skill_S_Queen_5_Draw}, // Uncontrollable Doll
			{ ModItemKeys.Skill_S_Miyuki_WarningStrike, GDEItemKeys.Skill_S_Control_3_Draw }, // Insight 
		};

		// Keys for additional shuffled skills in the deck
		private static readonly List<string> PosLucyDrawKeys = new List<string>()
		{
			GDEItemKeys.Skill_S_LucyD_3, // Search
			GDEItemKeys.Skill_S_LucyD_16, // Deep in Thought
			GDEItemKeys.Skill_S_PopcornGirl_Lucy_1, // Tasty Popcorn!
			GDEItemKeys.Skill_S_PopcornGirl_Lucy_2, // Caramel Popcorn!
			GDEItemKeys.Skill_S_PopcornGirl_Lucy_3, // Spicy Popcorn!
			GDEItemKeys.Skill_S_Lucy_CasinoDLC_7, // All in
		};

		public static readonly List<string> NegLucyDrawKeys = new List<string>()
		{
			GDEItemKeys.Skill_S_Transcendence_Main, // Bloodmist
			GDEItemKeys.Skill_S_LucyCurse_CursedClock,
			GDEItemKeys.Skill_S_LucyCurse_Banana,
			GDEItemKeys.Skill_S_LucyCurse_Late,
			GDEItemKeys.Skill_S_LucyCurse_Heavy,
			GDEItemKeys.Skill_S_S1_LittleMaid_0_Lucy,
		};
		#endregion

		#region Miyuki's Passive IP
		public override void Init()
		{
			base.Init();
			OnePassive = true;
		}

		public void CharImgChange()
		{
			MiyukiCharImg.ChangeCharacterImage();
		}

		public void BattleStart(BattleSystem Ins)
		{
			AvaliableCharacterDraw.Clear();
			BattleSystem.instance.AllyTeam.Skills_Deck.ForEach(s =>
			{
				if (s.MySkill.KeyID != ModItemKeys.Skill_S_Miyuki_GlitchingPhone && CharacterDrawList.TryGetValue(s.MySkill.KeyID, out string drawKey))
				{
					if (!AvaliableCharacterDraw.Contains(drawKey)) AvaliableCharacterDraw.Add(drawKey);
				}
			});

			if (!IsKuudere) PawsWithDeck();
			if (AvaliableCharacterDraw.Count > 0) BattleSystem.DelayInput(PawsWithDraw());
		}

		public void DrawNumChange(int DrawNum, out int OutNum)
		{
			OutNum = CreateCharacterDraw ? DrawNum - 1 : DrawNum;
		}

		public void Turn1()
		{
			BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != MiyukiBchar).ToList().ForEach(a => a.SecureBuff(ModItemKeys.Buff_B_Miyuki_Buff_Ally));
			BChar.SecureBuff(ModItemKeys.Buff_B_Miyuki_Passive);
			CreateCharacterLucyDraw();		
			if (BattleSystem.instance.TurnNum > 1) MiyukiTurn();
			else CheckIp();
			MiyukiTurnPaw();
		}

		public void TurnEnd()
		{
			RefreshMiyukiCharacterDraw();
		}

		public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
		{
			if (BChar.HP < 0 && BattleSystem.instance.AllyTeam.AliveChars.Where(a => a != BChar).Count() > 0)
			{
				var painSharing = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a == BattleSystem.instance.AllyTeam.LucyAlly).SelectMany(b => b.Buffs).FirstOrDefault(b => b.BuffData.Key == GDEItemKeys.Buff_B_BloodyMist_ShareDamage || b.BuffData.Key == GDEItemKeys.Buff_B_ProgramMaster_LucyMain);
				if (painSharing != null) return;
				resist = true;
				MiyukiTextEvent(MiyukiAffection.Kuudere);
			}
		}

		public void Targeted(Skill SkillD, List<BattleChar> Targets)
		{
			if (!SkillD.Master.Info.Ally || BChar.HP >= BChar.GetStat.maxhp / 2 && MiyukiDecides) return;

			BattleChar target = BattleSystem.instance.AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).RandomElement();

			if (target == null) return;

			if (edgeCaseSkills.Contains(SkillD.MySkill.KeyID) || SkillD.IsDamage)
			{
				Targets.Clear();
				Targets.Add(target);
				if (MiyukiDecides) Events.YandereActionCut();
				//BChar.StartCoroutine(ShowMiyukiEventText());
				MiyukiTextEvent(MiyukiAffection.Yandere);
			}
		}
		#endregion

		#region Miyuki's Turn Paws
		public void MiyukiTurnPaw()
		{
			for (int i = 0; i < MiyukiRandomResult(3); i++)
			{
				MiyukiPaws.RandomElement()?.Invoke();
			}

			MiyukiTextEvent();
		}

		private void PawsWithMana()
		{
			BattleSystem.instance.AllyTeam.AP += MiyukiResult(1);
		}

		private void PawsWithStandBy()
		{
			BattleSystem.instance.AllyTeam.WaitCount += MiyukiResult(1);
		}

		private void PawsWithExchange()
		{
			BattleSystem.instance.AllyTeam.DiscardCount += MiyukiResult(1);
		}

		private void PawsWithBlackFog()
		{
			BattleSystem.instance.FogTurn += MiyukiResult(1);
		}

		private void PawsWithCost()
		{
			BattleSystem.instance.AllyTeam.Skills.RandomElement()?.Let(s => s.APChange = MiyukiResult(1, false));
		}

		private void PawsWithSwift()
		{
			BattleSystem.instance.AllyTeam.Skills.Where(s => s.NotCount == !MiyukiResult()).RandomElement()?.Let(s => s.NotCount = MiyukiResult());
		}

		public static void PawsWithDeck()
		{
			List<string> selectedSkills = MiyukiResult() ? PosLucyDrawKeys : NegLucyDrawKeys;

			for (int i = 0; i < MiyukiRandomResult(3); i++)
			{
				var skill = Skill.TempSkill(selectedSkills.RandomElement(), BattleSystem.instance.AllyTeam.LucyAlly, BattleSystem.instance.AllyTeam.LucyAlly.MyTeam);
				if (skill != null) BattleSystem.instance?.AllyTeam.Skills_Deck.InsertRandom("MiyukiRandomInsert", skill);
				skill.isExcept = true;
				skill.MySkill.Name = "Miyuki's " + skill.MySkill.Name;
			}
			MiyukiTextEvent();
		}

		private IEnumerator PawsWithDraw()
		{
			yield return null;
			BChar.AddBuff(ModItemKeys.Buff_B_Miyuki_Passive);
			MiyukiBuff?.ChangeDraw();

		}
		#endregion

		private void CreateCharacterLucyDraw()
		{
			if (!CreateCharacterDraw) return;

			var skill = Skill.TempSkill(AvaliableCharacterDraw.Random("MiyukiCharacterDraw"), BattleSystem.instance.AllyTeam.LucyAlly, BattleSystem.instance.AllyTeam.LucyAlly.MyTeam);
			BattleSystem.instance.AllyTeam.Add(skill, true);
			skill.isExcept = true;
			skill.NotCount = WallBreakerEquipped;
			skill.APChange = WallBreakerEquipped ? 0 : 1;
			skill.AutoDelete = 1;
			skill.MySkill.Name = "Miyuki's " + skill.MySkill.Name;
		}

		private void CreateChatWindow()
		{
			chatInputField = MiyukiInputEvent.CreateChatInput(
				spritePath: "MiyukiVisual/dlog_test.png",
				parentWindow: BattleSystem.instance.ActWindow.transform,
				windowSize: new Vector2(700, 130),
				windowPosition: new Vector3(170, 170, 0),
				placeholder: "",
				inputPosition: new Vector2(0, -20),
				inputSize: new Vector2(400, 35));
		}
	}
}
