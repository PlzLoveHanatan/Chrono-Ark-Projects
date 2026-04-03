using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiyukiSone.Utils;

namespace MiyukiSone
{
	[Serializable]
	public class MiyukiSpecialRules : SpecialRule
	{
		//this.Key = "MiyukiSpecialRule";
		//this.Name = "Miyuki's Rule";
		//this.Desc = "Miyuki changes the game rules";
		//OpenSpecialStore = true;
		//CantStoryAndAchievethisrun = true;
		//RuleChange.PartyManaUnlock = true;
		//RuleChange.CharacterSkillMin = 7;
		//RuleChange.SkillBookPlusNum = 1;
		//RuleChange.OverlapSkillLimit = 4;
		//RuleChange.PlusMana = 2;
		//RuleChange.PlusFirstTurnDraw = 1;
		//RuleChange.PlusTurnDraw = 1;
		//RuleChange.PlusEquipSlot = 1;
		//RuleChange.CantNewPartymember = false;
		//RuleChange.BanBoss = new List<string> { "Boss1", "Boss2" };
		//RuleChange.BanEvent = new List<string>();
		//RuleChange.EndisStage4 = false;
		//RuleChange.CharacterRareSkillInfinityGet = true;
		//RuleChange.NoSpring = false;
		//RuleChange.PlusItem = new List<string>();
		//RuleChange.PlusSoul = 10;
		//RuleChange.DeleteStartItem = new List<string>();
		//RuleChange.PlusGold = 500;
		//RuleChange.StorePricePer = 70;                        // Процент цен в магазине (70 = скидка 30%)
		//RuleChange.NoUnChangeablePassive = true;
		//RuleChange.UseShuffleLimit = false
		//RuleChange.Shuffle = false;
		//RuleChange.NoGameOverAfterBattle = false;
		//RuleChange.EnemyStat = new Stat();
		//RuleChange.EnemyPerStat = new PerStat(); 

		public override void Init()
		{
			base.Init();
			CantStoryAndAchievethisrun = true;
			OnePassive = true;
		}

		public override void GameSetting()
		{
			RuleChange.CharacterSkillMin = MinSkillNum;
			RuleChange.OverlapSkillLimit = SameSkillNum;
			RuleChange.CharacterRareSkillInfinityGet = true;
			RuleChange.UseShuffleLimit = true;
			RuleChange.Shuffle = false;
			RuleChange.PartyManaUnlock = true;
		}
	}
}
