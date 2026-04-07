using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static MiyukiSone.Affection;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsUI;

namespace MiyukiSone
{
	public static class MiyukiCharImg
	{
		// Old assets
		private static readonly List<string> FolderName = new List<string>()
		{
			"Arm Crossed",
			"Playful",
			"Hand Hip",
			"Side View",
		};

		public static void ChangeCharacterImage()
		{
			string relativePath = $"Assets/Images/MiyukiAffection/{CurrentAffection}/";
			string battleCharPath = relativePath + "Dress.prefab";
			string faceSmallPath = relativePath + "Portrait.prefab";
			string skillFacePath = relativePath + "SkillFace.png";

			//MiyukiBchar.Info.GetData.face_Path -> Sprite
			//MiyukiBchar.Info.GetData.Face_BigButton_Path -> Sprite
			//MiyukiBchar.Info.GetData.Face_SmallButton_Path -> Sprite
			//MiyukiBchar.Info.GetData.FaceOriginChar_Path -> Prefab

			MiyukiBchar.Info.GetData.BattleChar_Path = GetPrefabAdress(battleCharPath); // Full standing pose -> Prefab
			MiyukiBchar.Info.GetData.FaceSmallChar_Path = GetPrefabAdress(faceSmallPath); // 197 x 352 -> Prefab
			MiyukiBchar.Info.GetData.Face_SmallButton_Path = GetSpriteAddress(skillFacePath); // 58 x 42 -> Sprite


			string spriteAddress = GetSpriteAddress($"{relativePath}MiyukiBattleFace.png"); // 405 x 118 -> Sprite
			AddressableLoadManager.LoadAsyncAction(spriteAddress, AddressableLoadManager.ManageType.Character, MiyukiBchar.UI.CharImage.GetComponent<Image>());

			foreach (Skill skill in MiyukiBchar.MyTeam.Skills)
			{
				if (skill.Master == MiyukiBchar)
				{
					skill.MyButton?.ChangeFace();
				}
			}

			foreach (CastingSkill castingSkill in MiyukiBchar.BattleInfo.CastSkills)
			{
				if (castingSkill.skill.Master == MiyukiBchar)
				{
					castingSkill.skill.MyButton?.ChangeFace();
				}
			}

			foreach (CastingSkill castingSkill in MiyukiBchar.BattleInfo.SaveSkill)
			{
				if (castingSkill.skill.Master == MiyukiBchar)
				{
					castingSkill.skill.MyButton?.ChangeFace();
				}
			}
		}
	}
}
