using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using NLog.Targets;

namespace EmotionSystem
{
	public partial class Equip
	{
		public class Natural
		{
			public class Sword : EquipBase, IP_SkillUse_Target, IP_PlayerTurn, IP_Draw
			{
				private bool oncePerTurn;

				public override string DescExtended(string desc)
				{
					string text = oncePerTurn ? ModLocalization.EmotionSystem_Status_Inactive : ModLocalization.EmotionSystem_Status_Active;
					return base.DescExtended(desc).Replace("&a", text.ToString());
				}

				public override void Init()
				{
					PlusStat.cri = 10;
					PlusStat.hit = 10;
				}

				public void Turn()
				{
					OnePassive = true;
					oncePerTurn = false;
				}

				public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
				{
					if (SP.SkillData.IsDamage && !hit.Info.Ally && Cri && !oncePerTurn)
					{
						oncePerTurn = true;
						Scripts.DestroyActions(hit);
					}
				}

				public IEnumerator Draw(Skill Drawskill, bool NotDraw)
				{
					if (Drawskill.Master == BChar)
					{
						Utils.ApplyExtended(Drawskill, ModItemKeys.SkillExtended_Ex_Equip_SwordTears, false, true);
					}
					yield break;
				}
			}

			public class GoldenUrn : EquipBase, IP_SkillUse_Target
			{
				public override void Init()
				{
					PlusStat.PlusCriDmg = 25;
				}

				public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
				{
					bool canEarnMoney = BattleSystem.instance.TurnNum < BattleSystem.instance.FogTurn;

					if (SP.SkillData.IsDamage && canEarnMoney)
					{
						PlayData.TSavedata._Gold += 40;
					}
				}
			}
		}
	}
}
