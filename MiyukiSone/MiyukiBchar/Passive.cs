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
	public class Passive : Passive_Char, IP_PlayerTurn, IP_SkillUse_User, IP_BattleStart_Ones
	{
		public override void Init()
		{
			base.Init();
			OnePassive = true;
		}

		private GameObject testWindow;
		private MiyukiInputEvent chatInputField;
		private bool EvenTurn => Bs?.TurnNum % 2 == 0;

		public void BattleStart(BattleSystem Ins)
		{
			//CreateWindow();
			//CreateChatWindow();
		}

		private void CreateWindow()
		{
			Sprite sprite = UtilsUI.GetSprite("MiyukiVisual/dlog_test.png");
			testWindow = UtilsUI.CreateUIImage("test", BattleSystem.instance.ActWindow.transform, sprite, new Vector2(700, 130), new Vector3(170, 170, 0), true);
			//testWindow.AddComponent<MiyukiWindow>();
			testWindow.AddComponent<DialogueBoxWindow>();
			testWindow.AddComponent<DialogueBoxDragHandler>();
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

		public void SkillUse(Skill SkillD, List<BattleChar> Targets)
		{
			if (SkillD.IsHeal && !Targets.Contains(BChar) && BChar.GetStat.maxhp != BChar.HP && SkillD.Master != BChar)
			{
				//MiyukiText(BChar, MiyukiTextState.heal, false);
				//Targets.Clear();
				//Targets.Add(MiyukiBchar);
				//MiyukiAffection.ChangePoints();
			}
		}

		public void Turn()
		{
			Affection.MiyukiTurn();
		}

		private IEnumerator FetchSkill()
		{
			//BattleSystem.I_OtherSkillSelect(Bs.AllyTeam.Skills_Deck, b => ())
			yield return null;
		}
	}
}
