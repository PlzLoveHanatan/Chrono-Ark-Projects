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
		public class BaralSkill : Skill_Extended
		{
			private readonly Dictionary<string, string> BaralSounds = new Dictionary<string, string>
			{
				{ ModItemKeys.Skill_S_Baral_Serum, "Baral_Serum" },
				{ ModItemKeys.Skill_S_Baral_Serum_0, "Baral_Serum" },
				{ ModItemKeys.Skill_S_Baral_Serum_1, "Baral_Serum" },
				{ ModItemKeys.Skill_S_Baral_Extirpation, "Baral_Extirpation" },
				{ ModItemKeys.Skill_S_Baral_Trail, "Baral_Trail" },
				{ ModItemKeys.Skill_S_Baral_Rare_Cocktail, "Baral_Tri_Serum" },
			};

			public override void Init()
			{
				base.Init();

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
				if (BaralSounds.TryGetValue(key, out string sfx))
				{
					ZenaScripts.PlaySounds(sfx);
				}
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class Trail : BaralSkill
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.ApplyBleed(Targets, BChar, 10);
			}
		}

		public class Extirpation : BaralSkill
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				foreach (var target in Targets)
				{
					if (target.Info.Ally) continue;

					int damage = (int)(target.GetStat.maxhp * 0.1f);
					target.Damage(Utils.DummyChar, damage, false, true);
				}
			}
		}
	}
}
