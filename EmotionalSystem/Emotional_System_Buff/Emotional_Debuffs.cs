using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalSystem;
using EmotionSystem;

namespace EmotionalSystemBuff
{
	public class Debuffs
	{
		public class Bleed : Buff, IP_BuffObject_Updata
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
				OnePassive = true;
				isStackDestroy = true;
			}

			public void TurnEnd()
			{
				BChar.Damage(BattleSystem.instance.DummyChar, CurrentBurn * 2, false, true, true, 0, false, false, false);

				if (CurrentBurn >= 3)
				{
					CurrentBurn -= CurrentBurn / 3;
				}
				else
				{
					CurrentBurn = 1;
				}
			}
		}

		public class Paralysis : Buff, IP_SkillUse_User_After
		{
			public override void Init()
			{
				PlusPerStat.Damage = -20;
			}

			public void SkillUseAfter(Skill SkillD)
			{
				if (SkillD.IsDamage)
				{
					SelfDestroy();
				}
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
