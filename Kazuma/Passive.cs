using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using UnityEngine;

namespace Kazuma
{
	public class Passive
	{
		public class KazumaPassive : Passive_Char, IP_BattleStart_Ones
		{
			private readonly List<string> contractList = new List<string>()
			{
				ModItemKeys.Skill_Contract_01_Dangerous,
				ModItemKeys.Skill_Contract_02_Work,
				ModItemKeys.Skill_Contract_03_Emergency,
				ModItemKeys.Skill_Contract_04_Inspection,
				ModItemKeys.Skill_Contract_05_Delivery,
				ModItemKeys.Skill_Contract_06_Axis,
				ModItemKeys.Skill_Contract_07_Guild,
				ModItemKeys.Skill_Contract_08_Landlord,
			};

			private readonly Dictionary<string, string> contractMap = new Dictionary<string, string>()
			{
				{ ModItemKeys.Skill_Contract_01_Dangerous, ModItemKeys.Buff_B_Contract_01_Dangerous },
				{ ModItemKeys.Skill_Contract_02_Work, ModItemKeys.Buff_B_Contract_02_Work },
				{ ModItemKeys.Skill_Contract_03_Emergency, ModItemKeys.Buff_B_Contract_03_Emergency },
				{ ModItemKeys.Skill_Contract_04_Inspection, ModItemKeys.Buff_B_Contract_04_Inspection },
				{ ModItemKeys.Skill_Contract_05_Delivery, ModItemKeys.Buff_B_Contract_05_Delivery },
				{ ModItemKeys.Skill_Contract_06_Axis, ModItemKeys.Buff_B_Contract_06_Axis },
				{ ModItemKeys.Skill_Contract_07_Guild, ModItemKeys.Buff_B_Contract_07_Guild },
				{ ModItemKeys.Skill_Contract_08_Landlord, ModItemKeys.Buff_B_Contract_08_Landlord },
			};

			public override void Init()
			{
				OnePassive = true;
			}

			public void BattleStart(BattleSystem Ins)
			{
				BattleSystem.DelayInputAfter(ContractSelection());
			}

			private IEnumerator ContractSelection()
			{
				List<Skill> skillList = new List<Skill>();
				List<string> contracts = new List<string>(contractList);

				int contractsToAdd = Mathf.Min(2, contracts.Count);

				for (int i = 0; i < contractsToAdd; i++)
				{
					int randomIndex = RandomManager.RandomInt(RandomClassKey.Event, 0, contracts.Count);
					string key = contracts[randomIndex];
					contracts.RemoveAt(randomIndex);

					var skill = Skill.TempSkill(key, BattleSystem.instance.DummyChar, BattleSystem.instance.DummyChar.MyTeam);
					skillList.Add(skill);
				}

				if (skillList.Count > 0)
				{
					BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(skillList, ApplyContract, "Select Contract", true, false, true, false, true));
				}

				yield break;
			}

			private void ApplyContract(SkillButton button)
			{
				string key = button.Myskill.MySkill.KeyID;

				if (contractMap.TryGetValue(key, out var contractKey))
				{
					BChar?.BuffAdd(contractKey, BattleSystem.instance.DummyChar);

					var enemies = BattleSystem.instance.EnemyTeam.AliveChars;

					if (enemies.Count > 0)
					{
						int randomIndex = RandomManager.RandomInt(RandomClassKey.Enemy, 0, enemies.Count);
						var randomEnemy = enemies[randomIndex];
					}
				}
				button.Myskill.isExcept = true;
			}
		}
	}
}
