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
using static MiyukiSone.Affection;
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
				int multiplier = MiyukiResult(15);
				if (multiplier == 0 || !Hit.Info.Ally) return Dmg;
				float factor = 1f - (multiplier / 100f);
				Dmg = (int)(Dmg * factor);
				return Dmg;
			}

			// Сhange all deal damage for all enemies depends on Miyuki affection
			public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
			{
				int multiplier = MiyukiResult(15);
				if (multiplier == 0 || !Target.Info.Ally) return Damage;
				float factor = 1f - (multiplier / 100f);
				Damage = (int)(Damage * factor);
				return Damage;
			}
		}

		public class MiyukiBuffEnemy : Buff, IP_PlayerTurn
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

		public class MiyukiDebuffAlly : Buff
		{
			public override void Init()
			{
				PlusPerStat.Damage = -100;
				base.Init();
			}
		}


		public class FixedAbility : Buff, IP_SkillUse_BasicSkill, IP_PlayerTurn
		{
			// Pattern Matching
			//public MiyukiPassive MiyukiPassive => BChar.Info.Passive is MiyukiPassive mp ? mp : null;
			public MiyukiPassive MiyukiPassive => BChar.Info.Passive as MiyukiPassive;
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

				MiyukiPassive?.AvaliableCharacterDraw.Add(GDEItemKeys.Skill_S_Mement_LucyDraw);
			}

			public override void SelfdestroyPlus()
			{
				MiyukiPassive?.AvaliableCharacterDraw.Remove(GDEItemKeys.Skill_S_Mement_LucyDraw);
				base.SelfdestroyPlus();
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
				if (Drawskill.IsCreatedInBattle && Drawskill.AP >= 1 && Drawskill.ExtendedFind("Mement_Ex_0", true) == null && Drawskill.Master.Info.KeyData != ModItemKeys.Character_Miyuki)
				{
					Drawskill.ExtendedAdd(Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_Mement_Ex_0));
				}
				yield return null;
				yield break;
			}
		}

		public class MiyukiMight : FixedAbility
		{
			private readonly string[] PhoenixChoices = new[]
			{
				GDEItemKeys.Skill_S_Phoenix_P,      // Feast
				GDEItemKeys.Skill_S_Phoenix_3_0,    // Fetch
				GDEItemKeys.Skill_S_Phoenix_4,      // Undying Sanctuary
				GDEItemKeys.Skill_S_Phoenix_5_0,    // Fly
				GDEItemKeys.Skill_S_Phoenix_5_0,    // Fly
				GDEItemKeys.Skill_S_Phoenix_6_1,    // Eternal Flame
				GDEItemKeys.Skill_S_Phoenix_10_0,   // Find Bread
				GDEItemKeys.Skill_S_Phoenix_10_1    // Throw Bread
			};

			public override void BuffOneAwake()
			{
				base.BuffOneAwake();
				if (MiyukiPassive == null) return;
				MiyukiPassive.MiyukiChoiceList = MiyukiPassive.MiyukiChoiceList ?? new List<string>();
				MiyukiPassive.MiyukiChoiceList.Clear();
				MiyukiPassive.MiyukiChoiceList.AddRange(PhoenixChoices);
				MiyukiPassive.AvaliableCharacterDraw.Add(GDEItemKeys.Skill_S_Phoenix_Draw);
			}

			public override void SelfdestroyPlus()
			{
				MiyukiPassive?.AvaliableCharacterDraw.Remove(GDEItemKeys.Skill_S_Phoenix_Draw);
				base.SelfdestroyPlus();
			}
		}

		public class Recovery : FixedAbility
		{
			public override bool RecoverySkill => true;
		}
	}
}
