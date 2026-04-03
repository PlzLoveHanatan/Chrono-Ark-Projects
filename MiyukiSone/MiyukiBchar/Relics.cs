using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public class Relics
	{
		public class ScenarioEditor : PassiveItemBase, PassiveItem_EnableItem, IP_DrawNumChange
		{
			private static readonly Dictionary<string, string> RoleSkillMap = new Dictionary<string, string>
			{
				{ GDEItemKeys.CharRole_Role_DPS, GDEItemKeys.Skill_S_DefultSkill_0 },
				{ GDEItemKeys.CharRole_Role_Support, GDEItemKeys.Skill_S_DefultSkill_1 },
				{ GDEItemKeys.CharRole_Role_Tank, GDEItemKeys.Skill_S_DefultSkill_0 }
			};

			public override string DescExtended(string desc)
			{
				return base.DescExtended(desc).Replace("&a", SameSkillNum.ToString()).Replace("&b", MinSkillNum.ToString());
			}

			public override void Init()
			{
				OnePassive = true;
				base.Init();
			}

			public void DrawNumChange(int DrawNum, out int OutNum)
			{
				OutNum = DrawNum + 2;
			}

			public void EnableItem()
			{
				IncreaseArkPassiveNum();

				if (!(PlayData.TSavedata.SpRule is MiyukiSpecialRules))
				{
					PlayData.TSavedata.Party.ForEach(c =>
					{
						for (int i = 0; i < MinSkillNum - PlayData.TSavedata.SkillMinNum; i++)
						{
							if (RoleSkillMap.TryGetValue(c.GetData.Role.Key, out string skillKey)) c.SkillDatas.Add(new CharInfoSkillData(skillKey));
						}
					});

					PlayData.TSavedata.SpRule = new MiyukiSpecialRules();
					PlayData.TSavedata.SpRule.Init();
					PlayData.TSavedata.SpRule.GameSetting();
					PlayData.SpalcialRule = PlayData.TSavedata.SpRule.Key;
					SaveManager.savemanager.ProgressOneSave();
				}

				InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity, 2));
			}
		}
	}
}
