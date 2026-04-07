using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			List<Skill_Extended> validPartyUpgrades = new List<Skill_Extended>();

			foreach (string key in allKeys)
			{
				GDESkillExtendedData data = new GDESkillExtendedData(key);
				if (!data.Drop || data.Debuff) continue;
				if (PlayData.TSavedata.Party.Find(c => c.KeyData == data.NeedCharacter) == null) continue;

				Skill_Extended upgrade = Skill_Extended.DataToExtended(data);
				if (PlayData.Battleallys.SelectMany(bc => bc.Skills).Any(s => upgrade.CanEnforce(s))) validPartyUpgrades.Add(upgrade);
			}


			if (validPartyUpgrades.Count > 0)
			{
				List<Skill_Extended> validSkillUpgrades = validPartyUpgrades.Where(upgrade => upgrade.CanEnforce(skill)).ToList();

				if (validSkillUpgrades.Count > 0)
				{
					Skill_Extended selectedUpgrade = validSkillUpgrades.RandomElement();
					skill.ExtendedAdd_Battle(selectedUpgrade);
					skill.SaveLucyUpgrade(selectedUpgrade);
				}
				else
				{
					skill.NormalUpgrade();
				}
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