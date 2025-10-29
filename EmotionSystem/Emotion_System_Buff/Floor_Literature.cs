using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;
using GameDataEditor;
using I2.Loc;
using NLog.Targets;
using UnityEngine;
using UnityEngine.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static EmotionSystem.DataStore;
using static EmotionSystem.Extended.EGO;

namespace EmotionSystem
{
	public class LiteratureBuff
	{
		public class Abnormality
		{
			public class Lv1
			{
				public class Axe : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						int chanceDOT = (int)(BChar.GetStat.HIT_DOT + 100);
						return base.DescExtended().Replace("&a", chanceDOT.ToString());
					}

					public override void Init()
					{
						PlusPerStat.Damage = 20;
						PlusStat.DMGTaken = 20;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.ApplyBleed(hit, BChar);
						}
					}
				}

				public class Cocoon : Buff, IP_SkillUse_Target, IP_PlayerTurn
				{
					private bool oncePerTurn;

					public override string DescExtended()
					{
						int chanceWeak = (int)(BChar.GetStat.HIT_DEBUFF + 100);
						int chanceCC = (int)(BChar.GetStat.HIT_CC + 100);
						return base.DescExtended().Replace("&a", chanceWeak.ToString()).Replace("&b", chanceCC.ToString());
					}

					public override void Init()
					{
						PlusStat.hit = 10;
					}

