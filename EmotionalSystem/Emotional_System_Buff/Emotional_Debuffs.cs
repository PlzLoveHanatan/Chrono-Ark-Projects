using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using EmotionSystem;
using Unity.Collections.LowLevel.Unsafe;

namespace EmotionalSystemBuff
{
	public class Debuffs
	{
		public class Bleed : Buff, IP_BuffObject_Updata, IP_TurnEnd
		{
			public int CurrentBleed;

			public void BuffObject_Updata(BuffObject obj)
			{
				string num = CurrentBleed.ToString();
				obj.StackText.text = num;
			}

			public override string DescExtended()
			{
				string text = ModLocalization.EmotionalSystem_Bleed_1;
				if (BChar is BattleEnemy)
				{
					text = ModLocalization.EmotionalSystem_Bleed_0;
				}
				return base.DescExtended().Replace("Description", text).Replace("&a", CurrentBleed.ToString()).Replace("&b", (CurrentBleed * 3).ToString());
			}

			public override void Init()
			{
				isStackDestroy = true;
			}

			public void TurnEnd()
			{
				if (CurrentBleed <= 1)
				{
					SelfDestroy();
				}
			}

			public override void TurnUpdate()
			{
				BChar.Damage(BattleSystem.instance.DummyChar, (CurrentBleed * 3), false, true, true, 0, false, false, false);

				if (CurrentBleed >= 3)
				{
					CurrentBleed -= (int)Math.Ceiling(CurrentBleed / 3f);
				}
			}
		}

		public class Burn : Buff, IP_TurnEnd, IP_BuffObject_Updata
		{
			public int CurrentBurn;

			public void BuffObject_Updata(BuffObject obj)
			{
				string num = CurrentBurn.ToString();
				obj.StackText.text = num;
			}

			public override string DescExtended()
			{
				string text = ModLocalization.EmotionalSystem_Burn_1;
				if (BChar is BattleEnemy)
				{
					text = ModLocalization.EmotionalSystem_Burn_0;
				}
				return base.DescExtended().Replace("Description", text).Replace("&a", CurrentBurn.ToString()).Replace("&b", (CurrentBurn * 2).ToString());
			}

			public override void Init()
			{
				isStackDestroy = true;
			}

			public void TurnEnd()
			{
				BChar.Damage(BattleSystem.instance.DummyChar, CurrentBurn * 2, false, true, true, 0, false, false, false);

				if (CurrentBurn >= 3)
				{
					CurrentBurn -= CurrentBurn / 3;
				}
				else if (CurrentBurn <= 1)
				{
					SelfDestroy();
				}
			}
		}

		public class Paralysis : Buff, IP_SkillUse_User_After
		{
			public override void BuffStat()
			{
				PlusPerStat.Damage = -10 * StackNum;
			}

			public void SkillUseAfter(Skill SkillD)
			{
				if (SkillD.IsDamage)
				{
					SelfDestroy();
				}
			}
		}

		public class Bind : Buff, IP_PlayerTurn
		{
			public override void BuffStat()
			{
				PlusStat.spd = 1;
				PlusStat.dod = -10 * StackNum;
			}

			public void Turn()
			{
				SelfStackDestroy();
			}
		}

		public class Fragile : Buff, IP_DamageTake
		{
			public override void BuffStat()
			{
				PlusStat.DMGTaken = 10 * StackNum;
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (Dmg >= 1)
				{
					SelfStackDestroy();
				}
			}
		}

		public class Disarm : Buff
		{
			public override void BuffStat()
			{
				PlusStat.def = -10 * StackNum;
			}
		}

		public class Feeble : Buff
		{
			public override void BuffStat()
			{
				PlusPerStat.Damage = -10 * StackNum;
			}
		}

		public class B_EmotionalSystem_ColorlessDepth : Buff, IP_EmotionLvUpBefore, IP_Awake
		{
			public void EmotionLvUp(CharEmotion charEmotion, int nextLevel)
			{
				if (charEmotion.BChar == BChar && nextLevel > 0)
				{
					SelfDestroy();
				}
			}

			public override void Init()
			{
				PlusPerStat.Damage = -20;
				PlusPerStat.Heal = -20;
			}

			public void Awake()
			{
				if (BChar.EmotionLevel() >= 5)
				{
					SelfDestroy();
				}
			}
		}
	}
}
