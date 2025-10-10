using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;

namespace EmotionalSystem
{
	public class EmotionalSystem_DataStore
	{
		public static List<string> ExceptSkills = new List<string>
		{
			GDEItemKeys.Skill_S_FanaticBoss_Phase1AllyCard,
			//GDEItemKeys.Skill_S_Momori_P
		};

		public static List<string> LucyNecklace = new List<string>
		{
			GDEItemKeys.Item_Active_LucysNecklace,
			GDEItemKeys.Item_Active_LucysNecklace2,
			GDEItemKeys.Item_Active_LucysNecklace3,
			GDEItemKeys.Item_Active_LucysNecklace4
		};

		public static List<string> DerSkills = new List<string>
		{
			ModItemKeys.Skill_S_Synchronize_Technological_FloodingBullets,
			ModItemKeys.Skill_S_Synchronize_Technological_FloodingBullets,
			ModItemKeys.Skill_S_Synchronize_Technological_InevitableBullet,
			ModItemKeys.Skill_S_Synchronize_Technological_InevitableBullet,
			ModItemKeys.Skill_S_Synchronize_Technological_MagicBullet,
			ModItemKeys.Skill_S_Synchronize_Technological_MagicBullet,
			ModItemKeys.Skill_S_Synchronize_Technological_SilentBullet,
			ModItemKeys.Skill_S_Synchronize_Technological_SilentBullet
		};

		public Dictionary<BattleChar, CharacterSkills> SavedSkills = new Dictionary<BattleChar, CharacterSkills>();

		public class CharacterSkills
		{
			public List<Skill> Hand { get; } = new List<Skill>();
			public List<Skill> DrawPile { get; } = new List<Skill>();
			public List<Skill> DiscardPile { get; } = new List<Skill>();
			public List<string> FixedAbility { get; } = new List<string>();
		}
	}
}
