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
using static MiyukiSone.DialogueBoxData;
using static MiyukiSone.UtilsScripts;
using static MiyukiSone.DialogueBox;
using DarkTonic.MasterAudio;

namespace MiyukiSone
{
	public class Passive : Passive_Char, IP_PlayerTurn, IP_BattleStart_Ones, IP_DamageTake, IP_Healed, IP_DrawNumChange, IP_LevelUp, IP_Targeted, IP_TurnEnd
	{
		private MiyukiInputEvent chatInputField;

		private readonly HashSet<string> edgeCaseSkills = new HashSet<string>()
		{
			GDEItemKeys.Skill_S_Witch_P_0,
			GDEItemKeys.Skill_S_Witch_2,
			GDEItemKeys.Skill_S_ShadowPriest_12,
			GDEItemKeys.Skill_S_ShadowPriest_3,
			GDEItemKeys.Skill_S_Queen_7,
			GDEItemKeys.Skill_S_Queen_13,
		};

		public override void Init()
		{
			base.Init();
			OnePassive = true;
		}

		public void LevelUp()
		{
			ChangeAffectionPoints(2);
		}

		public void BattleStart(BattleSystem Ins)
		{
			//CreateWindow();
			//CreateChatWindow();
			//ChangeAffectionPoints(25);
			if (MiyukiDecides) PawsWithDeck(MiyukiMood);
		}

		public void DrawNumChange(int DrawNum, out int OutNum)
		{
			int newDrawNum = MiyukiOutcome(1);
			OutNum = newDrawNum != 0 ? DrawNum += newDrawNum : DrawNum;
			if (newDrawNum != 0) MiyukiTextEvent();
		}

		public void Turn()
		{
			MiyukiTurn();
			//MiyukiTurnAction();
		}

		// decrease affection
		public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
		{
			if (User != BChar) return;
			if (Dmg >= 1) ChangeAffectionPoints(-1);
			if (BChar.HP < 0) resist = true;
		}

		// increase affection
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

			if (edgeCaseSkills.Contains(SkillD.MySkill.KeyID)) newTarget = aliveAllies[randomIndex];
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

		private void MiyukiTurnAction()
		{
			AllyTeam.AliveChars.Where(a => a != BChar).ToList().ForEach(a => SecureBuff(a, BChar, ""));
			if (!MiyukiDecides) return;

			if (Bs.TurnNum == Bs.FogTurn && !IsYandere) goto MiyukiHelp;

			var actions = new List<Action<bool>>
			{
				PawsWithHand,
				PawsWithAllies,
				PawsWithEnemies,
				PawsWithMana
			};

			if (MiyukiData.LastTurnAction != -1 && actions.Count > 1) actions.RemoveAt(MiyukiData.LastTurnAction);
			int randomIndex = RandomManager.RandomInt("MiyukiTurnAction", 0, actions.Count);
			actions[randomIndex].Invoke(MiyukiMood);
			MiyukiData.LastTurnAction = randomIndex;

		MiyukiHelp:;
			CreateDialogueBox(DialogueBoxState.help);
		}

		private void PawsWithHand(bool isPositive)
		{
			var skillList = isPositive ? AllyTeam.Skills_Deck : AllyTeam.Skills;
			var action = isPositive ? new SkillButton.SkillClickDel(b => b.Myskill.Master.MyTeam.ForceDraw(b.Myskill)) : new SkillButton.SkillClickDel(b => b.Waste());
			var title = isPositive ? ScriptLocalization.System_SkillSelect.DrawSkill : ScriptLocalization.System_SkillSelect.WasteSkill;
			BattleSystem.DelayInputAfter(BattleSystem.I_OtherSkillSelect(skillList, action, title, false, true, true, false, true));
		}


		private void PawsWithAllies(bool isPositive)
		{
			if (isPositive) HealLowestAlly(BChar, (int)BChar.GetStat.reg);
			else AllyTeam.AliveChars.Where(a => a.Info.KeyData != ModItemKeys.Character_Miyuki).ToList().Random("MiyukiRandom").Damage(MiyukiBchar, PlayData.TSavedata.StageNum * 10, false, true);

		}

		private void PawsWithEnemies(bool isPositive)
		{
			if (isPositive) RemoveActions(Bs.EnemyTeam.AliveChars_Vanish);
			else Bs.EnemyTeam.AliveChars_Vanish.ForEach(e => AddBuff(e, ""));
		}

		private void PawsWithMana(bool isPositive)
		{
			AllyTeam.AP += MiyukiOutcome(1);
		}

		private void PawsWithDeck(bool isPositive)
		{
			if (isPositive)
			{
				for (int i = 0; i < PlayData.TSavedata.LucySkills.Count; i++)
				{
					var skillData = new GDESkillData(PlayData.TSavedata.LucySkills[i]);
					if (skillData.User == "LucyCurse" || skillData.KeyID == GDEItemKeys.Skill_S_S1_LittleMaid_0_Lucy)
					{
						PlayData.TSavedata.LucySkills.RemoveAt(i);
						i--;
					}
				}

				for (int i = 0; i < AllyTeam.Skills_Deck.Count; i++)
				{
					var skill = AllyTeam.Skills_Deck[i];

					if (skill?.MySkill != null && skill.MySkill.User == "LucyCurse" || skill?.MySkill?.KeyID == GDEItemKeys.Skill_S_S1_LittleMaid_0_Lucy || skill?.MySkill.KeyID == GDEItemKeys.Skill_S_Transcendence_Main)
					{
						AllyTeam.Skills_Deck.Remove(skill);
						MasterAudio.StopBus("SE");
						MasterAudio.PlaySound("WaterSpell", 100f, null, 0f, null, null, false, false);
					}
				}
			}
			else
			{
				for (int i = 0; i < 2; i++)
				{
					Bs.AllyTeam.Skills_Deck.InsertRandom("MiyukiBm", Skill.TempSkill(GDEItemKeys.Skill_S_Transcendence_Main));
				}
			}
		}
	}
}
