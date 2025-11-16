using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Extended
	{
		public class Abnormality
		{
			public class HappyMemories : BuffSkillExHand
			{
				public override void Init()
				{
					base.Init();
					APChange = -1;
				}
			}

			public class SeventhBullet : BuffSkillExHand
			{

			}

			public class Friend : Skill_Extended
			{
				public override void Init()
				{
					PlusSkillPerFinal.Heal = 40;
					PlusSkillPerFinal.Damage = 40;
				}
			}

			public class Friend_0 : Skill_Extended
			{
				public override void Init()
				{
					PlusSkillPerFinal.Heal = 80;
					PlusSkillPerFinal.Damage = 80;
					MySkill.AutoDelete = 1;
				}
			}

			public class Friend_1 : Skill_Extended
			{
				public override void Init()
				{
					PlusSkillPerFinal.Heal = 80;
					PlusSkillPerFinal.Damage = 80;
				}
			}

			public class Peeble : Skill_Extended
			{
				public override void Init()
				{
					//MySkill.MySkill.Disposable = true;
				}

				public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
				{
					BChar.StartCoroutine(Utils.HealingParticle(null, BChar, 10, true, true, true, true, true));
					Utils.RemoveSkill(MySkill, true);
				}
			}
		}
	}
}
