using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using GameDataEditor;
using UnityEngine;
using static MiyukiSone.Affection;

namespace MiyukiSone
{
	public static class MiyukiPatchesSkillsHelpers
	{
		public static List<Skill> GetLucySkillEx(BattleChar user, BattleTeam team)
		{
			List<Skill> skills = PlayData.TSavedata.LucySkills.Select(key => Skill.TempSkill(key, user, team)).ToList();
			MiyukiCV.Instance.LoadLucySkills(skills);
			return skills;
		}

		public static void ExtendedAdd_Battle(this Skill skill, List<string> extendeds)
		{
			foreach (var ex in extendeds.Select(key => Skill_Extended.DataToExtended(key)))
			{
				skill.ExtendedAdd_Battle(ex);
			}
		}

		public static void CelestialUpgrade(this Skill skill)
		{
			if (skill == null) return;

			List<string> allKeys = new List<string>();
			GDEDataManager.GetAllDataKeysBySchema(GDESchemaKeys.SkillExtended, out allKeys);

			List<Skill_Extended> validUpgrades = new List<Skill_Extended>();
			Skill_Extended upgrade;

			foreach (string key in allKeys)
			{
				// All Special Character Upgrades
				//if (MiyukiDecides)
				//{
				//	GDESkillExtendedData data = new GDESkillExtendedData(key);

				//	if (!data.Drop || data.Debuff || string.IsNullOrEmpty(data.NeedCharacter)) continue;

				//	GDECharacterData characterData = new GDECharacterData(data.NeedCharacter);
				//	if (characterData == null || string.IsNullOrEmpty(characterData.Key)) continue;

				//	upgrade = Skill_Extended.DataToExtended(data);
				//}

				GDESkillExtendedData data = new GDESkillExtendedData(key);
				if (!data.Drop || data.Debuff) continue;
				if (PlayData.TSavedata.Party.Find(c => c.KeyData == data.NeedCharacter) == null) continue;

				upgrade = Skill_Extended.DataToExtended(data);
				if (PlayData.Battleallys.SelectMany(bc => bc.Skills).Any(s => upgrade.CanEnforce(s))) validUpgrades.Add(upgrade);


				if (upgrade.CanEnforce(skill))
				{
					validUpgrades.Add(upgrade);
				}
			}


			if (validUpgrades.Count > 0)
			{
				Skill_Extended selectedUpgrade = validUpgrades.RandomElement();
				skill.ExtendedAdd_Battle(selectedUpgrade);
				skill.SaveLucyUpgrade(selectedUpgrade);
			}
			else
			{
				skill.NormalUpgrade();
			}
		}

		public static void NormalUpgrade(this Skill skill)
		{
			if (skill == null) return;
			var upgradeList = PlayData.GetEnforce(!MiyukiResult(), skill);
			var ex = upgradeList?.RandomElement();
			if (ex == null) return;
			skill.ExtendedAdd_Battle(ex);
			var skillData = skill.Master.Info.SkillDatas.FirstOrDefault(sd => sd == skill.CharinfoSkilldata);
			skill.SaveLucyUpgrade(ex);
			if (skillData != null && skillData.SKillExtended == null) skillData.SKillExtended = ex;
		}

		private static void SaveLucyUpgrade(this Skill skill, Skill_Extended ex)
		{
			if ((skill.Master.IsLucy || skill.Master.Dummy) && !skill.IsCreatedInBattle && ex != null) MiyukiCV.Instance.SaveLucySkill(skill);
		}
	}
}