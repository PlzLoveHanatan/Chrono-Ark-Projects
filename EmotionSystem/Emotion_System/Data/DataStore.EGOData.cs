﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionSystem
{
	public partial class DataStore
	{
		public class EGOData
		{
			public readonly List<string> HistoryKeyList = new List<string>
			{
				ModItemKeys.Skill_S_EGO_History_FourthMatchFlame,
				ModItemKeys.Skill_S_EGO_History_GreenStem,
				ModItemKeys.Skill_S_EGO_History_Hornet,
				ModItemKeys.Skill_S_EGO_History_TheForgotten,
				ModItemKeys.Skill_S_EGO_History_Wingbeat,
			};

			public readonly List<string> TechnologicalKeyList = new List<string>
			{
				ModItemKeys.Skill_S_EGO_Technological_GrinderMk,
				ModItemKeys.Skill_S_EGO_Technological_Harmony,
				ModItemKeys.Skill_S_EGO_Technological_MagicBullet,
				ModItemKeys.Skill_S_EGO_Technological_Regret,
				ModItemKeys.Skill_S_EGO_Technological_SolemnLament,
			};

			public readonly List<string> LiteratureKeyList = new List<string>
			{
				ModItemKeys.Skill_S_EGO_Literature_BlackSwan,
				ModItemKeys.Skill_S_EGO_Literature_Laetitia,
				ModItemKeys.Skill_S_EGO_Literature_RedEyes,
				ModItemKeys.Skill_S_EGO_Literature_SanguineDesire,
				ModItemKeys.Skill_S_EGO_Literature_TodayExpression,
			};
		}
	}
}
