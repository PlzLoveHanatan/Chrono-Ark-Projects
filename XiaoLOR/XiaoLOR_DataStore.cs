using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XiaoLOR
{
	public class XiaoLOR_DataStore
	{
		public static readonly Dictionary<string, string> SkillsList = new Dictionary<string, string>
		{
			{ ModItemKeys.Skill_S_XiaoLORLv1_FrontalAssault, ModItemKeys.Skill_S_XiaoLORLv2_SinglePointStab },
			{ ModItemKeys.Skill_S_XiaoLORLv1_HighKick, ModItemKeys.Skill_S_XiaoLORLv2_GaleKick },
			{ ModItemKeys.Skill_S_XiaoLORLv1_InnerArdor, ModItemKeys.Skill_S_XiaoLORLv2_AlloutWar },
			{ ModItemKeys.Skill_S_XiaoLORLv1_RushDown, ModItemKeys.Skill_S_XiaoLORLv2_FleetEdge },
			{ ModItemKeys.Skill_S_XiaoLORLv1_ViolentFlame, ModItemKeys.Skill_S_XiaoLORLv2_JiāoTú },
			{ ModItemKeys.Skill_S_XiaoLORLv1_SturdyDefense, ModItemKeys.Skill_S_XiaoLORLv2_IronWall },
			{ ModItemKeys.Skill_S_XiaoLORLv1_FlowoftheSword, ModItemKeys.Skill_S_XiaoLORLv2_BìXì },
			{ ModItemKeys.Skill_S_XiaoLORLv1_FieryDragonSlash, ModItemKeys.Skill_S_XiaoLORLv2_BìÀn },
			{ ModItemKeys.Skill_S_XiaoLORLv1_FieryWaltz, ModItemKeys.Skill_S_XiaoLORLv2_FervidEmotions },
			{ ModItemKeys.Skill_S_XiaoLORRareLv1_ChīWěn, ModItemKeys.Skill_S_XiaoLORRareLv2_JīnNí },
			{ ModItemKeys.Skill_S_XiaoLORRareLv1_FlamingDragonFist, ModItemKeys.Skill_S_XiaoLORRareLv2_Tiěshānkào },
		};

		public static readonly List<string> XiaoEGO = new List<string>
		{
			ModItemKeys.Skill_S_XiaoLOREGO_PúLáo,
			ModItemKeys.Skill_S_XiaoLOREGO_YáZì,
			ModItemKeys.Skill_S_XiaoLOREGO_TāoTiè,
			ModItemKeys.Skill_S_XiaoLORUnique_FormingStorm
		};
	}
}
