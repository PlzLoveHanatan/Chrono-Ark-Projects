using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiyukiSone.Utils;
using static MiyukiSone.Affection;
using GameDataEditor;

namespace MiyukiSone
{
	public class Skills
	{
		public class Test : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				ChangeAffectionPoints(25);
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class Test2 : Skill_Extended
		{
			public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
			{
				ChangeAffectionPoints(-25);
				base.SkillUseSingle(SkillD, Targets);
			}
		}

		public class Class
		{
			public class MiyukiSoneSkill : Skill_Extended
			{
				public override void Init()
				{
					if (IsYandere)
					{
						var targetKey = MySkill.MySkill.Target.Key;

						if (targetKey == GDEItemKeys.s_targettype_enemy) MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_otherally);
						else if (targetKey == GDEItemKeys.s_targettype_all_enemy) MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_ally);
						else if (targetKey == GDEItemKeys.s_targettype_ally) MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_enemy);
						else if (targetKey == GDEItemKeys.s_targettype_all_ally) MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_all_enemy);
					}
					base.Init();
				}
			}

			public class BloomingQueen : Skill_Extended
			{

			}

			public class EternalPromise : Skill_Extended
			{

			}

			public class GlitchingPhone : Skill_Extended
			{

			}

			public class GracefulSwing : Skill_Extended
			{

			}

			public class HappyBirthday : MiyukiSoneSkill, IP_MiyukiSkillImgChange
			{
				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class LingeringDesire : Skill_Extended
			{

			}

			public class MeasuredLove : MiyukiSoneSkill, IP_MiyukiSkillImgChange
			{
				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}

			public class Pandemonium : Skill_Extended
			{

			}

			public class SweetRestraint : Skill_Extended
			{

			}

			public class WarningStrike : MiyukiSoneSkill, IP_MiyukiSkillImgChange
			{
				public void SkillImgChange(Skill mySkill)
				{
					mySkill.ChangeImg();
				}
			}
		}

		public class Rare
		{
			public class EternalKiss : Skill_Extended
			{

			}

			public class GameUpdate : Skill_Extended
			{

			}
		}

		public class Lucy
		{
			public class JustforYOU : Skill_Extended
			{

			}

			public class OnlyYOUandMe : Skill_Extended
			{

			}
		}
	}
}
