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
using PItem;

namespace QoH
{
	public class Buffs
	{
		public class QoHSanity : Buff, IP_Awake, IP_PlayerTurn, IP_SomeOneDead, IP_DamageTake, IP_Healed
		{
			private GameObject sanityIcon;
			public bool MagicalGirlMode = true;
			public int CanSwitchForm = 1;
			public bool UnlimitedSwitches = false;
			public bool SanityDrawBack = true;
			public bool UnlimitedSwitchesTurn = false;
			private int sfxPerTurn = 2;
			private bool lastMode;

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
				SetSanityFlags();
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				MagicalGirlMode = false;
				ChangeIcon(MagicalGirlMode);
			}

			public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
			{
				if (HealedChar == BChar)
				{
					MagicalGirlMode = true;
					ChangeIcon(MagicalGirlMode);
				}
			}

			public void SomeOneDead(BattleChar DeadChar)
			{
				if (DeadChar == BChar)
				{
					Utils_Ui.DestroyObject(sanityIcon);
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

				if (CanSwitchForm > 0 || UnlimitedSwitches || UnlimitedSwitchesTurn)
				{
					MagicalGirlMode = !MagicalGirlMode;
					ChangeIcon(MagicalGirlMode);

					if (!UnlimitedSwitches && !UnlimitedSwitchesTurn)
					{
						CanSwitchForm--;
					}
				}
			}

			private void ChangeIcon(bool MagicalGirlMode)
			{
				if (lastMode == MagicalGirlMode) return;

				lastMode = MagicalGirlMode;
				string sound;
				string text;

				if (MagicalGirlMode)
				{
					Sprite.GetSpriteByAddress(BuffData.Icon_Path);
					BE?.gameObject?.SetActive(true);
					sound = "S_QoH_Sanity_M";
					text = ModLocalization.QoH_Sanity_Text_M;
				}
				else
				{
					var path = "Visual/QoH/Sanity/Sanity_H.png";
					Sprite.GetSpriteByPath(path);
					BE?.gameObject?.SetActive(false);
					sound = "S_QoH_Sanity_H";
					text = ModLocalization.QoH_Sanity_Text_H;
				}

				if (sfxPerTurn > 0)
				{
					sfxPerTurn--;
					Utils.PlaySound(sound, true);
					BChar.StartCoroutine(ShowText(BChar.GetTopPos(), text));
				}
				Init();
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
				lastMode = MagicalGirlMode;
				CanSwitchForm = MyChar.LV >= 4 ? 2 : 1;
				Sprite sprite = Utils_Ui.GetSprite("Visual/QoH/Sanity/Sanity_M.png");
				sanityIcon = Utils_Ui.CreateUIImage("Sanity_M", BChar.transform, sprite, new Vector2(150, 150), new Vector3(170, 170, 0), true);
				sanityIcon.AddComponent<QoH_Sanity_Script>();
				sanityIcon.AddComponent<QoH_Sanity_Tooltip>();
			}

			private void SetSanityFlags()
			{
				MagicalGirlMode = true;
				CanSwitchForm = MyChar.LV >= 4 ? 2 : 1;
				UnlimitedSwitchesTurn = false;
				sfxPerTurn = 2;
				ChangeIcon(MagicalGirlMode);

				if (sanityIcon != null)
				{
					var sanity = sanityIcon.GetComponent<QoH_Sanity_Script>();
					if (sanity != null)
					{
						sanity.AllowPulse = true;
					}
				}
			}

			private IEnumerator ShowText(Vector3 position, string text)
			{
				var topText = BattleText.CustomText(position, text);
				yield return new WaitForSecondsRealtime(2f);
				topText?.End();
			}
		}

		public class QoHDOT : Buff
		{
			protected virtual float HealingPower => 0f;
			protected virtual float AttackPower => 0f;
			protected virtual BattleChar Bchar => Usestate_F ?? BChar;
			protected virtual bool OncePerTun => false;

