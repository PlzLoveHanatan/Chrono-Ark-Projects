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
using static MiyukiSone.MiyukiAffection;
using static MiyukiSone.EventData;
using static MiyukiSone.DialogueData;
using static MiyukiSone.UtilsScripts;
using static MiyukiSone.Dialogue;
using static MiyukiSone.MiyukiMood;
using DarkTonic.MasterAudio;
using System.EnterpriseServices;
using UnityEngine.UI;

namespace MiyukiSone
{
	public class MiyukiPassive : Passive_Char, IP_PlayerTurn, IP_BattleStart_Ones, IP_DamageTake, IP_Healed, IP_DrawNumChange, IP_LevelUp, IP_Targeted, IP_TurnEnd, IP_MiyukiMoodChange
	{
		private MiyukiInputEvent chatInputField;


		public List<string> MiyukiChoiceList = new List<string>();
		private readonly List<Action> MiyukiPaws;

		public MiyukiPassive()
		{
			MiyukiPaws = new List<Action>()
			{
				PawsWithMana,
				PawsWithExchange,
				PawsWithStandBy,
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

		private readonly Dictionary<string, string> characterDrawList = new Dictionary<string, string>()
		{
			{ ModItemKeys.Skill_S_GracefulSwing, GDEItemKeys.Skill_S_Priest_7_LucyD },// Divine Revelation
			{ ModItemKeys.Skill_S_WarningStrike, GDEItemKeys.Skill_S_Control_3_Draw }, // Insight 
			{ ModItemKeys.Skill_S_EternalVow, GDEItemKeys.Skill_S_MissChain_12_LucyD }, // Burning Draw
			//{ ModItemKeys.Skill_S_HappyBirthday, GDEItemKeys.Skill_S_Lucy_24 }, // Change of Plans
			{ ModItemKeys.Skill_S_HappyBirthday, GDEItemKeys.Skill_S_LucyD_7 }, // Renovate
		};

		private readonly List<string> avaliableCharacterDraw = new List<string>();

		public override void Init()
		{
			base.Init();
			OnePassive = true;
		}

		public void MiyukiMoodChange()
		{
			ChangeMood();
		}

		public void LevelUp()
		{
			ChangeAffectionPoints();
		}

		public void BattleStart(BattleSystem Ins)
		{
			avaliableCharacterDraw.Clear();
			AllyTeam.Skills_Deck.ForEach(s =>
			{
				if (characterDrawList.TryGetValue(s.MySkill.KeyID, out string drawKey))
				{
					avaliableCharacterDraw.Add(drawKey);
				}
			});

			//BattleFaceChange();
			PawsWithDeck();

			//CreateWindow();
			//CreateChatWindow();
			//ChangeAffectionPoints(25);
			ChangeAffectionPoints(0);
			//if (MiyukiDecides) PawsWithDeck(MiyukiMood);
		}

		public void DrawNumChange(int DrawNum, out int OutNum)
		{
			int newDrawNum = MiyukiResult(1);
			OutNum = newDrawNum != 0 ? DrawNum += newDrawNum : DrawNum;
			if (newDrawNum != 0) MiyukiTextEvent(MiyukiInMood);
			Debug.Log($"Draw num = {newDrawNum}");
		}

		public void Turn()
		{
			MiyukiTurn();
			MiyukiTurnPaw();
			CreateCharacterLucyDraw();
		}

		public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
		{
			if (Dmg >= 1) ChangeAffectionPoints(-1);
			if (BChar.HP < 0 && AllyTeam.AliveChars.Count > 0) resist = true;
		}

		public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
		{
			if (HealedChar == BChar && Healer != BChar) ChangeAffectionPoints(1);
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
			ChangeAffectionPoints(-1);
			//EventYandere.YandereAction(false);
			BChar.StartCoroutine(ShowMiyukiEventText());
		}

		private IEnumerator ShowMiyukiEventText()
		{
			yield return null;
			MiyukiTextEvent(false);
		}

		public void TurnEnd()
		{
			UnlockNextTurnEndTry();
		}

		private void MiyukiTurnPaw()
		{
			AllyTeam.AliveChars.Where(a => a != BChar).ToList().ForEach(a => SecureBuff(a, BChar, ModItemKeys.Buff_B_Miyuki_Buff));
			if (!MiyukiDecides) return;

			if (Bs.TurnNum >= Bs.FogTurn && !IsYandere) goto MiyukiHelp;
			else
			{
				var paw = MiyukiPaws.ToList();
				if (MiyukiData.LastTurnAction != -1 && paw.Count > 1) paw.RemoveAt(MiyukiData.LastTurnAction);
				int randomIndex = RandomManager.RandomInt("MiyukiPaw", 0, paw.Count);
				paw[randomIndex].Invoke();
				MiyukiData.LastTurnAction = randomIndex;
			}

		MiyukiHelp:;
			//CreateDialogue(DialogueState.help);
		}


		private void CreateInnerDesire()
		{
			// for allies
		}

		private void CreateCharacterLucyDraw()
		{
			if (avaliableCharacterDraw.Count == 0 || Bs.TurnNum == 1) return;

			var skill = Skill.TempSkill(avaliableCharacterDraw.Random("MiyukiRandomDraw"), AllyTeam.LucyAlly, AllyTeam.LucyAlly.MyTeam);
			AllyTeam.Add(skill, true);
			skill.isExcept = true;
			skill.APChange++;
			skill.AutoDelete = 1;
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


		private void PawsWithDeck()
		{
			if (!MiyukiDecides) return;

			List<string> posSkillKey = new List<string>()
			{
				GDEItemKeys.Skill_S_LucyD_3, // Search
				GDEItemKeys.Skill_S_LucyD_16, // Deep in Thought
				GDEItemKeys.Skill_S_PopcornGirl_Lucy_1, // Tasty Popcorn!
				GDEItemKeys.Skill_S_PopcornGirl_Lucy_2, // Caramel Popcorn!
				GDEItemKeys.Skill_S_PopcornGirl_Lucy_3, // Spicy Popcorn!
			};

			List<string> negSkillKey = new List<string>()
			{
				GDEItemKeys.Skill_S_Transcendence_Main, // Bloodmist
				GDEItemKeys.Skill_S_LucyCurse_CursedClock,
				GDEItemKeys.Skill_S_LucyCurse_Banana,
				GDEItemKeys.Skill_S_LucyCurse_Late,
				GDEItemKeys.Skill_S_LucyCurse_Heavy,
			};

			List<string> selectedSkills = MiyukiInMood ? posSkillKey : negSkillKey;

			for (int i = 0; i < MiyukiResult(1); i++)
			{
				var skill = Skill.TempSkill(selectedSkills.Random("MiyukiRandomSkill"), AllyTeam.LucyAlly, AllyTeam.LucyAlly.MyTeam);
				if (skill != null) Bs.AllyTeam.Skills_Deck.InsertRandom("MiyukiRandomInsert", skill);
				//skill.isExcept = true;
			}
		}
	}
}
