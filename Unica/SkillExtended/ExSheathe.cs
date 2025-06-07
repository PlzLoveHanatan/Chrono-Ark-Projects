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
namespace Unica
{
	/// <summary>
	/// Sheathe : Cast this skill. Draw 1 additional skill next turn.
	/// </summary>
    public class ExSheathe : SkillExtedned_IlyaP
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.AP <= 2 && base.CanSkillEnforce(MainSkill) && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_0 && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_9_Rare && MainSkill.MySkill.KeyID != GDEItemKeys.Skill_S_Ilya_8_Rare;
        }

        public override void IlyaWaste()
        {

            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(this.BChar, this.MySkill, false, true, false));
            BattleSystem.DelayInputAfter(this.Del());
            BattleSystem.instance.AllyTeam.LucyAlly.BuffAdd(ModItemKeys.Buff_DrawNextTurn, this.BChar, false, 0, false, -1, false);
        }
        public IEnumerator Del()
        {
            yield return new WaitForFixedUpdate();
            if (this.MySkill.isExcept || this.MySkill.Disposable)
            {
                Skill skill = BattleSystem.instance.AllyTeam.Skills_UsedDeck.Find((Skill a) => a.CharinfoSkilldata == this.MySkill.CharinfoSkilldata);
                if (skill == null)
                {
                    skill = BattleSystem.instance.AllyTeam.Skills_Deck.Find((Skill a) => a.CharinfoSkilldata == this.MySkill.CharinfoSkilldata);
                    if (skill != null)
                    {
                        BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(skill);
                    }
                    else
                    {
                        skill = BattleSystem.instance.AllyTeam.Skills.Find((Skill a) => a.CharinfoSkilldata == this.MySkill.CharinfoSkilldata);
                        if (skill != null)
                        {
                            BattleSystem.instance.AllyTeam.Skills.Remove(skill);
                        }
                    }
                }
                else
                {
                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(skill);
                }
            }
            yield break;
        }
    }
}