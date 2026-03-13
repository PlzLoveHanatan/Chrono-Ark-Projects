using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using static MiyukiSone.EventData;
using static MiyukiSone.MiyukiAffection;
using static MiyukiSone.Utils;


namespace MiyukiSone
{
	public class Buffs
	{
		// Hidden buff
		public class MiyukiBuff : Buff, IP_DamageChange, IP_DamageTakeChange
		{
			// Сhange all taking damage for allies depends on Miyuki current affection
			public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
			{
				int multiplier = MiyukiResult(10);
				if (multiplier == 0 || !Hit.Info.Ally) return Dmg;
				float factor = 1f - (multiplier / 100f);
				Dmg = (int)(Dmg * factor);
				return Dmg;
			}

			// Сhange all deal damage for all enemies depends on Miyuki affection
			public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
			{
				int multiplier = MiyukiResult(10);
				if (multiplier == 0 || !Target.Info.Ally) return Damage;
				float factor = 1f - (multiplier / 100f);
				Damage = (int)(Damage * factor);
				return Damage;
			}
		}

		// Hidden Debuff
		public class MiyukiDebuff : Buff, IP_PlayerTurn
		{
			public override void Init()
			{
				BChar.Info.PlusActCount.Add(1);
				base.Init();
			}

			public void Turn()
			{
				SelfDestroy();
			}
		}


		public class FixedAbility : Buff, IP_SkillUse_BasicSkill, IP_PlayerTurn
		{
			public virtual bool RecoverySkill => false;
			private bool usedThisTurn = false;

			public override void Init()
			{
				if (BChar is BattleAlly ally && ally.MyBasicSkill?.buttonData != null) ally.MyBasicSkill.buttonData.BasicOption = true;
				base.Init();
			}

			public void SkillUseBasicSkill(Skill skill)
			{
				if (skill.Master == BChar) BattleSystem.instance.StartCoroutine(RefreshFixedSkill());
			}

			private IEnumerator RefreshFixedSkill()
			{
				yield return new WaitForEndOfFrame();

				if (RecoverySkill && !usedThisTurn)
				{
					BChar.MyTeam.BasicSkillRefill(BChar, BChar.BattleBasicskillRefill);
					if (BChar is BattleAlly ally && ally.MyBasicSkill?.buttonData != null) ally.MyBasicSkill.buttonData.APChange++;
					usedThisTurn = true;
				}
				else (BChar as BattleAlly)?.MyBasicSkill.SkillInput(BChar.BattleBasicskillRefill);
				yield break;
			}

			public void Turn()
			{
				usedThisTurn = false;
			}
		}

		public class CloseRangeShot : FixedAbility, IP_SkillUseHand_Team, IP_Draw
		{
			public override bool RecoverySkill => false;

			public override void BuffOneAwake()
			{
				base.BuffOneAwake();

				foreach (var skill in AllyTeam.Skills)
				{
					if (skill.IsCreatedInBattle && skill.ExtendedFind("Mement_Ex_0", true) == null)
					{
						skill.ExtendedAdd(Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_Mement_Ex_0));
					}
				}
			}

			public void SKillUseHand_Team(Skill skill)
			{
				BattleSystem.DelayInputAfter(PewPew(skill));
			}

			private IEnumerator PewPew(Skill skill)
			{
				if (skill.ExtendedFind("SkillEn_Mement_0", true) != null || skill.UsedApNum >= 1 && skill.ExtendedFind("Mement_Ex_0", true) == null)
				{
					BChar.MyTeam.BasicSkillRefill(BChar, BChar.BattleBasicskillRefill);
				}
				yield break;
			}

			public IEnumerator Draw(Skill Drawskill, bool NotDraw)
			{
				if (Drawskill.IsCreatedInBattle && Drawskill.AP >= 1 && Drawskill.ExtendedFind("Mement_Ex_0", true) == null/* && Drawskill.Master.Info.KeyData != GDEItemKeys.Character_Mement*/)
				{
					Drawskill.ExtendedAdd(Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_Mement_Ex_0));
				}
				yield return null;
				yield break;
			}
		}

		public class MiyukiMight : FixedAbility
		{
			public override bool RecoverySkill => false;

			public override void BuffOneAwake()
			{
				base.BuffOneAwake();

				if (BChar.Info.Passive is MiyukiPassive miyukiPassive)
				{
					miyukiPassive.MiyukiChoiceList = miyukiPassive.MiyukiChoiceList ?? new List<string>();
					miyukiPassive.MiyukiChoiceList.Clear();
					miyukiPassive.MiyukiChoiceList.Add(GDEItemKeys.Skill_S_Phoenix_3_0); // Fetch
					miyukiPassive.MiyukiChoiceList.Add(GDEItemKeys.Skill_S_Phoenix_4_0); // Undying Sanctuary
					miyukiPassive.MiyukiChoiceList.Add(GDEItemKeys.Skill_S_Phoenix_5_0); // Fly
					miyukiPassive.MiyukiChoiceList.Add(GDEItemKeys.Skill_S_Phoenix_5_0); // Fly
					miyukiPassive.MiyukiChoiceList.Add(GDEItemKeys.Skill_S_Phoenix_6_1); // Eternal Flame
					miyukiPassive.MiyukiChoiceList.Add(GDEItemKeys.Skill_S_Phoenix_P); // Feeast
					miyukiPassive.MiyukiChoiceList.Add(GDEItemKeys.Skill_S_Phoenix_10_0); // Find Bread
					miyukiPassive.MiyukiChoiceList.Add(GDEItemKeys.Skill_S_Phoenix_10_1); // Throw Bread
				}
			}
		}

		public class Recovery : FixedAbility
		{
			public override bool RecoverySkill => true;
		}
	}
}
