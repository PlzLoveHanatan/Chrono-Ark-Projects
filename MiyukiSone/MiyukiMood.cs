using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MiyukiSone.MiyukiAffection;
using static MiyukiSone.Utils;
using static MiyukiSone.UtilsUI;

namespace MiyukiSone
{
	public static class MiyukiMood
	{
		// I didnt create this names myself, I just take it from the files data
		private static readonly List<string> FolderName = new List<string>()
		{
			"Arm",
			"Love",
			"Normal",
			"Sick",
		};

		public static void ChangeMood()
		{
			if (!MiyukiDecides) return;

			var availableMood = FolderName.ToList();
			if (MiyukiData.LastMood != -1 && availableMood.Count > 1) availableMood.RemoveAt(MiyukiData.LastMood);
			int randomIndex = RandomManager.RandomInt("MiyukiMood", 0, availableMood.Count);
			string moodFolder = availableMood[randomIndex];
			MiyukiData.LastMood = randomIndex;

			string affectionFolder;
			switch (CurrentAffectionState)
			{
				case MiyukiAffectionState.DereDere: affectionFolder = "DereDere"; break;
				case MiyukiAffectionState.Kuudere: affectionFolder = "Kuudere"; break;
				case MiyukiAffectionState.Yandere: affectionFolder = "Yandere"; break;
				default: affectionFolder = "Kuudere"; break;
			}

			//string relativePath = $"Assets/Images/MiyukiMood/normal/Yandere/";
			string relativePath = $"Assets/Images/MiyukiMood/{moodFolder}/{affectionFolder}/";
			string dress = SelectDressByAffection();
			string battleCharPath = relativePath + dress;
			string faceSmallPath = (dress == "DressBlood") ? relativePath + "MoodBlood" : relativePath + "Mood";
			string skillFacePath = (dress == "DressBlood") ? relativePath + "SkillFaceBlood" : relativePath + "SkillFace";

			//MiyukiBchar.Info.GetData.face_Path -> Sprite
			//MiyukiBchar.Info.GetData.Face_BigButton_Path -> Sprite
			//MiyukiBchar.Info.GetData.Face_SmallButton_Path -> Sprite
			//MiyukiBchar.Info.GetData.FaceOriginChar_Path -> Prefab

			MiyukiBchar.Info.GetData.BattleChar_Path = GetPrefabAdress(battleCharPath + ".prefab"); // Full standing pose -> Prefab
			MiyukiBchar.Info.GetData.FaceSmallChar_Path = GetPrefabAdress(faceSmallPath + ".prefab"); // 197 x 352 -> Prefab
			MiyukiBchar.Info.GetData.Face_SmallButton_Path = GetSpriteAddress(skillFacePath + ".png"); // 58 x 42 -> Sprite

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

		private static string SelectDressByAffection()
		{
			Dictionary<MiyukiAffectionState, int[]> chances = new Dictionary<MiyukiAffectionState, int[]>
			{
				{ MiyukiAffectionState.DereDere, new int[] { 30, 50, 20 } },
				{ MiyukiAffectionState.Kuudere, new int[] { 50, 30, 20 } },
				{ MiyukiAffectionState.Yandere, new int[] { 20, 20, 60 } }
			};

			if (!chances.ContainsKey(CurrentAffectionState)) return "Uniform";

			int[] stateChances = chances[CurrentAffectionState];
			int random = RandomManager.RandomInt("MiyukiRandomDress", 0, 100);
			int cumulative = 0;

			string[] dresses = new string[] { "Uniform", "Dress", "DressBlood" };

			for (int i = 0; i < stateChances.Length; i++)
			{
				cumulative += stateChances[i];
				if (random < cumulative) return dresses[i];
			}

			return "Uniform";
		}
	}
}
