using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EmotionSystem;
using GameDataEditor;
using static EmotionSystem.Investigators.Emotion.Level;

namespace EmotionSystem
{
	public class Buffs
	{
		public class Lucy
		{
			public class MusicBox : Buff, IP_SkillUse_User, IP_Dodge, IP_DamageTake, IP_Healed, IP_DealDamage, IP_Kill, IP_SomeOneDead
			{
				private void GainPosOrNegPoints(BattleChar user, Vector3? pos = null, int amount = 1, bool isPositive = true)
				{
					if (Utils.ReturnBuff(user, ModItemKeys.Buff_B_Investigator_Emotional_Level) is Investigators.Emotion.Level level)
					{
						level.GainPosOrNegPoints(user, pos, amount, isPositive);
					}
					else
					{
						if (isPositive)
						{
							user.GetPosEmotion(pos, amount);
						}
						else
						{
							user.GetNegEmotion(pos, amount);
						}
					}
				}

				public override void SelfdestroyPlus()
				{
					EmotionManager.SetEmotionCapInvestigator(BChar, true);
				}

				public void SomeOneDead(BattleChar DeadChar)
				{
					if (DeadChar != BChar && DeadChar.Info.Ally)
					{
						var pos = DeadChar.GetPosUI();
						GainPosOrNegPoints(BChar, pos, 3, false);
					}
				}

				public void SkillUse(Skill SkillD, List<BattleChar> Targets)
				{
					if (SkillD.Master == BChar)
					{
						if (SkillD.IsHeal || SkillD.IsDamage)
						{
							GainPosOrNegPoints(BChar, SkillD.GetPosUI(), 1, true);
						}
						else
						{
							GainPosOrNegPoints(BChar, SkillD.GetPosUI(), 1, false);
						}
					}
				}

				public void KillEffect(SkillParticle SP)
				{
					if (SP.UseStatus == BChar)
					{
						var pos = SP.UseStatus.GetPosUI();
						GainPosOrNegPoints(BChar, pos, 3, true);
					}
				}

				public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
				{
					if (Damage >= 1)
					{
						GainPosOrNegPoints(BChar, null, 1, true);
					}

					if (IsCri)
					{
						GainPosOrNegPoints(BChar, null, 1, true);
					}
				}

				public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
				{
					if (Dmg >= 1)
					{
						GainPosOrNegPoints(BChar, User.GetPosUI(), 1, false);
						BattleSystem.DelayInput(DeathDoorCheck());

						if (Cri)
						{
							GainPosOrNegPoints(BChar, User.GetPosUI(), 1, false);
						}
					}
				}

				private IEnumerator DeathDoorCheck()
				{
					if (Utils.ReturnBuff(BChar, GDEItemKeys.Buff_B_Neardeath) != null)
					{
						foreach (var ally in BChar.MyTeam.AliveChars_Vanish)
						{
							GainPosOrNegPoints(ally, null, 1, false);
						}
					}

					yield break;
				}

				public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
				{
					if (HealedChar == BChar)
					{
						GainPosOrNegPoints(BChar, Healer.GetPosUI(), 1, true);
					}
				}

				public void Dodge(BattleChar Char, SkillParticle SP)
				{
					if (Char == BChar)
					{
						GainPosOrNegPoints(BChar, Char.GetPosUI(), 1, true);
					}
				}
			}
		}

		public class Potions
		{
			public class PureTune : Lucy.MusicBox
			{

			}

			public class DarkTune : Buff, IP_PlayerTurn
			{
				public override void SelfdestroyPlus()
				{
					EmotionManager.SetEmotionCapGuest(BChar, false);
				}

				public void Turn()
				{
					SelfDestroy();
				}
			}

			public class EtherealNote : Buff
			{
				public override void Init()
				{
					PlusStat.MPR = StackNum;
					PlusStat.PlusDiscard = 1;
				}
			}
		}
	}
}