			public override string DescExtended()
			{
				int heal = 0;
				string user = "";
				int percentage = 0;
				string status = OncePerTun ? EmotionSystem.ModLocalization.EmotionSystem_Status_Inactive : EmotionSystem.ModLocalization.EmotionSystem_Status_Active;

				if (Bchar != null)
				{
					if (HealingPower != 0)
					{
						heal = (int)(Bchar.GetStat.reg * HealingPower);
						percentage = (int)(HealingPower * 100);
					}
					else
					{
						heal = (int)(Bchar.GetStat.atk * AttackPower);
						percentage = (int)(AttackPower * 100);
					}

					if (Bchar.Info != null && !string.IsNullOrEmpty(Bchar.Info.Name))
					{
						user = Bchar.Info.Name;
					}
				}

				string desc = base.DescExtended() ?? "";
				desc = desc.Replace("&a", heal.ToString());
				desc = desc.Replace("&b", percentage.ToString());
				desc = desc.Replace("&c", string.IsNullOrEmpty(user) ? "" : user + " ");
				desc = desc.Replace("&d", status);
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
				try
				{
					var list = BChar.MyTeam.AliveChars.Where(a => a != BChar).ToList();

					if (list.Count > 0)
					{
						var index = RandomManager.RandomInt(RandomClassKey.Curse, 0, list.Count);
						var enemy = list[index];

						if (enemy != null)
						{
							int damage = (int)(Usestate_F.GetStat.atk * 0.3) * StackNum * LifeTime;
							enemy.Damage(Usestate_F, damage / 2, false, true);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		public class Chant : QoHDOT
		{
			protected override float AttackPower => 0.5f;

			public override void BuffStat()
			{
				PlusStat.RES_DOT = -10 * StackNum;
			}

			public override void DestroyByTurn()
			{
				HealAlly();
			}

			private void HealAlly()
			{
				try
				{
					int heal = (int)(Usestate_F.GetStat.atk * AttackPower);
					BattleSystem.instance.StartCoroutine(Utils.HealingParticle(null, Usestate_F, heal, true, false, true, false, false, false));
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		public class Affection : QoHDOT, IP_Dead, IP_DebuffResist, IP_PlayerTurn, IP_DamageChange_Hit_sumoperation
		{
			protected override float AttackPower => 0.75f;
			protected override bool OncePerTun => oncePerTun;

			private bool oncePerTun;

			public override void Init()
			{
				PlusStat.RES_DOT = -15;
				PlusStat.dod = -5;
			}

			public void Dead()
			{
				BattleSystem.instance.AllyTeam.Add(Skill.TempSkill(ModItemKeys.Skill_S_QoH_Shot, Usestate_F, Usestate_F.MyTeam), true);
			}

			public void Resist()
			{
				if (!BChar.IsDead)
				{
					BattleSystem.instance.AllyTeam.Skills_UsedDeck.Add(Skill.TempSkill(ModItemKeys.Skill_S_QoH_Shot, Usestate_F, Usestate_F.MyTeam));
				}
			}

			public void Turn()
			{
				oncePerTun = false;
			}

			public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
			{
				if (SkillD.Master == Usestate_F && !OncePerTun)
				{
					oncePerTun = true;
					int heal = (int)(Usestate_F.GetStat.atk * AttackPower);
					BattleSystem.instance.StartCoroutine(Utils.HealingParticle(null, Usestate_F, heal, true, false, true, false, false, false));
				}
			}
		}

		public class ArcanaSlave : QoHDOT, IP_SkillUse_Target
		{
			protected override float HealingPower => 0.25f;

			public override void Init()
			{
				PlusStat.HIT_DOT = 10;
				PlusStat.hit = 5;
			}

			public override string DescExtended()
			{
				return base.DescExtended().Replace("&f", Utils.ChanceDOT(BChar, 105).ToString());
			}

			public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
			{
				if (SP.SkillData.IsDamage && hit != null && !hit.Info.Ally)
				{
					int heal = (int)(Usestate_F.GetStat.reg * HealingPower);
					BChar.StartCoroutine(Utils.HealingParticle(Bchar, Usestate_F, heal, true, false, true, false, false, false));
					Utils.AddDebuff(hit, Usestate_F, ModItemKeys.Buff_B_QoH_Shattered, 1, Utils.ChanceDOT(BChar, 105));
					SelfStackDestroy();
				}
			}
		}

		public class LoveJustice : QoHDOT, IP_DamageTake
		{
			protected override float HealingPower => 0.5f;

			public override void Init()
			{
				PlusStat.RES_DOT = 10;
				PlusStat.dod = 5;
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (Dmg >= 1)
				{
					int heal = (int)(Usestate_F.GetStat.reg * HealingPower);
					BChar.StartCoroutine(Utils.HealingParticle(Bchar, Usestate_F, heal, true, false, true, false, false, false));
					SelfDestroy();
				}
			}
		}

		public class LoveHate : QoHDOT
		{
			protected override float AttackPower => 0.5f;

			public override void Init()
			{
				PlusPerStat.Damage = -5;
			}

			//public override void TurnUpdate()
			//{
			//	int damage = (int)(Usestate_F.GetStat.atk * AttackPower);
			//	BChar.Damage(Usestate_F, damage, false, true);
			//	base.TurnUpdate();
			//}
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

		public class WhatUse : Buff, IP_BuffAddAfter, IP_BuffUpdate
		{
			public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
			{
				if (BuffTaker.Info.Ally) return;

				if (addedbuff.BuffData.Debuff && addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT && stackBuff.RemainTime != 0)
				{
					stackBuff.RemainTime++;
				}
			}

			public void BuffUpdate(Buff MyBuff)
			{
				if (!MyBuff.BChar.Info.Ally && MyBuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
				{
					if (!MyBuff.BuffExtended.Any((Buff_Ex p) => p is WhatUse_Ex))
					{
						MyBuff.AddBuffEx(new WhatUse_Ex());
					}
				}
			}
		}

		public class WhatUse_Ex : Buff_Ex
		{
			public override void BuffStat()
			{
				PlusDamageTick = 2;
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

		public class SunMoon : Buff, IP_Awake, IP_PlayerTurn_1
		{
			public void Awake()
			{
				EmotionManager.RemoveEmotionCapGuest(BChar);
			}

			public override void Init()
			{
				PlusPerStat.MaxHP = 25;
			}

			public void Turn1()
			{
				BChar.GetNegEmotion(null, 3);
			}
		}
		
		public class MagicalCandy : Buff
		{
			public override void Init()
			{
				PlusStat.RES_DOT = 10;
			}
		}
	}
}
