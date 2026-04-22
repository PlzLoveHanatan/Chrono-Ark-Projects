using System;
using System.Collections.Generic;
using System.Linq;
using NLog.Filters;
using Steamworks;
using UnityEngine;

namespace MiyukiSone
{
	public class MiyukiCV : CustomValue
	{
		// Saving Lucy skills Ex to addd it to Lucy Init
		public MiyukiLucyExSave LucyExSave = new MiyukiLucyExSave();
		// Saving equipment slots for characters
		public MiyukiEquipmentCountSave EquipmentCount = new MiyukiEquipmentCountSave();

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

		public void LoadLucySkills(List<Skill> skills) => LucyExSave.LoadLucySkills(skills);
		public void SaveLucySkills(List<Skill> skills) => LucyExSave.SaveLucySkills(skills);
		public void SaveLucySkill(Skill skill) => LucyExSave.SaveLucySkill(skill);
		public void SaveEquipSlots(List<Character> characters) => EquipmentCount.SaveEquipSlot(characters);
		public void SaveEquipSlots(Character character) => EquipmentCount.SaveEquipSlot(character);
		public void LoadEquipSlots(List<Character> characters) => EquipmentCount.LoadEquipSlot(characters);
		public void LoadEquipSlots(Character character) => EquipmentCount.LoadEquipSlot(character);

		public class MiyukiLucyExSave : MiyukiSerializableDictionary<string, List<string>>
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

		public class MiyukiEquipmentCountSave : MiyukiSerializableDictionary<string, int>
		{
			public void SaveEquipSlot(List<Character> characters)
			{
				characters.ForEach(c => SaveEquipSlot(c));
			}

			public void SaveEquipSlot(Character character)
			{
				if (character == null) return;
				this[character.KeyData] = (int)(character.Equip?.Count > 0 ? character.Equip?.Count : 0);
			}

			public void LoadEquipSlot(List<Character> characters)
			{
				characters.ForEach(c => LoadEquipSlot(c));
			}

			public void LoadEquipSlot(Character character)
			{
				if (character == null || character.Equip == null) return;

				if (TryGetValue(character.KeyData, out int savedEquipSlots))
				{
					int diff = savedEquipSlots - character.Equip.Count;

					if (diff > 0)
					{
						for (int i = 0; i < diff; i++)
						{
							character.Equip.Add(null);
						}			
					}
					else if (diff < 0)
					{
						for (int i = 0; i < -diff; i++)
						{
							character.Equip.RemoveAt(character.Equip.Count - 1);
						}		
					}
				}
			}
		}
	}
}