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
		public static void ChangeCharacterImage()
		{
			string relativePath = $"Assets/Images/MiyukiAffection/{CurrentAffection}/";
			string battleCharPath = relativePath + "Dress.prefab";
			string faceSmallPath = relativePath + "Portrait.prefab";
			string skillFacePath = relativePath + "SkillFace.png";
			string faceOrigin = relativePath + "BattleFace.prefab";
			string faceBattle = relativePath + "BattleFace.png"; // 405 x 118 -> Sprite

			//MiyukiBchar.Info.GetData.face_Path -> Sprite
			//MiyukiBchar.Info.GetData.Face_BigButton_Path -> Sprite
			//MiyukiBchar.Info.GetData.Face_SmallButton_Path -> Sprite
			//MiyukiBchar.Info.GetData.FaceOriginChar_Path -> Prefab

			if (BattleSystem.instance != null && MiyukiBchar != null)
			{
				MiyukiBchar.Info.GetData.BattleChar_Path = GetPrefabAdress(battleCharPath); // Full standing pose -> Prefab
				MiyukiBchar.Info.GetData.FaceSmallChar_Path = GetPrefabAdress(faceSmallPath); // 197 x 352 -> Prefab
				MiyukiBchar.Info.GetData.Face_SmallButton_Path = GetSpriteAddress(skillFacePath); // 58 x 42 -> Sprite
				MiyukiBchar.Info.GetData.FaceOriginChar_Path = GetPrefabAdress(faceOrigin);

				AddressableLoadManager.LoadAsyncAction(GetSpriteAddress(faceBattle), AddressableLoadManager.ManageType.Character, MiyukiBchar.UI.CharImage.GetComponent<Image>());

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
			

			//if (FieldSystem.instance != null)
			//{
			//	foreach (AllyWindow window in FieldSystem.instance.PartyWindow)
			//	{
			//		if (window.Info.KeyData == ModItemKeys.Character_Miyuki)
			//		{
			//			Image targetImage = window.CharFace.GetComponentInChildren<Image>();
			//			if (targetImage != null)
			//			{
			//				AddressableLoadManager.LoadAsyncAction(spriteAddress, AddressableLoadManager.ManageType.Character, targetImage);
			//				Debug.Log($"[Miyuki] Field image changed to: {spriteAddress}");
			//			}
			//		}
			//		else
			//		{
			//			Debug.LogWarning("[Miyuki] Miyuki window not found");
			//		}					
			//	}
			//}	
		}
	}
}
