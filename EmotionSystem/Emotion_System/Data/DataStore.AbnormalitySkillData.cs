using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class DataStore
	{
		public class AbnormalitySkillsData
		{
			public readonly Dictionary<AbnormalityNatural, List<(int Gold, string Skill)>> NaturalSkills = new Dictionary<AbnormalityNatural, List<(int Gold, string Skill)>>
			{
				{ AbnormalityNatural.QueenHatred, new List<(int Gold, string Skill)> { (0, ModItemKeys.Skill_S_Abnormality_GuestLv2_Present), } },
			};

			public enum AbnormalityNatural
			{
				QueenHatred,
				KingGreed,
				Nix,
			}

			public readonly Dictionary<string, int> NaturalSkillCost = new Dictionary<string, int>
			{
				{ ModItemKeys.Skill_S_Abnormality_GuestLv2_Present, 500},
			};
		}
	}
}
