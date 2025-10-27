using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionSystem;

namespace EmotionSystem
{
	public partial class DataStore
	{
		public class Synchronize
		{
			public Dictionary<BattleChar, CharacterSkills> SavedSkills = new Dictionary<BattleChar, CharacterSkills>();

			public class CharacterSkills
			{
				public List<Skill> Hand { get; } = new List<Skill>();
				public List<Skill> DrawPile { get; } = new List<Skill>();
				public List<Skill> DiscardPile { get; } = new List<Skill>();
				public List<string> FixedAbility { get; } = new List<string>();
			}

			public readonly List<string> DerSkills = new List<string>
			{
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_FloodingBullets,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_FloodingBullets,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_InevitableBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_InevitableBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_MagicBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_MagicBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_SilentBullet,
				ModItemKeys.Skill_S_EGO_Synchronize_MagicBullet_SilentBullet
			};
		}
	}
}
