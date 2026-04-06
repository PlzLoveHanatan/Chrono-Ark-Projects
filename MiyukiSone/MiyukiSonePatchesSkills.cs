using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GameDataEditor;
using HarmonyLib;
using PItem;
using Spine;
using UnityEngine;
using UseItem;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	[HarmonyPatch]
	public class MiyukiPatchesSkills
	{
		#region Data & Constructor
		private static List<Skill> _lucyCurseSkills;
		private static List<Skill> LucyCurseSkills
		{
			get
			{
				if (_lucyCurseSkills == null)
				{
					_lucyCurseSkills = new List<Skill>()
					{
						Skill.TempSkill(GDEItemKeys.Skill_S_LucyCurse_Banana, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam),
						Skill.TempSkill(GDEItemKeys.Skill_S_LucyCurse_CursedClock, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam),
						Skill.TempSkill(GDEItemKeys.Skill_S_LucyCurse_Heavy, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam),
						Skill.TempSkill(GDEItemKeys.Skill_S_LucyCurse_Late, PlayData.TempBattleTeam.DummyChar, PlayData.TempBattleTeam),
					};
				}
				return _lucyCurseSkills;
			}
		}

		private static readonly List<string> CharacterBadSkillKeys = new List<string>
		{
			GDEItemKeys.Skill_S_DefultSkill_0,
			GDEItemKeys.Skill_S_DefultSkill_1,
			GDEItemKeys.Skill_S_DefultSkill_2,
			ModItemKeys.Skill_S_Miyuki_Special_SacrificedKnowledge,
			ModItemKeys.Skill_S_Miyuki_Special_Yabeley
		};
		#endregion

		[HarmonyTranspiler]
		[HarmonyPatch(typeof(CharFace), nameof(CharFace.GetRandomSkill))]
		[HarmonyPatch(typeof(CharStatV4), nameof(CharStatV4.ReturnLucyDrawCard))]
		private static IEnumerable<CodeInstruction> CharacterUpgradeTranspiler(IEnumerable<CodeInstruction> instructions, MethodBase original)
		{
			var codes = instructions.ToList();
			int index = codes.FindLastIndex(c => c.opcode == OpCodes.Ldloc_0);

			if (index != -1)
			{
				MethodInfo targetMethod;
				if (original.DeclaringType == typeof(CharFace)) targetMethod = AccessTools.Method(typeof(MiyukiPatchesSkills), nameof(ReplaceCharacterSkills));
				else targetMethod = AccessTools.Method(typeof(MiyukiPatchesSkills), nameof(ReplaceLucySkills));
				codes.Insert(index + 1, new CodeInstruction(OpCodes.Call, targetMethod));
			}

			return codes;
		}

		// Normal Skills
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(SkillBookCharacter), nameof(SkillBookCharacter.Use))]
		[HarmonyPatch(typeof(SkillBookCharacter_Rare), nameof(SkillBookCharacter_Rare.Use))]
		[HarmonyPatch(typeof(SkillBookInfinity), nameof(SkillBookInfinity.Use))]
		[HarmonyPatch(typeof(SkillBookSuport), nameof(SkillBookSuport.Use))]
		private static IEnumerable<CodeInstruction> SkillBookUseTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			var codes = instructions.ToList();
			int insertIndex = codes.FindLastIndex(c => c.opcode == OpCodes.Ldloc_0);
			if (insertIndex >= 0) codes.Insert(insertIndex + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkills), nameof(ReplaceCharacterSkills))));
			return codes.AsEnumerable();
		}

		// Lucy Skills
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(SkillBookLucy), nameof(SkillBookLucy.Use))]
		[HarmonyPatch(typeof(SkillBookLucy_Rare), nameof(SkillBookLucy_Rare.Use))]
		private static IEnumerable<CodeInstruction> LucySkillBookUseTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			var codes = instructions.ToList();
			int insertIndex = codes.FindLastIndex(c => c.opcode == OpCodes.Ldloc_0);
			if (insertIndex >= 0) codes.Insert(insertIndex + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkills), nameof(ReplaceLucySkills))));
			return codes.AsEnumerable();
		}


		//[HarmonyPatch(typeof(CuteComputer), "LucySkillAdd", MethodType.Enumerator)]
		// PhotoCamera_SwimDLC
		// RedBlossoms


		[HarmonyTranspiler]
		[HarmonyPatch(typeof(OldRule), "ShowSkill", MethodType.Enumerator)] // Add this or not?
		[HarmonyPatch(typeof(BlueRose), nameof(BlueRose.Turn))]
		[HarmonyPatch(typeof(S_Potion_Battle), nameof(S_Potion_Battle.SkillUseSingle))]
		[HarmonyPatch(typeof(S_Potion_Healer), nameof(S_Potion_Healer.SkillUseSingle))]
		[HarmonyPatch(typeof(S_Potion_Tanker), nameof(S_Potion_Tanker.SkillUseSingle))]
		[HarmonyPatch(typeof(Extended_Golem_P_1), nameof(Extended_Golem_P_1.SkillUseSingle))]
		[HarmonyPatch(typeof(Extended_Lucy_8), nameof(Extended_Lucy_8.SkillUseSingle))]
		[HarmonyPatch(typeof(Extended_Public_35), nameof(Extended_Public_35.SkillUseSingle))]
		[HarmonyPatch(typeof(P_TrialofBrave_Enemy_summon), "Del", MethodType.Enumerator)]
		[HarmonyPatch(typeof(S_Lian_12), nameof(S_Lian_12.SkillUseSingle))]
		[HarmonyPatch(typeof(S_Lucy_CasinoDLC_9), nameof(S_Lucy_CasinoDLC_9.SkillUseSingle))]
		[HarmonyPatch(typeof(S_Trisha_9), nameof(S_Trisha_9.SkillUseSingle))]
		[HarmonyPatch(typeof(SR_QuickStart), "SkillSelect_Rare")]
		[HarmonyPatch(typeof(CrimsonBattle), nameof(CrimsonBattle.Draw), MethodType.Enumerator)]

		//[HarmonyTranspiler]
		//[HarmonyPatch(typeof(SR_QuickStart), nameof(SR_QuickStart.SkillSelect))]
		//static IEnumerable<CodeInstruction> SkillCreateMod2(IEnumerable<CodeInstruction> instructions)
		//{
		//	var codeMatcher = new CodeMatcher(instructions);
		//	codeMatcher.MatchStartForward(
		//		new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(SR_QuickStart), nameof(SR_QuickStart.GetRandomSkill)))
		//	).Advance(1).Insert(
		//		new CodeInstruction(OpCodes.Ldc_I4_0),
		//		new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkills), nameof(ReplaceCharacterSkillsDummy)))
		//	);
		//	return codeMatcher.InstructionEnumeration();
		//}

		public static List<Skill> ReplaceCharacterSkillsDummy(List<Skill> skills)
		{
			return ReplaceCharacterSkills(skills);
		}

		public static List<Skill> ReplaceCharacterSkills(List<Skill> skills)
		{
			if (IsKuudere) return skills;
			if (MiyukiDecides) ChangeCharacterSkill(skills);
			if (MiyukiDecides) ChangeUpgrade(skills);
			return skills;
		}

		public static List<Skill> ReplaceLucySkills(List<Skill> skills)
		{
			if (IsKuudere) return skills;
			if (MiyukiDecides) ChangeLucySkill(skills);
			return skills;
		}

		private static void ChangeLucySkill(List<Skill> skills)
		{
			int index = skills.IndexOf(skills.RandomElement());
			Skill skill = skills[index];
			var newSkill = (IsYandere ? LucyCurseSkills : PlayData.GetLucySkill(false)).RandomElement();
			if (MiyukiDecides) skills[index] = newSkill;
			else skills = skills.Select(s => newSkill).ToList();
		}

		private static void ChangeCharacterSkill(List<Skill> skills)
		{
			int index = skills.IndexOf(skills.RandomElement());
			Skill skill = skills[index];
			var allSkills = PlayData.ALLSKILLLIST.Where(s => s.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && s.User != "" && s.Category.Key != GDEItemKeys.SkillCategory_LucySkill & s.User != ModItemKeys.Character_Miyuki).Select(s => s.KeyID);
			var allRareSkills = PlayData.ALLRARESKILLLIST.Where(s => s.User != ModItemKeys.Character_Miyuki).Select(s => s.KeyID);
			var list = IsYandere ? CharacterBadSkillKeys : MiyukiDecides ? allSkills.ToList() : allRareSkills.ToList();
			if (MiyukiDecides) skills[index] = Skill.TempSkill(list.RandomElement(), skill.Master, skill.MyTeam);
			else skills = skills.Select(s => Skill.TempSkill(list.RandomElement(), s.Master, s.MyTeam)).ToList();
		}

		private static void ChangeUpgrade(List<Skill> skills)
		{
			int index = skills.IndexOf(skills.RandomElement());
			Skill skill = skills[index];
			if (skill?.Master?.Info?.KeyData == ModItemKeys.Character_Miyuki && IsYandere) return;
			if (MiyukiDecides) ApplyUpgrade(skill);
			else skills.ForEach(s => ApplyUpgrade(s));
		}

		private static void ApplyUpgrade(Skill skill)
		{
			(MiyukiDecides && IsDere ? (Action)(() => skill.CelestialUpgrade()) : () => skill.NormalUpgrade())();
		}

		//public static List<Skill> ReplaceSkills(List<Skill> skills, bool isCharacterDraw = true)
		//{
		//	if (IsKuudere) return skills;

		//	if (isCharacterDraw)
		//	{
		//		if (MiyukiDecides) CharacterSkill(skills, RandomManager.RandomInt("SkillIndex", 0, skills.Count));
		//		if (MiyukiDecides) ApplyUpgrade(skills, RandomManager.RandomInt("ExIndex", 0, skills.Count));
		//	}
		//	else
		//	{
		//		if (MiyukiDecides) LucySkill(skills, RandomManager.RandomInt("LucySkillIndex", 0, skills.Count));
		//		//if (MiyukiDecides) ApplyUpgrade(skills, RandomManager.RandomInt("LucyExIndex", 0, skills.Count));
		//	}
		//	return skills;
		//}

		//private static void LucySkill(List<Skill> skills, int skillIndex)
		//{
		//	var skill = (IsYandere ? LucyCurseSkills : PlayData.GetLucySkill(false)).RandomElement("RandomLucySkill");
		//	skills[skillIndex] = skill;
		//}

		//private static void CharacterSkill(List<Skill> skills, int skillIndex)
		//{
		//	var targetSkill = skills[skillIndex];
		//	if (targetSkill?.Master?.Info?.KeyData == ModItemKeys.Character_Miyuki /*&& IsYandere*/) return;
		//	var normalSkills = PlayData.ALLSKILLLIST.Where(s => s.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && s.User != "" && s.Category.Key != GDEItemKeys.SkillCategory_LucySkill).Select(s => s.KeyID);
		//	var rareSkills = PlayData.ALLRARESKILLLIST.Where(s => s.User != ModItemKeys.Character_Miyuki).Select(s => s.KeyID);
		//	var skillList = IsYandere ? CharacterBadSkillKeys : MiyukiDecides ? normalSkills : rareSkills.ToList();
		//	if (skillList.Count() > 0) skills[skillIndex] = Skill.TempSkill(skillList.RandomElement("RandomCharacterSkill"), targetSkill.Master, targetSkill.MyTeam);
		//}

		//private static void ApplyUpgrade(List<Skill> skills, int skillIndex)
		//{
		//	var targetSkill = skills[skillIndex];
		//	if (targetSkill?.Master?.Info?.KeyData == ModItemKeys.Character_Miyuki && IsYandere) return;
		//	if (targetSkill?.CharinfoSkilldata?.SKillExtended != null) return;
		//	(MiyukiDecides && IsDere ? (Action)(() => targetSkill.CelestialUpgrade()) : () => targetSkill.NormalUpgrade())();
		//}

		private static bool HasRareSkill(Character character)
		{
			if (character.BasicSkill?.SkillInfo?.Rare == true) return true;
			return character.SkillDatas?.Any(sd => sd.SkillInfo?.Rare == true) == true;
		}
	}
}
