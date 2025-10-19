using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using EmotionalSystem;
using GameDataEditor;
using static CharacterDocument;
using UnityEngine;
using static EmotionalSystemSkillExtended.ExEGO;
using static EmotionalSystem.Scripts;

namespace EmotionalSystemSkill
{
	public class FloorHistory
	{
		public class Abnormality
		{
			public class Loyalty : Skill_Extended
			{
				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					Utils.PlaySound("Floor_History_Loyality");

					foreach (var ally in Utils.AllyTeam.AliveChars)
					{
						//if (ally != BChar)
						//{

						//}
						Utils.AddBuff(BChar, ally, ModItemKeys.Buff_B_Abnormality_HistoryLv3_Loyalty);
					}
					Targets[0].Dead(false, true);
				}
			}
		}
	}

	public class EGO
	{
		public class FourthMatchFlame : Ex_EGO
		{
			public override void Init()
			{
				base.Init();
				SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
				Cooldown = 3;
			}

			public override void FixedUpdate()
			{
				if (BattleSystem.instance.EnemyList.Count == 1)
				{
					SkillParticleOn();
				}
				else
				{
					SkillParticleOff();
				}
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.PlaySound("Floor_History_Matchlight");

				if (Targets.Count == 1)
				{
					Utils.ApplyBurn(Targets[0], BChar, 20);
				}
				else
				{
					foreach (var target in Targets)
					{
						Utils.ApplyBurn(target, BChar, 10);
					}
				}
			}
		}

		public class GreenStem : Ex_EGO
		{
			public override void Init()
			{
				base.Init();
				Cooldown = 3;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.PlaySound("Floor_History_Malice");
			}
		}

		public class Hornet : Ex_EGO
		{
			public override void Init()
			{
				base.Init();
				Cooldown = 3;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.PlaySound("Floor_History_Hornet");
			}
		}

		public class TheForgotten : Ex_EGO
		{
			public override void Init()
			{
				base.Init();
				Cooldown = 3;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.PlaySound("Floor_History_Forgotten");
				DestroyActions(Targets[0], 2);
			}
		}

		public class Wingbeat : Ex_EGO
		{
			public override void Init()
			{
				base.Init();
				Cooldown = 3;
			}

			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				Utils.PlaySound("Floor_History_Wingbeat");
				BattleSystem.DelayInput(WingBeatHeal(BChar));
				BattleSystem.DelayInput(RecastSkill(Targets[0], BChar, ModItemKeys.Skill_S_EGO_History_Wingbeat, 2, true));
			}
		}
	}
}
