using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using GameDataEditor;
using UnityEngine;

namespace EmotionalSystemSkill
{
	public class Guest
	{
		public class Abnormality
		{
			public class Present : Skill_Extended, IP_Draw
			{
				public override void Init()
				{
					OnePassive = true;
				}

				public IEnumerator Draw(Skill Drawskill, bool NotDraw)
				{
					if (Drawskill == MySkill)
					{
						var ally = Utils.RandomAlly();
						Utils.TakeNonLethalDamage(ally, 10, true);
						Utils.AllyTeam.Draw();
					}
					return base.DrawAction();
				}
			}

			public class DimensionalRefraction : Skill_Extended
			{
				public override bool Terms()
				{
					return false;
				}

				private GameObject glitchEffect;

				public override void FixedUpdate()
				{
					if (BattleSystem.instance == null || MySkill?.MyButton == null) return;

					if (glitchEffect == null)
					{
						var prefab = Resources.Load<GameObject>("StoryGlitch/GlitchSkillEffect");

						glitchEffect = UnityEngine.Object.Instantiate(prefab, MySkill.MyButton.transform);
						glitchEffect.SetActive(true);
					}
				}
			}
		}

		public class GuestSkill
		{
			public class Witch_Curse_Pain : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.CreateSkill(Utils.AllyTeam.LucyAlly, GDEItemKeys.Skill_S_Witch_P_0, true);
				}
			}

			public class Witch_Curse_Weak : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.CreateSkill(Utils.AllyTeam.LucyAlly, GDEItemKeys.Skill_S_Witch_2, true);
				}
			}
		}
	}
}
