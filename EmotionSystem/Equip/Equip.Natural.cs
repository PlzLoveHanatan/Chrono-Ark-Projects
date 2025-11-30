using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using NLog.Targets;

namespace EmotionSystem
{
	public partial class Equip
	{
		public class Natural
		{
			public class SwordTears : EquipBase, IP_SkillUse_Target, IP_PlayerTurn, IP_Draw
			{
				public bool OncePerTurn;

				public override string DescExtended(string desc)
				{
					string text = OncePerTurn ? ModLocalization.EmotionSystem_Status_Inactive : ModLocalization.EmotionSystem_Status_Active;
					return base.DescExtended(desc).Replace("&a", text.ToString());
				}

				public override void Init()
				{
					PlusStat.cri = 10;
					PlusStat.hit = 10;
				}

				public void Turn()
				{
					Utils.GetOrAddBuff(BChar, ModItemKeys.Buff_B_EmotionSystem_SwordTears);
					OncePerTurn = false;
				}

				public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
				{
					if (SP.SkillData.IsDamage && !hit.Info.Ally && Cri && !OncePerTurn)
					{
						OncePerTurn = true;
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
						PlayData.TSavedata._Gold += DMG / 2;
					}
				}
			}
		}
	}
}
