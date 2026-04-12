using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using static MiyukiSone.Affection;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsUI;

namespace MiyukiSone
{
	public static class MiyukiCharImg
	{
		public static void UpdateCharacterImage()
		{
			if (MiyukiData.LastAffection == (int)CurrentAffection) return;

			string affection = CurrentAffection.ToString();
			string basePath = $"Assets/Images/MiyukiAffection/{affection}/";

			if (FieldSystem.instance != null && MiyukiChar != null)
			{
				UpdateCharUI(MiyukiChar, basePath);
				FieldSystem.instance.PartyWindowInit();
				EventsData.MiyukiTextEvent();
			}

			if (BattleSystem.instance != null && MiyukiBchar != null)
			{
				UpdateCharUI(MiyukiBchar, basePath);
				UpdateSkillFaces();
			}
		}

		private static void UpdateCharUI(BattleChar character, string basePath)
		{
			if (character == null) return;

			character.Info.GetData.BattleChar_Path = GetPrefabAdress(basePath + "Dress.prefab"); // Full standing pose -> Prefab
			character.Info.GetData.FaceSmallChar_Path = GetPrefabAdress(basePath + "Portrait.prefab"); // 197 x 352 -> Prefab
			character.Info.GetData.Face_SmallButton_Path = GetSpriteAddress(basePath + "SkillFace.png"); // 58 x 42 -> Sprite
			character.Info.GetData.FaceOriginChar_Path = GetPrefabAdress(basePath + "BattleFace.prefab"); // Full standing pose which is cut to small window ??? -> Prefab

			if (BattleSystem.instance != null && character == MiyukiBchar)
			{
				string faceBattle = basePath + "BattleFace.png"; // 405 x 118 -> Sprite
				AddressableLoadManager.LoadAsyncAction(GetSpriteAddress(faceBattle), AddressableLoadManager.ManageType.Character, MiyukiBchar.UI.CharImage.GetComponent<Image>());
			}
		}

		private static void UpdateSkillFaces()
		{
			foreach (Skill skill in MiyukiBchar.MyTeam.Skills)
			{
				if (skill.Master == MiyukiBchar) skill.MyButton?.ChangeFace();
			}

			foreach (CastingSkill castingSkill in MiyukiBchar.BattleInfo.CastSkills)
			{
				if (castingSkill.skill.Master == MiyukiBchar) castingSkill.skill.MyButton?.ChangeFace();
			}

			foreach (CastingSkill castingSkill in MiyukiBchar.BattleInfo.SaveSkill)
			{
				if (castingSkill.skill.Master == MiyukiBchar) castingSkill.skill.MyButton?.ChangeFace();
			}
		}
	}
}