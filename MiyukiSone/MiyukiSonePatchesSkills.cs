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
using I2.Loc;
using PItem;
using Spine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UseItem;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	[HarmonyPatch]
	public class MiyukiPatchesSkills
	{
		private const string OriginalAuthor = "surprise4u~";
		private const string ModifiedBy = "MiyukiSone";
		private const string Licenced = "MiyukiSone";

		#region Data & Constructor
		private static List<string> _allGameSkills;
		private static List<string> AllGameSkills
		{
			get
			{
				if (_allGameSkills == null)
				{
					_allGameSkills = PlayData.ALLSKILLLIST.Where(s => s.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && s.User != "" && s.Category.Key != GDEItemKeys.SkillCategory_LucySkill && s.User != ModItemKeys.Character_Miyuki).Select(s => s.KeyID).ToList();
				}
				return _allGameSkills;
			}
		}

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

		#region Lucy

		#region Lucy Init
		// Add ex to Lucy skills in stats window
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(CharStatV4), nameof(CharStatV4.Init))]
		[HarmonyPatch(typeof(CharStatV4), "LucySkillAdd")]
		[HarmonyPatch(typeof(CharStatV4), "_SkillUPdate", MethodType.Enumerator)]
		static IEnumerable<CodeInstruction> LucyStatMod(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			LocalBuilder skillsLocal = generator.DeclareLocal(typeof(List<Skill>));
			LocalBuilder iLocal = generator.DeclareLocal(typeof(int));

			var codeMatcher = new CodeMatcher(instructions, generator);
			codeMatcher.MatchStartForward(
				new CodeMatch(OpCodes.Ldsfld, AccessTools.Field(typeof(PlayData), nameof(PlayData.TSavedata))),
				new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(TempSaveData), nameof(TempSaveData.LucySkills))),
				new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(List<string>), nameof(List<string>.GetEnumerator)))
			).InsertAndAdvance(
				new CodeInstruction(OpCodes.Ldc_I4_0),
				new CodeInstruction(OpCodes.Stloc_S, iLocal), // i = 0
				new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(PlayData), nameof(PlayData.BattleDummy))), // PlayData.BattleDummy
				new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(PlayData), nameof(PlayData.TempBattleTeam))), // PlayData.TempBattleTeam
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkillsHelpers), nameof(MiyukiPatchesSkillsHelpers.GetLucySkillEx))),
				new CodeInstruction(OpCodes.Stloc_S, skillsLocal) // skills = MiyukiPatchesSkillsHelpers.GetLucySkillEx(PlayData.BattleDummy, PlayData.TempBattleTeam)
			).MatchStartForward(new CodeMatch(code => code.opcode == OpCodes.Br || code.opcode == OpCodes.Br_S));

			// inside loop
			codeMatcher.MatchStartForward(
				new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(Skill), nameof(Skill.TempSkill), new Type[] { typeof(string), typeof(BattleChar), typeof(BattleTeam) }))
			).RemoveInstruction().InsertAndAdvance(
				new CodeInstruction(OpCodes.Pop),
				new CodeInstruction(OpCodes.Pop),
				new CodeInstruction(OpCodes.Pop),
				new CodeInstruction(OpCodes.Ldloc_S, skillsLocal),
				new CodeInstruction(OpCodes.Ldloc_S, iLocal),
				new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<Skill>), "get_Item")) // Skill.TempSkill() => skills[i]
			).Advance(1).InsertAndAdvance(
				new CodeInstruction(OpCodes.Ldloc_S, iLocal),
				new CodeInstruction(OpCodes.Ldc_I4_1),
				new CodeInstruction(OpCodes.Add),
				new CodeInstruction(OpCodes.Stloc_S, iLocal) // i++
			);
			return codeMatcher.InstructionEnumeration();
		}

		// Add ex to Lucy skills added at the begining of a battle
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(BattleTeam), nameof(BattleTeam.init))]
		public static IEnumerable<CodeInstruction> LucyInitMod(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			LocalBuilder skillsLocal = generator.DeclareLocal(typeof(List<Skill>));
			LocalBuilder iLocal = generator.DeclareLocal(typeof(int));

			var codeMatcher = new CodeMatcher(instructions, generator);
			codeMatcher.MatchStartForward(
				new CodeMatch(OpCodes.Ldsfld, AccessTools.Field(typeof(PlayData), nameof(PlayData.TSavedata))),
				new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(TempSaveData), nameof(TempSaveData.LucySkills))),
				new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(List<string>), nameof(List<string>.GetEnumerator)))
			).InsertAndAdvance(
				new CodeInstruction(OpCodes.Ldc_I4_0),
				new CodeInstruction(OpCodes.Stloc_S, iLocal), // i = 0
				new CodeInstruction(OpCodes.Ldarg_0),
				new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BattleTeam), nameof(BattleTeam.LucyAlly))), // BattleTeam.LucyAlly
				new CodeInstruction(OpCodes.Ldarg_0), // BattleTeam
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkillsHelpers), nameof(MiyukiPatchesSkillsHelpers.GetLucySkillEx))),
				new CodeInstruction(OpCodes.Stloc_S, skillsLocal) // skills = MiyukiUtils.GetChimeraLucySkill(BattleTeam.LucyAlly, BattleTeam)
			).MatchStartForward(new CodeMatch(code => code.opcode == OpCodes.Br || code.opcode == OpCodes.Br_S));

			// inside loop
			codeMatcher.MatchStartForward(
				new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(Skill), nameof(Skill.TempSkill), new Type[] { typeof(string), typeof(BattleChar), typeof(BattleTeam) }))
			).RemoveInstruction().InsertAndAdvance(
				new CodeInstruction(OpCodes.Pop),
				new CodeInstruction(OpCodes.Pop),
				new CodeInstruction(OpCodes.Pop),
				new CodeInstruction(OpCodes.Ldloc_S, skillsLocal),
				new CodeInstruction(OpCodes.Ldloc_S, iLocal),
				new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<Skill>), "get_Item")) // Skill.TempSkill() => skills[i]
			).Advance(1).InsertAndAdvance(
				new CodeInstruction(OpCodes.Ldloc_S, iLocal),
				new CodeInstruction(OpCodes.Ldc_I4_1),
				new CodeInstruction(OpCodes.Add),
				new CodeInstruction(OpCodes.Stloc_S, iLocal) // i++
			);
			return codeMatcher.InstructionEnumeration();
		}

		// Add ex to Lucy skills in ResultUI, also fix basic skill in ResultUi
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(ResultUI), nameof(ResultUI.Init))]
		public static IEnumerable<CodeInstruction> LucyResultMod(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			LocalBuilder skillsLocal = generator.DeclareLocal(typeof(List<Skill>));
			LocalBuilder iLocal = generator.DeclareLocal(typeof(int));

			var codeMatcher = new CodeMatcher(instructions, generator);

			// Basic skill fix
			//codeMatcher.MatchStartForward(
			//	new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(Skill), nameof(Skill.TempSkill), new Type[] { typeof(string), typeof(BattleChar), typeof(BattleTeam) })),
			//	new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(Result_FixedSkill), nameof(Result_FixedSkill.MySkill)))
			//).RemoveInstruction().Insert(
			//	new CodeInstruction(OpCodes.Pop),
			//	new CodeInstruction(OpCodes.Pop)
			//).MatchStartBackwards(
			//	new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(BattleAlly), nameof(BattleAlly.BasicSkill))),
			//	new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(Skill), nameof(Skill.MySkill))), // to be removed
			//	new CodeMatch(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(GDESkillData), nameof(GDESkillData.KeyID))) // to be removed
			//).RemoveInstructionsWithOffsets(1, 2);

			//Fix character skill name
			//codeMatcher.MatchStartForward(
			//	new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(Skill), nameof(Skill.MySkill))),
			//	new CodeMatch(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(GDESkillData), nameof(GDESkillData.Name)))
			//).Set(OpCodes.Call, AccessTools.Method(typeof(MiyukiUtils), nameof(MiyukiUtils.GetRawName))).Advance(1).RemoveInstruction();

			// Lucy skill fix
			codeMatcher.MatchStartForward(
				new CodeMatch(OpCodes.Ldsfld, AccessTools.Field(typeof(PlayData), nameof(PlayData.TSavedata))),
				new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(TempSaveData), nameof(TempSaveData.LucySkills))),
				new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(List<string>), nameof(List<string>.GetEnumerator)))
			).InsertAndAdvance(
				new CodeInstruction(OpCodes.Ldc_I4_0),
				new CodeInstruction(OpCodes.Stloc_S, iLocal), // i = 0
				new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(PlayData), nameof(PlayData.BattleLucy))), // PlayData.BattleLucy
				new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(PlayData), nameof(PlayData.TempBattleTeam))), // PlayData.TempBattleTeam
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkillsHelpers), nameof(MiyukiPatchesSkillsHelpers.GetLucySkillEx))),
				new CodeInstruction(OpCodes.Stloc_S, skillsLocal) // skills = MiyukiUtils.GetChimeraLucySkill(PlayData.BattleDummy, PlayData.TempBattleTeam)
			);

			// inside loop
			// fix skill name
			//codeMatcher.MatchStartForward(
			//	new CodeMatch(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(GDESkillData), nameof(GDESkillData.Name)))
			//).Advance(1).InsertAndAdvance(
			//	new CodeInstruction(OpCodes.Pop),
			//	new CodeInstruction(OpCodes.Ldloc_S, skillsLocal),
			//	new CodeInstruction(OpCodes.Ldloc_S, iLocal),
			//	new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<Skill>), "get_Item")), // skills[i]
			//	new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiUtils), nameof(MiyukiUtils.GetRawName))) // skills[i].GetRawName()
			//);

			// add ex
			codeMatcher.MatchStartForward(
				new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(Skill), nameof(Skill.CloneSkill)))
			).Advance(1).InsertAndAdvance(
				new CodeInstruction(OpCodes.Pop),
				new CodeInstruction(OpCodes.Ldloc_S, skillsLocal),
				new CodeInstruction(OpCodes.Ldloc_S, iLocal),
				new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<Skill>), "get_Item")) // Skill.TempSkill().CloneSkill() => skills[i]
			).Advance(1).InsertAndAdvance(
				new CodeInstruction(OpCodes.Ldloc_S, iLocal),
				new CodeInstruction(OpCodes.Ldc_I4_1),
				new CodeInstruction(OpCodes.Add),
				new CodeInstruction(OpCodes.Stloc_S, iLocal) // i++
			);
			return codeMatcher.InstructionEnumeration();
		}

		// Changing Lucy skills colour in the ResultUI
		[HarmonyPatch(typeof(ResultUI), nameof(ResultUI.Init))]
		public static class ResultUI_LucyEnforcePatch
		{
			[HarmonyPostfix]
			public static void Postfix(ResultUI __instance)
			{
				var allCharInfos = __instance.GetComponentsInChildren<Result_CharInfoV2>();
				Result_CharInfoV2 lucyCharInfo = null;

				foreach (var info in allCharInfos)
				{
					if (info.NameText.text == ScriptLocalization.Name_Character.Lucy)
					{
						lucyCharInfo = info;
						break;
					}
				}

				if (lucyCharInfo == null) return;

				foreach (var skillPrefab in lucyCharInfo.SkillAlign.GetComponentsInChildren<Result_SkillPrefab>())
				{
					var skill = skillPrefab.MySkill;
					if (skill == null) continue;

					if (skill.Enforce)
					{
						skillPrefab.SkillName.text = skill.MySkill.Name + "+";
						skillPrefab.SkillName.color = SkillButton.EnforceColor;
						skillPrefab.SkillName.fontSharedMaterial = __instance.EnforceMaterial;
					}
					else if (skill.Enforce_Weak)
					{
						skillPrefab.SkillName.color = SkillButton.EnforceWeakColor;
					}
				}
			}
		}
		#endregion

		#region Lucy Skills
		// Change Lucy skills or add Ex from Lucy books and level up Lucy draw
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(CharStatV4), nameof(CharStatV4.ReturnLucyDrawCard))]
		[HarmonyPatch(typeof(SkillBookLucy), nameof(SkillBookLucy.Use))] // Mysterious Skill Book
		[HarmonyPatch(typeof(SkillBookLucy_Rare), nameof(SkillBookLucy_Rare.Use))] // Transcendent Tome
		private static IEnumerable<CodeInstruction> LucySkillTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			var codes = instructions.ToList();
			int insertIndex = codes.FindLastIndex(c => c.opcode == OpCodes.Ldloc_0);
			if (insertIndex >= 0) codes.Insert(insertIndex + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkills), nameof(ReplaceLucySkills))));
			return codes.AsEnumerable();
		}

		// Lucy skills created from relics or battle skills
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(Extended_Lucy_8), nameof(Extended_Lucy_8.SkillUseSingle))] // Possibilities																		  
		[HarmonyPatch(typeof(DeliciousCarrot), nameof(DeliciousCarrot.EnableItem))] // Yummy Carrot
		[HarmonyPatch(typeof(CuteComputer), "LucySkillAdd", MethodType.Enumerator)] // Tiny Computer
		public static IEnumerable<CodeInstruction> LucySkillCreateMod(IEnumerable<CodeInstruction> instructions, MethodBase original)
		{
			try
			{
				var codeMatcher = new CodeMatcher(instructions).End();
				var result = MatchAndReplaceSkills(codeMatcher, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkills), nameof(ReplaceLucySkills))));
				return result.InstructionEnumeration();
			}
			catch
			{
				return instructions;
			}
		}
		#endregion

		#endregion

		#region Character Skills
		// Change Character skills or add Ex from Lucy books and Character up
		[HarmonyTranspiler]
		[HarmonyPatch(typeof(CharFace), nameof(CharFace.GetRandomSkill))]
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

		[HarmonyTranspiler]
		[HarmonyPatch(typeof(S_Lucy_CasinoDLC_9), nameof(S_Lucy_CasinoDLC_9.SkillUseSingle))] // Gold Card
		[HarmonyPatch(typeof(Extended_Lucy_2), nameof(Extended_Lucy_2.SkillUseSingle))] // Encourage
		[HarmonyPatch(typeof(S_Lucy_18), nameof(S_Lucy_18.SkillUseSingle))] // Trick Show
		[HarmonyPatch(typeof(OldRule), "ShowSkill", MethodType.Enumerator)]
		[HarmonyPatch(typeof(BlueRose), nameof(BlueRose.Turn))]
		[HarmonyPatch(typeof(S_Potion_Battle), nameof(S_Potion_Battle.SkillUseSingle))]
		[HarmonyPatch(typeof(S_Potion_Healer), nameof(S_Potion_Healer.SkillUseSingle))]
		[HarmonyPatch(typeof(Extended_Golem_P_1), nameof(Extended_Golem_P_1.SkillUseSingle))]
		[HarmonyPatch(typeof(Extended_Public_35), nameof(Extended_Public_35.SkillUseSingle))]
		[HarmonyPatch(typeof(P_TrialofBrave_Enemy_summon), "Del", MethodType.Enumerator)]
		[HarmonyPatch(typeof(S_Lian_12), nameof(S_Lian_12.SkillUseSingle))]
		[HarmonyPatch(typeof(S_Trisha_9), nameof(S_Trisha_9.SkillUseSingle))]
		[HarmonyPatch(typeof(CrimsonBattle), nameof(CrimsonBattle.Draw), MethodType.Enumerator)]
		public static IEnumerable<CodeInstruction> CharacterSkillCreateMod(IEnumerable<CodeInstruction> instructions, MethodBase original)
		{
			try
			{
				var codeMatcher = new CodeMatcher(instructions).End();
				var result = MatchAndReplaceSkills(codeMatcher, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MiyukiPatchesSkills), nameof(ReplaceCharacterSkills))));
				return result.InstructionEnumeration();
			}
			catch
			{
				return instructions;
			}
		}

		public static CodeMatcher MatchAndReplaceSkills(CodeMatcher codeMatcher, params CodeInstruction[] instructions)
		{
			var matcher = codeMatcher.MatchStartBackwards(new CodeMatch(code => code.opcode == OpCodes.Call &&
			((MethodInfo)code.operand == AccessTools.Method(typeof(BattleSystem), nameof(BattleSystem.I_OtherSkillSelect)) ||
			(MethodInfo)code.operand == AccessTools.Method(typeof(SelectSkillList), nameof(SelectSkillList.NewSelectSkillList)))));

			if (!matcher.IsValid) return codeMatcher;
			matcher = matcher.MatchStartBackwards(new CodeMatch(code => code.opcode == OpCodes.Ldarg_0 || code.IsLdloc() || code.opcode == OpCodes.Ldnull));
			if (!matcher.IsValid) return codeMatcher;
			return matcher.Insert(instructions);
		}
		#endregion

		public static List<Skill> ReplaceLucySkills(List<Skill> skills)
		{
			for (int i = 0; i < skills.Count; i++)
			{
				skills[i] = ReplaceLucySkill(skills[i]);
			}
			return skills;
		}

		public static Skill ReplaceLucySkill(Skill skill)
		{
			if (IsKuudere) return skill;
			skill = ChangeLucySkill(skill);
			skill = ChangeUpgrade(skill);
			return skill;
		}

		public static List<Skill> ReplaceCharacterSkills(List<Skill> skills)
		{
			for (int i = 0; i < skills.Count; i++)
			{
				skills[i] = ReplaceCharacterSkill(skills[i]);
			}
			return skills;
		}

		public static Skill ReplaceCharacterSkill(Skill skill)
		{
			if (IsKuudere) return skill;
			skill = ChangeCharacterSkill(skill);
			skill = ChangeUpgrade(skill);
			return skill;
		}

		private static Skill ChangeLucySkill(Skill skill)
		{
			if (skill == null || MiyukiForces) return skill;
			var newSkill = (IsYandere ? LucyCurseSkills : PlayData.GetLucySkill(false)).RandomElement();
			if (newSkill == null) return skill;
			return newSkill;
		}

		private static Skill ChangeCharacterSkill(Skill skill)
		{
			if (skill?.Master?.Info?.KeyData == ModItemKeys.Character_Miyuki && IsYandere) return skill;
			if (skill == null || MiyukiForces) return skill;
			var allRareSkills = PlayData.ALLRARESKILLLIST.Where(s => s.User != ModItemKeys.Character_Miyuki).Select(s => s.KeyID);
			var list = IsYandere ? CharacterBadSkillKeys : Utils.RandomPer(10) /*&& !HasRareSkill(skill.Master.Info)*/ ? allRareSkills.ToList() : null /*allSkills.ToList()*/ ;
			if (list == null || list.Count == 0) return skill;
			return Skill.TempSkill(list.RandomElement(), skill.Master, skill.MyTeam);
		}

		private static Skill ChangeUpgrade(Skill skill)
		{
			if (skill?.Master?.Info?.KeyData == ModItemKeys.Character_Miyuki && IsYandere) return skill;
			if (skill == null || MiyukiForces) return skill;
			if (IsDere && Utils.RandomPer(10)) skill.CelestialUpgrade();
			else skill.NormalUpgrade();
			return skill;
		}

		private static bool HasRareSkill(Character character)
		{
			if (character.BasicSkill?.SkillInfo?.Rare == true) return true;
			return character.SkillDatas?.Any(sd => sd.SkillInfo?.Rare == true) == true;
		}
	}
}
