using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using ChronoArkMod.ModEditor;
using UnityEngine;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using static MiyukiSone.EventStringLoader;
using static MiyukiSone.EventData;
using System.Runtime.CompilerServices;
using GameDataEditor;
using NLog.Targets;
using System.EnterpriseServices;

namespace MiyukiSone
{
	public class Passive : Passive_Char, IP_PlayerTurn, IP_BattleStart_Ones, IP_DamageTake, IP_Healed, IP_DrawNumChange, IP_LevelUp, IP_Targeted
	{
		private MiyukiInputEvent chatInputField;

		private readonly HashSet<string> edgeCaseSkills = new HashSet<string>()
		{
			GDEItemKeys.Skill_S_Witch_P_0,
			GDEItemKeys.Skill_S_Witch_2,
			//GDEItemKeys.Skill_S_ShadowPriest_12,
			//GDEItemKeys.Skill_S_ShadowPriest_3,
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
			ChangeAffectionPoints(1);
		}

		public void BattleStart(BattleSystem Ins)
		{
			//CreateWindow();
			//CreateChatWindow();
			ChangeAffectionPoints(25);
		}

		public void DrawNumChange(int DrawNum, out int OutNum)
		{
			if (!MiyukiActing)
			{
				OutNum = DrawNum;
			}
			else
			{
				OutNum = IsLoving ? DrawNum + 1 : IsHating ? DrawNum - 1 : DrawNum;
				MiyukiTextEvent(EventState.draw);
			}
		}

		public void Turn()
		{
			AllyTeam.AliveChars.Where(a => a != BChar).ToList().ForEach(a => SecureBuff(a, BChar, ""));
			MiyukiTurn();
		}

		// decrease affection
		public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
		{
			if (User == BChar && Dmg >= 1 && !resist)
			{
				ChangeAffectionPoints(-1);
			}
		}

		// increase affection
		public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
		{
			if (HealedChar == BChar && Healer != BChar)
			{
				ChangeAffectionPoints(1);
			}
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

			EventState state = EventState.other;
			BattleChar newTarget = null;

			if (edgeCaseSkills.Contains(SkillD.MySkill.KeyID))
			{
				newTarget = aliveAllies[randomIndex];
				state = EventState.other;
			}
			else if (SkillD.IsDamage)
			{
				newTarget = aliveAllies[randomIndex];
				state = EventState.redirectAttack;
			}
			else return;

			Targets.Clear();
			Targets.Add(newTarget);

			ChangeAffectionPoints(-1);
			BattleSystem.DelayInputAfter(ShowMiyukiEventText(state));
		}

		private IEnumerator ShowMiyukiEventText(EventState state)
		{
			yield return null;
			MiyukiTextEvent(state);
		}

		//public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		//{
		//	if (SkillD.IsHeal && !Targets.Contains(BChar) && BChar.HP < BChar.GetStat.maxhp)
		//	{
		//		Targets.Clear();
		//		Targets.Add(BChar);
		//		MiyukiTextEvent(EventState.redirectHeal);
		//	}
		//}
	}
}
