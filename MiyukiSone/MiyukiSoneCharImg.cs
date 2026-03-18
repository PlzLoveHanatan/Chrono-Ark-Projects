using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MiyukiSone.Affection;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsUI;

namespace MiyukiSone
{
	public static class MiyukiCharImg
	{
		// I didnt create this names myself, I just take it from the files data
		private static readonly List<string> FolderName = new List<string>()
		{
			"Arm Crossed",
			"Playful",
			"Hand Hip",
			"Side View",
		};

		public static void ChangeCharacterImage()
		{
			var availablePose = FolderName.ToList();
			if (MiyukiData.LastPose != -1 && availablePose.Count > 1) availablePose.RemoveAt(MiyukiData.LastPose);
			int randomIndex = RandomManager.RandomInt("MiyukiPose", 0, availablePose.Count);
			string poseFolder = availablePose[randomIndex];
			MiyukiData.LastPose = randomIndex;

			string affectionFolder = null;
			switch (CurrentAffection)
			{
				case MiyukiAffection.DereDere: affectionFolder = "DereDere"; break;
				case MiyukiAffection.Kuudere: affectionFolder = "Kuudere"; break;
				case MiyukiAffection.Yandere: affectionFolder = "Yandere"; break;
				default: break;
			}

			if (string.IsNullOrEmpty(affectionFolder)) return;

			string relativePath = $"Assets/Images/MiyukiPose/{poseFolder}/{affectionFolder}/";
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


			string spriteAddress = ThisMod.assetInfo.ImageFromFile("Assets/Images/Face/Smile/Uniform.png");
			//AddressableLoadManager.LoadAsyncAction(spriteAddress, AddressableLoadManager.ManageType.Character, MiyukiBchar.UI.CharImage.GetComponent<Image>());

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
