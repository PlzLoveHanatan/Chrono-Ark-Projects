using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace ZenaBaral
{
	public partial class Skills
	{
		public class ZenaSkill : Skill_Extended
		{
			private readonly Dictionary<string, string> ZenaSounds = new Dictionary<string, string>
			{
				{ ModItemKeys.Skill_S_Zena_Line, "Zena_Line" },
				{ ModItemKeys.Skill_S_Zena_Line_0, "Zena_Line_0" },
				{ ModItemKeys.Skill_S_Zena_Line_1, "Zena_Line_1" },
				{ ModItemKeys.Skill_S_Zena_Rare_BirdCage, "Zena_Bird_Cage" },
				{ ModItemKeys.Skill_S_Zena_Rare_Shockwave, "Zena_Shockwave" },
			};

			public override void Init()
			{
				base.Init();
				PlusSkillStat.Penetration = 100f;

				if (MySkill.AllExtendeds != null && MySkill.AllExtendeds.Count > 0 && MySkill.MySkill.Name.Contains("<color="))
				{
					string name = MySkill.MySkill.Name;
					name = System.Text.RegularExpressions.Regex.Replace(name, "<color=#.*?>", string.Empty);
					name = name.Replace("</color>", string.Empty);
					MySkill.MySkill.Name = name;
				}
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				var key = MySkill.MySkill.KeyID;
				if (ZenaSounds.TryGetValue(key, out string sfx))
				{
					ZenaScripts.PlaySounds(sfx);
				}
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class BirdCage : ZenaSkill
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				ZenaScripts.PlaySounds("Zena_Bird_Cage");
				Scripts.DestroyActions(Targets);
			}
		}
	}
}
