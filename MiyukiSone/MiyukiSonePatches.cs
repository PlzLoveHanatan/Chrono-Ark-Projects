using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using GameDataEditor;
using UseItem;
using static MiyukiSone.Affection;
using PItem;
using System.Reflection;
using DG.Tweening.Plugins.Core;

namespace MiyukiSone
{
	[HarmonyPatch]
	public static class Patches
	{
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(CharFace), "GetRandomSkill")]
		private static IEnumerable<CodeInstruction> CharacterUpgradeTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = instructions.ToList<CodeInstruction>();
			int num = list.FindLastIndex((CodeInstruction code) => code.opcode == OpCodes.Ldloc_0);
			list.InsertRange(num + 1, new List<CodeInstruction>
			{
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patches), nameof(MiyukiModifySkills)))
			});
			return list.AsEnumerable<CodeInstruction>();
		}

		[HarmonyTranspiler]
		[HarmonyPatch(typeof(SkillBookCharacter), nameof(SkillBookCharacter.Use))]
		[HarmonyPatch(typeof(SkillBookCharacter_Rare), nameof(SkillBookCharacter_Rare.Use))]
		[HarmonyPatch(typeof(SkillBookInfinity), nameof(SkillBookInfinity.Use))]
		[HarmonyPatch(typeof(SkillBookSuport), nameof(SkillBookSuport.Use))]
		private static IEnumerable<CodeInstruction> SkillBookUseTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			var codes = instructions.ToList();

			// Ищем последний ldloc.0 — это обычно наш список скиллов
			int insertIndex = codes.FindLastIndex(c => c.opcode == OpCodes.Ldloc_0);

			if (insertIndex >= 0)
			{
				// Вставляем вызов MiyukiModifySkills
				codes.InsertRange(insertIndex + 1, new List<CodeInstruction>
			{
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patches), nameof(MiyukiModifySkills)))
			});
			}

			return codes.AsEnumerable();
		}

		public static List<Skill> MiyukiModifySkills(List<Skill> skills)
		{
			if (!MiyukiDecides && MiyukiSone_Plugin.MiyukiInParty()) return skills;

			UnityEngine.Debug.Log("[Miyuki] === ModifySkills START ===");

			if (skills == null || skills.Count == 0)
			{
				UnityEngine.Debug.Log("[Miyuki] skills == null or empty");
				return skills;
			}

			// Проверяем, есть ли у персонажа уже редкий скилл
			bool hasRare = skills.Any(s => s?.Master?.Info?.HasRareSkill() == true);
			if (hasRare && IsDere)
			{
				UnityEngine.Debug.Log("[Miyuki] Character already has rare skill, returning original skills");
				return skills;
			}

			UnityEngine.Debug.Log("[Miyuki] Skill count: " + skills.Count);

			var customSkillKeys = new List<string>
			{
				GDEItemKeys.Skill_S_DefultSkill_0,
				GDEItemKeys.Skill_S_DefultSkill_1,
				GDEItemKeys.Skill_S_DefultSkill_2,
				//GDEItemKeys.Skill_S_SacrificeSkill
			};

			var skillPoolKeys = IsYandere || !MiyukiSone_Plugin.MiyukiInParty() ? customSkillKeys : PlayData.ALLRARESKILLLIST.Select(s => s.KeyID).ToList();

			UnityEngine.Debug.Log("[Miyuki] Skill pool size: " + skillPoolKeys.Count);

			if (skillPoolKeys.Count == 0)
			{
				UnityEngine.Debug.Log("[Miyuki] skillPoolKeys is empty");
				return skills;
			}

			int slotIndex = RandomManager.RandomInt("MiyukiRandomIndex", 0, skills.Count);
			var oldSkill = skills[slotIndex];
			var newSkillKey = skillPoolKeys.Random("MiyukiRandomSkill");

			UnityEngine.Debug.Log($"[Miyuki] Replacing slot {slotIndex} | {oldSkill?.MySkill?.KeyID} -> {newSkillKey}");

			skills[slotIndex] = Skill.TempSkill(newSkillKey, oldSkill?.Master, oldSkill?.MyTeam);

			UnityEngine.Debug.Log("[Miyuki] === ModifySkills END ===");

			return skills;
		}

		private static bool HasRareSkill(this Character character)
		{
			if (character.BasicSkill?.SkillInfo?.Rare == true) return true;

			return character.SkillDatas?.Any(sd => sd.SkillInfo?.Rare == true) == true;
		}
	}
}