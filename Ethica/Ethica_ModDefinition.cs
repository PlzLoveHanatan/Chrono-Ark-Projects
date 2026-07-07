using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameDataEditor;
using I2.Loc;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using ChronoArkMod.Plugin;
using ChronoArkMod.Template;
using Debug = UnityEngine.Debug;
using ChronoArkMod.ModData;
using static Ethica.CommonSkills;
namespace Ethica
{
    public class Ethica_ModDefinition : ModDefinition
    {
        public override Type ModItemKeysType => typeof(ModItemKeys);

		public override List<object> BattleSystem_ModIReturn()
		{
            return base.BattleSystem_ModIReturn().Let(l => l.Add(new FunnyClass()));
		}

		public class FunnyClass : IP_BattleStart_Ones, IP_Draw, IP_SkillUseHand_Team, IP_SkillUseHand_Basic_Team
		{
            private static readonly List<string> RareList = new List<string>()
            {
				ModItemKeys.Skill_S_Ethica_Common_Rare_Alchemize,
				ModItemKeys.Skill_S_Ethica_Common_Rare_Anointed,
				ModItemKeys.Skill_S_Ethica_Common_Rare_BeatDown,
				ModItemKeys.Skill_S_Ethica_Common_Rare_Bolas,
				ModItemKeys.Skill_S_Ethica_Common_Rare_Calamity
			};

			public void BattleStart(BattleSystem Ins)
			{
				Ins.AllyTeam.Skills_Deck.Where(s => RareList.Contains(s.MySkill.KeyID)).ToList().ForEach(s => s.MySkill.Rare = true);
				Ins.AllyTeam.AliveChars.Select(c => Ins.AllyTeam.Skills_Basic[Ins.AllyTeam.Chars.IndexOf(c)]).Where(s => RareList.Contains(s.MySkill.KeyID)).ToList().ForEach(s => s.MySkill.Rare = true);
				//BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_B_Ethica_Common_Rare_GoldAxe, BattleSystem.instance.AllyTeam.DummyChar);
			}

			public IEnumerator Draw(Skill Drawskill, bool NotDraw)
			{
				yield return null;
				if (RareList.Contains(Drawskill.MySkill.KeyID) && !Drawskill.MySkill.Rare) Drawskill.MySkill.Rare = true;
			}

			public void SKillUseHand_Basic_Team(Skill skill)
			{
				Utils.EthicaBV.GoldAxeDamage++;
			}

			public void SKillUseHand_Team(Skill skill)
			{
				Utils.EthicaBV.GoldAxeDamage++;
			}
		}
	}
}