using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using MiyukiSone;
using UnityEngine;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	public class Equips
	{
		public class WallBreaker : EquipBase, IP_PlayerTurn
		{
			public override void Init()
			{
				base.Init();
				PlusPerStat.Damage = 15;
				PlusPerStat.Heal = 15;
				PlusStat.cri = 10;
				PlusStat.dod = 5;
			}

			public void Turn()
			{
				if (MyChar.KeyData == ModItemKeys.Character_Miyuki) GetMiyukiPassive.WallBreakerEquipped = true;
				BattleSystem.DelayInput(CleanDeck());
			}

			private IEnumerator CleanDeck()
			{
				yield return null;

				var cursedKeys = new HashSet<string>
				{
					GDEItemKeys.Skill_S_S1_LittleMaid_0_Lucy,
					GDEItemKeys.Skill_S_Transcendence_Main,
					ModItemKeys.Skill_S_Miyuki_Special_Yabeley,
					GDEItemKeys.Skill_S_Witch_P_0,
					GDEItemKeys.Skill_S_Witch_2,
					GDEItemKeys.Skill_S_Joker_0,
					GDEItemKeys.Skill_S_FanaticBoss_Phase1AllyCard,
					GDEItemKeys.Skill_S_BombClown_B_0,
				};

				BattleSystem.instance.AllyTeam.Skills_Deck.RemoveAll(s => s.MySkill.User == "LucyCurse" || cursedKeys.Contains(s.MySkill.KeyID));
				BattleSystem.instance.AllyTeam.Skills.RemoveAll(s => s.MySkill.User == "LucyCurse" || cursedKeys.Contains(s.MySkill.KeyID));
				BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAll(s => s.MySkill.User == "LucyCurse" || cursedKeys.Contains(s.MySkill.KeyID));

				BattleSystem.instance.ActWindow?.Draw(BattleSystem.instance.AllyTeam, false);
			}
		}
	}
}
