using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using Spine.Unity.Examples;
using DarkTonic.MasterAudio;
using System.Collections;

namespace QoH
{
	public class Buffs
	{
		public class QoHSanity : Buff, IP_Awake, IP_PlayerTurn, IP_SomeOneDead, IP_DamageTake
		{
			private GameObject Sanity;
			public bool MagicalGirlMode = true;
			public bool CanSwitchForm = true;
			public bool UnlimitedSwitches = false;
			public bool SanityDrawBack = true;

			public override string NameExtended(string Name)
			{
				string text = MagicalGirlMode ? ModLocalization.QoH_Sanity_Mod_M : ModLocalization.QoH_Sanity_Mod_H;
				return base.NameExtended(Name).Replace("Description", text);
			}

			public override string DescExtended()
			{
				string text = MagicalGirlMode ? ModLocalization.QoH_Sanity_Mod_M_Desc : ModLocalization.QoH_Sanity_Mod_H_Desc;
				return base.DescExtended().Replace("Description", text);
			}

			public override void Init()
			{
				if (MagicalGirlMode)
				{
					PlusPerStat.Heal = 15;
					PlusPerStat.Damage = SanityDrawBack ? -15 : 0;
				}
				else
				{
					PlusPerStat.Damage = 15;
					PlusPerStat.Heal = SanityDrawBack ? -15 : 0;
				}

				if (MagicalGirlMode)
				{
					EmotionManager.GainOnlyPositivePoints(BChar);
				}
				else
				{
					EmotionManager.GainOnlyNegativePoints(BChar);
				}
			}

			public void Awake()
			{
				CreateSanity();
			}

			public void Turn()
			{
				MagicalGirlMode = true;
				CanSwitchForm = true;
				Sprite.GetSpriteByAddress(BuffData.Icon_Path);
				BE?.gameObject?.SetActive(true);

				var sanity = Sanity.GetComponent<QoH_Sanity_Script>();
				if (sanity != null)
				{
					sanity.AllowPulse = true;
				}
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				MagicalGirlMode = false;
			}

			public void SomeOneDead(BattleChar DeadChar)
			{
				if (DeadChar == BChar)
				{
					Utils_Ui.DestroyObject(Sanity);
				}
			}

			public override void BuffOneAwake()
			{
				if (BuffIcon.GetComponent<Button>() == null)
				{
					Button button = BuffIcon.AddComponent<Button>();
					button.onClick.AddListener(ChangeSanity);
				}
				BattleSystem.instance.StartCoroutine(SetBEDel());
			}

			public void ChangeSanity()
			{
				if (BChar.GetStat.Stun || !BattleSystem.instance.ActWindow.CanAnyMove) return;

				if (CanSwitchForm || UnlimitedSwitches)
				{
					//MasterAudio.PlaySound("WaitButton");
					MagicalGirlMode = !MagicalGirlMode;

					if (MagicalGirlMode)
					{
						Sprite.GetSpriteByAddress(BuffData.Icon_Path);
						BE?.gameObject?.SetActive(true);
					}
					else
					{
						var path = "Visual/QoH/Sanity/Sanity_H.png";
						Sprite.GetSpriteByPath(path);
						BE?.gameObject?.SetActive(false);
					}

					Init();
					CanSwitchForm = false;
				}
			}

			private IEnumerator SetBEDel()
			{
				yield return new WaitUntil(() => BE != null);
				BE.transform.localScale = new Vector3(40f, 40f, 1f);
				BE.transform.localPosition = new Vector3(0f, 150f, 0f);
				yield break;
			}

			private void CreateSanity()
			{
				Sprite sprite = Utils_Ui.GetSprite("Visual/QoH/Sanity/Sanity_M.png");
				Sanity = Utils_Ui.CreateUIImage("Sanity_M", BChar.transform, sprite, new Vector2(150, 150), new Vector3(170, 170, 0), true);
				Sanity.AddComponent<QoH_Sanity_Script>();
				Sanity.AddComponent<QoH_Sanity_Tooltip>();
			}
		}

		public class QoHDOT : Buff
		{
			protected virtual float HealingPower => 0.5f;
			protected virtual BattleChar Bchar => Usestate_F ?? BChar;
			protected virtual bool OncePerTun => false;

			public override string DescExtended()
			{
				int heal = 0;
				string user = "";
				string status = OncePerTun ? EmotionSystem.ModLocalization.EmotionSystem_Status_Inactive : EmotionSystem.ModLocalization.EmotionSystem_Status_Active;

				if (Bchar != null)
				{
					heal = (int)(Bchar.GetStat.reg * HealingPower);

					if (Bchar.Info != null && !string.IsNullOrEmpty(Bchar.Info.Name))
					{
						user = Bchar.Info.Name;
					}
				}

				string desc = base.DescExtended() ?? "";
				desc = desc.Replace("&a", heal.ToString());
				desc = desc.Replace("&b", string.IsNullOrEmpty(user) ? "" : user + " ");
				desc = desc.Replace("&c", status);
				return desc;
			}
		}

		public class Mark : Buff, IP_PlayerTurn
		{
			public override void Init()
			{
				PlusStat.DMGTaken = 5;
				PlusStat.IgnoreTaunt_EnemySelf = true;
			}

