using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using GameDataEditor;
using I2.Loc;
using UnityEngine;

namespace EmotionalSystemBuff
{
	public class Floor_Literature
	{
		public class Abnormality
		{
			public class Lv1
			{
				public class LookoftheDay : Buff
				{
					private float timer = 0f;

					public override void FixedUpdate()
					{
						timer += Time.deltaTime;

						if (timer >= 1f)
						{
							timer = 0f;

							int[] possibleValues = { -20, -10, 0, 10, 20 };

							int index = RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, possibleValues.Length);
							int currentPercent = possibleValues[index];

							PlusPerStat.Damage = currentPercent;

							Debug.Log($"[Shifting Face] New face → {currentPercent:+0;-0}% Damage");
						}
					}
				}

				public class SocialDistancing : Buff, IP_Hit, IP_Dodge, IP_PlayerTurn
				{
					private bool oncePerTurn;

					public override string DescExtended()
					{
						int chance = (int)(BChar.GetStat.HIT_CC + 50);
						return base.DescExtended().Replace("&a", chance.ToString());
					}

					public void Turn()
					{
						oncePerTurn = false;
					}

					public void Dodge(BattleChar Char, SkillParticle SP)
					{
						ApplyStun(SP.UseStatus);
					}

					public void Hit(SkillParticle SP, int Dmg, bool Cri)
					{
						ApplyStun(SP.UseStatus);
					}

					public void ApplyStun(BattleChar target)
					{
						if (!oncePerTurn)
						{
							int chance = (int)(BChar.GetStat.HIT_CC + 50);
							Utils.AddDebuff(target, BChar, GDEItemKeys.Buff_B_Common_Rest, 1, chance);
							oncePerTurn = true;
						}
					}
				}

				public class Shyness : Buff
				{
					public override void Init()
					{
						PlusStat.DMGTaken = -10;
					}
				}

				public class Glitter : Buff
				{
					public override void Init()
					{
						PlusPerStat.Damage = 20;
						PlusStat.AggroPer = 60;
					}
				}

				public class Obsession : Buff, IP_BuffAdd
				{
					private bool isBuffAdding;

					public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
					{
						if (isBuffAdding) return;
						isBuffAdding = true;

						BuffTaker.BuffAdd(addedbuff.BuffData.Key, BChar, false, 0, false, -1, false);
						isBuffAdding = false;
					}
				}

				public class Axe : Buff, IP_DealDamage
				{
					public override void Init()
					{
						PlusPerStat.Damage = 20;
					}

					public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
					{
						if (Damage < 20)
						{
							int damage = (int)(BChar.GetStat.maxhp * 0.1f);
							Utils.TakeNonLethalDamage(BChar, damage, true);
						}
					}
				}

				public class Cocoon : Buff
				{
					
				}
			}
		}
	}
}
