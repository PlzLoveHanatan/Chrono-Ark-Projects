using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using static CharacterDocument;
using static EmotionSystem.Extended.EGO;
using static EmotionSystem.Scripts;

namespace EmotionSystem
{
	public class TechnologicalSkill
	{
		public class Abnormality
		{
			public class DarkFlame : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Technological_DarkFlame");

					foreach (var target in Utils.AllyTeam.AliveChars.Concat(Utils.EnemyTeam.AliveChars))
					{
						Utils.AddBuff(target, ModItemKeys.Buff_B_Abnormality_TechnologicalLv3_DarkFlame_0);
					}
				}
			}

			public class Music : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Technological_Music");
				}
			}
		}

		public class EGO
		{
			public class GrinderMk : Ex_EGO
			{
				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Technological_Clean");

					foreach (var target in Targets)
					{
						Utils.ApplyBleed(target, BChar, 10);
					}
				}
			}

			public class Harmony : Ex_EGO
			{
				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Technological_Harmony");
				}
			}

			public class MagicBullet : Ex_EGO
			{
				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.AllyTeam.Draw(2);
				}
			}

			public class Regret : Ex_EGO
			{
				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_Technological_Violence");

					if (Targets[0].BuffReturn(GDEItemKeys.Buff_B_Common_Rest) != null && !SkillD.FreeUse)
					{
						DestroyActions(Targets[0], 1);
					}
					BattleSystem.DelayInput(RecastSkill(Targets[0], BChar, MySkill.MySkill.KeyID, 3));
				}
			}

			public class SolemnLament : Ex_EGO
			{
				public override void Init()
				{
					base.Init();
					Cooldown = 3;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					BattleSystem.DelayInput(RecastSkill(Targets[0], BChar, MySkill.MySkill.KeyID, 8));
				}
			}
		}

		public class Syncronize
		{
			public class Desynchronize : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					DeSynchronize(BChar);
				}
			}

			public class FloodingBullets : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					BattleSystem.DelayInput(RecastSkill(Targets[0], BChar, MySkill.MySkill.KeyID, 2));
				}
			}

			public class InevitableBullet : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					// ???
				}
			}

			public class MagicBullet : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					BattleSystem.instance.AllyTeam.AP += 1;
				}
			}

			public class SilentBullet : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					//DestroyActions(Targets[0]);
				}
			}
		}
	}
}
