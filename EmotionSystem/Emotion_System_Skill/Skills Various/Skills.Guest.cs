using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;

namespace EmotionSystem
{
	public partial class Skills
	{
		public class Guest
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

			public class WitchCurses
			{
				public class Pain : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						Utils.CreateSkill(Utils.AllyTeam.LucyAlly, GDEItemKeys.Skill_S_Witch_P_0, true);
					}
				}

				public class Weak : Skill_Extended
				{
					public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
					{
						Utils.CreateSkill(Utils.AllyTeam.LucyAlly, GDEItemKeys.Skill_S_Witch_2, true);
					}
				}
			}

			public class Burst : Skill_Extended
			{
				public override string DescExtended(string desc)
				{
					int damage = 10 + (PlayData.TSavedata.StageNum * 5);
					return base.DescExtended(desc).Replace("&a", damage.ToString());
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					int damage = 10 + (PlayData.TSavedata.StageNum * 5);
					int splitDamage = (int)Math.Ceiling(damage / (double)Targets.Count);

					foreach (var target in Targets)
					{
						Utils.TakeNonLethalDamage(target, splitDamage);
					}
				}
			}
		}
	}
}