					public void Turn()
					{
						oncePerTurn = false;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage && !oncePerTurn)
						{
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Paralysis);
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile);
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Bind);
							oncePerTurn = true;
						}
					}
				}

				public class Glitter : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						int chanceDOT = (int)(BChar.GetStat.HIT_DOT + 100);
						return base.DescExtended().Replace("&a", chanceDOT.ToString());
					}

					public override void Init()
					{
						PlusPerStat.Damage = 20;
						PlusStat.AggroPer = 100;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.ApplyBleed(hit, BChar);
						}
					}
				}

				public class LookDay : Buff, IP_Awake, IP_SomeOneDead
				{
					private float timer = 0f;
					private GameObject currentFace;
					public Dictionary<VisualUi.LookDayUi.SpriteTypeFace, Sprite> FaceSprites = new Dictionary<VisualUi.LookDayUi.SpriteTypeFace, Sprite>();

					public void Awake()
					{
						LoadFaceSprites();
					}

					public override void FixedUpdate()
					{
						if (BattleSystem.instance == null) return;

						timer += Time.deltaTime;

						if (timer < 1.5f) return;
						timer = 0f;

						int[] possibleValues = { -40, -20, 0, 20, 40 };
						int currentPercent = possibleValues[RandomManager.RandomInt(RandomClassKey.Active, 0, possibleValues.Length)];
						PlusPerStat.Damage = currentPercent;

						VisualUi.LookDayUi.SpriteTypeFace faceType;
						string faceName;

						// Соответствие значений лицам
						switch (currentPercent)
						{
							case -40: faceType = VisualUi.LookDayUi.SpriteTypeFace.Face_VeryHappy; faceName = "Face_VeryHappy"; break;
							case -20: faceType = VisualUi.LookDayUi.SpriteTypeFace.Face_Happy; faceName = "Face_Happy"; break;
							case 0: faceType = VisualUi.LookDayUi.SpriteTypeFace.Face_Normal; faceName = "Face_Normal"; break;
							case 20: faceType = VisualUi.LookDayUi.SpriteTypeFace.Face_Angry; faceName = "Face_Angry"; break;
							case 40: faceType = VisualUi.LookDayUi.SpriteTypeFace.Face_VeryAngry; faceName = "Face_VeryAngry"; break;
							default: faceType = VisualUi.LookDayUi.SpriteTypeFace.Face_Normal; faceName = "Face_Normal"; break;
						}

						if (FaceSprites.TryGetValue(faceType, out Sprite faceSprite) && faceSprite != null)
						{
							// Если иконка уже есть, просто меняем спрайт
							if (currentFace != null)
							{
								var img = currentFace.GetComponent<Image>();
								if (img != null) img.sprite = faceSprite;
							}
							else
							{
								currentFace = Utils_Ui.CreateUIImage(faceName, BChar.transform, faceSprite, new Vector2(100, 100), new Vector3(170, 170, 0), false);
							}
						}
					}

					private void LoadFaceSprites()
					{
						foreach (var kvp in DataStore.Instance.Visual.LookDay.SpritePathsFace)
						{
							Utils_Ui.LoadSpriteAsync(kvp.Value, sprite =>
							{
								if (sprite != null)
								{
									FaceSprites[kvp.Key] = sprite;
									Debug.Log($"[LoadFaceSprites] Sprite loaded for {kvp.Key}");
								}
								else
								{
									Debug.LogWarning($"[LoadFaceSprites] Failed to load sprite for {kvp.Key} from {kvp.Value}");
								}
							});
						}
					}

					public void SomeOneDead(BattleChar DeadChar)
					{
						if (DeadChar == BChar)
						{
							Utils_Ui.DestroyObject(currentFace);
						}
					}
				}

				public class SocialDistancing : Buff
				{
					public override void Init()
					{
						PlusStat.def = 10;
						PlusStat.Strength = true;
					}
				}

				public class SurpriseGift : Buff, IP_PlayerTurn_1
				{
					public override void Init()
					{
						PlusStat.PlusCriDmg = 10;
					}

					public void Turn1()
					{
						Utils.ApplyExtended(BChar.MyTeam.Skills, ModItemKeys.SkillExtended_Ex_Abnormality_Friend, true, true, true, 1, true);
					}
				}
			}

			public class Lv2
			{
				public class Alertness : Buff, IP_SkillUse_Target, IP_PlayerTurn
				{
					private int attackPlayed;

					public override string DescExtended()
					{
						int chanceWeak = (int)(BChar.GetStat.HIT_DEBUFF + 100);
						int chanceCC = (int)(BChar.GetStat.HIT_CC + 100);
						return base.DescExtended().Replace("&a", chanceWeak.ToString()).Replace("&b", chanceCC.ToString());
					}

					public override void Init()
					{
						PlusStat.hit = 20;
					}

					public void Turn()
					{
						attackPlayed = 0;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage && attackPlayed < 2)
						{
							attackPlayed++;
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Paralysis);
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile);
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Bind);
						}
					}
				}

				public class Friend : Buff, IP_PlayerTurn_1
				{
					public override void Init()
					{
						PlusStat.PlusCriDmg = 40;
					}

					public void Turn1()
					{
						Utils.ApplyExtended(BChar.MyTeam.Skills, ModItemKeys.SkillExtended_Ex_Abnormality_Friend_0, true, true, true, 2, true);
					}
				}

				public class FunnyPrank : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						int damage = (int)(BChar.GetStat.maxhp * 0.4f);
						return base.DescExtended().Replace("&a", damage.ToString());
					}

					public override void Init()
					{
						PlusStat.PlusCriDmg = 40;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage && !Cri)
						{
							bool alwaysLucky = RandomManager.RandomPer(BattleRandom.PassiveItem, 100, 50);

							if (alwaysLucky)
							{
								Cri = true;
							}
							else
							{
								int damage = (int)(BChar.GetStat.maxhp * 0.4f);
								Utils.TakeNonLethalDamage(BChar, damage);
							}
						}
					}
				}

				public class Meal : Buff, IP_DealDamage
				{
					public override void Init()
					{
						PlusStat.hit = 20;
						PlusStat.HIT_CC = 20;
						PlusStat.HIT_DEBUFF = 20;
						PlusStat.HIT_DOT = 20;
					}

					public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
					{
						if (Damage >= 1)
						{
							BChar.Heal(BChar, (int)(Damage * 0.2), false, false, null);
						}
					}
				}

				public class Obsession : Buff, IP_SkillUse_Target
				{
					public override void Init()
					{
						PlusStat.DMGTaken = 40;
						PlusPerStat.Damage = 40;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.ApplyBleed(hit, BChar, 2);
						}
					}
				}

				public class Shyness : Buff, IP_Hit, IP_Dodge
				{
					public override string DescExtended()
					{
						int damage = (int)(BChar.GetStat.def * 0.5);
						return base.DescExtended().Replace("&a", damage.ToString());
					}

					public override void Init()
					{
						PlusStat.def = 20;
						PlusStat.RES_CC = 20;
						PlusStat.RES_DEBUFF = 20;
						PlusStat.RES_DOT = 20;
					}

					public void Dodge(BattleChar Char, SkillParticle SP)
					{
						DealDamage(SP.SkillData.Master);
					}

					public void Hit(SkillParticle SP, int Dmg, bool Cri)
					{
						DealDamage(SP.SkillData.Master);
					}

					private void DealDamage(BattleChar target)
					{
						if (target.Info.Ally) return;

						int damage = (int)(BChar.GetStat.def * 0.5);
						int finalDamage = Math.Min(damage, 15);
						target.Damage(BChar, finalDamage, false, true);
					}
				}
			}

			public class Lv3
			{
				public class GooeyWaste : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						int chanceWeak = (int)(BChar.GetStat.HIT_DEBUFF + 100);
						int chanceCC = (int)(BChar.GetStat.HIT_CC + 100);
						int chanceDOT = (int)(BChar.GetStat.HIT_DOT + 100);
						return base.DescExtended().Replace("&a", chanceWeak.ToString()).Replace("&b", chanceCC.ToString()).Replace("&c", chanceDOT.ToString());
					}

					public override void Init()
					{
						PlusStat.HIT_CC = 40;
						PlusStat.HIT_DEBUFF = 40;
						PlusStat.HIT_DOT = 40;
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.Master == BChar && SP.SkillData.IsDamage)
						{
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Paralysis);
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Fragile);
							Utils.AddDebuff(hit, BChar, ModItemKeys.Buff_B_EmotionSystem_Bind);
							Utils.ApplyBleed(hit, BChar);
						}
					}
				}

				public class WornParasol : Buff, IP_DamageTake
				{
					public override void Init()
					{
						PlusStat.AggroPer = 100;
					}

					public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
					{
						bool alwaysLucky = RandomManager.RandomPer(BChar.GetRandomClass().Target, 100, 50);

						if (alwaysLucky)
						{
							resist = true;
							User.Damage(BChar, Dmg * 2, false, true);
						}
					}
				}

				public class LovingFamily : Buff
				{
					public override void Init()
					{
						PlusStat.DMGTaken = -20;
					}
				}

				public class LovingFamily_0 : Buff, IP_DamageTake
				{
					public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
					{
						if (Dmg >= 1)
						{
							resist = true;
							SelfStackDestroy();
						}
					}
				}
			}

			public class EGO
			{
				public class SanguineDesire : Buff, IP_SkillUse_Target
				{
					public override string DescExtended()
					{
						int chanceDOT = (int)(BChar.GetStat.HIT_DOT + 100);
						return base.DescExtended().Replace("&a", chanceDOT.ToString());
					}

					public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
					{
						if (SP.SkillData.IsDamage && SP.SkillData.Master == BChar)
						{
							Utils.ApplyBleed(hit, BChar);
						}
					}
				}
			}
		}
	}
}
