using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EmotionSystem;
using GameDataEditor;

namespace EmotionSystem
{
	public class Buffs
	{
		public class Lucy
		{
			public class MusicBox : Buff, IP_SkillUse_User, IP_Dodge, IP_DamageTake, IP_Healed, IP_DealDamage, IP_Kill, IP_SomeOneDead
			{
				private bool Invert
				{
					get
					{
						if (Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_Investigator_Emotional_Level) is Investigators.Emotion.Level level)
						{
							return level.InvertPoints;
						}
						return false;
					}
				}

				private void GainPosOrNegPoints(Vector3? pos = null, int amount = 1)
				{
					if (Invert)
					{
						BChar.GetNegEmotion(pos, amount);
					}
					else
					{
						BChar.GetPosEmotion(pos, amount);
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
						BChar.GetNegEmotion(pos, 3);
					}
				}

				public void SkillUse(Skill SkillD, List<BattleChar> Targets)
				{
					if (SkillD.Master == BChar)
					{
						if (SkillD.IsHeal || SkillD.IsDamage)
						{
							GainPosOrNegPoints(SkillD.GetPosUI());
						}
						else
						{
							BChar.GetNegEmotion(SkillD.GetPosUI());
						}
					}
				}

				public void KillEffect(SkillParticle SP)
				{
					if (SP.UseStatus == BChar)
					{
						GainPosOrNegPoints(SP.UseStatus.GetPosUI(), 3);
					}
				}

				public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
				{
					if (Damage >= 1)
					{
						GainPosOrNegPoints();
					}

					if (IsCri)
					{
						GainPosOrNegPoints();
					}
				}

				public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
				{
					if (Dmg >= 1)
					{
						GainPosOrNegPoints(User.GetPosUI());
						BattleSystem.DelayInput(DeathDoorCheck());

						if (Cri)
						{
							GainPosOrNegPoints(User.GetPosUI());
						}
					}
				}

				private IEnumerator DeathDoorCheck()
				{
					if (Utils.ReturnBuff(BChar, GDEItemKeys.Buff_B_Neardeath) != null)
					{
						foreach (var ally in BChar.MyTeam.AliveChars_Vanish)
						{
							ally.GetNegEmotion();
						}
					}
					yield break;
				}

				public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
				{
					if (HealedChar == BChar)
					{
						GainPosOrNegPoints(Healer.GetPosUI());
					}
				}

				public void Dodge(BattleChar Char, SkillParticle SP)
				{
					if (Char == BChar)
					{
						GainPosOrNegPoints(Char.GetPosUI());
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
		}
	}
}
