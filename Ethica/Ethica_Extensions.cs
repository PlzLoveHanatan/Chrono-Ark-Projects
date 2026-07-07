using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace Ethica
{
	public static class Extensions
	{
		public static void ToolTip(this Skill skill)
		{
			if (skill.MySkill.PlusKeyWords.FirstOrDefault(k => k.Key == ModItemKeys.SkillKeyword_Ethica_Rare_Skill) == null) skill.MySkill.PlusKeyWords.Add(new GDESkillKeywordData(ModItemKeys.SkillKeyword_Ethica_Rare_Skill));
		}
	}
}
