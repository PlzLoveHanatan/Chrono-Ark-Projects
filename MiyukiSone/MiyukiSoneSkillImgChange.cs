using System.Collections.Generic;
using UnityEngine;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class MiyukiSkillImgChange
	{
		private static readonly Dictionary<string, string> SkillPaths = new Dictionary<string, string>
		{
			{ "S_Miyuki_HappyBirthday_DereDere", "Assets/Images/Skills/HappyBirthday/DereDere" },
			{ "S_Miyuki_HappyBirthday_Yandere", "Assets/Images/Skills/HappyBirthday/Yandere" },
			{ "S_Miyuki_WarningStrike_DereDere", "Assets/Images/Skills/WarningStrike/DereDere" },
			{ "S_Miyuki_MeasuredLove_DereDere", "Assets/Images/Skills/MeasuredLove/DereDere" },
			{ "S_Miyuki_MeasuredLove_Yandere", "Assets/Images/Skills/MeasuredLove/Yandere" },
			{ "S_Miyuki_EternalPromise_DereDere", "Assets/Images/Skills/EternalPromise/DereDere" },
			{ "S_Miyuki_EternalPromise_Yandere", "Assets/Images/Skills/EternalPromise/Yandere" },
			{ "S_Miyuki_Rare_GameUpdate_Yandere", "Assets/Images/Skills/GameUpdate/Yandere" }
		};

		public static void ChangeSkillImg(this Skill skill)
		{
			if (skill == null || Utils.MiyukiData.LastAffection == (int)CurrentAffection) return;

			string key = $"{skill.MySkill.KeyID}_{CurrentAffection}";

			if (SkillPaths.TryGetValue(key, out string folderPath))
			{
				skill.ChangeSkillImage(folderPath + "/skill", folderPath + "/button", folderPath + "/basic", isGlicthEffect: true );
			}
			else
			{
				skill.ChangeSkillImage(isRestoreImg: true, defaultSkillKey: skill.MySkill.KeyID, isGlicthEffect: true);
			}
		}
	}
}