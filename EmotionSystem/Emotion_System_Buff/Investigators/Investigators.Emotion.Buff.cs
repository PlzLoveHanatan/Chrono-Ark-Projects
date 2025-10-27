using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class Investigators
	{
		public class EmotionBuff
		{
			public class Draw : Buff, IP_SkillUseHand_Team
			{
				public int skillUse = 0;

				public override void Init()
				{
					base.Init();
					skillUse = 0;
					LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Investigator_Draw);
				}

				public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
				{
					return AddedSkill.ExtendedFind<Extended.Emotion.Draw>() == null && AddedSkill.Master == BChar;
				}

				public void SKillUseHand_Team(Skill skill)
				{
					if (skill.Master == this.BChar)
					{
						skillUse++;

						if (skillUse >= 2)
						{
							BattleSystem.instance.AllyTeam.Draw(1);
							SelfDestroy();
						}
					}
				}
			}

			public class ManaReduction : Buff, IP_SkillUseHand_Team
			{
				public override void Init()
				{
					base.Init();
					LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended(ModItemKeys.SkillExtended_Ex_Investigator_ManaReduction);
				}

				public void SKillUseHand_Team(Skill skill)
				{
					if (skill.Master == this.BChar)
					{
						SelfDestroy();
					}
				}

				public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
				{
					return AddedSkill.ExtendedFind<Extended.Emotion.ManaReduction>() == null && AddedSkill.Master == BChar;
				}
			}
		}
	}
}
