using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoArkMod;
using GameDataEditor;
using I2.Loc;
using UnityEngine;
using static CharacterDocument;

namespace Kazuma
{
	public static class Scripts
	{
		private static BattleChar DummyChar => BattleSystem.instance?.AllyTeam.DummyChar;
		private static readonly RandomClass Rand = RandomClass.CreateRandomClass();
		private const double LuckDivider = 25.0;

		private static readonly Dictionary<string, string> Panties = new Dictionary<string, string>()
		{
			{ ModItemKeys.Skill_Panties_01_Standart, ModItemKeys.Item_Consume_C_Panties_01_Standart },
			{ ModItemKeys.Skill_Panties_02_FloweredPatterned, ModItemKeys.Item_Consume_C_Panties_02_FloweredPatterned },
			{ ModItemKeys.Skill_Panties_03_Regular, ModItemKeys.Item_Consume_C_Panties_03_Regular },
			{ ModItemKeys.Skill_Panties_04_Star, ModItemKeys.Item_Consume_C_Panties_04_Star },
			{ ModItemKeys.Skill_Panties_05_SimpleWhite, ModItemKeys.Item_Consume_C_Panties_05_SimpleWhite },
			{ ModItemKeys.Skill_Panties_06_SimpleDotted, ModItemKeys.Item_Consume_C_Panties_06_SimpleDotted },
			{ ModItemKeys.Skill_Panties_07_SimpleStriped, ModItemKeys.Item_Consume_C_Panties_07_SimpleStriped },
			{ ModItemKeys.Skill_Panties_08_Striped, ModItemKeys.Item_Consume_C_Panties_08_Striped },
			{ ModItemKeys.Skill_Panties_09_StandartStudent, ModItemKeys.Item_Consume_C_Panties_09_StandartStudent },
			{ ModItemKeys.Skill_Panties_10_StripedStandart, ModItemKeys.Item_Consume_C_Panties_10_StripedStandart },
			{ ModItemKeys.Skill_Panties_11_FittedLace, ModItemKeys.Item_Consume_C_Panties_11_FittedLace },
			{ ModItemKeys.Skill_Panties_12_FittedLaceGray, ModItemKeys.Item_Consume_C_Panties_12_FittedLaceGray },
			{ ModItemKeys.Skill_Panties_13_Fluffy, ModItemKeys.Item_Consume_C_Panties_13_Fluffy },
			{ ModItemKeys.Skill_Panties_14_DottedFluffy, ModItemKeys.Item_Consume_C_Panties_14_DottedFluffy },
			{ ModItemKeys.Skill_Panties_15_CheckeredFluffy, ModItemKeys.Item_Consume_C_Panties_15_CheckeredFluffy },
			{ ModItemKeys.Skill_Panties_16_FittedRegular, ModItemKeys.Item_Consume_C_Panties_16_FittedRegular },
			{ ModItemKeys.Skill_Panties_17_FittedPolkaDot, ModItemKeys.Item_Consume_C_Panties_17_FittedPolkaDot },
			{ ModItemKeys.Skill_Panties_18_FittedPolkaDotGray, ModItemKeys.Item_Consume_C_Panties_18_FittedPolkaDotGray },
			{ ModItemKeys.Skill_Panties_19_Frilly, ModItemKeys.Item_Consume_C_Panties_19_Frilly },
			{ ModItemKeys.Skill_Panties_20_String, ModItemKeys.Item_Consume_C_Panties_20_String },
			{ ModItemKeys.Skill_Panties_21_Pumpkin, ModItemKeys.Item_Consume_C_Panties_21_Pumpkin },
			{ ModItemKeys.Skill_Panties_22_Sport, ModItemKeys.Item_Consume_C_Panties_22_Sport },
			{ ModItemKeys.Skill_Panties_23_PinkRibbon, ModItemKeys.Item_Consume_C_Panties_23_PinkRibbon },
			{ ModItemKeys.Skill_Panties_24_Comfy, ModItemKeys.Item_Consume_C_Panties_24_Comfy },
			{ ModItemKeys.Skill_Panties_25_Velvet, ModItemKeys.Item_Consume_C_Panties_25_Velvet }
		};

		private static readonly List<string> PantyKeys = Panties?.Keys.ToList() ?? new List<string>();

		public static void Steal(BattleChar user, BattleChar target, int luck)
		{
			//if (target == null) return;

			Debug.Log("Steal Start");

			var targetData = target.Info.KeyData;
			var gender = new GDECharacterData(targetData).Gender;

			if (target.Info.Ally && gender == 1 || Utils.FemaleEnemy.Contains(targetData))
			{
				ChanceGainPanties(user, target, luck);
				Debug.Log("Stealing panties");
			}
			else
			{
				GainRandomEquip(luck);
				Debug.Log("Stealing equip");
			}
		}

		private static void ChanceGainPanties(BattleChar user, BattleChar target, int luck)
		{
			bool alwaysLucky = RandomManager.RandomPer(Rand, 100, luck);

			if (alwaysLucky)
			{
				BattleSystem.instance.StartCoroutine(GainPanties(user, target));
			}
			else
			{
				GainRandomEquip(luck);
			}
		}

		private static IEnumerator GainPanties(BattleChar user, BattleChar target)
		{
			if (target.Info.Ally)
			{
				yield return BattleSystem.instance.StartCoroutine(SlapKazuma(user, target));

				if (user.IsDead) yield break;
			}

			List<Skill> randomPantiesList = new List<Skill>();
			int randomIndex = RandomManager.RandomInt(Rand, 0, Panties.Count - 1);
			string randomSkillKey = PantyKeys[randomIndex];
			var skill = Skill.TempSkill(randomSkillKey, DummyChar, DummyChar.MyTeam);

			//string imagePath = "Misc/Panties/1.png";
			//string address = ModManager.getModInfo("Kazuma").assetInfo.ImageFromFile(imagePath);
			//skill.Image_Skill = address; 
			randomPantiesList.Add(skill);

			BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(
				randomPantiesList,
				new SkillButton.SkillClickDel(Selection),
				ScriptLocalization.System_SkillSelect.EffectSelect,
				false, false, true, false, false
			));

			yield break;
		}

		private static IEnumerator SlapKazuma(BattleChar user, BattleChar target)
		{
			var skill = Skill.TempSkill(ModItemKeys.Skill_S_Panties_Counter, user, user.MyTeam);
			skill.PlusHit = true;
			skill.FreeUse = true;
			target.ParticleOut(skill, user);

			string text = "Pervert!";
			yield return target.StartCoroutine(ShowText(target.GetTopPos(), text));
			yield break;
		}

		private static IEnumerator ShowText(Vector3 position, string text)
		{
			var topText = BattleText.CustomText(position, text);
			yield return new WaitForSecondsRealtime(1f);
			topText?.End();
		}


		private static void Selection(SkillButton Mybutton)
		{
			BattleSystem.DelayInput(RandomPantiesSelect(Mybutton));
		}

		private static IEnumerator RandomPantiesSelect(SkillButton Mybutton)
		{
			string key = Mybutton.Myskill.MySkill.KeyID;

			if (Panties.TryGetValue(key, out var rewardPanties))
			{
				InventoryManager.Reward(ItemBase.GetItem(rewardPanties, 1));
				Utils.UnlockSkillPreview(rewardPanties);
			}
			yield break;
		}

		private static void GainRandomEquip(int luck)
		{
			int equipTier = (int)Math.Ceiling(luck / LuckDivider);
			var randomEquip = PlayData.GetEquipRandom(equipTier, false, new List<string>());
			InventoryManager.Reward(ItemBase.GetItem(randomEquip));
		}
	}
}