			public void Turn()
			{
				SelfDestroy();
			}
		}


		public class Shattered : Buff, IP_Dead
		{
			public override void BuffStat()
			{
				PlusPerStat.Damage = -1 * StackNum;
			}

			public void Dead()
			{
				var list = BChar.MyTeam.AliveChars.Where(a => a != BChar).ToList();
				var index = RandomManager.RandomInt(RandomClassKey.Curse, 0, list.Count);
				var enemy = list[index];

				if (enemy != null)
				{
					int damage = (int)(Usestate_F.GetStat.atk * 0.3) * StackNum * LifeTime;
					enemy.Damage(Usestate_F, damage, false, true);
				}
			}
		}

		public class Chant : QoHDOT, IP_Dead
		{
			protected override float HealingPower => 0.25f;

			public override void BuffStat()
			{
				PlusStat.RES_DOT = -10 * StackNum;
			}

			public override void DestroyByTurn()
			{
				HealAlly();
			}

			public void Dead()
			{
				HealAlly();
			}

			private void HealAlly()
			{
				int heal = (int)(Usestate_F.GetStat.reg * HealingPower);
				BattleSystem.instance.StartCoroutine(Utils.HealingParticle(null, Usestate_F, heal, true, false, true, false, false, false));
			}
		}

		public class Affection : QoHDOT, IP_Dead, IP_DebuffResist, IP_DamageTake
		{
			protected override float HealingPower => 0.5f;
			protected override bool OncePerTun => oncePerTun;

			private bool oncePerTun;

			public override void Init()
			{
				PlusStat.RES_DOT = -15;
				PlusStat.dod = -5;
			}

			public void Dead()
			{
				BattleSystem.instance.AllyTeam.Add(Skill.TempSkill(ModItemKeys.Skill_S_QoH_Shot, base.Usestate_F, base.Usestate_F.MyTeam), true);
			}

			public void Resist()
			{
				if (!this.BChar.IsDead)
				{
					BattleSystem.instance.AllyTeam.Skills_UsedDeck.Add(Skill.TempSkill(ModItemKeys.Skill_S_QoH_Shot, base.Usestate_F, base.Usestate_F.MyTeam));
				}
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (User == Usestate_F && !oncePerTun)
				{
					oncePerTun = true;
					int heal = (int)(Usestate_F.GetStat.reg * HealingPower);
					BattleSystem.instance.StartCoroutine(Utils.HealingParticle(null, Usestate_F, heal, true, false, true, false, false, false));
				}
			}
		}

		public class LoveJustice : QoHDOT, IP_SkillUse_Target
		{
			protected override float HealingPower => 1f;

			public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
			{
				if (SP.SkillData.IsDamage)
				{
					int heal = (int)(Usestate_F.GetStat.reg * HealingPower);
					BChar.StartCoroutine(Utils.HealingParticle(Bchar, Usestate_F, heal, true, false, false, false, false, false));
					SelfDestroy();
				}
			}
		}

		public class ArcanaSlave : QoHDOT, IP_DamageTake
		{
			protected override float HealingPower => 0.5f;

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (Dmg >= 1)
				{
					int heal = (int)(Usestate_F.GetStat.reg * HealingPower);
					BChar.StartCoroutine(Utils.HealingParticle(Bchar, Usestate_F, heal, true, false, false, false, false, false));
					SelfDestroy();
				}
			}
		}

		public class LoveHate : QoHDOT
		{
			protected override float HealingPower => 0.5f;

			public override void Init()
			{
				PlusPerStat.Damage = -5;
			}

			public override void TurnUpdate()
			{
				int damage = (int)(Usestate_F.GetStat.reg * HealingPower);
				BChar.Damage(Usestate_F, damage, false, true);
				base.TurnUpdate();
			}
		}

		public class Power : Buff
		{
			public override void Init()
			{
				PlusPerStat.Heal = -50;
			}
		}

		public class MagicalGirl : Buff, IP_PlayerTurn_1
		{
			public void Turn1()
			{
				var enemies = Utils.EnemyTeam.AliveChars;
				var index = RandomManager.RandomInt(RandomClassKey.Curse, 0, enemies.Count);
				var target = enemies[index];

				if (target != null)
				{
					Utils.AddDebuff(target, BChar, ModItemKeys.Buff_B_QoH_Mark);
				}
			}
		}

		public class WhatUse : Buff, IP_BuffAddAfter
		{
			public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
			{
				if (BuffTaker.Info.Ally) return;

				if (addedbuff.BuffData.Debuff && addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT && stackBuff.RemainTime != 0)
				{
					stackBuff.RemainTime++;
					stackBuff.RemainTime++;
				}
			}
		}

		public class Transformation : Buff, IP_Awake
		{
			public void Awake()
			{
				var sanity = Utils.ReturnBuff(BChar, ModItemKeys.Buff_B_QoH_Sanity) as QoHSanity;

				if (sanity != null)
				{
					sanity.UnlimitedSwitches = true;
					sanity.SanityDrawBack = false;
					sanity.Init();
				}
			}
		}
	}
}
