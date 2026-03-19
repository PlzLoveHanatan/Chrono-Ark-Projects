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
using static MiyukiSone.EventData;
using static MiyukiSone.DialogueData;
using static MiyukiSone.UtilsScripts;
using static MiyukiSone.Dialogue;
using DarkTonic.MasterAudio;
using System.EnterpriseServices;
using UnityEngine.UI;
using System.ServiceModel.Channels;
using static MiyukiSone.Buffs;

namespace MiyukiSone
{
	public class MiyukiPassive : Passive_Char, IP_PlayerTurn, IP_BattleStart_Ones, IP_DamageTake, IP_DrawNumChange, IP_Targeted, IP_TurnEnd, IP_MiyukiCharImgChange
	{
		#region Data & Constructors
		private MiyukiInputEvent chatInputField;

		public bool CreateCharacterDraw = false;

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
				PawsWithUpgrade,
				//PawsWithBlackFog
			};
		}

		// Ally damage skills
		private readonly HashSet<string> allyDamageSkills = new HashSet<string>()
		{
			GDEItemKeys.Skill_S_Witch_P_0,
			GDEItemKeys.Skill_S_Witch_2,
			GDEItemKeys.Skill_S_ShadowPriest_12,
			GDEItemKeys.Skill_S_ShadowPriest_3,
			GDEItemKeys.Skill_S_Queen_7,
			GDEItemKeys.Skill_S_Queen_13,
		};

		// Main List
		public readonly List<string> AvaliableCharacterDraw = new List<string>();

		private readonly Dictionary<string, string> characterDrawList = new Dictionary<string, string>()
		{
			{ ModItemKeys.Skill_S_Miyuki_GracefulSwing, GDEItemKeys.Skill_S_Priest_7_LucyD },// Divine Revelation
			{ ModItemKeys.Skill_S_Miyuki_WarningStrike, GDEItemKeys.Skill_S_Control_3_Draw }, // Insight 
			{ ModItemKeys.Skill_S_Miyuki_EternalVow, GDEItemKeys.Skill_S_MissChain_12_LucyD }, // Burning Draw
			//{ ModItemKeys.Skill_S_HappyBirthday, GDEItemKeys.Skill_S_Lucy_24 }, // Change of Plans
			{ ModItemKeys.Skill_S_Miyuki_HappyBirthday, GDEItemKeys.Skill_S_LucyD_7 }, // Renovate
			//{ ModItemKeys.Skill_S_Miyuki_WarningStrike, ModItemKeys.Skill_S_Miyuki_Draw_MiyukiHelp }, // Miyuki, Help
			{ ModItemKeys.Skill_S_Miyuki_Pandemonium, ModItemKeys.Skill_S_Miyuki_Draw_FracturedIllusion}, // Fractured Illusion
			{ ModItemKeys.Skill_S_Miyuki_EternalPromise, GDEItemKeys.Skill_S_Leryn_Draw}, // Inspiration
		};

		// Keys for additional shuffled skills in the deck
		private readonly List<string> PosLucyDrawKeys = new List<string>()
		{
			GDEItemKeys.Skill_S_LucyD_3, // Search
			GDEItemKeys.Skill_S_LucyD_16, // Deep in Thought
			GDEItemKeys.Skill_S_PopcornGirl_Lucy_1, // Tasty Popcorn!
			GDEItemKeys.Skill_S_PopcornGirl_Lucy_2, // Caramel Popcorn!
			GDEItemKeys.Skill_S_PopcornGirl_Lucy_3, // Spicy Popcorn!
			GDEItemKeys.Skill_S_Lucy_CasinoDLC_7, // All in
		};

		private readonly List<string> NegLucyDrawKeys = new List<string>()
		{
			GDEItemKeys.Skill_S_Transcendence_Main, // Bloodmist
			GDEItemKeys.Skill_S_LucyCurse_CursedClock,
			GDEItemKeys.Skill_S_LucyCurse_Banana,
			GDEItemKeys.Skill_S_LucyCurse_Late,
			GDEItemKeys.Skill_S_LucyCurse_Heavy,
		};
		#endregion

		#region Character Passive IP
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
			AllyTeam.Skills_Deck.ForEach(s =>
			{
				if (characterDrawList.TryGetValue(s.MySkill.KeyID, out string drawKey))
				{
					AvaliableCharacterDraw.Add(drawKey);
				}
			});

			PawsWithDeck();
			if (AvaliableCharacterDraw.Count > 0) BattleSystem.DelayInput(PawsWithDraw());
		}

		public void DrawNumChange(int DrawNum, out int OutNum)
		{
			OutNum = CreateCharacterDraw ? DrawNum - 1 : DrawNum;

			//int newDrawNum = MiyukiResult(1);
			//OutNum = newDrawNum != 0 ? DrawNum += newDrawNum : DrawNum;
			//if (newDrawNum != 0) MiyukiTextEvent(CurrentAffection);
			//Debug.Log($"Draw num = {newDrawNum}");
		}

		public void Turn()
		{
			AllyTeam.AliveChars.Where(a => a != MiyukiBchar).ToList().ForEach(a => SecureBuff(a, DummyChar, ModItemKeys.Buff_B_Miyuki_Buff));
			SecureBuff(BChar, DummyChar, ModItemKeys.Buff_B_Miyuki_Passive);
			CreateCharacterLucyDraw();

			if (Bs.TurnNum == 1)
			{
				CheckIp();
			}
			else
			{
				MiyukiTurn();
				MiyukiTurnPaw();
			}
		}

		public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
		{
			if (BChar.HP < 0 && AllyTeam.AliveChars.Count > 0)
			{
				resist = true;
			}
		}

		public void Targeted(Skill SkillD, List<BattleChar> Targets)
		{
			/*!Targets.Contains(BChar) && SkillD.Master == BChar*/
			if (!SkillD.Master.Info.Ally) return;

			var aliveAllies = AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList();
			if (aliveAllies.Count <= 0) return;

			int randomIndex = RandomManager.RandomInt("MiyukiTargetRedirect", 0, aliveAllies.Count);
			BattleChar newTarget = null;

			if (allyDamageSkills.Contains(SkillD.MySkill.KeyID)) newTarget = aliveAllies[randomIndex];
			else if (SkillD.IsDamage && BChar.HP <= 0) newTarget = aliveAllies[randomIndex];
			else return;

			Targets.Clear();
			Targets.Add(newTarget);
			//EventYandere.YandereAction(false);
			BChar.StartCoroutine(ShowMiyukiEventText());
		}

		private IEnumerator ShowMiyukiEventText()
		{
			yield return null;
			MiyukiTextEvent(CurrentAffection);
		}

		public void TurnEnd()
		{
			UnlockNextTurnEndTry();
		}
		#endregion

		#region Miyuki's Turn Paws
		public void MiyukiTurnPaw()
		{
			if (IsKuudere) return;

			//if (Bs.TurnNum >= Bs.FogTurn && !IsYandere) goto MiyukiHelp;

			for (int i = 0; i < 2; i++)
			{
				var paw = MiyukiPaws.ToList();
				if (MiyukiData.LastTurnPawAction != -1 && paw.Count > 1) paw.RemoveAt(MiyukiData.LastTurnPawAction);
				int randomIndex = RandomManager.RandomInt("MiyukiPaw", 0, paw.Count);
				paw[randomIndex].Invoke();
				MiyukiData.LastTurnPawAction = randomIndex;
			}


			//MiyukiHelp:;
			//CreateDialogue(DialogueState.help);
		}

		private void PawsWithMana()
		{
			AllyTeam.AP += MiyukiResult(1);
		}

		private void PawsWithStandBy()
		{
			AllyTeam.WaitCount += MiyukiResult(1);
		}

		private void PawsWithExchange()
		{
			AllyTeam.DiscardCount += MiyukiResult(1);
		}

		private void PawsWithBlackFog()
		{
			Bs.FogTurn += MiyukiResult(1);
		}

		private void PawsWithCost()
		{
			var skill = AllyTeam.Skills.Where(s => s != null).ToList().Random("MiyukiCost");
			if (skill != null) skill.APChange = MiyukiResult(1, false);
		}

		private void PawsWithSwift()
		{
			var skill = AllyTeam.Skills.Where(s => s.NotCount == !MiyukiResult()).ToList().Random("MiyukiSwift");
			if (skill != null) skill.NotCount = MiyukiResult();
		}

		private void PawsWithUpgrade()
		{
			Skill skill = AllyTeam.Skills.Where(s => s != null && s.MySkill.SkillExtended == null).ToList().Random("RandomSkill");			
			List<Skill_Extended> enforce = MiyukiResult() ? PlayData.GetEnforce(true, skill) : PlayData.GetEnforce(false, skill);
			skill?.ExtendedAdd_Battle(enforce.Random("RandomEx"));
		}

		private void PawsWithDeck()
		{
			if (IsKuudere) return;

			List<string> selectedSkills = MiyukiResult() ? PosLucyDrawKeys : NegLucyDrawKeys;

			for (int i = 0; i < 2; i++)
			{
				var skill = Skill.TempSkill(selectedSkills.Random("MiyukiRandomSkill"), AllyTeam.LucyAlly, AllyTeam.LucyAlly.MyTeam);
				if (skill != null) Bs.AllyTeam.Skills_Deck.InsertRandom("MiyukiRandomInsert", skill);
				skill.isExcept = true;
				skill.MySkill.Name = "Miyuki's " + skill.MySkill.Name;
			}
		}

		private IEnumerator PawsWithDraw()
		{
			yield return null;
			BChar.BuffAdd(ModItemKeys.Buff_B_Miyuki_Passive, DummyChar);
			var buff = BChar.BuffReturn(ModItemKeys.Buff_B_Miyuki_Passive, false) as AffectionOverflow;
			buff?.ChangeDraw();
		}
		#endregion

		private void CreateCharacterLucyDraw()
		{
			if (AvaliableCharacterDraw.Count == 0 || !CreateCharacterDraw) return;

			var skill = Skill.TempSkill(AvaliableCharacterDraw.Random("MiyukiCharacterDraw"), AllyTeam.LucyAlly, AllyTeam.LucyAlly.MyTeam);
			AllyTeam.Add(skill, true);
			skill.isExcept = true;
			skill.APChange++;
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
