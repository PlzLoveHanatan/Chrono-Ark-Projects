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
using NLog.Targets;
namespace Mikure
{
	/// <summary>
	/// Please Take Care of Yourself!!
	/// </summary>
    public class S_Mikure_Draw : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> strings = new List<Skill>
            {
                Skill.TempSkill(ModItemKeys.Skill_S_Mikure_Draw_0, Utils.AllyTeam.LucyAlly, Utils.AllyTeam),
            };

            int drawNum = 0;
            drawNum = Utils.Mikure ? 2 : 1;
            MySkill.isExcept = Utils.Mikure ? false : true;

            Utils.AllyTeam.Draw(drawNum);

            if (Utils.Mikure)
            {
                bool hasDebuffs = Utils.AllyTeam.AliveChars.Any(ally => ally.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).Count() > 0);

                if (hasDebuffs)
                {
                    BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(strings, new SkillButton.SkillClickDel(Selection), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
                }
            }
        }

        private void Selection(SkillButton Mybutton)
        {
            string key = Mybutton.Myskill.MySkill.KeyID;

            if (key == ModItemKeys.Skill_S_Mikure_Draw_0)
            {
                foreach (var ally in Utils.AllyTeam.AliveChars)
                {
                    var debuffs = ally.GetBuffs(BattleChar.GETBUFFTYPE.ALLDEBUFF, true, false).Random(BChar.GetRandomClass().Main, 1);

                    if (debuffs.Count > 0)
                    {
                        foreach (var b in debuffs)
                        {
                            b.SelfDestroy();
                        }
                        BattleSystem.DelayInput(Utils.HealingParticle(ally, BattleSystem.instance.DummyChar));
                    }
                }
            }
        }
    }
}