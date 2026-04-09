using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;
using static MiyukiSone.EventsData;
using static MiyukiSone.Affection;
using static MiyukiSone.Utils;
using UnityEngine.UI;
using UnityEngine.Experimental.UIElements.StyleEnums;
using System.Security.Cryptography;

namespace MiyukiSone
{
	public class Buffs
	{
		#region Miyuki class buffs
		public class AffectionOverflow : Buff
		{
			public override string DescExtended()
			{
				string desc = MiyukiPassive.CreateCharacterDraw ? ModLocalization.AffectionOverflow_0 : ModLocalization.AffectionOverflow_1;
				string text = desc + "\n" + ModLocalization.MiyukiAffection.Replace("&a", CurrentAffection.ToString());

				string baseDesc = base.DescExtended();
				int index = baseDesc.IndexOf("\n");

				if (MiyukiPassive.AvaliableCharacterDraw.Count > 0)
				{
					if (MiyukiPassive.CreateCharacterDraw)
					{
						string skillList = "";
						foreach (string key in MiyukiPassive.AvaliableCharacterDraw)
						{
							skillList += "\n - " + new GDESkillData(key).Name;
						}

						text += "\n" + baseDesc.Substring(0, index) + skillList + baseDesc.Substring(index);
					}
					else
					{
						text += "\n" + baseDesc.Substring(index + 1);
					}
				}
				else
				{
					return text;
				}

				return text;
			}

			public override void BuffOneAwake()
			{
				if (BuffIcon.GetComponent<Button>() == null)
				{
					Button button = BuffIcon.AddComponent<Button>();
					button.onClick.AddListener(ChangeDrawOnClick);
				}
				BattleSystem.instance.StartCoroutine(SetBEDel());
			}

			private IEnumerator SetBEDel()
			{
				yield return new WaitUntil(() => BE != null);
				BE.transform.localScale = new Vector3(40f, 40f, 1f);
				BE.transform.localPosition = new Vector3(0f, 150f, 0f);
				yield break;
			}

			private void ChangeDrawOnClick()
			{
				if (BChar.GetStat.Stun || !BattleSystem.instance.ActWindow.CanAnyMove || MiyukiPassive.AvaliableCharacterDraw.Count == 0) return;
				ChangeDraw();
			}

			public void ChangeDraw()
			{
				List<Skill> skills = new List<Skill>()
				{
					Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Passive_0, BChar, BChar.MyTeam),
					Skill.TempSkill(ModItemKeys.Skill_S_Miyuki_Passive_1, BChar, BChar.MyTeam),
				};

				BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(skills, Selection, "", false, false, true, false, true));
			}

			private void Selection(SkillButton btn)
			{
				MiyukiPassive.CreateCharacterDraw = btn.Myskill.MySkill.KeyID == ModItemKeys.Skill_S_Miyuki_Passive_1;
				ChangeIcon();
			}

			public void ChangeIcon()
			{
				if (!MiyukiPassive.CreateCharacterDraw)
				{
					Sprite.LoadSpriteByAddress(BuffData.Icon_Path);
					BE?.gameObject?.SetActive(true);
				}
				else
				{
					var path = "MiyukiVisual/Misc/OnePin.png";					
					Sprite.GetSpriteByPathFromMod(path);
					BE?.gameObject?.SetActive(false);
				}
			}
		}
		#endregion

		#region Miyuki misc buffs
		public class AllyConstantStats : Buff
		{
			public override void Init()
			{
				PlusStat.DMGTaken = MiyukiResult(10, false);
				PlusPerStat.Damage = MiyukiResult(10);
				PlusPerStat.Heal = MiyukiResult(10);
				PlusStat.hit = MiyukiResult(10);
				PlusStat.dod = MiyukiResult(10);
				PlusStat.RES_CC = MiyukiResult(10);
				PlusStat.RES_DOT = MiyukiResult(10);
				PlusStat.RES_DEBUFF = MiyukiResult(10);
				PlusStat.HIT_CC = MiyukiResult(10);
				PlusStat.HIT_DOT = MiyukiResult(10);
				PlusStat.HIT_DEBUFF = MiyukiResult(10);
				base.Init();
			}
		}

		public class EnemyExtraAction : Buff
		{
			public override void Init()
			{
				BChar.Info.PlusActCount.Add(1);
			}
		}

		public class AllyDebuff : Buff
		{
			public override void Init()
			{
				PlusPerStat.Damage = -100;
				base.Init();
			}
		}
		#endregion

		#region Glitching phone buffs
		public class FixedAbility : Buff, IP_SkillUse_BasicSkill, IP_PlayerTurn
		{
			// Pattern Matching
			//public GetMiyukiPassive GetMiyukiPassive => BChar.Info.Passive is GetMiyukiPassive mp ? mp : null;
			public virtual bool RecoverySkill => false;
			public virtual bool UsedThisTurn { get; set; } = false;

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

				if (RecoverySkill && !UsedThisTurn)
				{
					BChar.MyTeam.BasicSkillRefill(BChar, BChar.BattleBasicskillRefill);
					if (BChar is BattleAlly ally && ally.MyBasicSkill?.buttonData != null) ally.MyBasicSkill.buttonData.APChange++;
					UsedThisTurn = true;
				}
				else (BChar as BattleAlly)?.MyBasicSkill.SkillInput(BChar.BattleBasicskillRefill);
				yield break;
			}

			public void Turn()
			{
				UsedThisTurn = false;
			}
		}

		public class CloseRangeShot : FixedAbility, IP_SkillUseHand_Team, IP_Draw
		{
			public override void BuffOneAwake()
			{
				base.BuffOneAwake();
				foreach (var skill in BattleSystem.instance.AllyTeam.Skills.Where(s => s.Master.Info.KeyData != GDEItemKeys.Character_Mement && s.Master.Info.KeyData != ModItemKeys.Character_Miyuki && !s.Master.IsLucy))
				{
					if (skill.IsCreatedInBattle && skill.ExtendedFind("Mement_Ex_0", true) == null)
					{
						skill.ExtendedAdd(Skill_Extended.DataToExtended(GDEItemKeys.SkillExtended_Mement_Ex_0));
					}
				}

				MiyukiExtension.RefreshMiyukiCharacterDraw();
			}

			public override void SelfdestroyPlus()
			{
				MiyukiPassive.AvaliableCharacterDraw.Remove(GDEItemKeys.Skill_S_Mement_LucyDraw);
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
				if (Drawskill.IsCreatedInBattle && Drawskill.AP >= 1 && Drawskill.ExtendedFind("Mement_Ex_0", true) == null
					&& Drawskill.Master.Info.KeyData != GDEItemKeys.Character_Mement && Drawskill.Master.Info.KeyData != ModItemKeys.Character_Miyuki && !Drawskill.Master.IsLucy)
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
				if (GetMiyukiPassive == null) return;
				GetMiyukiPassive.MiyukiChoiceList = GetMiyukiPassive.MiyukiChoiceList ?? new List<string>();
				GetMiyukiPassive.MiyukiChoiceList.Clear();
				GetMiyukiPassive.MiyukiChoiceList.AddRange(PhoenixChoices);
				MiyukiPassive.AvaliableCharacterDraw.Add(GDEItemKeys.Skill_S_Phoenix_Draw);
				MiyukiExtension.RefreshMiyukiCharacterDraw();
			}
		}

		public class Recovery : FixedAbility
		{
			public override bool RecoverySkill => true;
			public override bool UsedThisTurn { get; set; } = false;
		}
		#endregion
	}
}
