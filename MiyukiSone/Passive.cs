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

namespace MiyukiSone
{
	public class Passive : Passive_Char, IP_DrawNumChange, IP_PlayerTurn, IP_SkillUse_User, IP_BattleStart_Ones
	{
		public override void Init()
		{
			base.Init();
			OnePassive = true;
		}

		private GameObject testWindow;
		private MiyukiInputField chatInputField;
		private bool EvenTurn => Bs?.TurnNum % 2 == 0;

		public void BattleStart(BattleSystem Ins)
		{
			//CreateWindow();
			CreateChatWindow();
		}

		private void CreateWindow()
		{
			Sprite sprite = MiyukiUI.GetSprite("MiyukiVisual/dlog_test.png");
			testWindow = MiyukiUI.CreateUIImage("test", BattleSystem.instance.ActWindow.transform, sprite, new Vector2(700, 130), new Vector3(170, 170, 0), true);
			//testWindow.AddComponent<MiyukiWindow>();
			testWindow.AddComponent<MiyukiWindow>();
			testWindow.AddComponent<MiyukiWindowDragHandler>();
		}

		private void CreateChatWindow()
		{
			chatInputField = MiyukiInputField.CreateChatInput(
				spritePath: "MiyukiVisual/dlog_test.png",
				parentWindow: BattleSystem.instance.ActWindow.transform,
				windowSize: new Vector2(700, 130),
				windowPosition: new Vector3(170, 170, 0),
				placeholder: "",
				inputPosition: new Vector2(0, -20),
				inputSize: new Vector2(400, 35));
		}

		public void DrawNumChange(int DrawNum, out int OutNum)
		{
			OutNum = EvenTurn ? -1 : 0;
		}

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
			if (SkillD.IsHeal && !Targets.Contains(MiyukiBchar) && MiyukiBchar.GetStat.maxhp != MiyukiBchar.HP && SkillD.Master != BChar)
			{
				//MiyukiText(BChar, MiyukiTextState.heal, false);
				//Targets.Clear();
				//Targets.Add(MiyukiBchar);
				//MiyukiAffection.ChangePoints();
			}
		}

		public void Turn()
		{
			if (EvenTurn)
			{
				BattleSystem.DelayInputAfter(FetchSkill());
			}
		}

		private IEnumerator FetchSkill()
		{
			//BattleSystem.I_OtherSkillSelect(Bs.AllyTeam.Skills_Deck, b => ())
			yield return null;
		}
	}
}
