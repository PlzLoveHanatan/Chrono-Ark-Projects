using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace EmotionSystem
{
	public partial class Equip
	{
		public class Technological
		{
			public class Deathseeker : EquipBase, IP_SkillUse_User, IP_PlayerTurn
			{
				private bool oncePerTurn;

				public override string DescExtended(string desc)
				{
					string text = oncePerTurn ? ModLocalization.EmotionSystem_Status_Inactive : ModLocalization.EmotionSystem_Status_Active;
					return base.DescExtended(desc).Replace("&a", text.ToString());
				}

				public override void Init()
				{
					PlusStat.HIT_CC = 25;
					PlusStat.HIT_DEBUFF = 25;
					PlusStat.HIT_DOT = 25;
				}

				public void Turn()
				{
					oncePerTurn = false;
				}

				public void SkillUse(Skill SkillD, List<BattleChar> Targets)
				{
					if (SkillD.IsDamage && SkillD.Master == BChar && !oncePerTurn)
					{
						oncePerTurn = true;
						Scripts.DestroyActions(Targets);
					}
				}
			}

			public class Devil : EquipBase
			{
				public override void Init()
				{
					PlusPerStat.Damage = 25;
					PlusStat.hit = 25;
					PlusStat.HitMaximum = true;
				}
			}
		}
	}
}
