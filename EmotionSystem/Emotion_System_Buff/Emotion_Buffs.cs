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
				private EmotionMode Mode
				{
					get
					{
						if (Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_Investigator_Emotional_Level) is Investigators.Emotion.Level level)
						{
							return level.Mode;
						}

						return EmotionMode.Normal;
					}
				}

				private void GainPosOrNegPoints(Vector3? pos = null, int amount = 1, bool isPositive = true)
				{
					if (Mode == EmotionMode.ForceNegative)
					{
						BChar.GetNegEmotion(pos, amount);
					}
					else if (Mode == EmotionMode.ForcePositive)
					{
						BChar.GetPosEmotion(pos, amount);
					}
					else
					{
						if (isPositive)
						{
							BChar.GetPosEmotion(pos, amount);
						}
						else
						{
							BChar.GetNegEmotion(pos, amount);
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
						GainPosOrNegPoints(pos, 3, false);
					}
				}

				public void SkillUse(Skill SkillD, List<BattleChar> Targets)
				{
					if (SkillD.Master == BChar)
					{
						if (SkillD.IsHeal || SkillD.IsDamage)
						{
							GainPosOrNegPoints(SkillD.GetPosUI(), 1, true);
						}
						else
						{
							GainPosOrNegPoints(SkillD.GetPosUI(), 1, false);
						}
					}
				}

				public void KillEffect(SkillParticle SP)
				{
					if (SP.UseStatus == BChar)
					{
						var pos = SP.UseStatus.GetPosUI();
						GainPosOrNegPoints(pos, 3, true);
					}
				}

				public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
				{
					if (Damage >= 1)
					{
						GainPosOrNegPoints(null, 1, true);
					}

					if (IsCri)
					{
						GainPosOrNegPoints(null, 1, true);
					}
				}

				public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
				{
					if (Dmg >= 1)
					{
						GainPosOrNegPoints(User.GetPosUI(), 1, false);
						BattleSystem.DelayInput(DeathDoorCheck());

						if (Cri)
						{
							GainPosOrNegPoints(User.GetPosUI(), 1, false);
						}
					}
				}

				private IEnumerator DeathDoorCheck()
				{
					if (Utils.ReturnBuff(BChar, GDEItemKeys.Buff_B_Neardeath) != null)
					{
						foreach (var ally in BChar.MyTeam.AliveChars_Vanish)
						{
							GainPosOrNegPoints(null, 1, false);
						}
					}

					yield break;
				}

				public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
				{
					if (HealedChar == BChar)
					{
						GainPosOrNegPoints(Healer.GetPosUI(), 1, true);
					}
				}

				public void Dodge(BattleChar Char, SkillParticle SP)
				{
					if (Char == BChar)
					{
						GainPosOrNegPoints(Char.GetPosUI(), 1, true);
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
