using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace EmotionSystem
{
	public class Buffs
	{
		public class Lucy
		{
			public class MusicBox : Buff, IP_SkillUse_User, IP_Dodge, IP_DamageTake, IP_Healed, IP_DealDamage, IP_Kill, IP_SomeOneDead
			{
				public override void SelfdestroyPlus()
				{
					EmotionalManager.SetEmotionCapInvestigator(BChar, true);
				}

				public void SomeOneDead(BattleChar DeadChar)
				{
					if (BChar.Info.Ally == DeadChar.Info.Ally && DeadChar != BChar)
					{
						BChar.GetNegEmotion(null, 3);
					}
				}

				public void SkillUse(Skill SkillD, List<BattleChar> Targets)
				{
					if (SkillD.IsHeal || SkillD.IsDamage && SkillD.Master == BChar)
					{
						BChar.GetPosEmotion();
					}

					else if (SkillD.Master == BChar)
					{
						BChar.GetNegEmotion();
					}
				}

				public void KillEffect(SkillParticle SP)
				{
					if (SP.UseStatus == BChar)
					{
						BChar.GetPosEmotion(null, 3);
					}
				}

				public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
				{
					if (Damage >= 1)
					{
						BChar.GetPosEmotion();
					}

					if (IsCri)
					{
						BChar.GetPosEmotion();
					}
				}

				public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
				{
					if (Dmg >= 1)
					{
						BChar.GetNegEmotion();

						if (Cri)
						{
							BChar.GetNegEmotion();
						}
					}
				}

				public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
				{
					if (HealedChar == BChar)
					{
						BChar.GetPosEmotion();
					}
				}

				public void Dodge(BattleChar Char, SkillParticle SP)
				{
					if (Char == BChar)
					{
						BChar.GetPosEmotion();
					}
				}
			}
		}

		public class Potions
		{
			public class PureTune : Lucy.MusicBox
			{
				
			}

			public class DarkTune : Buff
			{
				public override void SelfdestroyPlus()
				{
					EmotionalManager.SetEmotionCapGuest(BChar, false);
				}
			}
		}
	}	
}
