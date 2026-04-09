using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiyukiSone
{
	public class MiyukiCV : CustomValue
	{
		// Saving Lucy skills Ex to addd it to Lucy Init
		public MiyukiLucySave LucySave = new MiyukiLucySave();

		// Character Starting Equip
		public bool Equip = false;

		// Miyuki Special Interaction
		public int CurrentKissNoType = 0;
		public int KissNoCallCount = 0;
		public int FinalViewCharge = 1;
		public int FinalViewDamage = 0;
		public bool BGMVolumeIncreased = false;
		public bool PauseOpen = false;
		public bool SlotsCheck = false;
		public bool GameUpdated = false;

		// Indexes
		public int LastAffection = -1;
		public int LastArtIndex = -1;
		public int LastPhoneImage = -1;
		public int MiyukiArtIndex = -1;
		public int Affection;

		// Saving Buff Key for Skill
		public string LastGlitchedPhoneBuff;

		public static MiyukiCV Instance
		{
			get
			{
				if (PlayData.TSavedata == null)
				{
					return new MiyukiCV();
				}
				return PlayData.TSavedata.GetOrAddCustomValue<MiyukiCV>();
			}
		}

		public void LoadLucySkills(List<Skill> skills) => LucySave.LoadLucySkills(skills);
		public void SaveLucySkills(List<Skill> skills) => LucySave.SaveLucySkills(skills);
		public void SaveLucySkill(Skill skill) => LucySave.SaveLucySkill(skill);

		public class MiyukiLucySave : MiyukiSerializableDictionary<string, List<string>>
		{

			public void LoadLucySkills(List<Skill> skills)
			{
				foreach (var skill in skills)
				{
					if (TryGetValue(skill.MySkill.KeyID, out var upgradeKeys))
					{
						skill.ExtendedAdd_Battle(upgradeKeys);
					}
					else
					{
						this[skill.MySkill.KeyID] = new List<string>();
					}
				}
			}

			public void SaveLucySkills(List<Skill> skills)
			{
				Clear();
				foreach (var skill in skills)
				{
					SaveLucySkill(skill);
				}
			}

			public void SaveLucySkill(Skill skill)
			{
				var upgradeKeys = skill.AllExtendeds.Where(ex => ex.Data != null).Select(ex => ex.Data.Key).ToList();
				this[skill.MySkill.KeyID] = upgradeKeys;
			}
		}
	}
}