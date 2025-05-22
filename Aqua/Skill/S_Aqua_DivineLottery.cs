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
namespace Aqua
{
	/// <summary>
	/// Divine Lottery
	/// </summary>
    public class S_Aqua_DivineLottery : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<GDESkillData> list = new List<GDESkillData>();
            foreach (GDESkillData gdeskillData in PlayData.ALLSKILLLIST.Concat(PlayData.ALLRARESKILLLIST))
            {
                if (gdeskillData.User != "" && gdeskillData.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && !gdeskillData.NoDrop && !gdeskillData.Lock)
                {
                    GDECharacterData gdecharacterData = new GDECharacterData(gdeskillData.User);
                    if (!(gdeskillData.KeyID == GDEItemKeys.Skill_S_Phoenix_6) && !(gdeskillData.Key == GDEItemKeys.Skill_S_Phoenix_6) && gdecharacterData != null && Misc.IsUseableCharacter(gdecharacterData.Key))
                    {
                        list.Add(gdeskillData);
                    }
                }
            }

            BattleChar battleChar = BChar; /*BattleSystem.instance.AllyList.Random(BattleRandom.PassiveItem);*/
            Skill skill = Skill.TempSkill(list.Random(BattleRandom.PassiveItem).Key, battleChar, battleChar.MyTeam);
            skill.FreeUse = true;
            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(battleChar, skill, false, false, false));
        }
    }
}