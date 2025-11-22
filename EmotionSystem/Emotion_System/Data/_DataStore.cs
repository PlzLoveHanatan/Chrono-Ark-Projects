using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using static EmotionSystem.DataStore.VisualUi;

namespace EmotionSystem
{
	public partial class DataStore
	{
		private static DataStore _Instance;

		public static DataStore Instance
		{
			get
			{
				if (_Instance == null)
				{
					_Instance = new DataStore();
				}
				return _Instance;
			}
		}

		public Synchronize Synchronization { get; } = new Synchronize();
		public AbnormalityData Abnormalities { get; } = new AbnormalityData();
		public EGOData EGO { get; } = new EGOData();
		public GuestData Guest { get; } = new GuestData();
		public VisualUi Visual { get; } = new VisualUi();
		public AbnormalitySkillsData AbnormalitySkill { get; } = new AbnormalitySkillsData();

		public readonly List<string> ExceptSkills = new List<string>
		{
			GDEItemKeys.Skill_S_FanaticBoss_Phase1AllyCard,
			//GDEItemKeys.Skill_S_Momori_P
		};

		public readonly List<string> LucyNecklace = new List<string>
		{
			GDEItemKeys.Item_Active_LucysNecklace,
			GDEItemKeys.Item_Active_LucysNecklace2,
			GDEItemKeys.Item_Active_LucysNecklace3,
			GDEItemKeys.Item_Active_LucysNecklace4
		};

		public readonly List<string> LucyKeyList = new List<string>
		{
			ModItemKeys.Skill_S_EmotionSystem_Lucy_Candy,
			ModItemKeys.Skill_S_EmotionSystem_Lucy_HippityHop,
			ModItemKeys.Skill_S_EmotionSystem_Lucy_RainbowSea,
			ModItemKeys.Skill_S_EmotionSystem_Lucy_MusicBox,
		};
	}
}
