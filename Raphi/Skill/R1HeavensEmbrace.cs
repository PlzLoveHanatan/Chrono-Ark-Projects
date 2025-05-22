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
namespace Raphi
{
	/// <summary>
	/// Heaven's Embrace
	/// Spend 3 <color=#7B68EE>Celestial Connection</color> to gain an additional "Heaven's Embrace" buff.
	/// </summary>
    public class R1HeavensEmbrace : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            List<Skill> list = new List<Skill>();
            list.Add(Skill.TempSkill(ModItemKeys.Skill_R1HeavensEmbrace_0, this.MySkill.Master, this.MySkill.Master.MyTeam));
            list.Add(Skill.TempSkill(ModItemKeys.Skill_R1HeavensEmbrace_1, this.MySkill.Master, this.MySkill.Master.MyTeam));
            if (this.BChar.BuffFind(ModItemKeys.Buff_B_CelestialConnection, false) && this.BChar.BuffReturn(ModItemKeys.Buff_B_CelestialConnection, false).StackNum >= 3)
            {
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
            }
        }

        public void Del(SkillButton Mybutton)
        {
            if (Mybutton.Myskill.MySkill.KeyID == ModItemKeys.Skill_R1HeavensEmbrace_0)
            {
                foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    battleChar.BuffAdd(ModItemKeys.Buff_B_HeavensEmbrace, this.BChar, false, 0, false, -1, false);
                }
                this.BChar.BuffReturn(ModItemKeys.Buff_B_CelestialConnection, false).SelfDestroy();
            }
        }
    }
}