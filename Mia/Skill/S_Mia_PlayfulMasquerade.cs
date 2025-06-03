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
namespace Mia
{
	/// <summary>
	/// Playful Masquerade
	/// Sheathe : Cast this skill.
	/// </summary>
    public class S_Mia_PlayfulMasquerade : SkillExtedned_IlyaP
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.FreeUse) return;

            Utils.TryPlayMiaSound(MySkill, BChar);

            if (SkillD.BasicSkill) return;
        }

        public override void IlyaWaste()
        {
            Utils.TryPlayMiaSound(MySkill, BChar);

            BattleSystem.DelayInput(BattleSystem.instance.SkillRandomUseIenum(BChar, MySkill, false, true, false));
            BattleSystem.DelayInputAfter(Del());
        }

        public IEnumerator Del()
        {
            yield return new WaitForFixedUpdate();
            if (MySkill.isExcept || MySkill.Disposable)
            {
                Skill skill = BattleSystem.instance.AllyTeam.Skills_UsedDeck.Find((Skill a) => a.CharinfoSkilldata == MySkill.CharinfoSkilldata);
                if (skill == null)
                {
                    skill = BattleSystem.instance.AllyTeam.Skills_Deck.Find((Skill a) => a.CharinfoSkilldata == MySkill.CharinfoSkilldata);
                    if (skill != null)
                    {
                        BattleSystem.instance.AllyTeam.Skills_UsedDeck.Remove(skill);
                    }
                    else
                    {
                        skill = BattleSystem.instance.AllyTeam.Skills.Find((Skill a) => a.CharinfoSkilldata == MySkill.CharinfoSkilldata);
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