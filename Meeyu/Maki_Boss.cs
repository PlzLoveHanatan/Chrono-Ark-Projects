using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Meeyu
{
	public class Boss
	{
		public class BossAI
		{
			public class enemy_maqi : AI
			{
				public override void Update()
				{
					base.Update();
					this.turnCount++;
				}

				public override int SpeedChange(Skill skill, int ActionCount, int OriginSpeed)
				{
					bool firstTurn = this.FirstTurn;
					int result;
					if (firstTurn)
					{
						this.FirstTurn = false;
						result = 0;
					}
					else
					{
						result = base.SpeedChange(skill, ActionCount, OriginSpeed);
					}
					return result;
				}

				public override Skill SkillSelect(int ActionCount)
				{
					Skill result;
					switch (this.turnCount)
					{
						case 0:
							result = this.BChar.Skills[0];
							break;
						case 1:
							result = this.BChar.Skills[1];
							break;
						case 2:
							result = this.BChar.Skills[5];
							break;
						case 3:
							result = this.BChar.Skills[5];
							break;
						default:
							result = this.BChar.Skills[2];
							break;
					}
					return result;
				}

				private int turnCount = 0;

				public int Ready;

				public bool FirstTurn = true;
			}
		}

		public class Skills
		{
			public class MakiSkill : Skill_Extended
			{
				public override void Init()
				{
					CanUseStun = true;
				}
			}
		}

		public class Buffs
		{
			public class maqi_buff_03 : Buff, IP_BattleStart_UIOnBefore, IP_Dead
			{
				public void BattleStartUIOnBefore(BattleSystem Ins)
				{
					BattleSystem.DelayInput(this.Start1());
				}

				public IEnumerator Start1()
				{
					yield return new WaitForSecondsRealtime(0.3f);
					yield return BattleText.InstBattleText_Co(this.BChar, ModLocalization.Maki_phrase_0, true, 3, 1f);
					yield return BattleText.InstBattleText_Co(this.BChar, ModLocalization.Maki_phrase_1, true, 3, 1f);
					yield break;
				}

				public void Dead()
				{
					BattleSystem.DelayInput(this.Start2());
					BattleSystem.instance.Reward.Add(ItemBase.GetItem("PrismStick"));
					BattleSystem.instance.Reward.Add(ItemBase.GetItem("PyonHammer"));
					BattleSystem.instance.Reward.Add(ItemBase.GetItem("SkillBookCharacter_Rare"));
				}

				public IEnumerator Start2()
				{
					yield break;
				}
			}
		}
	}
}
