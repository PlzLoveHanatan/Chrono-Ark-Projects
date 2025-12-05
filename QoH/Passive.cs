using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod.ModEditor;
using EItem;
using EmotionSystem;
using HarmonyLib;
using Spine;
using UnityEngine;
using UnityEngine.UI;
using Type = EmotionSystem.DataStore.VisualUi.QoHUi.SpriteTypeChibi;

namespace QoH
{
	public class Passive
	{
		public class HereComesMagicalGirl : Passive_Char, IP_PlayerTurn, IP_Draw, IP_BattleStart_UIOnBefore, IP_SkillUse_User, IP_DamageTake, IP_Dodge, IP_Healed
		{
			private GameObject CurrentChibi;
			private Dictionary<Type, Sprite> chibiSprites;

			private Dictionary<Type, Sprite> ChibiSprites
			{
				get
				{
					if (chibiSprites == null)
					{
						chibiSprites = new Dictionary<Type, Sprite>();
					}
					return chibiSprites;
				}
			}

			private static readonly Type[] AllChibiTypes = (Type[])Enum.GetValues(typeof(Type));

			private readonly Dictionary<string, Type[]> ChibiOptions = new Dictionary<string, Type[]>
			{
				{ "Attack", new Type[] { Type.Chibi_Attack_Hand, Type.Chibi_Attack_Magic, Type.Chibi_Move, Type.Chibi_Wink_Seat, Type.Chibi_Wink_Stand } },
				{ "Heal", new Type[] { Type.Chibi_Idle, Type.Chibi_Move, Type.Chibi_Wink_Seat, Type.Chibi_Wink_Stand } },
				{ "Damaged", new Type[] { Type.Chibi_Damaged, Type.Chibi_Idle, Type.Chibi_Shrug, Type.Chibi_Shrug_Extra } },
				{ "Evade", new Type[] { Type.Chibi_Attack_Hand, Type.Chibi_Move, Type.Chibi_Shrug, Type.Chibi_Wink_Seat, Type.Chibi_Wink_Stand } },
				{ "Default", new Type[] { Type.Chibi_Idle, Type.Chibi_Move, Type.Chibi_Shrug, Type.Chibi_Wink_Seat, Type.Chibi_Wink_Stand } } 
			};

			private readonly Dictionary<Type, (Vector2 size, Vector3 position)> ChibiInfo = new Dictionary<Type, (Vector2 size, Vector3 position)>
			{
				{ Type.Chibi_Attack_Magic, ( new Vector2 (370, 370), new Vector3 (20, 260) ) },
				{ Type.Chibi_Attack_Hand, ( new Vector2 (300, 300), new Vector3 (20, 260) ) },
				{ Type.Chibi_Damaged, ( new Vector2 (300, 300), new Vector3 (-60, 240) ) },
				{ Type.Chibi_Idle, ( new Vector2 (270, 270), new Vector3 (40, 240) ) },
				{ Type.Chibi_Move, (new Vector2(250, 250), new Vector3(-20, 240) ) },
				{ Type.Chibi_Wink_Seat, (new Vector2(260, 260), new Vector3(-30, 220) ) },
				{ Type.Chibi_Wink_Stand, (new Vector2(280, 280), new Vector3(20, 240) ) },
				{ Type.Chibi_Shrug, (new Vector2(260, 260), new Vector3(20, 240) ) },
				{ Type.Chibi_Shrug_Extra, (new Vector2(260, 260), new Vector3(0, 240) ) },
			};

			public override void Init()
			{
				OnePassive = true;
			}

			public void BattleStartUIOnBefore(BattleSystem Ins)
			{
				LoadSprites();
			}

			public void Turn()
			{
				CheckFixedSkill();
				//Utils.GetOrAddBuff(BChar, ModItemKeys.Buff_B_QoH_PureHeart);
				Utils.GetOrAddBuff(BChar, ModItemKeys.Buff_B_QoH_MagicalGirl);
				Utils.GetOrAddBuff(BChar, ModItemKeys.Buff_B_QoH_Sanity);
			}

			public void SkillUse(Skill SkillD, List<BattleChar> Targets)
			{
				if (SkillD.Master == BChar)
				{
					ChooseChibi(SkillD);
				}				
			}

			public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
			{
				if (Dmg >= 1)
				{
					ChooseChibi(null, true);
				}
			}

			public void Dodge(BattleChar Char, SkillParticle SP)
			{
				if ( Char == BChar)
				{
					ChooseChibi(null, false, true);
				}
			}

			public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
			{
				if (HealedChar == BChar)
				{
					ChooseChibi(null);
				}
			}

			public IEnumerator Draw(Skill Drawskill, bool NotDraw)
			{
				if (Drawskill.Master == BChar)
				{
					Utils.ApplyExtended(Drawskill, ModItemKeys.SkillExtended_Ex_QoH_Hysteria, false, true);
				}
				yield break;
			}

			private void CheckFixedSkill()
			{
				var fixedSkill = (BChar as BattleAlly).MyBasicSkill.buttonData;

				if (fixedSkill != null)
				{
					Utils.ApplyExtended(fixedSkill, ModItemKeys.SkillExtended_Ex_QoH_Hysteria, false, true);
				}
			}

			private void LoadSprites()
			{
				foreach (var kvp in DataStore.Instance.Visual.QoHChibi.SpritePathsChibi)
				{
					Utils_Ui.LoadSpriteAsync(kvp.Value, sprite =>
					{
						if (sprite != null)
						{
							ChibiSprites[kvp.Key] = sprite;
						}
					});
				}
			}

			private void ChooseChibi(Skill skill, bool isTakingDamage = false, bool isEvade = false, bool randomChibi = false)
			{
				Type[] options;

				if (skill?.IsDamage == true)
				{
					options = ChibiOptions["Attack"];
				}
				else if (skill?.IsHeal == true)
				{
					options = ChibiOptions["Heal"];
				}
				else if (isTakingDamage)
				{
					options = ChibiOptions["Damaged"];
				}
				else if (isEvade)
				{
					options = ChibiOptions["Evade"];
				}
				else
				{
					options = ChibiOptions["Default"];
				}

				Type chosenType = options[RandomManager.RandomInt(BChar.GetRandomClass().Main, 0, options.Length)];
				CreateChibi(chosenType, randomChibi);
			}

			private void CreateChibi(Type? chibiType = null, bool randomChibi = false)
			{
				// Если нужно выбрать случайный чиби
				if (randomChibi || !chibiType.HasValue)
				{
					chibiType = AllChibiTypes[RandomManager.RandomInt(RandomClassKey.Active, 0, AllChibiTypes.Length)];
				}

				// Проверка, есть ли спрайт
				if (!ChibiSprites.TryGetValue(chibiType.Value, out Sprite chibiSprite) || chibiSprite == null)
				{
					return;
				}

				if (CurrentChibi != null)
				{
					Utils_Ui.DestroyObject(CurrentChibi);
				}

				var (size, position) = ChibiInfo[chibiType.Value];

				CurrentChibi = Utils_Ui.CreateUIImage("Chibi", BChar.transform, chibiSprite, size, position, false);
				CurrentChibi.AddComponent<QoH_Chibi_Script>();
			}
		}
	}
}
