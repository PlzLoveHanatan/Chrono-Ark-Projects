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
namespace ImaSuguRinne
{
    /// <summary>
    /// Endless Despair
    /// </summary>
    public class S_Rinne_Rare_EndlessDespair : Skill_Extended, IP_SkillUseHand_Team
    {
        public override void Init()
        {
            OnePassive = true;
        }

        public override IEnumerator DrawAction()
        {
            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_Rare_EndlessDespair_0)
            {
                Utils.CastSkill(BChar, MySkill);
            }
            return base.DrawAction();
        }

        public override void FixedUpdate()
        {
            if (MySkill.BasicSkill)
            {
                MySkill.APChange = -1;
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (MySkill.MySkill.KeyID == ModItemKeys.Skill_S_Rinne_Rare_EndlessDespair_0)
            {
                Utils.AllyTeam.Draw();
            }
            else if (!MySkill.BasicSkill)
            {
                Skill skill = Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_Rare_EndlessDespair_0, true, true, 0, 0, true, false);
                Utils.InsertSkillInDeck(BChar, skill, 5);
                //Utils.CopiesSelection(BChar, skill);
            }

            //if (!MySkill.BasicSkill)
            //{
            //    Utils.CreateSkill(BChar, ModItemKeys.Skill_S_Rinne_Rare_EndlessDespair, true, true, 1, 1, true, true);
            //}
        }


        public void SKillUseHand_Team(Skill skill)
        {
            BattleSystem.DelayInputAfter(Del(skill));
        }

        private IEnumerator Del(Skill skill)
        {
            if (skill.Master == BChar)
            {
                BChar.MyTeam.BasicSkillRefill(BChar, BChar.BattleBasicskillRefill);
            }
            yield break;
        }
    }
}